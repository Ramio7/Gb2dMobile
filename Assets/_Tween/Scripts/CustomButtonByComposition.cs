using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Tween
{
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(RectTransform))]
    public class CustomButtonByComposition : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Button _button;
        [SerializeField] private RectTransform _rectTransform;

        [Header("Settings")]
        [SerializeField] private AnimationButtonType _animationButtonType = AnimationButtonType.ChangePosition;
        [SerializeField] private Ease _curveEase = Ease.Linear;
        [SerializeField] private float _duration = 0.6f;
        [SerializeField] private float _strength = 30f;

        [Header("Loop Properties")]
        [SerializeField, Min(0)] private int _loopCount;
        [SerializeField, Min(0)] private float _timeDelay = 0;

        private Tweener _animation;

        private void OnValidate() => InitComponents();
        private void Awake() => InitComponents();

        private void Start() => _button.onClick.AddListener(OnButtonClick);
        private void OnDestroy() => _button.onClick.RemoveAllListeners();

        private void InitComponents()
        {
            _button = _button != null ? _button : GetComponent<Button>();
            _rectTransform = _rectTransform != null ? _rectTransform : GetComponent<RectTransform>();
        }


        private void OnButtonClick() =>
            ActivateAnimation();

        private void ActivateAnimation()
        {
            StopAnimation();

            switch (_animationButtonType)
            {
                case AnimationButtonType.ChangeRotation:
                    _animation = _rectTransform.DOShakeRotation(_duration, Vector3.forward * _strength).SetEase(_curveEase).SetLoops(_loopCount).SetDelay(_timeDelay);
                    break;

                case AnimationButtonType.ChangePosition:
                    _animation = _rectTransform.DOShakeAnchorPos(_duration, Vector2.one * _strength).SetEase(_curveEase).SetLoops(_loopCount).SetDelay(_timeDelay);
                    break;
            }
        }

        [ContextMenu(nameof(StopAnimation))]
        private void StopAnimation() => _animation?.Kill();
    }
}
