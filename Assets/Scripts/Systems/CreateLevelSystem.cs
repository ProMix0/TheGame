using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    /// <summary>
    /// Система, создающая новый уровень
    /// </summary>
    sealed class CreateLevelSystem : IEcsInitSystem
    {
        readonly EcsWorld world = null;
        private SceneData sceneData;
        private StaticData staticData;

        public void Init()
        {
            // Создание точек появления противников
            for (int i = Random.Range(0, 5); i <= 5; i++)
            {
                EcsEntity spawnPoint = world.NewEntity();

                // Привязка к GameObject'у
                Utility.Bind(Object.Instantiate(staticData.spawnPoint), spawnPoint);

                ref RadiationComponent radiation = ref spawnPoint.Get<RadiationComponent>();
                //TODO (SceneData or StaticData)

                ref EnemySpawnerComponent enemySpawner = ref spawnPoint.Get<EnemySpawnerComponent>();
                enemySpawner.shipData = staticData.ship;
                enemySpawner.spawnLimit = Random.Range(5, 15);
                enemySpawner.spawnPeriod = Random.Range(3, 10);
                enemySpawner.lastSpawnTime = 0;

                spawnPoint.Get<NeedRelocate>();
            }

            // Создание базы
            EcsEntity @base = world.NewEntity();

            Utility.Bind(Object.Instantiate(staticData.@base, new Vector3(Random.Range(0, sceneData.height), 0, Random.Range(0, sceneData.width)), Quaternion.identity), @base);

            ref BaseComponent baseComponent = ref @base.Get<BaseComponent>();

            //
            EcsEntity camera = world.NewEntity();

            Utility.Bind(new GameObject(), camera);

            camera.Get<CameraFollowComponent>();

            ref WasdMovableComponent movableComponent = ref camera.Get<WasdMovableComponent>();
            movableComponent.speed = 100;
        }
    }
}