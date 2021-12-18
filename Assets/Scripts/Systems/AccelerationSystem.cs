using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    sealed class AccelerationSystem : IEcsRunSystem
    {

        //readonly EcsWorld _world = null;
        private EcsFilter<MovableComponent, AccelerationEvent> ships;

        void IEcsRunSystem.Run()
        {
            foreach (var index in ships)
            {
                ref MovableComponent ship = ref ships.Get1(index);
                AccelerationEvent acceleration = ships.Get2(index);

                float velocity = ship.currentVelocity + acceleration.acceleration * Time.deltaTime;
                if (velocity > ship.maxVelocity) velocity = ship.maxVelocity;
                if (velocity < -ship.maxVelocity) velocity = -ship.maxVelocity;

                ship.currentVelocity = velocity;
            }
        }
    }
}