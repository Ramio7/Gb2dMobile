using System.Collections;
using Tool.Bundles.Examples;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Homework9
{
    internal class AssetBundleChangingSpriteButtonView : AssetBundleViewBase
    {
        [SerializeField] private string _assetBundleUrl;
        [SerializeField] private string _assetName;
        [SerializeField] private Button _buttonToChange;

        private AssetBundle _assetBundle;

        private void Awake()
        {
            _buttonToChange = _buttonToChange != null ? _buttonToChange : GetComponent<Button>();
        }

        private void Start()
        {
            StartCoroutine(nameof(LoadAssetBundle));
            _buttonToChange.onClick.AddListener(SetNewButtonProperties);
        }

        private void OnDestroy()
        {
            StopCoroutine(nameof(LoadAssetBundle));
            _buttonToChange.onClick.RemoveListener(SetNewButtonProperties);
        }

        private void SetNewButtonProperties()
        {
            DeactivateButton();
            ChangeButtonBackground();
            _assetBundle.Unload(true);
        }

        private IEnumerator LoadAssetBundle()
        {
            UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(_assetBundleUrl);

            yield return request.SendWebRequest();

            while (!request.isDone)
                yield return null;

            StateRequest(request, out _assetBundle);

            StopCoroutine(nameof(LoadAssetBundle));
        }

        private void StateRequest(UnityWebRequest request, out AssetBundle assetBundle)
        {
            if (request.error == null)
            {
                assetBundle = DownloadHandlerAssetBundle.GetContent(request);
                Debug.Log("Complete");
            }
            else
            {
                assetBundle = null;
                Debug.LogError(request.error);
            }
        }

        private void ChangeButtonBackground()
        {
            _buttonToChange.image.sprite = _assetBundle.LoadAsset<Sprite>(_assetName);
        }

        private void DeactivateButton()
        {
            _buttonToChange.interactable = false;
        }
    }
}