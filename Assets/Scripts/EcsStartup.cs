using Leopotam.Ecs;
using Leopotam.Ecs.UnityIntegration;
using UnityEngine;

namespace Client
{
    sealed class EcsStartup : MonoBehaviour
    {
        EcsWorld world;
        EcsSystems systems;

        public StaticData staticData;
        public SceneData sceneData;

        void Start()
        {
            world = new EcsWorld();
            systems = new EcsSystems(world);
#if UNITY_EDITOR
            EcsWorldObserver.Create(world);
            EcsSystemsObserver.Create(systems);
#endif
            systems
                
                .Add(new CreateLevelSystem())
                .Add(new EnemySpawnerSystem())
                .Add(new SpawnerRelocateSystem())
                .Add(new SetDestinationSystem())

                .Add(new MoveSystem())
                .Add(new ReachEndpointSystem())
                .Add(new RotateToDirectionSystem())
                .Add(new CameraFollowSystem())

                .Inject(staticData)
                .Inject(sceneData)

                .Init();
        }

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