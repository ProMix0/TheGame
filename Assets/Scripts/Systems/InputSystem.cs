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
        private EcsFilter<ShipComponent, MovableComponent> ships;
        private SceneData sceneData;

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

            Vector3? mouse = null;
            if (Input.GetMouseButtonDown(0))
            {
                Plane plane = new(Vector3.up, Vector3.zero);
                Ray ray = sceneData.camera.ScreenPointToRay(Input.mousePosition);
                if (plane.Raycast(ray, out var hitDistance))
                {
                    mouse = ray.GetPoint(hitDistance);
                }
            }

            foreach (var index in ships)
            {
                ref EcsEntity ship = ref ships.GetEntity(index);
                ShipComponent shipComponent = ships.Get1(index);
                MovableComponent movable = ships.Get2(index);

                if (sign != 0)
                {
                    ref AccelerationEvent move = ref ship.Get<AccelerationEvent>();
                    move.acceleration = movable.acceleration * sign;
                }

                if (isRotate)
                {
                    ref RotateAccelerationEvent rotate = ref ship.Get<RotateAccelerationEvent>();
                    rotate.acceleration = left ? movable.rotateAcceleration : -movable.rotateAcceleration;
                }

                if (mouse != null && mouse.HasValue)
                {
                    ref ShootEvent shoot = ref ship.Get<ShootEvent>();
                    shoot.direction = mouse.Value - shipComponent.ship.transform.position;
                }
            }
        }
    }
}
