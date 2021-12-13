using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    internal class CreateShipSystem : IEcsInitSystem
    {
        private StaticData staticData;
        private EcsWorld world;
        private ShipData shipData;

        public CreateShipSystem(ShipData shipData)
        {
            this.shipData = shipData;
        }

        public void Init()
        {
            EcsEntity entity = world.NewEntity();
            ref ShipComponent ship = ref entity.Get<ShipComponent>();
            ship.transform = Object.Instantiate(staticData.ship).transform;
            ship.velocity = shipData.velocity;
            ship.rotateVelocity = shipData.rotateVelocity;

            entity.Get<CameraFollowComponent>();
        }
    }
}