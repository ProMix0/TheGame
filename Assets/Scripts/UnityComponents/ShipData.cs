using UnityEngine;

namespace Client
{
    /// <summary>
    /// Параметры корабля
    /// </summary>
    [CreateAssetMenu]
    public class ShipData : ScriptableObject
    {
        public GameObject prefab;

        public int maxSpeed;
    }
}