//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class RadiationVisualFieldOfRadiation : MonoBehaviour
//{
//    public float radius;
//    public LayerMask targetMask;
//    public LayerMask obstacleMask;
//    public MeshFilter viewMeshFilter;
//    private Mesh viewMesh;
//    public int edgeRosolveIterations;
//    public float edgeDst;
//    private RadiationResistComponent rrc;
//    private float resist;

//    void Start()
//    {
//        viewMesh = new Mesh();
//        viewMesh.name = "View Mesh";
//        viewMeshFilter.mesh = viewMesh;
//        StartCoroutine("FindTargetsWithDelay", .2f);
//    }

//    void LateUpdate()
//    {
//        DrawFieldOfRadiation();
//    }
//    IEnumerator FindTargetsWithDelay(float delay)
//    {
//        while (true)
//        {
//            yield return new WaitForSeconds(delay);
//            FindVisibleTargets();
//        }
//    }
//void FindVisibleTargets()
//{
//    Collider[] targets = Physics.OverlapSphere(transform.position, radius, targetMask);
//    for (int i = 0; i < targets.Length; i++)
//    {
//        Transform target = targets[i].transform;
//        float dstToTarget = Vector3.Distance(transform.position, target.position);
//        Vector3 dirToTarget = (target.position - transform.position).normalized;
//        if (!Physics.Raycast(transform.position, dirToTarget, out RaycastHit hit, dstToTarget, obstacleMask))
//        {

//            Debug.Log("There's a target");
//        }
//    }
//}

//    void DrawFieldOfRadiation()
//    {
//        float stepAngleSize = 3;
//        List<Vector3> viewPoints = new List<Vector3>();
//        ViewCastInfo oldViewCast = new ViewCastInfo();
//        for (int i = 0; i <= 120; i++)
//        {
//            float angle = transform.eulerAngles.y - 180 + stepAngleSize * i;
//            //Debug.DrawLine(transform.position,transform.position+DirFromAngle(angle)*radius,Color.green);
//            ViewCastInfo newViewCast = ViewCast(angle);
//            if (i > 0)
//            {
//                bool edgeDstExceeded = Mathf.Abs(oldViewCast.dst - newViewCast.dst) > edgeDst;
//                if (oldViewCast.hit != newViewCast.hit || (oldViewCast.hit && newViewCast.hit && edgeDstExceeded))
//                {
//                    EdgeInfo edge = FindEdge(oldViewCast, newViewCast);
//                    if (edge.pointA != Vector3.zero)
//                    {
//                        viewPoints.Add(edge.pointA);
//                    }
//                    if (edge.pointB != Vector3.zero)
//                    {
//                        viewPoints.Add(edge.pointB);
//                    }
//                }
//            }
//            viewPoints.Add(newViewCast.point);
//            oldViewCast = newViewCast;
//        }

//        int vertexCount = viewPoints.Count + 1;
//        Vector3[] verticles = new Vector3[vertexCount];
//        int[] triangles = new int[(vertexCount - 2) * 3];
//        verticles[0] = Vector3.zero;
//        for (int i = 0; i < vertexCount - 1; i++)
//        {
//            verticles[i + 1] = transform.InverseTransformPoint(viewPoints[i]);
//            if (i < vertexCount - 2)
//            {
//                triangles[i * 3] = 0;
//                triangles[i * 3 + 1] = i + 1;
//                triangles[i * 3 + 2] = i + 2;
//            }
//        }
//        viewMesh.Clear();
//        viewMesh.vertices = verticles;
//        viewMesh.triangles = triangles;
//        viewMesh.RecalculateNormals();
//    }

//    ViewCastInfo ViewCast(float globalAngle)
//    {
//        Vector3 dir = DirFromAngle(globalAngle);
//        RaycastHit hit;
//        if (Physics.Raycast(transform.position, dir, out hit, radius, obstacleMask))
//        {
//            rrc = hit.collider.GetComponent<RadiationResistComponent>();
//            resist = rrc.ResistancePersent;
//            return new ViewCastInfo(true, transform.position + dir * ((1 - resist) * (radius - hit.distance) + hit.distance), (1 - resist) * (radius - hit.distance) + hit.distance, globalAngle);
//        }
//        else
//        {
//            return new ViewCastInfo(false, transform.position + dir * radius, radius, globalAngle);
//        }
//    }

//    EdgeInfo FindEdge(ViewCastInfo minViewCast, ViewCastInfo maxViewCast)
//    {
//        float minAngle = minViewCast.angle;
//        float maxAngle = maxViewCast.angle;
//        Vector3 minPoint = Vector3.zero;
//        Vector3 maxPoint = Vector3.zero;
//        for (int i = 0; i < edgeRosolveIterations; i++)
//        {
//            float angle = (minAngle + maxAngle) / 2;
//            ViewCastInfo newViewCast = ViewCast(angle);
//            bool edgeDstExceeded = Mathf.Abs(minViewCast.dst - newViewCast.dst) > edgeDst;
//            if (newViewCast.hit == minViewCast.hit && !edgeDstExceeded)
//            {
//                minAngle = angle;
//                minPoint = newViewCast.point;
//            }
//            if (newViewCast.hit == maxViewCast.hit)
//            {
//                maxAngle = angle;
//                maxPoint = newViewCast.point;
//            }
//        }

//        return new EdgeInfo(minPoint, maxPoint);
//    }
//    public Vector3 DirFromAngle(float degrees)
//    {
//        return new Vector3(Mathf.Sin(degrees * Mathf.Deg2Rad), 0, Mathf.Cos(degrees * Mathf.Deg2Rad));
//    }

//    public struct ViewCastInfo
//    {
//        public bool hit;
//        public Vector3 point;
//        public float dst;
//        public float angle;

//        public ViewCastInfo(bool _hit, Vector3 _point, float _dst, float _angle)
//        {
//            hit = _hit;
//            point = _point;
//            dst = _dst;
//            angle = _angle;
//        }
//    }

//    public struct EdgeInfo
//    {
//        public Vector3 pointA;
//        public Vector3 pointB;

//        public EdgeInfo(Vector3 _pointA, Vector3 _pointB)
//        {
//            pointA = _pointA;
//            pointB = _pointB;
//        }
//    }
//}
