using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Homework9
{
    public class AssetBundleChangingSpriteButtonView : MonoBehaviour
    {
        [SerializeField] private string _assetBundleUrl;
        [SerializeField] private Button _buttonToChange;

        private AssetBundle _assetBundle;

        private void Awake()
        {
            _buttonToChange = _buttonToChange != null ? _buttonToChange : GetComponent<Button>();
        }

        private void Start()
        {
            
        }

        private void StartBackgroundChange()
        {
            DeactivateButton();
            ChangeButtonBackground();
        }

        private IEnumerator LoadAssetBundle()
        {
            UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(_assetBundleUrl);

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
            _buttonToChange.image.sprite = _assetBundle.LoadAsset<Sprite>("board");
        }

        private void DeactivateButton()
        {
            _buttonToChange.interactable = false;
        }
    }
}