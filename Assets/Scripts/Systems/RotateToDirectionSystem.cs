using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    sealed class RotateToDirectionSystem : IEcsRunSystem
    {
        // auto-injected fields.
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