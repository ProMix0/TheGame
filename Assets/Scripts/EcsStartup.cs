using Leopotam.Ecs;
using Leopotam.Ecs.UnityIntegration;
using UnityEngine;

namespace Client
{
    /// <summary>
    /// Класс, запускающий все системы ECS
    /// </summary>
    sealed class EcsStartup : MonoBehaviour
    {
        EcsWorld world;
        EcsSystems systems, fixedSystems;

        // Свойства, задаваемые из Unity
        public StaticData staticData;
        public SceneData sceneData;

        void Start()
        {
            world = new EcsWorld();
            systems = new EcsSystems(world);
            fixedSystems = new EcsSystems(world);

            // ¯\_(ツ)_/¯
#if UNITY_EDITOR
            EcsWorldObserver.Create(world);
            EcsSystemsObserver.Create(systems);
            EcsSystemsObserver.Create(fixedSystems);
#endif

            systems
                
                // Добавляем системы
                .Add(new CreateLevelSystem())

                .Add(new PlaceTurretSystem())
                .Add(new EnemySpawnerSystem())
                .Add(new SpawnerRelocateSystem())
                .Add(new SetDestinationSystem())
                .Add(new TargetingSystem())

                .Add(new MoveToTargetSystem())
                .Add(new MoveSystem())
                .Add(new WasdMovingSystem())
                .Add(new ReachEndpointSystem())
                .Add(new RotateToDirectionSystem())
                .Add(new CameraFollowSystem())

                //.Add(new DrawRadiationSystem())

                .Add(new ProjectileCollisionSystem())
                .Add(new DeleteReachedSystem())
                .Add(new DeleteZeroHealthSystem())

                // Заполняем поля систем этими объектами (DI)
                .Inject(staticData)
                .Inject(sceneData)

                .Init();

            fixedSystems

                //.Add(new EffectRadiationSystem())

                .Inject(staticData)
                .Inject(sceneData)

                .Init();
        }

        /// <summary>
        /// Запуск систем каждый кадр
        /// </summary>
        void Update()
        {
            systems?.Run();
        }

        void FixedUpdate()
        {
            fixedSystems?.Run();    
        }

        void OnDestroy()
        {
            if (systems != null)
            {
                fixedSystems.Destroy();
                fixedSystems = null;
                systems.Destroy();
                systems = null;
                world.Destroy();
                world = null;
            }
        }
    }
}