using Agava.YandexGames;

using UnityEngine;

public class Leaderboard : MonoBehaviour
{
    private const string LeaderboardName = "Leaderboard";

    public void SetPlayerScore(int score)
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        if (PlayerAccount.IsAuthorized == false)
            return;

        Agava.YandexGames.Leaderboard.GetPlayerEntry(LeaderboardName, (result) =>
        {
            if (result == null || result.score < score)
                Agava.YandexGames.Leaderboard.SetScore(LeaderboardName, score);
        });
#endif
    }
}
