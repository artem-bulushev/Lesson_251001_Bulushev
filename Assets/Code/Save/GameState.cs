using System;

namespace Code
{
    [Serializable]
    public sealed class GameState
    {
        public SerializableVector3 PlayerPosition;
        public SerializableVector3 PlayerRotation;
        public int TargetCount;
        public SerializableVector3[] TargetPositions;

        public GameState(int target)
        {
            TargetPositions = new SerializableVector3[target];
        }
    }
}