using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    sealed class MoveSystem : IEcsRunSystem
    {

        private EcsFilter<MovableComponent, GameObjectComponent>.Exclude<ReachEndpointComponent> toMove;

        public void Run()
        {
            foreach (var index in toMove)
            {
                Transform transform = toMove.Get2(index).gameObject.transform;
                MovableComponent movable = toMove.Get1(index);
                Vector3 moving = movable.destination - transform.position;

                Vector3 normalized = moving.normalized * (movable.maxSpeed * Time.deltaTime);

                if (moving.magnitude < normalized.magnitude)
                    transform.position += moving;
                else
                {
                    transform.position += normalized;
                }
            }
        }
    }
}