using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    sealed class TargetingSystem : IEcsRunSystem
    {
        private EcsWorld world;
        private EcsFilter<TurretComponent, GameObjectComponent> turrets;
        private StaticData staticData;

        public void Run()
        {
            foreach (var index in turrets)
            {
                EcsEntity turret = turrets.GetEntity(index);
                GameObject gameObject = turrets.Get2(index).gameObject;
                ref TurretComponent turretComponent = ref turrets.Get1(index);

                if (!turret.Has<TargetingComponent>() || !turret.Get<TargetingComponent>().target.IsAlive() || DistanceToTarget(turret) > turretComponent.range )
                {
                    turret.Del<TargetingComponent>();

                    Collider[] targets = Physics.OverlapSphere(gameObject.transform.position, turretComponent.range);
                    for (int i = 0; i < targets.Length; i++)
                    {
                        GameObject targetGameObject = targets[i].gameObject;
                        if (targetGameObject == gameObject) continue;
                        EntityScript entityScript = targetGameObject.GetComponent<EntityScript>();
                        if (entityScript == null) continue;
                        EcsEntity target = entityScript.entity;
                        if (!target.Has<HealthComponent>()) continue;
                        ref TargetingComponent targeting = ref turret.Get<TargetingComponent>();
                        targeting.target = target;
                        break;
                    }
                }

                if (turret.Has<TargetingComponent>())
                {
                    turretComponent.lastShootTime += Time.deltaTime;

                    if (turretComponent.lastShootTime >= 1 / turretComponent.firerate)
                    {
                        turretComponent.lastShootTime = 0;

                        EcsEntity projectile = world.NewEntity();

                        Utility.Bind(Object.Instantiate(staticData.projectile, gameObject.transform.position, Quaternion.identity), projectile);

                        ref TargetingComponent targeting = ref projectile.Get<TargetingComponent>();
                        targeting.target = turret.Get<TargetingComponent>().target;

                        ref MovableComponent movable = ref projectile.Get<MovableComponent>();
                        movable.maxSpeed = 50;

                        ref ProjectileComponent projectileComponent = ref projectile.Get<ProjectileComponent>();
                        projectileComponent.damage = 1;
                        projectileComponent.radius = 5;
                    }
                }
            }
        }

        float DistanceToTarget(EcsEntity entity)
        {
            Vector3 position = entity.Get<GameObjectComponent>().gameObject.transform.position;
            Vector3 destination = entity.Get<TargetingComponent>().target.Get<GameObjectComponent>().gameObject.transform.position;
            return (destination - position).magnitude;
        }
    }
}