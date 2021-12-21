using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    sealed class SpawnerRelocateSystem : IEcsRunSystem
    {
        private EcsFilter<EnemySpawnerComponent, GameObjectComponent, NeedRelocate> toRelocate;
        private SceneData sceneData;

        public void Run()
        {
            foreach (var index in toRelocate)
            {
                toRelocate.Get2(index).gameObject.transform.position = new Vector3(Random.Range(0, sceneData.height), 0, Random.Range(0, sceneData.width));

                ref EnemySpawnerComponent spawner = ref toRelocate.Get1(index);
                spawner.spawnLimit += Random.Range(1, 3);
                spawner.spawned = 0;
                spawner.spawnPeriod -= Random.Range(0, 2);

                toRelocate.GetEntity(index).Del<NeedRelocate>();
            }
        }
    }
}