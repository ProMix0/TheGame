using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    /// <summary>
    /// Система передвижения камеры
    /// </summary>
    sealed class CameraFollowSystem : IEcsRunSystem
    {
        private EcsFilter<GameObjectComponent, CameraFollowComponent> toFollow;
        private SceneData sceneData;

        public void Run()
        {
            foreach (var index in toFollow)
            {
                Vector3 position = toFollow.Get1(index).gameObject.transform.position;
                position.y = sceneData.cameraHeight;
                sceneData.camera.transform.position = position;
            }
        }
    }
}