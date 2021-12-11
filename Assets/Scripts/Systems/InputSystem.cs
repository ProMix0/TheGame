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
        private EcsWorld world;
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

                ref MoveEvent move = ref ship.Get<MoveEvent>();
                move.movingVelocity = ships.Get1(index).velocity * sign;

                if (isRotate)
                {
                    ref RotateEvent rotate = ref ship.Get<RotateEvent>();
                    rotate.left = left;
                }
            }
        }
    }
}
