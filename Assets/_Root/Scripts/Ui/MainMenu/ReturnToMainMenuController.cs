using Profile;
using Tool;
using UnityEngine;

internal class ReturnToMainMenuController : BaseController
{
    private readonly ResourcePath _resourcePath = new("Prefabs/Ui/MainMenuButtonView");

    private readonly ReturnToMainMenuView _view;
    private readonly ProfilePlayer _profilePlayer;


    public ReturnToMainMenuController(Transform placeForUi, ProfilePlayer profilePlayer)
    {
        _profilePlayer = profilePlayer;
        _view = LoadView(placeForUi);
        _view.Init(ReturnToMainMenu);
    }


    private ReturnToMainMenuView LoadView(Transform placeForUi)
    {
        GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
        GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
        AddGameObject(objectView);

        return objectView.GetComponent<ReturnToMainMenuView>();
    }

    private void ReturnToMainMenu() =>
        _profilePlayer.CurrentState.Value = GameState.Start;
}
