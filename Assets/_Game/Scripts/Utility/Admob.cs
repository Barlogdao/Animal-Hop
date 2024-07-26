using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Admob : Singleton<Admob>
{

    private PauseService _pauseService;
    private AdvertisingService _advertisingService;
    [SerializeField] private FocusController _focusController;


    bool initDone = false;

    public static event Action OnRewardedAdWatched;


    private void OnEnable() => SceneManager.sceneLoaded += HandleOnSceneLoaded;
    private void OnDisable() => SceneManager.sceneLoaded -= HandleOnSceneLoaded;

    private void HandleOnSceneLoaded(Scene scene, LoadSceneMode lsm)
    {
        if (scene.buildIndex == 0) Destroy(gameObject);
    }

    protected override void Awake()
    {
        base.Awake();

        _pauseService = new PauseService();
        _advertisingService = new AdvertisingService(_pauseService);
        _focusController.Initialize(_pauseService, _advertisingService);
    }

     public void ShowInterstitialAd()
    {
        _advertisingService.ShowInterstitialAd(() => { });
    }


    public void ShowRewardAd()
    {
        _advertisingService.ShowRewardAd(
            () => OnRewardedAdWatched?.Invoke(),
            () => { });
    }
}
