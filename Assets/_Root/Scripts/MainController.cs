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
        _activeController?.Dispose();

        _profilePlayer.CurrentState.UnSubscribeOnChange(OnChangeGameState);
    }


    private void OnChangeGameState(GameState state)
    {
        switch (state)
        {
            case GameState.Start:
                _activeController?.Dispose();
                _activeController = new MainMenuController(_placeForUi, _profilePlayer, _iAPService);
                break;
            case GameState.Game:
                _activeController?.Dispose();
                _activeController = new GameController(_profilePlayer, _analyticsManager);
                break;
            case GameState.Settings:
                _activeController?.Dispose();
                _activeController = new SettingsMenuController(_placeForUi, _profilePlayer);
                break;
            case GameState.ShowRewardedAdd:
                _adsService.RewardedPlayer.Play();
                break;
            default:
                _activeController?.Dispose();
                break;
        }
    }
}
