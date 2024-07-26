using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public int Score = 0;
    public int CurrentAnimalID = 0;
    public int Gold = 0;

    public List<int> UnlockedAnimalsID = new() { 0 };

    public bool IsEnoughGold(int amount)
    {
        return Gold >= amount;
    }

    public void SpendGold(int amount)
    {
        Gold -= amount;
    }

    public void AddGold(int amount)
    {
        Gold += amount;
    }

    public bool IsAnimalUnlocked(int id)
    {
        return UnlockedAnimalsID.Contains(id);
    }

    public void UnlockAnimal(int id)
    {
        if (!UnlockedAnimalsID.Contains(id))
        UnlockedAnimalsID.Add(id);
    }
}
