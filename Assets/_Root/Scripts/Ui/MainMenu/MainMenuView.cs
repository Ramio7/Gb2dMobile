using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Ui
{
    public class MainMenuView : MonoBehaviour
    {
        [SerializeField] private Button _buttonStart;
        [SerializeField] private Button _buttonSettings;
        [SerializeField] private Button _buttonRewardedAdd;
        [SerializeField] private Button _buttonBuy;


        public void Init(UnityAction startGame, UnityAction settings, UnityAction showAdd, UnityAction purshaseCoins)
        {
            _buttonStart.onClick.AddListener(startGame);
            _buttonSettings.onClick.AddListener(settings);
            _buttonRewardedAdd.onClick.AddListener(showAdd);
            _buttonBuy.onClick.AddListener(purshaseCoins);
        }

        public void OnDestroy()
        {
            _buttonStart.onClick.RemoveAllListeners();
            _buttonSettings.onClick.RemoveAllListeners();
            _buttonRewardedAdd.onClick.RemoveAllListeners();
            _buttonBuy.onClick.RemoveAllListeners();
        }
    }
}
