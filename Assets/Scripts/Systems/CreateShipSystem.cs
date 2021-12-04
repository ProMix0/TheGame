using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    internal class CreateShipSystem : IEcsInitSystem
    {
        private StaticData staticData;

        public void Init()
        {
            Object.Instantiate(staticData.ship, Vector3.zero, Quaternion.Euler(90,0,0));
        }
    }
}