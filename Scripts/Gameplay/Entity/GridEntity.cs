using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using FourXUtilities.Tools;

using RPG_Utilities.Behaviours;

namespace FourXUtilities.Entity
{
    public class GridEntity : RPGBehaviour
    {

        #region GridEntity Variable

        #region Public

        #endregion

        #region Protected

        #endregion

        #region Private

        GridManager m_gridManager;

        #endregion

        #endregion

        #region Unity Methods 

        private void Start()
        {
            m_gridManager = GridManager.Instance;
        }

        #endregion

        #region GridEntity Methods 

        #region Public

        #endregion

        #region Protected

        #endregion

        #region Private

        #endregion

        #endregion
    }
}