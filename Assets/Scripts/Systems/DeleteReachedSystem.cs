using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    /// <summary>
    /// Система для удаления объектов, достигших цели
    /// </summary>
    sealed class DeleteReachedSystem : IEcsRunSystem
    {
        private EcsFilter<GameObjectComponent, ReachEndpointComponent> toDestroy;

        public void Run()
        {
            foreach(var index in toDestroy)
            {
                GameObject gameObject = toDestroy.Get1(index).gameObject;
                EcsEntity entity = toDestroy.GetEntity(index);

                Utility.Disbind(entity);

                Object.Destroy(gameObject);

                entity.Destroy();
            }
        }
    }
}