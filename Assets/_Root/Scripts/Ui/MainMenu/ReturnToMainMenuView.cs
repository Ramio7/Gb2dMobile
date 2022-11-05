using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ReturnToMainMenuView : MonoBehaviour
{
    [SerializeField] private Button _returnToMainMenuButton;

    public void Init(UnityAction returnToMainMenu) =>
        _returnToMainMenuButton.onClick.AddListener(returnToMainMenu);

    private void OnDestroy() =>
        _returnToMainMenuButton.onClick.RemoveAllListeners();
}
