using Leopotam.Ecs;
using UnityEngine;

namespace Client {
    sealed class CreateLevelSystem : IEcsInitSystem {
        // auto-injected fields.
        readonly EcsWorld world = null;
        private StaticData staticData;
        private ShipData shipData;

        public void Init () {
            EcsEntity entity = world.NewEntity();

            ref ShipComponent ship = ref entity.Get<ShipComponent>();
            ship.ship = Object.Instantiate(staticData.ship);
            ship.ship.transform.position = new(100,0,0);

            ref MovableComponent movable = ref entity.Get<MovableComponent>();
            movable.maxVelocity = shipData.maxVelocity;
            movable.maxRotateVelocity = shipData.maxRotateVelocity;
            movable.acceleration = shipData.acceleration;
            movable.rotateAcceleration = shipData.rotateAcceleration;

            entity.Get<CameraFollowComponent>();

            ref HealthComponent health = ref entity.Get<HealthComponent>();
            health.maxHealth = shipData.maxHealth;
            health.currentHealth = shipData.maxHealth;
        }
    }
}