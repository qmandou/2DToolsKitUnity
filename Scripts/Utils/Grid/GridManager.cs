using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FourXUtilities
{
    [RequireComponent(typeof(Grid))]
    public class GridManager : MonoBehaviour
    {

        #region GridManager Variable

        #region Public

        #endregion

        #region Protected

        #endregion

        #region Private

        static GridManager m_gridManager = null;

        Grid m_Grid = null;

        List<GridLayer> m_gridLayers = null;

        #endregion

        #endregion

        #region Unity Methods 

        GridManager()
        {
            if (m_gridManager == null)
            {
                m_gridLayers = new List<GridLayer>();
                m_gridManager = this;
            }
        }

        // Only Use for init at scene start
        void Awake()
        {
            if (m_gridManager)
            {
                GetGridLayerOnScene();
            }
            else
            {
                m_gridLayers = new List<GridLayer>();
                m_Grid = GetComponent<Grid>();
                GetGridLayerOnScene();
                m_gridManager = this;
            }
        }

        #endregion

        #region GridManager Methods 

        #region Public

        static public GridManager Instance
        {
            get
            {
                return m_gridManager;
            }
        }

        public void AddLayer(string _layerName)
        {
            if (!ContainLayer(_layerName))
            {
                m_gridLayers.Add(new GridLayer(_layerName, transform, m_gridLayers.Count));
                RefreshSortingOrder();
            }
            else
            {
                Debug.LogWarning(_layerName + " has already used !");
            }
        }

        public void GetGridLayerOnScene()
        {
            m_gridLayers.Clear();

            if (m_Grid == null)
            {
                m_Grid = GetComponent<Grid>();
            }

            for (int i = 0; i < m_Grid.transform.childCount; i++)
            {
                m_gridLayers.Add(new GridLayer(m_Grid.transform.GetChild(i).gameObject));
            }

            RefreshSortingOrder();
        }

        public void RefreshSortingOrder()
        {
            int index = 0;
            foreach (GridLayer gl in m_gridLayers)
            {
                gl.sortingOrder = index;
                index++;
            }
        }

        #endregion

        #region Protected

        #endregion

        #region Private

        bool ContainLayer(string _layerName)
        {
            if (m_gridLayers.Count < 0) return false;

            foreach (GridLayer gl in m_gridLayers)
            {
                if (gl.gameObject.name == _layerName)
                {
                    return true;
                }
            }

            return false;
        }

        #endregion

        #endregion
    }
}