using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEditor;



#if (UNITY_EDITOR)

namespace FourXUtilities.Controller
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(InputManager))]
    public class InputManagerEditor : Editor
    {
        string key = "";
        KeyCode keycode = KeyCode.A;

        public override void OnInspectorGUI()
        {
            InputManager imTarget = (InputManager)target;
            imTarget.LoadData();

            GUILayout.BeginVertical();
            {
                List<string> keys = imTarget.Keys;
                if (keys != null)
                {
                    string erasekey = null;
                    foreach (string key in keys)
                    {
                        GUILayout.BeginHorizontal();
                        {
                            GUILayout.Label(key, GUILayout.MaxWidth(250));
                            KeyCode kc = imTarget.GetInput(key);
                            kc = (KeyCode)EditorGUILayout.EnumPopup("", kc, GUILayout.MaxWidth(100));

                            if (kc != imTarget.GetInput(key))
                            {
                                imTarget.SetInput(key, kc);
                                imTarget.SaveData();
                            }

                            if (GUILayout.Button("X"))
                            {
                                erasekey = key;
                            }
                        }
                        GUILayout.EndHorizontal();
                    }

                    if (erasekey != null)
                    {
                        imTarget.Remove(erasekey);
                        imTarget.SaveData();
                    }
                }
            }
            GUILayout.EndVertical();

            GUILayout.BeginHorizontal();
            {
                key = GUILayout.TextField(key, GUILayout.MaxWidth(200));
                keycode = (KeyCode)EditorGUILayout.EnumPopup("", keycode, GUILayout.MaxWidth(100));
                if (GUILayout.Button("Add new Input", GUILayout.MaxWidth(100)))
                {
                    imTarget.SetInput(key, keycode);
                    imTarget.SaveData();
                }
            }
            GUILayout.EndHorizontal();

            serializedObject.ApplyModifiedProperties();
        }
    }
}

#endif
