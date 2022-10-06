using Features.Inventory;
using Game.Car;
using Tool;

namespace Profile
{
    internal class ProfilePlayer
    {
        public readonly SubscriptionProperty<GameState> CurrentState;
        public readonly InventoryModel Inventory;
        public readonly CarModel CurrentCar;


        public ProfilePlayer(float speedCar, float jumpHeight, GameState initialState) : this(speedCar, jumpHeight)
        {
            CurrentState.Value = initialState;
        }

        public ProfilePlayer(float speedCar, float jumpHeightCar)
        {
            CurrentState = new SubscriptionProperty<GameState>();
            CurrentCar = new CarModel(speedCar, jumpHeightCar);
            Inventory = new InventoryModel();
        }
    }
}
