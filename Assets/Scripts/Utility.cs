using Leopotam.Ecs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Client
{
    static class Utility
    {
        public static void Bind(GameObject gameObject, ref EcsEntity entity)
        {
            gameObject.AddComponent<EntityScript>().entity = entity;
            ref GameObjectComponent component = ref entity.Get<GameObjectComponent>();
            component.gameObject = gameObject;
        }
    }
}
