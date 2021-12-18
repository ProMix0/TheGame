using Leopotam.Ecs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Client
{
    class RotateSystem : IEcsRunSystem
    {
        private EcsFilter<ShipComponent, MovableComponent> ships;
        public void Run()
        {
            foreach (var index in ships)
            {
                ShipComponent ship = ships.Get1(index);
                Quaternion temp = ship.ship.transform.rotation;

                MovableComponent movable = ships.Get2(index);
                temp.eulerAngles += new Vector3(0, -movable.currentRotateVelocity, 0) * Time.deltaTime;
                ship.ship.transform.rotation = temp;
            }
        }
    }
}
