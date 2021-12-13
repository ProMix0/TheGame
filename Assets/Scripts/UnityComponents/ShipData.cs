using UnityEngine;

namespace Client
{
    [CreateAssetMenu]
    public class ShipData : ScriptableObject
    {
        public int velocity;
        public int rotateVelocity;
    }
}