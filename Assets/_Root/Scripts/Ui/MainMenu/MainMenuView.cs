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
        [SerializeField] private Button _buttonShed;


        public void Init(UnityAction startGame, UnityAction settings, UnityAction showAdd, UnityAction purshaseCoins, UnityAction enterShed)
        {
            _buttonStart.onClick.AddListener(startGame);
            _buttonSettings.onClick.AddListener(settings);
            _buttonRewardedAdd.onClick.AddListener(showAdd);
            _buttonBuy.onClick.AddListener(purshaseCoins);
            _buttonShed.onClick.AddListener(enterShed);
        }

        public void OnDestroy()
        {
            _buttonStart.onClick.RemoveAllListeners();
            _buttonSettings.onClick.RemoveAllListeners();
            _buttonRewardedAdd.onClick.RemoveAllListeners();
            _buttonBuy.onClick.RemoveAllListeners();
            _buttonShed.onClick.RemoveAllListeners();
        }
    }
}
