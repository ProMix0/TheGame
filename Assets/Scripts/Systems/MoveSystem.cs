using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    /// <summary>
    /// Система передвижения объектов 
    /// </summary>
    sealed class MoveSystem : IEcsRunSystem
    {
        // Объекты, не достигшие конечной точки
        private EcsFilter<MovableComponent, GameObjectComponent>.Exclude<ReachEndpointComponent> toMove;

        public void Run()
        {
            foreach (var index in toMove)
            {
                Transform transform = toMove.Get2(index).gameObject.transform;
                MovableComponent movable = toMove.Get1(index);
                Vector3 moving = movable.destination - transform.position; // Требуемые расстояние и направление

                Vector3 normalized = moving.normalized * (movable.maxSpeed * Time.deltaTime);

                // Если всё расстояние меньше пути за один кадр
                if (moving.magnitude < normalized.magnitude)
                    transform.position += moving;
                else
                    transform.position += normalized;
            }
        }
    }
}