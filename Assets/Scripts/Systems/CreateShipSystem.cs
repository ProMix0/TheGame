using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    internal class CreateShipSystem : IEcsInitSystem
    {
        private StaticData staticData;
        private EcsWorld world;

        public void Init()
        {
            ref ShipComponent ship = ref world.NewEntity().Get<ShipComponent>();
            ship.transform = Object.Instantiate(staticData.ship, Vector3.zero, Quaternion.Euler(90, 90, 0)).transform;
            ship.velocity = 100;
        }
    }
}