using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace FourXUtilities.AI
{
    public class BehaviorTree : MonoBehaviour
    {
        protected Behavior m_root;

        public Behavior GetRoot() { return m_root; }
        public Behavior SetRoot(Behavior _root) { return m_root = _root; }

        // Update is called once per frame
        void Update()
        {
            // Debug.Log("BehaviorTree Update");

            if (m_root != null) m_root.Tick();
        }
    }
}