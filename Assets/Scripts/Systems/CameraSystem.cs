using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    sealed class CameraSystem : IEcsRunSystem, IEcsInitSystem
    {
        // auto-injected fields.
        //readonly EcsWorld _world = null;
        private SceneData sceneData;
        private EcsFilter<ShipComponent> ships;

        public void Init()
        {
            //sceneData.camera.transform.rotation = new Quaternion(90, 90, 0, 0);
        }

        void IEcsRunSystem.Run()
        {
            foreach (var index in ships)
            {
                ShipComponent ship = ships.Get1(index);
                sceneData.camera.transform.position = ship.transform.position + new Vector3(0, 30, 0);
            }
        }
    }
}