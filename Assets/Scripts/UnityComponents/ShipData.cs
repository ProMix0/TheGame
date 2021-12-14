using UnityEngine;

namespace Client
{
    [CreateAssetMenu]
    public class ShipData : ScriptableObject
    {
        public int maxVelocity;
        public int acceleration;

        public int maxRotateVelocity;
        public int rotateAcceleration;
    }
}