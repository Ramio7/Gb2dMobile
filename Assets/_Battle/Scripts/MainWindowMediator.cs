using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BattleScripts
{
    internal class MainWindowMediator : MonoBehaviour
    {
        [Header("Player Stats")]
        [SerializeField] private TMP_Text _countMoneyText;
        [SerializeField] private TMP_Text _countHealthText;
        [SerializeField] private TMP_Text _countPowerText;
        [SerializeField] private TMP_Text _securityStatusText;

        [Header("Enemy Stats")]
        [SerializeField] private TMP_Text _countPowerEnemyText;

        [Header("Money Buttons")]
        [SerializeField] private Button _addMoneyButton;
        [SerializeField] private Button _substractMoneyButton;

        [Header("Health Buttons")]
        [SerializeField] private Button _addHealthButton;
        [SerializeField] private Button _substractHealthButton;

        [Header("Power Buttons")]
        [SerializeField] private Button _addPowerButton;
        [SerializeField] private Button _substractPowerButton;

        [Header("Security Status Buttons")]
        [SerializeField] private Button _addSecurityStatusButton;
        [SerializeField] private Button _substractSecurityStatusButton;

        [Header("Other Buttons")]
        [SerializeField] private Button _fightButton;
        [SerializeField] private Button _escapeTheFightButton;

        private const int _maxSecurityStatus = 5;
        private const int _minSecurityStatus = 0;

        private PlayerData _money;
        private PlayerData _heath;
        private PlayerData _power;
        private PlayerData _securityStatus;


        private Enemy _enemy;


        private void Start()
        {
            _enemy = new Enemy("Enemy Flappy");

            _money = CreatePlayerData(DataType.Money);
            _heath = CreatePlayerData(DataType.Health);
            _power = CreatePlayerData(DataType.Power);
            _securityStatus = CreatePlayerData(DataType.SecurityStatus);

            Subscribe();
        }

        private void OnDestroy()
        {
            DisposePlayerData(ref _money);
            DisposePlayerData(ref _heath);
            DisposePlayerData(ref _power);
            DisposePlayerData(ref _securityStatus);

            Unsubscribe();
        }


        private PlayerData CreatePlayerData(DataType dataType)
        {
            PlayerData playerData = new(dataType);
            playerData.Attach(_enemy);

            return playerData;
        }

        private void DisposePlayerData(ref PlayerData playerData)
        {
            playerData.Detach(_enemy);
            playerData = null;
        }


        private void Subscribe()
        {
            _addMoneyButton.onClick.AddListener(IncreaseMoney);
            _substractMoneyButton.onClick.AddListener(DecreaseMoney);

            _addHealthButton.onClick.AddListener(IncreaseHealth);
            _substractHealthButton.onClick.AddListener(DecreaseHealth);

            _addPowerButton.onClick.AddListener(IncreasePower);
            _substractPowerButton.onClick.AddListener(DecreasePower);

            _addSecurityStatusButton.onClick.AddListener(IncreaseSecurityStatus);
            _substractSecurityStatusButton.onClick.AddListener(DecreaseSecurityStatus);

            _fightButton.onClick.AddListener(Fight);
            _escapeTheFightButton.onClick.AddListener(Escape);
        }

        private void Unsubscribe()
        {
            _addMoneyButton.onClick.RemoveAllListeners();
            _substractMoneyButton.onClick.RemoveAllListeners();

            _addHealthButton.onClick.RemoveAllListeners();
            _substractHealthButton.onClick.RemoveAllListeners();

            _addPowerButton.onClick.RemoveAllListeners();
            _substractPowerButton.onClick.RemoveAllListeners();

            _fightButton.onClick.RemoveAllListeners();
            _escapeTheFightButton.onClick.RemoveAllListeners();
        }


        private void IncreaseMoney() => IncreaseValue(_money);
        private void DecreaseMoney() => DecreaseValue(_money);

        private void IncreaseHealth() => IncreaseValue(_heath);
        private void DecreaseHealth() => DecreaseValue(_heath);

        private void IncreasePower() => IncreaseValue(_power);
        private void DecreasePower() => DecreaseValue(_power);

        private void IncreaseSecurityStatus()
        {
            if (_securityStatus.Value < _maxSecurityStatus) IncreaseValue(_securityStatus);
        }

        private void DecreaseSecurityStatus()
        {
            if (_securityStatus.Value > _minSecurityStatus) DecreaseValue(_securityStatus);
        }

        private void IncreaseValue(PlayerData playerData) => AddToValue(1, playerData);
        private void DecreaseValue(PlayerData playerData) => AddToValue(-1, playerData);

        private void AddToValue(int addition, PlayerData playerData)
        {
            playerData.Value += addition;
            ChangeDataWindow(playerData);
        }


        private void ChangeDataWindow(PlayerData playerData)
        {
            int value = playerData.Value;
            DataType dataType = playerData.DataType;
            TMP_Text textComponent = GetTextComponent(dataType);
            textComponent.text = $"Player {dataType:F} {value}";

            int enemyPower = _enemy.CalcPower();
            _countPowerEnemyText.text = $"Enemy Power {enemyPower}";

            _escapeTheFightButton.gameObject.SetActive(_enemy.SecurityCheck());
        }

        private TMP_Text GetTextComponent(DataType dataType) =>
            dataType switch
            {
                DataType.Money => _countMoneyText,
                DataType.Health => _countHealthText,
                DataType.Power => _countPowerText,
                DataType.SecurityStatus => _securityStatusText,
                _ => throw new ArgumentException($"Wrong {nameof(DataType)}")
            };


        private void Fight()
        {
            int enemyPower = _enemy.CalcPower();
            bool isVictory = _power.Value >= enemyPower;

            string color = isVictory ? "#07FF00" : "#FF0000";
            string message = isVictory ? "Win" : "Lose";

            Debug.Log($"<color={color}>{message}!!!</color>");
        }

        private void Escape() => Debug.Log("Escaped the fight");
    }
}
