using Leopotam.Ecs;
using UnityEngine;

namespace Client {
    public class RotateAccelerationSystem : IEcsRunSystem {
        // auto-injected fields.
        private EcsFilter<MovableComponent, RotateAccelerationEvent> ships;
        
        public void Run () {
            foreach(var index in ships)
            {
                ref MovableComponent movable = ref ships.Get1(index);
                RotateAccelerationEvent rotate = ships.Get2(index);

                float rotation = movable.currentRotateVelocity + rotate.acceleration * Time.deltaTime;
                if (rotation > movable.maxRotateVelocity) rotation = movable.maxRotateVelocity;
                if (rotation < -movable.maxRotateVelocity) rotation = -movable.maxRotateVelocity;

                movable.currentRotateVelocity = rotation;
            }
        }
    }
}