using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace FourXUtilities.AI
{
    public class Sequence : Composite
    {
        public override BH_STATUS OnUpdate()
        {
            for (int i = m_CurrentIterator; i < m_behaviors.Count; i++)
            {
                Behavior behavior = m_behaviors[i];
                m_CurrentIterator = i;
                BH_STATUS status = behavior.Tick();

                if (status != BH_STATUS.BH_SUCCESS)
                {
                    return status;
                }
            }

            return BH_STATUS.BH_SUCCESS;
        }

        public override void OnTerminate(BH_STATUS _status)
        {
            if (m_CurrentIterator < m_behaviors.Count)
            {
                m_behaviors[m_CurrentIterator].OnTerminate(_status);
            }
        }

    }
}