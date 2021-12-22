using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    /// <summary>
    /// Поворот по направлению движения
    /// </summary>
    sealed class RotateToDirectionSystem : IEcsRunSystem
    {
        private EcsFilter<MovableComponent, GameObjectComponent>.Exclude<ReachEndpointComponent> toRotate;

        public void Run()
        {
            foreach (var index in toRotate)
            {
                Transform transform = toRotate.Get2(index).gameObject.transform;
                transform.forward = toRotate.Get1(index).destination - transform.position;
            }
        }
    }
}