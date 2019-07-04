using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

#if (UNITY_EDITOR)
namespace FourXUtilities
{

    [CanEditMultipleObjects]
    [CustomEditor(typeof(GridManager))]
    public class GridManagerEditor : Editor
    {
        string key = "";
        int oldChildCount = 0;

        public override void OnInspectorGUI()
        {

            GridManager imTarget = (GridManager)target;
            imTarget.GetGridLayerOnScene();

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