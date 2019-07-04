using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FourXUtilities
{
    public class GridCharacter : GridEntity
    {

        #region GridCharacter Variable

        #region Public

        #endregion

        #region Protected

        #endregion

        #region Private

        GridManager m_gridManager = null;

        #endregion

        #endregion

        #region Unity Methods 

        // Start is called before the first frame update
        void Start()
        {
            if (m_gridManager == null)
            {
                m_gridManager = GridManager.Instance;
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        #endregion

        #region GridCharacter Methods 

        #region Public

        #endregion

        #region Protected

        #endregion

        #region Private

        #endregion

        #endregion
    }
}