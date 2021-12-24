using Leopotam.Ecs;

namespace Client
{
    sealed class DeleteZeroHealthSystem : IEcsRunSystem
    {

        private EcsFilter<HealthComponent> haveHealth;
        public void Run()
        {
            foreach (var index in haveHealth)
            {
                HealthComponent health = haveHealth.Get1(index);

                if (health.health <= 0)
                {
                    EcsEntity entity = haveHealth.GetEntity(index);

                    Utility.Disbind(entity);

                    entity.Destroy();
                }
            }
        }
    }
}