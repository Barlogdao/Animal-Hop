using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{
    [SerializeField] private int _gameSceneIndex = 1;

    void Start()
    {
        SceneManager.LoadScene(_gameSceneIndex);
    }
}
