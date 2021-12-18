using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    sealed class CameraSystem : IEcsRunSystem, IEcsInitSystem
    {
        private const int cameraHeight = 200;

        // auto-injected fields.
        //readonly EcsWorld _world = null;
        private SceneData sceneData;
        private EcsFilter<ShipComponent, CameraFollowComponent> ships;

        public void Init()
        {
            //sceneData.camera.transform.rotation = new Quaternion(90, 90, 0, 0);
        }

        public void Run()
        {
            foreach (var index in ships)
            {
                ShipComponent ship = ships.Get1(index);
                sceneData.camera.transform.position = ship.ship.transform.position + new Vector3(0, cameraHeight, 0);
                break;
            }
        }
    }
}