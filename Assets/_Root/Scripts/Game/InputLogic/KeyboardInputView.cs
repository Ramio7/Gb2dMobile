using JoostenProductions;
using UnityEngine;

namespace Game.InputLogic
{
    internal class KeyboardInputView : BaseInputView
    {
        [SerializeField] private float _inputMultiplier = 1;

        private void Start() =>
            UpdateManager.SubscribeToUpdate(Move);

        private void OnDestroy() =>
            UpdateManager.UnsubscribeFromUpdate(Move);


        private void Move()
        {
            float axisOffset = Input.GetAxis("Horizontal");
            float moveValue = _inputMultiplier * Time.deltaTime * axisOffset * _speed;

            float abs = Mathf.Abs(moveValue);
            float sign = Mathf.Sign(moveValue);

            if (sign > 0)
                OnRightMove(abs);
            else if (sign < 0)
                OnLeftMove(abs);
        }
    }
}