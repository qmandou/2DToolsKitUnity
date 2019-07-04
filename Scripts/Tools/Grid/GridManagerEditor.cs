using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEditor;

#if (UNITY_EDITOR)
namespace FourXUtilities.Tools
{

    [CanEditMultipleObjects]
    [CustomEditor(typeof(GridManager))]
    public class GridManagerEditor : Editor
    {
        string key = "";
        int oldChildCount = -1;

        public override void OnInspectorGUI()
        {

            GridManager imTarget = (GridManager)target;


            // todo : need to find a methods for optimise that call when the childCount is modify
            // because when the user destroy a child manually the list isn't automatically refresh 
            // and need to clic on the manager in the inspector for trigger this methods
            if (imTarget.ChildCount < 0 || imTarget.ChildCount != oldChildCount)
            {
                imTarget.InitGridLayerList();
                oldChildCount = imTarget.ChildCount;
            }

            GUILayout.BeginVertical();
            {
                GUILayout.BeginHorizontal();
                {
                    GUILayout.Label("Layer name : ");
                    key = GUILayout.TextField(key, GUILayout.MaxWidth(200));

                }
                GUILayout.EndHorizontal();

                if (GUILayout.Button("Add Layer") && key.Length > 0)
                {
                    imTarget.AddLayer(key);
                }
            }
            GUILayout.EndVertical();

        }
    }
}
#endif