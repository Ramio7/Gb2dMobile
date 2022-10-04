using Profile;
using Services.IAP;
using Tool;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Ui
{
    internal class MainMenuController : BaseController
    {
        private static readonly ResourcePath resourcePath = new("Prefabs/MainMenu");
        private readonly ResourcePath _resourcePath = resourcePath;
        private readonly ProfilePlayer _profilePlayer;
        private readonly MainMenuView _view;

        private readonly IAPService _iAPService;


        public MainMenuController(Transform placeForUi, ProfilePlayer profilePlayer, IAPService iAPService)
        {
            _profilePlayer = profilePlayer;
            _iAPService = iAPService;
            _view = LoadView(placeForUi);
            _view.Init(StartGame, Settings, ShowRewardedAdd, PurshaseCoins);
        }

        private MainMenuView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<MainMenuView>();
        }

        private void StartGame() => _profilePlayer.CurrentState.Value = GameState.Game;

        private void Settings() => _profilePlayer.CurrentState.Value = GameState.Settings;

        private void ShowRewardedAdd() => _profilePlayer.CurrentState.Value = GameState.ShowRewardedAdd;

        private void PurshaseCoins()
        {
            _iAPService.Buy("product_1");
        }
    }
}
