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
            ship.transform = Object.Instantiate(staticData.ship).transform;
            ship.velocity = 100;
            ship.rotateVelocity = 10;
        }
    }
}