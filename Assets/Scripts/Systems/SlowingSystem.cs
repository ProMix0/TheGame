using Leopotam.Ecs;
using System;
using UnityEngine;

namespace Client
{
    sealed class SlowingSystem : IEcsRunSystem
    {
        // auto-injected fields.
        //readonly EcsWorld _world = null;
        private EcsFilter<ShipComponent>.Exclude<AccelerationEvent> ships;

        void IEcsRunSystem.Run()
        {
            foreach (var index in ships)
            {
                ref ShipComponent ship = ref ships.Get1(index);

                float velocity = ship.currentVelocity;
                if (velocity > 1) velocity -= 1 * Time.deltaTime;
                else if (velocity < -1) velocity += 1 * Time.deltaTime;
                else velocity = 0;
                ship.currentVelocity = velocity;
            }
        }
    }
}