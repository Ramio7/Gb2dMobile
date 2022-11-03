using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Tween
{
    [RequireComponent(typeof(RectTransform))]
    public class CustomButtonByInheritance : Button
    {
        public static string AnimationTypeName => nameof(_animationButtonType);
        public static string CurveEaseName => nameof(_curveEase);
        public static string DurationName => nameof(_duration);
        public static string TimeDelay => nameof(_timeDelay);
        public static string LoopCount => nameof(_loopCount);

        [SerializeField] private RectTransform _rectTransform;

        [SerializeField] private AnimationButtonType _animationButtonType = AnimationButtonType.ChangePosition;
        [SerializeField] private Ease _curveEase = Ease.Linear;
        [SerializeField] private float _duration = 0.6f;
        [SerializeField] private float _strength = 30f;

        [Header("Loop Properties")]
        [SerializeField, Min(0)] private int _loopCount;
        [SerializeField, Min(0)] private float _timeDelay = 0;

        private Tweener _animation;

        protected override void Awake()
        {
            base.Awake();
            InitRectTransform();
        }

        protected override void OnValidate()
        {
            base.OnValidate();
            InitRectTransform();
        }

        private void InitRectTransform() =>
            _rectTransform = _rectTransform != null ? _rectTransform : GetComponent<RectTransform>();


        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);
            ActivateAnimation();
        }

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
