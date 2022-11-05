using Features.Shed.Upgrade;

namespace Game.Car
{
    internal class CarModel : IUpgradable
    {
        private readonly float _defaultSpeed;
        private readonly float _jumpHeight;

        public float Speed { get; set; }
        public float JumpHeight { get; set; }

        public CarModel(float speed, float jumpHeight)
        {
            _defaultSpeed = speed;
            _jumpHeight = jumpHeight;
            Speed = speed;
            JumpHeight = jumpHeight;
        }


        public void Restore()
        {
            Speed = _defaultSpeed;
            JumpHeight = _jumpHeight;
        }
    }
}
