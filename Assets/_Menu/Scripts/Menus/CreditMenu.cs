using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sans.UI.Menu
{
    public class CreditMenu : Menu
    {
        private PlayerDataProvider _playerDataProvider;

        [Header("UI References :")]
        [SerializeField] TMP_Text _goldAmount;
        [SerializeField] TMP_Text _sfxText;
        [SerializeField] TMP_Text _contactText;
        [Space]
        [SerializeField] Button _backButton;

        [Header("Database References :")]
        [SerializeField] MenuData _data;

        [SerializeField] private List<AnimalSlot> _slots;

        private void OnEnable()
        {
            _playerDataProvider = FindFirstObjectByType<PlayerDataProvider>();
            ChangeGold();

            foreach (var slot in _slots)
            {
                slot.Setup(_playerDataProvider);
            }

            _playerDataProvider.AnimalChanged += OnAnimalChanged;
            _playerDataProvider.AnimalUnlocked += OnAnimalUnlocked;
            _backButton.interactable = true;
        }

 

        private void ChangeGold()
        {
            _goldAmount.text = _playerDataProvider.GetGoldAmount().ToString();
        }

        private void OnDisable()
        {
            _playerDataProvider.AnimalChanged -= OnAnimalChanged;
            _playerDataProvider.AnimalUnlocked -= OnAnimalUnlocked;


        }

        private void OnAnimalUnlocked(int id)
        {
            ChangeGold();

            foreach (var slot in _slots)
            {
                slot.CheckUnlock(_playerDataProvider);
            }
        }

        private void OnAnimalChanged(int id)
        {
            foreach (var slot in _slots)
            {
                slot.CheckSelect(_playerDataProvider);
            }
        }



        private void Start()
        {
            OnButtonPressed(_backButton, BackButtonPressed);
        }

        private void BackButtonPressed()
        {
            _backButton.interactable = false;
            _menuController.CloseMenu();
        }
    }
}
