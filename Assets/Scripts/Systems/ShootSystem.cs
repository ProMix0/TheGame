using Leopotam.Ecs;
using UnityEngine;

namespace Client {
    sealed class ShootSystem : IEcsRunSystem {
        // auto-injected fields.
        private EcsWorld world;
        private EcsFilter<ShipComponent, ShootEvent> shoots;
        private StaticData staticData;
        
        void IEcsRunSystem.Run () {
            foreach(var index in shoots)
            {
                ShipComponent ship = shoots.Get1(index);
                ShootEvent shoot = shoots.Get2(index);

                EcsEntity projectile = world.NewEntity();

                ref ProjectileComponent projectileComponent=ref projectile.Get<ProjectileComponent>();
                projectileComponent.projectile = Object.Instantiate(staticData.projectile, ship.ship.transform.position, Quaternion.FromToRotation(Vector3.zero, shoot.direction));

                ref MovableComponent movable=ref projectile.Get<MovableComponent>();
                movable.acceleration = 0;
                movable.currentVelocity = 13;
                movable.maxVelocity = 13;

                movable.rotateAcceleration = 0;
                movable.currentRotateVelocity = 0;
                movable.maxRotateVelocity = 0;

                ref DamageComponent damage = ref projectile.Get<DamageComponent>();
                damage.damage = 10;
            }
        }
    }
}