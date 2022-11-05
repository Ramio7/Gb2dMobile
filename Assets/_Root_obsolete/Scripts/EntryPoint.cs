using Game;
using Profile;
using UnityEngine;

internal class EntryPoint : MonoBehaviour
{
    [Header("Initial Settings")]
    [SerializeField] private GameConfig _gameConfig;

    [Header("Scene Objects")]
    [SerializeField] private Transform _uiContainer;

    private MainController _mainController;


    private void Start()
    {
        var profilePlayer = new ProfilePlayer(_gameConfig.CarSpeed, _gameConfig.CarJumpHeight, _gameConfig.StartingGameState);
        _mainController = new MainController(_uiContainer, profilePlayer);
    }

    private void OnDestroy()
    {
        _mainController.Dispose();
    }
}
