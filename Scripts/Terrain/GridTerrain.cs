using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using FourXUtilities.Tools;

namespace FourXUtilities.Terrain
{
    public class GridTerrain : MonoBehaviour
    {
        MeshFilter m_meshFilter = null;
        MeshRenderer m_meshRenderer = null;
        Mesh m_mesh = null;
        Grid m_grid;

        int m_widthCount = 0;
        int m_depthCount = 0;

        [SerializeField] float TileWidthSize;
        [SerializeField] float TileDepthSize;

        [SerializeField] int MapWidthSize;
        [SerializeField] int MapDepthSize;

        [SerializeField] Material material = null;

        void Start()
        {
            m_grid = GetComponentInParent<Grid>();

            CreateMesh("GridTerrain");
            SetCaseHeight(new Vector2Int(0, 0), -1.0f);
        }

        public void CreateMesh(string _meshName)
        {
            if (m_mesh != null)
            {
                Destroy(m_mesh);
            }

            //Debug.Log(m_grid.GetCellCenterWorld(Vector3Int.zero));
            transform.position = m_grid.GetCellCenterWorld(Vector3Int.zero);
            transform.rotation = Quaternion.identity;

            m_meshFilter = GetComponent<MeshFilter>();
            if (m_meshFilter == null)
                m_meshFilter = gameObject.AddComponent<MeshFilter>();

            m_meshRenderer = GetComponent<MeshRenderer>();
            if (m_meshRenderer == null)
                m_meshRenderer = gameObject.AddComponent<MeshRenderer>();

            m_mesh = new Mesh();
            m_mesh.name = _meshName;

            m_widthCount = MapWidthSize + 1;
            m_depthCount = MapDepthSize + 1;
            int numTriangles = MapWidthSize * MapDepthSize * 6;
            int verticesCount = m_widthCount * m_depthCount;

            Vector3[] vertices = new Vector3[verticesCount];
            Vector2[] uvs = new Vector2[verticesCount];
            int[] triangles = new int[numTriangles];
            Vector4[] tangents = new Vector4[verticesCount];
            Vector4 tangent = new Vector4(1f, 0f, 0f, -1f);

            int index = 0;
            float uvFactorX = 1.0f / TileWidthSize;
            float uvFactorY = 1.0f / TileDepthSize;
            //float scaleX = TileWidthSize / MapWidthSize;
            //float scaleY = TileDepthSize / MapDepthSize;

            for (float y = 0.0f; y < m_depthCount; y++)
            {
                for (float x = 0.0f; x < m_widthCount; x++)
                {
                    vertices[index] = new Vector3(x - TileWidthSize / 2f, 0.0f, y - TileDepthSize / 2f);
                    tangents[index] = tangent;
                    uvs[index++] = new Vector2(x * uvFactorX, y * uvFactorY);
                }
            }

            index = 0;

            for (int y = 0; y < MapDepthSize; y++)
            {
                for (int x = 0; x < MapWidthSize; x++)
                {
                    triangles[index] = (y * m_widthCount) + x;
                    triangles[index + 1] = ((y + 1) * m_widthCount) + x;
                    triangles[index + 2] = (y * m_widthCount) + x + 1;

                    triangles[index + 3] = ((y + 1) * m_widthCount) + x;
                    triangles[index + 4] = ((y + 1) * m_widthCount) + x + 1;
                    triangles[index + 5] = (y * m_widthCount) + x + 1;
                    index += 6;
                }
            }

            m_mesh.vertices = vertices;
            m_mesh.triangles = triangles;
            m_mesh.uv = uvs;
            m_mesh.tangents = tangents;

            m_meshFilter.sharedMesh = m_mesh;
            m_meshRenderer.material = material;

            m_mesh.RecalculateBounds();
            m_mesh.RecalculateNormals();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_caseIndex"></param>
        /// <param name="_height"></param>
        public void SetCaseHeight(Vector2Int _caseIndex, float _height)
        {
            SetVerticeHeight(new Vector2Int(_caseIndex.x, _caseIndex.y), _height);
            SetVerticeHeight(new Vector2Int(_caseIndex.x + 1, _caseIndex.y), _height);
            SetVerticeHeight(new Vector2Int(_caseIndex.x + 1, _caseIndex.y + 1), _height);
            SetVerticeHeight(new Vector2Int(_caseIndex.x, _caseIndex.y + 1), _height);
            m_mesh.RecalculateNormals();
        }

        public void SetVerticeHeight(Vector2Int _verticeIndex, float _height)
        {
            int index = GridTools.ConvertTo2DID(_verticeIndex, m_widthCount);
            Vector3[] vertices = m_mesh.vertices;
            vertices[index] = new Vector3(vertices[index].x, _height, vertices[index].z);

            m_meshFilter.sharedMesh = m_mesh;

            m_mesh.vertices = vertices;
        }
    }
}
