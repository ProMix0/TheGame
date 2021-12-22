using Leopotam.Ecs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Client
{
    /// <summary>
    /// Вспомогательный класс
    /// </summary>
    static class Utility
    {
        /// <summary>
        /// Метод связывания сущности и GameIbject'а. Добавляет перекрёстные ссылки
        /// </summary>
        /// <param name="gameObject">GameObject</param>
        /// <param name="entity">Сущность</param>
        public static void Bind(GameObject gameObject, EcsEntity entity)
        {
            // Добавляем компонент Unity
            gameObject.AddComponent<EntityScript>().entity = entity;

            // Добавляем компонент ECS
            ref GameObjectComponent component = ref entity.Get<GameObjectComponent>();
            component.gameObject = gameObject;
        }

        /// <summary>
        /// Удаляет перекрёстные ссылки
        /// </summary>
        /// <param name="entity"></param>
        public static void Disbind(EcsEntity entity)
        {
            ref GameObjectComponent component = ref entity.Get<GameObjectComponent>();

            GameObject gameObject = component.gameObject;
            UnityEngine.Object.Destroy(gameObject.GetComponent<EntityScript>());

            component.gameObject = null;
            entity.Del<GameObjectComponent>();
        }
    }
}
