using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnimalSlot : MonoBehaviour
{
    [SerializeField] private AnimalConfig _config;

    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _price;
    [SerializeField] RectTransform _priceLabel;
    [SerializeField] Outline _outline;

    [SerializeField] private Button _button;

    private PlayerDataProvider _provider;

    public event Action<int> AnimalSelected;
    public event Action<int> AnimalUnlocked;

    private void Awake()
    {
        _price.text = _config.Price.ToString();
        _image.sprite = _config.Sprite;
        _button.onClick.AddListener(OnSlotPressed);
    }

    public void Setup(PlayerDataProvider provider)
    {
        _provider = provider;
      

        _outline.enabled =false;
        _priceLabel.gameObject.SetActive(true);
        CheckUnlock(provider);
        CheckSelect(provider);
    }

    public void CheckSelect(PlayerDataProvider provider)
    {
        if (provider.GetCurrentAnimalID() == _config.ID)
        {
            _outline.enabled = true;
        }
        else
        {
            _outline.enabled = false;
        }
    }

    public void CheckUnlock(PlayerDataProvider provider)
    {
        if (IsAnimalUnlocked(provider))
        {
            _priceLabel.gameObject.SetActive(false);
        }
    }

    public void OnSlotPressed()
    {
        if (IsAnimalUnlocked(_provider))
        {
            _provider.ChangeAninmal(_config.ID);
            AnimalSelected?.Invoke(_config.ID);
        }
        else if (_provider.GetGoldAmount() >= _config.Price)
        {
            _provider.SpendGold(_config.Price);
            _provider.UnlockAnimal(_config.ID);
            AnimalUnlocked?.Invoke(_config.ID);
        }
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(OnSlotPressed);
    }

    private bool IsAnimalUnlocked(PlayerDataProvider provider)
    {
        return provider.PlayerData.IsAnimalUnlocked(_config.ID);
    }
}
