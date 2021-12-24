using UnityEngine;

namespace Client
{
    struct RadiationComponent
    {
        public float radius;
        public LayerMask targetMask;
        public LayerMask obstacleMask;
        public MeshFilter viewMeshFilter;
        public Mesh viewMesh;
        public int edgeResolveIterations;
        public float edgeDst;
    }
}