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
        private EcsFilter<ShipComponent, RotateEvent> ships;
        public void Run()
        {
            foreach (var index in ships)
            {
                ShipComponent ship = ships.Get1(index);
                RotateEvent rotate = ships.Get2(index);
                Quaternion temp = ship.transform.rotation;
                temp.eulerAngles += new Vector3(0, rotate.left ? -ship.rotateVelocity : ship.rotateVelocity, 0) * Time.deltaTime;
                ship.transform.rotation = temp;
            }
        }
    }
}
