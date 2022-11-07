using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace Homework9
{
    public class AssetBundleLoadingButtonController
    {
        private readonly AssetBundleLoadingButtonView _view;

        private AssetBundle _assetBundle;

        public AssetBundleLoadingButtonController(AssetBundleLoadingButtonView view)
        {
            _view = view;
            _view.ButtonToChange.onClick.AddListener(StartBackgroundChange);
            _view.StartCoroutine(LoadAssetBundle());
        }

        private void StartBackgroundChange()
        {
            DeactivateButton();
            ChangeButtonBackground();
        }

        private IEnumerator LoadAssetBundle()
        {
            UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(_view.AssetBundleUrl);

            yield return request.SendWebRequest();

            while (!request.isDone)
                yield return null;

            StateRequest(request, out _assetBundle);
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
            _view.ButtonToChange.image.sprite = _assetBundle.LoadAsset<Sprite>("board");
        }

        private void DeactivateButton()
        {
            _view.ButtonToChange.interactable = false;
        }
    }
}