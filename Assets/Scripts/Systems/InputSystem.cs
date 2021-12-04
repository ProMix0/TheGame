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
            Vector2 vector = new Vector2();
            if (Input.GetKeyDown(KeyCode.W))
                vector.x = 1;
            if (Input.GetKeyDown(KeyCode.S))
                vector.x = -1;
            foreach (var index in ships)
            {
                ref EcsEntity ship = ref ships.GetEntity(index);
                ref InputEvent input = ref ship.Get<InputEvent>();
                input.direction = vector;
            }
        }
    }
}
