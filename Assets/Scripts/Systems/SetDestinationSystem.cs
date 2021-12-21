using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    sealed class SetDestinationSystem : IEcsRunSystem
    {
        private EcsFilter<MovableComponent, SetDestination> setDestination;
        private EcsFilter<BaseComponent, GameObjectComponent> @base;

        public void Run()
        {
            Vector3 destination = default;
            foreach (var index in @base)
            {
                destination = @base.Get2(index).gameObject.transform.position;
                break;
            }
            foreach (var index in setDestination)
            {
                ref MovableComponent movable = ref setDestination.Get1(index);
                movable.destination = destination;
                setDestination.GetEntity(index).Del<SetDestination>();
            }
        }
    }
}