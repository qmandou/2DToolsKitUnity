using System.Collections;
using System.Collections.Generic;
using System.IO;

using UnityEngine;

namespace FourXUtilities
{
    public interface ISaveable
    {
        void LoadData();
        void SaveData();
    }

    [System.Serializable]
    public class SerializedMap<K, D>
    {
        public SerializedMap()
        {
            m_keysData = new List<K>();
            m_keyCodeData = new List<D>();
        }

        [SerializeField] public List<K> m_keysData = null;
        [SerializeField] public List<D> m_keyCodeData = null;

        public static void LoadJsonData(string _path, ref Dictionary<K, D> _refDico)
        {
            SerializedMap<K, D> fromLoad = JsonUtility.FromJson<SerializedMap<K, D>>(File.ReadAllText(_path));

            for (int i = 0; i < fromLoad.m_keysData.Count; i++)
            {
                _refDico[fromLoad.m_keysData[i]] = fromLoad.m_keyCodeData[i];
            }
        }

        public static void SaveJsonData(string _path, ref Dictionary<K, D> _refDico)
        {
            SerializedMap<K, D> data = new SerializedMap<K, D>();

            foreach (K key in _refDico.Keys)
            {
                data.m_keysData.Add(key);
                data.m_keyCodeData.Add(_refDico[key]);
            }

            if (File.Exists(_path))
            {
                string dataAsJson = JsonUtility.ToJson(data);
                File.WriteAllText(_path, dataAsJson);
            }
            else
            {
                File.Create(_path);
                string dataAsJson = JsonUtility.ToJson(data);
                File.WriteAllText(_path, dataAsJson);
            }
        }
    }
}