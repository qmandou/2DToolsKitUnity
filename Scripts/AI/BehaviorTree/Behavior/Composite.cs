using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace FourXUtilities.AI
{
    public class Composite : Behavior
    {
        protected List<Behavior> m_behaviors;
        protected int m_CurrentIterator = 0;

        public Composite()
        {
            Debug.Log("Show Composite status at init : " + GetStatus());
            m_behaviors = new List<Behavior>();
        }

        // Behavior Methods

        public override void OnInitialize() { }
        public override BH_STATUS OnUpdate() { return BH_STATUS.BH_SUCCESS; }
        public override void OnTerminate(BH_STATUS _status) { }

        // Composite Methods

        public void AddChild(Behavior _child) { m_behaviors.Add(_child); }

        public void RemoveChild(Behavior _child) { m_behaviors.Remove(_child); }
        public void RemoveAllChild(System.Predicate<Behavior> _match) { m_behaviors.RemoveAll(_match); }

        public Behavior GetChild(int _index) { return _index < m_behaviors.Count ? m_behaviors[_index] : null; }
        public int GetChildCount() { return m_behaviors.Count; }
    }
}