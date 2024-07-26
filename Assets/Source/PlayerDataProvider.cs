using System;
using UnityEngine;

public class PlayerDataProvider : MonoBehaviour
{
    private const string Data = "Data";
    private PlayerData _playerData;
    public PlayerData PlayerData => _playerData;

    public event Action<int> AnimalChanged;
    public event Action<int> AnimalUnlocked;

    private void Awake()
    {
        _playerData = Load();
    }

    public int GetGoldAmount()
    {
        return _playerData.Gold;
    }

    private void OnEnable()
    {
        Platform.OnGoldCollected += CollectGold;
    }

    private void OnDisable()
    {
        Platform.OnGoldCollected -= CollectGold;
    }

    private void CollectGold()
    {
        _playerData.AddGold(1);
        Save();
    }

    public void SpendGold(int amount)
    {
        _playerData.SpendGold(amount);
        Save();
    }

    public int GetCurrentAnimalID()
    {
        return _playerData.CurrentAnimalID;
    }

    public int GetScore()
    {
        return _playerData.Score;
    }

    public void SetScore(int score)
    {
        if (_playerData.Score < score)
        {
            _playerData.Score = score;
            Save();
        }
    }

    public void ChangeAninmal(int id)
    {
        _playerData.CurrentAnimalID = id;
        AnimalChanged?.Invoke(id);
        Save();
    }

    public void UnlockAnimal(int id)
    {
        _playerData.UnlockAnimal(id);
        AnimalUnlocked?.Invoke(id);
        Save();
    }

    public PlayerData Load()
    {

#if UNITY_WEBGL && !UNITY_EDITOR
        string json = Agava.YandexGames.Utility.PlayerPrefs.GetString(Data, null);
#else
        string json = PlayerPrefs.GetString(Data, null);
#endif
        if (json == null)
            return new PlayerData();

        PlayerData data = JsonUtility.FromJson<PlayerData>(json);
        return data ?? new PlayerData();
    }

    public void Save()
    {
        string json = JsonUtility.ToJson(_playerData);
#if UNITY_WEBGL && !UNITY_EDITOR
        Agava.YandexGames.Utility.PlayerPrefs.SetString(Data, json);
        Agava.YandexGames.Utility.PlayerPrefs.Save();
#else
        PlayerPrefs.SetString(Data, json);
        PlayerPrefs.Save();

#endif
        
    }
}
