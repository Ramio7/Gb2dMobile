using Ui;
using Game;
using Profile;
using UnityEngine;
using Features.Shed;

internal class MainController : BaseController
{
    private readonly Transform _placeForUi;
    private readonly ProfilePlayer _profilePlayer;

    private BaseController _activeController;


    public MainController(Transform placeForUi, ProfilePlayer profilePlayer)
    {
        _placeForUi = placeForUi;
        _profilePlayer = profilePlayer;

        profilePlayer.CurrentState.SubscribeOnChange(OnChangeGameState);
        OnChangeGameState(_profilePlayer.CurrentState.Value);
    }

    protected override void OnDispose()
    {
        _activeController.Dispose();
        _profilePlayer.CurrentState.UnSubscribeOnChange(OnChangeGameState);
    }


    private void OnChangeGameState(GameState state)
    {
        _activeController?.Dispose();

        switch (state)
        {
            case GameState.Start:
                _activeController = new MainMenuController(_placeForUi, _profilePlayer);
                break;
            case GameState.Settings:
                _activeController = new SettingsMenuController(_placeForUi, _profilePlayer);
                break;
            case GameState.Shed:
                _activeController = new ShedController(_placeForUi, _profilePlayer);
                break;
            case GameState.Game:
                _activeController = new GameController(_placeForUi, _profilePlayer);
                break;
        }
    }
}
