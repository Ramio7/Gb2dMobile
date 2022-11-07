using UnityEngine;
using UnityEngine.UI;

namespace Homework9
{
    public class AssetBundleLoadingButtonView : MonoBehaviour
    {
        [field: SerializeField] public string AssetBundleUrl { get; private set; }
        [field: SerializeField] public Button ButtonToChange { get; private set; }

        private void Awake()
        {
            ButtonToChange = ButtonToChange != null ? ButtonToChange : GetComponent<Button>();
        }

        private void Start()
        {
            new AssetBundleLoadingButtonController(this);
        }
    }
}