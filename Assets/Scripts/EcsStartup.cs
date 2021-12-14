using Leopotam.Ecs;
using Leopotam.Ecs.UnityIntegration;
using UnityEngine;

namespace Client
{
    sealed class EcsStartup : MonoBehaviour
    {
        EcsWorld _world;
        EcsSystems _systems;

        public StaticData staticData;
        public SceneData sceneData;
        public CameraData cameraData;

        void Start()
        {
            _world = new EcsWorld();
            _systems = new EcsSystems(_world);
#if UNITY_EDITOR
            EcsWorldObserver.Create(_world);
            EcsSystemsObserver.Create(_systems);
#endif
            _systems
                .Add(new CreateShipSystem(sceneData.ship))
                .Add(new InputSystem())

                .Add(new AccelerationSystem())
                .Add(new RotateAccelerationSystem())

                .Add(new MoveSystem())
                .Add(new RotateSystem())

                .Add(new CameraSystem())

                .Inject(cameraData)
                .Inject(staticData)
                .Inject(sceneData)

                .OneFrame<AccelerationEvent>()
                .OneFrame<RotateAccelerationEvent>()

                .Init();
        }

        void Update()
        {
            _systems?.Run();
        }

        void OnDestroy()
        {
            if (_systems != null)
            {
                _systems.Destroy();
                _systems = null;
                _world.Destroy();
                _world = null;
            }
        }
    }
}