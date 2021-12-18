using Leopotam.Ecs;

namespace Client {
    sealed class DamageSystem : IEcsRunSystem {
        // auto-injected fields.
        private EcsFilter<HealthComponent, DealDamageComponent> ships;
        
        void IEcsRunSystem.Run () {
            foreach(var index in ships)
            {
                ref HealthComponent health = ref ships.Get1(index);
                health.currentHealth -= ships.Get2(index).damage;
            }
        }
    }
}