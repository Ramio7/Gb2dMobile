using Profile;
using Services.Ads;
using Services.IAP;
using Tool;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Ui
{
    internal class MainMenuController : BaseController
    {
        private static readonly ResourcePath resourcePath = new("Prefabs/UI/MainMenu");
        private readonly ResourcePath _resourcePath = resourcePath;
        private readonly ProfilePlayer _profilePlayer;
        private readonly MainMenuView _view;

        private readonly IAPService _iapService;
        private readonly IAdsService _adsService;


        public MainMenuController(Transform placeForUi, ProfilePlayer profilePlayer, IAPService iapService, IAdsService adsService)
        {
            _profilePlayer = profilePlayer;
            _iapService = iapService;
            _adsService = adsService;
            _view = LoadView(placeForUi);
            _view.Init(StartGame, Settings, ShowRewardedAdd, PurshaseCoins, EnterShed);
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

        private void EnterShed() => _profilePlayer.CurrentState.Value = GameState.Shed;

        private void ShowRewardedAdd() => _adsService.RewardedPlayer.Play();

        private void PurshaseCoins() => _iapService.Buy("product_1");
    }
}
