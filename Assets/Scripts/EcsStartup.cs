using Leopotam.Ecs;
using Leopotam.Ecs.UnityIntegration;
using UnityEngine;

namespace Client {
    sealed class EcsStartup : MonoBehaviour {
        EcsWorld _world;
        EcsSystems _systems;

        public StaticData staticData;
        public SceneData sceneData;

        void Start () {
            _world = new EcsWorld ();
            _systems = new EcsSystems (_world);
#if UNITY_EDITOR
            EcsWorldObserver.Create (_world);
            EcsSystemsObserver.Create (_systems);
#endif
            _systems
                .Add(new CreateShipSystem())
                .Add(new InputSystem())
                .Add(new MoveShipSystem())
                .Add(new CameraSystem())

                .Inject(staticData)
                .Inject(sceneData)

                .OneFrame<InputEvent>()

                .Init ();
        }

        void Update () {
            _systems?.Run ();
        }

        void OnDestroy () {
            if (_systems != null) {
                _systems.Destroy ();
                _systems = null;
                _world.Destroy ();
                _world = null;
            }
        }
    }
}