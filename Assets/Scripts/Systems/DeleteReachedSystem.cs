using Leopotam.Ecs;

namespace Client {
    sealed class DeleteReachedSystem : IEcsRunSystem {
        // auto-injected fields.
        readonly EcsWorld _world = null;
        
        void IEcsRunSystem.Run () {
            // add your run code here.
        }
    }
}