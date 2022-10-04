using Ui;
using Game;
using Profile;
using UnityEngine;
using Services.Ads.UnityAds;
using Services.Analytics;
using Services.IAP;

internal class MainController : BaseController
{
    private readonly Transform _placeForUi;
    private readonly ProfilePlayer _profilePlayer;

    private MainMenuController _mainMenuController;
    private GameController _gameController;
    private SettingsMenuController _settingsMenuController;

    private BaseController _activeController;

    private UnityAdsService _adsService;
    private AnalyticsManager _analyticsManager;
    private IAPService _iAPService;

    public MainController(Transform placeForUi, ProfilePlayer profilePlayer, UnityAdsService unityAdsService, AnalyticsManager analyticsManager, IAPService iAPService)
    {
        _placeForUi = placeForUi;
        _profilePlayer = profilePlayer;

        _adsService = unityAdsService;
        _analyticsManager = analyticsManager;
        _iAPService = iAPService;

        profilePlayer.CurrentState.SubscribeOnChange(OnChangeGameState);
        OnChangeGameState(_profilePlayer.CurrentState.Value);
    }

    protected override void OnDispose()
    {
        _mainMenuController?.Dispose();
        _gameController?.Dispose();

        _profilePlayer.CurrentState.UnSubscribeOnChange(OnChangeGameState);
    }


    private void OnChangeGameState(GameState state)
    {
        switch (state)
        {
            case GameState.Start:
                _mainMenuController = new MainMenuController(_placeForUi, _profilePlayer, _iAPService);
                _activeController?.Dispose();
                _activeController = _mainMenuController;
                break;
            case GameState.Game:
                _gameController = new GameController(_profilePlayer, _analyticsManager);
                _activeController?.Dispose();
                _activeController = _gameController;
                break;
            case GameState.Settings:
                _settingsMenuController = new SettingsMenuController(_placeForUi, _profilePlayer);
                _activeController?.Dispose();
                _activeController = _settingsMenuController;
                break;
            case GameState.ShowRewardedAdd:
                _adsService.RewardedPlayer.Play();
                break;
            default:
                _mainMenuController?.Dispose();
                _gameController?.Dispose();
                _settingsMenuController?.Dispose();
                break;
        }
    }
}
