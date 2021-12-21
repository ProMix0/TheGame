using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    sealed class CreateLevelSystem : IEcsInitSystem
    {
        readonly EcsWorld world = null;
        private SceneData sceneData;
        private StaticData staticData;

        public void Init()
        {
            for (int i = Random.Range(0, 5); i <= 5; i++)
            {
                EcsEntity spawnPoint = world.NewEntity();

                Utility.Bind(Object.Instantiate(staticData.spawnPoint), ref spawnPoint);

                ref EnemySpawnerComponent enemySpawner = ref spawnPoint.Get<EnemySpawnerComponent>();
                enemySpawner.shipData = staticData.ship;
                enemySpawner.spawnLimit = Random.Range(5, 15);
                enemySpawner.spawnPeriod = Random.Range(3, 10);
                enemySpawner.lastSpawnTime = 0;

                spawnPoint.Get<NeedRelocate>();
            }

            EcsEntity @base = world.NewEntity();

            Utility.Bind(Object.Instantiate(staticData.@base, new Vector3(Random.Range(0, sceneData.height), 0, Random.Range(0, sceneData.width)), Quaternion.identity), ref @base);

            ref BaseComponent baseComponent = ref @base.Get<BaseComponent>();
        }
    }
}