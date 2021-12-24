using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    sealed class ProjectileCollisionSystem : IEcsRunSystem
    {
        private EcsFilter<ProjectileComponent, GameObjectComponent> projectiles;

        public void Run()
        {
            foreach (var index in projectiles)
            {
                GameObject gameObject = projectiles.Get2(index).gameObject;
                ProjectileComponent projectileComponent = projectiles.Get1(index);

                bool destroyed = false;

                Collider[] targets = Physics.OverlapSphere(gameObject.transform.position, projectileComponent.radius);
                for (int i = 0; i < targets.Length; i++)
                {
                    EntityScript entityScript = targets[i].gameObject.GetComponent<EntityScript>();
                    if (entityScript == null) continue;
                    EcsEntity target = entityScript.entity;
                    if (target.Has<HealthComponent>())
                    {
                        ref HealthComponent health= ref target.Get<HealthComponent>();
                        health.health -= projectileComponent.damage;

                        destroyed = true;
                    }
                }

                if (destroyed)
                {
                    EcsEntity projectile = projectiles.GetEntity(index);

                    Utility.Disbind(projectile);

                    projectile.Destroy();
                }
            }
        }
    }
}