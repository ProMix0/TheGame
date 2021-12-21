using UnityEngine;

namespace Client
{
    [CreateAssetMenu]
    public class ShipData : ScriptableObject
    {
        public GameObject prefab;

        public int maxSpeed;
    }
}