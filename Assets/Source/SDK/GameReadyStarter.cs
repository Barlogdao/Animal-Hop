using UnityEngine;

public class GameReadyStarter : MonoBehaviour
{
    void Start()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        Agava.YandexGames.YandexGamesSdk.GameReady();
#endif
    }
}
