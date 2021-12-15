using Leopotam.Ecs;
using System;
using UnityEngine;

namespace Client
{
    sealed class RotateSlowingSystem : IEcsRunSystem
    {
        // auto-injected fields.
        private EcsFilter<ShipComponent>.Exclude<RotateAccelerationEvent> ships;

        void IEcsRunSystem.Run()
        {
            foreach (var index in ships)
            {
                ref ShipComponent ship = ref ships.Get1(index);

                float velocity = ship.currentRotateVelocity;
                if (velocity > 1) velocity -= 1 * Time.deltaTime;
                else if (velocity < -1) velocity += 1 * Time.deltaTime;
                else velocity = 0;
                ship.currentRotateVelocity = velocity;
            }
        }
    }
}