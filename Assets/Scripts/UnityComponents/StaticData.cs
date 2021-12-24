using UnityEngine;

namespace Client
{
    /// <summary>
    /// Общие настройки
    /// </summary>
    [CreateAssetMenu]
    internal class StaticData:ScriptableObject
    {
        public GameObject projectile;
        public ShipData ship;
        public GameObject turret;
        public GameObject spawnPoint;
        public GameObject @base;
    }
}