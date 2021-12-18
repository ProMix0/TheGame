﻿using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    internal class CreateShipSystem : IEcsInitSystem
    {
        private StaticData staticData;
        private EcsWorld world;
        private ShipData shipData;

        public CreateShipSystem(ShipData shipData)
        {
            this.shipData = shipData;
        }

        public void Init()
        {
            EcsEntity entity = world.NewEntity();

            ref ShipComponent ship = ref entity.Get<ShipComponent>();
            ship.ship = Object.Instantiate(staticData.ship);

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