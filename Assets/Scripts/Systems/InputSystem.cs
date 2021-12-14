using Leopotam.Ecs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Client
{
    class InputSystem : IEcsRunSystem
    {
        //private EcsWorld world;
        private EcsFilter<ShipComponent> ships;

        public void Run()
        {
            int sign = 0;
            if (Input.GetKey(KeyCode.W))
                sign = 1;
            if (Input.GetKey(KeyCode.S))
                sign = -1;

            bool isRotate = false;
            bool left = false;
            if (Input.GetKey(KeyCode.A))
            {
                isRotate = true;
                left = true;
            }
            if (Input.GetKey(KeyCode.D))
            {
                left = false;
                isRotate = true;
            }

            foreach (var index in ships)
            {
                ref EcsEntity ship = ref ships.GetEntity(index);
                ShipComponent shipComponent = ships.Get1(index);

                ref AccelerationEvent move = ref ship.Get<AccelerationEvent>();
                move.acceleration = shipComponent.acceleration * sign;

                if (isRotate)
                {
                    ref RotateAccelerationEvent rotate = ref ship.Get<RotateAccelerationEvent>();
                    rotate.acceleration = left ? shipComponent.rotateAcceleration : -shipComponent.rotateAcceleration;
                }
            }
        }
    }
}
