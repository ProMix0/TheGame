using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    sealed class EffectRadiationSystem : IEcsRunSystem
    {
        private EcsWorld world;
        private EcsFilter<RadiationComponent, GameObjectComponent> radioactive;

        public void Run()
        {
            foreach (var index in radioactive)
            {
                RadiationComponent radiation = radioactive.Get1(index);
                GameObject gameObject = radioactive.Get2(index).gameObject;
                Transform transform = gameObject.transform;

                Collider[] targets = Physics.OverlapSphere(transform.position, radiation.radius, radiation.targetMask);
                for (int i = 0; i < targets.Length; i++)
                {
                    Transform target = targets[i].transform;
                    float dstToTarget = Vector3.Distance(transform.position, target.position);
                    Vector3 dirToTarget = (target.position - transform.position).normalized;
                    if (!Physics.Raycast(transform.position, dirToTarget, out RaycastHit hit, dstToTarget, radiation.obstacleMask))
                    {
                        EcsEntity entity = world.NewEntity();

                        Utility.Bind(hit.collider.gameObject, entity);

                        ref RadiationComponent newRadiation = ref entity.Get<RadiationComponent>(); //TODO
                        newRadiation.edgeDst = radiation.edgeDst;
                        newRadiation.edgeResolveIterations = radiation.edgeResolveIterations;
                        newRadiation.obstacleMask = radiation.obstacleMask;
                        newRadiation.radius = radiation.radius;
                        newRadiation.targetMask = radiation.targetMask;
                        newRadiation.viewMesh = new Mesh();
                        newRadiation.viewMeshFilter = radiation.viewMeshFilter;
                    }
                }

            }
        }
    }
}