using BattleScripts;
using Profile;
using System;
using TMPro;
using Tool;
using UnityEngine;
using Object = UnityEngine.Object;

internal class BattleController : BaseController
{
    private readonly ResourcePath _resourcePath = new("Prefabs/Ui/BattleMenu");
    private readonly ProfilePlayer _profilePlayer;
    private readonly BattleView _battleView;

    private const int _maxSecurityStatus = 5;
    private const int _minSecurityStatus = 0;
    private PlayerData _money;
    private PlayerData _heath;
    private PlayerData _power;
    private PlayerData _securityStatus;


    private Enemy _enemy;

    public BattleController(Transform uiContainer, ProfilePlayer profilePlayer)
    {
        _battleView = LoadView(uiContainer);
        _profilePlayer = profilePlayer;

        _enemy = new Enemy("Enemy Flappy");

        _money = CreatePlayerData(DataType.Money);
        _heath = CreatePlayerData(DataType.Health);
        _power = CreatePlayerData(DataType.Power);
        _securityStatus = CreatePlayerData(DataType.SecurityStatus);

        Subscribe();
    }

    private BattleView LoadView(Transform placeForUi)
    {
        GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
        GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
        AddGameObject(objectView);

        return objectView.GetComponent<BattleView>();
    }

    protected override void OnDispose()
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
        _battleView.AddMoneyButton.onClick.AddListener(IncreaseMoney);
        _battleView.SubstractMoneyButton.onClick.AddListener(DecreaseMoney);

        _battleView.AddHealthButton.onClick.AddListener(IncreaseHealth);
        _battleView.SubstractHealthButton.onClick.AddListener(DecreaseHealth);

        _battleView.AddPowerButton.onClick.AddListener(IncreasePower);
        _battleView.SubstractPowerButton.onClick.AddListener(DecreasePower);

        _battleView.AddSecurityStatusButton.onClick.AddListener(IncreaseSecurityStatus);
        _battleView.SubstractSecurityStatusButton.onClick.AddListener(DecreaseSecurityStatus);

        _battleView.FightButton.onClick.AddListener(Fight);
        _battleView.EscapeTheFightButton.onClick.AddListener(Escape);
    }

    private void Unsubscribe()
    {
        _battleView.AddMoneyButton.onClick.RemoveAllListeners();
        _battleView.SubstractMoneyButton.onClick.RemoveAllListeners();

        _battleView.AddHealthButton.onClick.RemoveAllListeners();
        _battleView.SubstractHealthButton.onClick.RemoveAllListeners();

        _battleView.AddPowerButton.onClick.RemoveAllListeners();
        _battleView.SubstractPowerButton.onClick.RemoveAllListeners();

        _battleView.FightButton.onClick.RemoveAllListeners();
        _battleView.EscapeTheFightButton.onClick.RemoveAllListeners();
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
        _battleView.CountPowerEnemyText.text = $"Enemy Power {enemyPower}";

        _battleView.EscapeTheFightButton.gameObject.SetActive(_enemy.SecurityCheck());
    }

    private TMP_Text GetTextComponent(DataType dataType) =>
        dataType switch
        {
            DataType.Money => _battleView.CountMoneyText,
            DataType.Health => _battleView.CountHealthText,
            DataType.Power => _battleView.CountPowerText,
            DataType.SecurityStatus => _battleView.SecurityStatusText,
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
