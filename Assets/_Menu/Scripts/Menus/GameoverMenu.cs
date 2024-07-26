using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Sans.UI.Menu
{
    public class GameoverMenu : Menu
    {
        private const string Last = "Last";
        private const string Best = "Best";

        [Header("UI References :")]
        [SerializeField] TMP_Text _scoreText;
        [SerializeField] TMP_Text _bestScoreText;
        [SerializeField] Button _restartButton;
        [SerializeField] Button _homeButton;



        protected override void Awake()
        {
            base.Awake();
        }

        private void OnEnable()
        {
            _restartButton.interactable = true;
            _homeButton.interactable = true;

            ScoreManager sc = FindObjectOfType<ScoreManager>();
            int lastScore = sc.Score;
            int bestScore = sc.GetBestScore();

            SetScoreDisplay(lastScore, bestScore);
        }

        private void Start()
        {
            OnButtonPressed(_restartButton, RestartButton);
            OnButtonPressed(_homeButton, HomeButton);
        }

        private void SetScoreDisplay(int lastScore, int bestScore)
        {
            _scoreText.text = $"{Lean.Localization.LeanLocalization.GetTranslationText(Last)} : {lastScore}";
            _bestScoreText.text = $"{Lean.Localization.LeanLocalization.GetTranslationText(Best)} : {bestScore}";
        }

        private void HomeButton()
        {
            _restartButton.interactable = false;

            StartCoroutine(ReloadLevelAsync(() =>
            {
                _menuController.SwitchMenu(MenuType.Main);
            }));
        }


        private void RestartButton()
        {
            _homeButton.interactable = false;
            Admob.Instance.ShowInterstitialAd();

            StartCoroutine(ReloadLevelAsync(() =>
            {
                _menuController.SwitchMenu(MenuType.Gameplay);
            }));
        }

        IEnumerator ReloadLevelAsync(Action OnSceneLoaded = null)
        {
            yield return SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
            OnSceneLoaded?.Invoke();
        }
    }
}
