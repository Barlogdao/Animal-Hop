using System.Collections;
using Agava.YandexGames;
using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class SDKInitializer : MonoBehaviour
{
    [SerializeField] private int _gameSceneIndex = 1;

    private void Awake()
    {
        YandexGamesSdk.CallbackLogging = true;
    }

    private IEnumerator Start()
    {
        yield return YandexGamesSdk.Initialize();
        Agava.YandexGames.Utility.PlayerPrefs.Load(LoadScene);
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(_gameSceneIndex);
    }
}

