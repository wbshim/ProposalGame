using System.Collections.Generic;
using UnityEngine;

namespace LowPolyCloudGenerator
{
    public class CloudGenerator : MonoBehaviour
    {
        List<Vector3> vertices = new List<Vector3>();

        public bool fill;

        public bool addPos;

        public bool MeshInChildren;

        public float space;

        Vector3[] startPos;

        public int vertexCount;


        public void Initialize()
        {
            Primitive[] primitives = GetComponentsInChildren<Primitive>();

            startPos = new Vector3[primitives.Length];

            for (int i = 0; i < primitives.Length; i++)
            {
                startPos[i] = primitives[i].transform.position - transform.position;
            }
        }


        public void Generate(int seed)
        {
            MeshFilter filter = GetComponent<MeshFilter>();

            Random.InitState (seed);

            Primitive[] primitives = GetComponentsInChildren<Primitive>();

            if (addPos)
            {
                for (int i = 0; i < primitives.Length; i++)
                {
                    primitives[i].transform.position = startPos[i];
                }
            }


            for (int i = 0; i < primitives.Length; i++)
            {
                List<Vector3> verts = new List<Vector3>();

                for (int y = 0; y < vertexCount; y++)
                {
                    Vector3 test = Random.insideUnitSphere * primitives[i].multiplier;

                    if (!fill)
                        verts.Add(test);

                    vertices.Add(test + primitives[i].transform.position);
                }

                if (!fill)
                {
                    Mesh m = MeshMaker.GenerateRunTimeRock(verts);

                    primitives[i].gameObject.GetComponent<MeshFilter>().sharedMesh = m;

                    if (addPos)
                    {
                        primitives[i].transform.position += new Vector3(Random.Range(-space, space),
                                                                     Random.Range(-space, space),
                                                                     Random.Range(-space, space));
                    }
                }
                else
                {
                    primitives[i].gameObject.GetComponent<MeshFilter>().sharedMesh = null;
                }
            }

            if (fill)
            {
                Fill(vertices);
            }
            else
            {
                filter.sharedMesh = null;
            }

            if (!MeshInChildren && !fill)
            {
                filter.sharedMesh = GetChildren();

                for (int i = 0; i < primitives.Length; i++)
                {
                    primitives[i].GetComponent<MeshFilter>().sharedMesh = null;
                }
            }
            else if (!fill)
            {
                filter.sharedMesh = null;
            }

        }

        void Fill(IEnumerable<Vector3> points)
        {
            GetComponent<MeshFilter>().sharedMesh = MeshMaker.GenerateRunTimeRock(points);

            vertices = new List<Vector3>();
        }

        private Mesh GetChildren()
        {                
            List<CombineInstance> meshes = new List<CombineInstance>();

            MeshFilter[] meshFilters = gameObject.GetComponentsInChildren<MeshFilter>(false);

            for (int j = 1; j < meshFilters.Length; j++)
            {
                MeshFilter meshFilter = meshFilters[j];

                Vector3 pos = transform.position;

                transform.position = Vector3.zero;

                CombineInstance combine = new CombineInstance();

                combine.mesh = meshFilter.sharedMesh;
                combine.transform = meshFilter.transform.localToWorldMatrix;

                meshes.Add(combine);

                transform.position = pos;
            }

            Mesh combinedMesh = new Mesh();
            combinedMesh.name = "Cloud";
            combinedMesh.CombineMeshes(meshes.ToArray());

            return combinedMesh;
        }

        public Mesh RuntimeCloud ()
        {
            Vector3 pos = transform.position;
            transform.position = Vector3.zero;

            Generate(Random.Range(0, 10000));

            transform.position = pos;

            if (fill || !fill && !MeshInChildren)
            {
                return GetComponent<MeshFilter>().sharedMesh;
            }
            else
            {
                return GetChildren();
            }
        }
    }
}

