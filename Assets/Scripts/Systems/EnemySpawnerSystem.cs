using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    /// <summary>
    /// Система спавна кораблей
    /// </summary>
    sealed class EnemySpawnerSystem : IEcsRunSystem
    {
        private EcsWorld world;
        private EcsFilter<EnemySpawnerComponent, GameObjectComponent> spawners;

        public void Run()
        {
            foreach (var index in spawners)
            {
                ref EnemySpawnerComponent spawner = ref spawners.Get1(index);

                // Если точка выработала лимит
                if (spawner.spawnLimit < spawner.spawned)
                {
                    spawners.GetEntity(index).Get<NeedRelocate>();
                    continue;
                }

                // Проверка на частоту спавна
                spawner.lastSpawnTime += Time.deltaTime;
                if (spawner.lastSpawnTime * Random.Range(0.5f, 2) > spawner.spawnPeriod)
                {
                    spawner.lastSpawnTime = 0;
                    spawner.spawned++;

                    // Создание корабля
                    EcsEntity ship = world.NewEntity();

                    ShipData shipData = spawner.shipData;
                    Utility.Bind(Object.Instantiate(shipData.prefab, spawners.Get2(index).gameObject.transform.position, Quaternion.identity), ship);

                    ref MovableComponent movable = ref ship.Get<MovableComponent>();
                    movable.maxSpeed = shipData.maxSpeed;
                    movable.destination = Vector3.zero;

                    ship.Get<SetDestination>();
                }
            }
        }
    }
}