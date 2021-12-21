using UnityEngine;

namespace Client
{
    [CreateAssetMenu]
    internal class StaticData:ScriptableObject
    {
        public GameObject projectile;
        public ShipData ship;
        public GameObject spawnPoint;
        public GameObject @base;
    }
}