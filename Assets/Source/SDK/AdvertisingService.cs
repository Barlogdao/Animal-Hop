using System;




    public class AdvertisingService
    {
        private readonly PauseService _pauseService;

        public AdvertisingService(PauseService pauseService)
        {
            _pauseService = pauseService;
        }

        public bool IsAdsPlaying { get; private set; } = false;

        public void ShowInterstitialAd(Action onClose)
        {
#if UNITY_WEBGL && !UNITY_EDITOR
        Agava.YandexGames.InterstitialAd.Show(OnOpenCallback, OnCloseInterstitial);
#else
            OnCloseInterstitial(true);
#endif
            void OnCloseInterstitial(bool wasShown)
            {
                OnCloseCallback();
                onClose();
            }
        }

        public void ShowRewardAd(Action onSuccess, Action onFail)
        {
#if UNITY_WEBGL && !UNITY_EDITOR
        Agava.YandexGames.VideoAd.Show(OnOpenCallback, OnRewardedCallback, OnCloseCallback, OnErrorCallback);
#else
            OnRewardedCallback();
#endif
            void OnRewardedCallback()
            {
                onSuccess();
            }

            void OnErrorCallback(string error)
            {
                onFail();
            }
        }

        private void OnCloseCallback()
        {
            IsAdsPlaying = false;
            _pauseService.Unpause();
        }

        private void OnOpenCallback()
        {
            IsAdsPlaying = true;
            _pauseService.Pause();
        }
    }
