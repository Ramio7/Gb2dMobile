using Profile;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = nameof(GameConfig), menuName = "Configs/" + nameof(GameConfig))]
    internal class GameConfig : ScriptableObject
    {
        [field: SerializeField] public GameState StartingGameState { get; private set; }
        [field: SerializeField] public float CarSpeed { get; private set; }
        [field: SerializeField] public float CarJumpHeight { get; private set; }
    }
}