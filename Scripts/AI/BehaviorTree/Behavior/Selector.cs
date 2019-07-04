using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace FourXUtilities.AI
{
    public class Selector : Composite
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

            return BH_STATUS.BH_FAILURE;
        }
    }
}