using Leopotam.Ecs;
using UnityEngine;

namespace Client {
    public class RotateAccelerationSystem : IEcsRunSystem {
        // auto-injected fields.
        private EcsFilter<ShipComponent, RotateAccelerationEvent> ships;
        
        public void Run () {
            foreach(var index in ships)
            {
                ref ShipComponent ship = ref ships.Get1(index);
                RotateAccelerationEvent rotate = ships.Get2(index);

                float rotation = ship.currentRotateVelocity + rotate.acceleration * Time.deltaTime;
                if (rotation > ship.maxRotateVelocity) rotation = ship.maxRotateVelocity;
                if (rotation < -ship.maxRotateVelocity) rotation = -ship.maxRotateVelocity;

                ship.currentRotateVelocity = rotation;
            }
        }
    }
}