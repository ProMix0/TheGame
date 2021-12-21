using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    sealed class EnemySpawnerSystem : IEcsRunSystem
    {
        private EcsWorld world;
        private EcsFilter<EnemySpawnerComponent, GameObjectComponent> spawners;

        public void Run()
        {
            foreach (var index in spawners)
            {
                ref EnemySpawnerComponent spawner = ref spawners.Get1(index);

                if (spawner.spawnLimit < spawner.spawned)
                {
                    spawners.GetEntity(index).Get<NeedRelocate>();
                    continue;
                }

                spawner.lastSpawnTime += Time.deltaTime;
                if (spawner.lastSpawnTime * Random.Range(0.5f, 2) > spawner.spawnPeriod)
                {
                    spawner.lastSpawnTime = 0;
                    spawner.spawned++;

                    EcsEntity ship = world.NewEntity();

                    ShipData shipData = spawner.shipData;
                    Utility.Bind(Object.Instantiate(shipData.prefab, spawners.Get2(index).gameObject.transform.position, Quaternion.identity), ref ship);

                    //ship.Get<CameraFollowComponent>();

                    ref MovableComponent movable = ref ship.Get<MovableComponent>();
                    movable.maxSpeed = shipData.maxSpeed;
                    movable.destination = Vector3.zero;

                    ship.Get<SetDestination>();
                }
            }
        }
    }
}