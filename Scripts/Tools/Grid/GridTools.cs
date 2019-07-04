using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
///  That class is used for pre-calculate the grid position
///  and optimise AI algo, is not neccessary used on the dynamic game comnpoent but 
///  sooner on static tile and object for avoid the great quantity of cast
/// </summary>

namespace FourXUtilities.Tools
{
    public class GridData
    {
        public class TwoDimGridData
        {
            Vector2 m_wordlPosition;
            Vector2Int m_cellPosition;
            int m_ID;
            Tilemap m_tilemap;

            public TwoDimGridData(Vector2Int _cellPosition, Tilemap _tilemap)
            {
                m_cellPosition = _cellPosition;
                m_wordlPosition = _tilemap.CellToWorld((Vector3Int)_cellPosition);
                m_ID = GridTools.ConvertTo2DID(_cellPosition, _tilemap.size.x);
                m_tilemap = _tilemap;
            }

            public TwoDimGridData(Vector2 _worldPosition, Tilemap _tilemap)
            {
                m_wordlPosition = _worldPosition;
                m_cellPosition = (Vector2Int)_tilemap.WorldToCell(_worldPosition);
                m_ID = GridTools.ConvertTo2DID(m_cellPosition, _tilemap.size.x);
                m_tilemap = _tilemap;
            }

            public Vector2 WorldPosition
            {
                get
                {
                    return m_wordlPosition;
                }

                set
                {
                    m_wordlPosition = value;
                    m_cellPosition = (Vector2Int)m_tilemap.WorldToCell(value);
                    m_ID = GridTools.ConvertTo2DID(m_cellPosition, m_tilemap.size.x);
                }
            }

            public Vector2Int CellPosition
            {
                get
                {
                    return m_cellPosition;
                }

                set
                {
                    m_cellPosition = value;
                    m_wordlPosition = m_tilemap.CellToWorld((Vector3Int)value);
                    m_ID = GridTools.ConvertTo2DID(m_cellPosition, m_tilemap.size.x);
                }
            }

            public int GetID()
            {
                return m_ID;
            }
        }

        public class ThreeDimGridData
        {
            Vector3 m_wordlPosition;
            Vector3Int m_cellPosition;
            int m_ID;
            Tilemap m_tilemap;

            public ThreeDimGridData(Vector3Int _cellPosition, Tilemap _tilemap)
            {
                m_cellPosition = _cellPosition;
                m_wordlPosition = _tilemap.CellToWorld(_cellPosition);
                m_ID = GridTools.ConvertTo3DID(_cellPosition, _tilemap.size.x, _tilemap.size.y);
                m_tilemap = _tilemap;
            }

            public ThreeDimGridData(Vector3 _worldPosition, Tilemap _tilemap)
            {
                m_wordlPosition = _worldPosition;
                m_cellPosition = _tilemap.WorldToCell(_worldPosition);
                m_ID = GridTools.ConvertTo3DID(m_cellPosition, _tilemap.size.x, _tilemap.size.y);
                m_tilemap = _tilemap;
            }

            public Vector3 WordlPosition
            {
                get
                {
                    return m_wordlPosition;
                }

                set
                {
                    m_wordlPosition = value;
                    m_cellPosition = m_tilemap.WorldToCell(value);
                    m_ID = GridTools.ConvertTo3DID(m_cellPosition, m_tilemap.size.x, m_tilemap.size.y);
                }
            }

            public Vector3Int CellPosition
            {
                get
                {
                    return m_cellPosition;
                }

                set
                {
                    m_cellPosition = value;
                    m_wordlPosition = m_tilemap.CellToWorld(value);
                    m_ID = GridTools.ConvertTo3DID(m_cellPosition, m_tilemap.size.x, m_tilemap.size.y);
                }
            }

            public int GetID()
            {
                return m_ID;
            }
        }

        TwoDimGridData m_2dData = null;
        ThreeDimGridData m_3dData = null;

        public GridData(TwoDimGridData _data)
        {
            m_2dData = _data;
        }

        public GridData(ThreeDimGridData _data)
        {
            m_3dData = _data;
        }

        public bool Is2D()
        {
            return m_2dData != null;
        }
    }

    public class GridDataMap
    {
        Dictionary<int, GridData> m_dico;

        public GridDataMap()
        {
            m_dico = new Dictionary<int, GridData>();
        }

        public void AddDico(int _id, GridData _data)
        {
            m_dico[_id] = _data;
        }

        public void Remove(int _id)
        {
            m_dico.Remove(_id);
        }
    }

    public class GridLayer
    {
        Tilemap m_tilemap = null;
        TilemapRenderer m_tilemapRenderer = null;

        public GridLayer(GameObject _gameobject)
        {
            m_tilemap = _gameobject.GetComponent<Tilemap>();
            m_tilemapRenderer = _gameobject.GetComponent<TilemapRenderer>();
        }

        /// <summary>
        /// Instanciate new layer on grid
        /// </summary>
        public GridLayer(string _name, Transform _gridParent, int _sortingOrder)
        {
            GameObject gameObject = new GameObject(_name);
            gameObject.transform.SetParent(_gridParent);
            m_tilemap = gameObject.AddComponent<Tilemap>();
            m_tilemapRenderer = gameObject.AddComponent<TilemapRenderer>();
            m_tilemapRenderer.sortingOrder = _sortingOrder;
        }

        public Tilemap GetTilemap()
        {
            return m_tilemap;
        }

        public int sortingOrder
        {
            get
            {
                return m_tilemapRenderer.sortingOrder;
            }

            set
            {
                m_tilemapRenderer.sortingOrder = value;
            }
        }

        public GameObject gameObject
        {
            get
            {
                return m_tilemap.gameObject;
            }
        }

        public bool IsNull()
        {
            return m_tilemap == null;
        }
    }
    
    public class GridTools
    {
        static public int ConvertTo2DID(Vector2Int _vector, int _width)
        {
            return _vector.x * _width + _vector.y;
        }

        static public int ConvertTo3DID(Vector3Int _vector, int _width, int _height)
        {
            return _vector.x * _width + _vector.y * _height + _vector.z;
        }
    }

}