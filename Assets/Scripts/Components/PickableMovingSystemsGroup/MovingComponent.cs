using UnityEngine;

namespace DemoProject
{
    public struct MovingComponent
    {
        public Vector3 DirectionToMove;
        public float TimeCounter;
        public float MovingSpeedRange;
        public bool IsSet;
    }
}