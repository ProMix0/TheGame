using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    sealed class CreateLevelSystem : IEcsInitSystem
    {
        // auto-injected fields.
        readonly EcsWorld world = null;
        private SceneData sceneData;
        private StaticData staticData;

        public void Init()
        {
            EcsEntity ship = world.NewEntity();

            Utility.Bind(Object.Instantiate(staticData.ship, new Vector3(1000, 0, 1000), Quaternion.identity), ref ship);

            ship.Get<CameraFollowComponent>();

            ref MovableComponent movable = ref ship.Get<MovableComponent>();
            movable.maxSpeed = sceneData.ship.maxSpeed;
            movable.destination = Vector3.zero;
        }
    }
}