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
        public CameraData cameraData;

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
                .Add(new MoveSystem())
                .Add(new CameraFollowSystem())

                .Inject(cameraData)
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