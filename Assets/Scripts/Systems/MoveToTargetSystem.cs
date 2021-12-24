using Leopotam.Ecs;

namespace Client
{
    sealed class MoveToTargetSystem : IEcsRunSystem
    {
        private EcsFilter<TargetingComponent> targeted;

        public void Run()
        {
            foreach (var index in targeted)
            {
                ref MovableComponent movable = ref targeted.GetEntity(index).Get<MovableComponent>();
                EcsEntity target = targeted.Get1(index).target;
                if (target.IsAlive())
                    movable.destination = target.Get<GameObjectComponent>().gameObject.transform.position;
            }
        }
    }
}