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
        EcsSystems systems;

        // Свойства, задаваемые из Unity
        public StaticData staticData;
        public SceneData sceneData;

        void Start()
        {
            world = new EcsWorld();
            systems = new EcsSystems(world);

            // ¯\_(ツ)_/¯
#if UNITY_EDITOR
            EcsWorldObserver.Create(world);
            EcsSystemsObserver.Create(systems);
#endif

            systems
                
                // Добавляем системы
                .Add(new CreateLevelSystem())
                .Add(new EnemySpawnerSystem())
                .Add(new SpawnerRelocateSystem())
                .Add(new SetDestinationSystem())

                .Add(new MoveSystem())
                .Add(new ReachEndpointSystem())
                .Add(new RotateToDirectionSystem())
                .Add(new CameraFollowSystem())

                .Add(new DeleteReachedSystem())

                // Заполняем поля систем этими объектами (DI)
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

        void OnDestroy()
        {
            if (systems != null)
            {
                systems.Destroy();
                systems = null;
                world.Destroy();
                world = null;
            }
        }
    }
}