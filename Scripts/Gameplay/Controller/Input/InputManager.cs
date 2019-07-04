using System.Collections;
using System.Collections.Generic;
using System.IO;

using UnityEngine;

namespace FourXUtilities.Controller
{
    public class InputManager : MonoBehaviour, ISaveable
    {
        #region InputManager Variable

        #region Public

        #endregion

        #region Protected

        #endregion

        #region Private

        static InputManager m_InputManager = null;

        Dictionary<string, KeyCode> m_inputMap;

        #endregion

        #endregion

        #region InputManager Methods 

        InputManager()
        {
            m_inputMap = new Dictionary<string, KeyCode>();
        }

        private void Awake()
        {
            if (m_InputManager == null)
            {
                m_inputMap = new Dictionary<string, KeyCode>();
                LoadData();
                m_InputManager = this;
            }
        }

        #region Public

        public static bool KeyDown(string _key)
        {
            return Instance._KeyDown(_key);
        }

        public static bool KeyUp(string _key)
        {
            return Instance._KeyUp(_key);
        }

        public static bool Key(string _key)
        {
            return Instance._Key(_key);
        }

        static InputManager Instance
        {
            get
            {
                if (m_InputManager == null)
                {
                    m_InputManager = new InputManager();
                    m_InputManager.LoadData();
                }

                return m_InputManager;
            }
        }

        public void SetInput(string _name, KeyCode _key)
        {
            m_inputMap[_name] = _key;
        }

        public KeyCode GetInput(string _key)
        {
            return m_inputMap[_key];
        }

        public void Remove(string _key)
        {
            m_inputMap.Remove(_key);
        }

        public void Clear()
        {
            m_inputMap.Clear();
        }

        public List<string> Keys
        {
            get
            {
                if (m_inputMap.Count <= 0) return null;

                List<string> toReturn = new List<string>();
                foreach (string key in m_inputMap.Keys)
                {
                    toReturn.Add(key);
                }

                return toReturn;
            }
        }

        // ISaveable

        public void LoadData()
        {
            string filePath = Path.Combine(Application.dataPath, "FourX_Utilities/Config/DefaultInput.json");

            if (File.Exists(filePath))
            {
                SerializedMap<string, KeyCode>.LoadJsonData(filePath, ref m_inputMap);
            }
            else
            {
                Debug.Log("File not found at : " + filePath);
            }
        }

        public void SaveData()
        {
            string filePath = Path.Combine(Application.dataPath, "FourX_Utilities/Config/DefaultInput.json");
            SerializedMap<string, KeyCode>.SaveJsonData(filePath, ref m_inputMap);
        }

        #endregion

        #region Protected

        #endregion

        #region Private

        bool _KeyDown(string _key)
        {
            if (m_inputMap.ContainsKey(_key))
            {
                return Input.GetKeyDown(m_inputMap[_key]);
            }

            return false;
        }

        bool _KeyUp(string _key)
        {
            if (m_inputMap.ContainsKey(_key))
            {
                return Input.GetKeyUp(m_inputMap[_key]);
            }

            return false;
        }

        bool _Key(string _key)
        {
            if (m_inputMap.ContainsKey(_key))
            {
                return Input.GetKey(m_inputMap[_key]);
            }

            return false;
        }

        #endregion

        #endregion
    }
}
