using Leopotam.Ecs;

namespace Client {
    sealed class ReachEndpointSystem : IEcsRunSystem {
        // auto-injected fields.
        private EcsFilter<MovableComponent, GameObjectComponent>.Exclude<ReachEndpointComponent> reached;
        
        void IEcsRunSystem.Run () {
            foreach(var index in reached)
            {
                if (reached.Get1(index).destination == reached.Get2(index).gameObject.transform.position)
                    reached.GetEntity(index).Get<ReachEndpointComponent>();
            }
        }
    }
}