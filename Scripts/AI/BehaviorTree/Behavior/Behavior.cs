using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace FourXUtilities.AI
{
    public enum BH_STATUS
    {
        BH_INVALID,
        BH_SUCCESS,
        BH_FAILURE,
        BH_RUNNING,
        BH_ABORTED
    };

    abstract public class Behavior
    {
        BH_STATUS m_eStatus;

        public abstract void OnInitialize();
        public abstract BH_STATUS OnUpdate();
        public abstract void OnTerminate(BH_STATUS _status);

        public Behavior()
        {
            m_eStatus = BH_STATUS.BH_ABORTED;
        }

        public BH_STATUS Tick()
        {
            if (m_eStatus != BH_STATUS.BH_RUNNING)
            {
                OnInitialize();
            }

            m_eStatus = OnUpdate();

            if (m_eStatus != BH_STATUS.BH_RUNNING)
            {
                OnTerminate(m_eStatus);
            }

            return m_eStatus;
        }

        public void Reset()
        {
            m_eStatus = BH_STATUS.BH_INVALID;
        }

        public void Abort()
        {
            OnTerminate(BH_STATUS.BH_ABORTED);
            m_eStatus = BH_STATUS.BH_ABORTED;
        }

        public bool IsTerminated()
        {
            return m_eStatus == BH_STATUS.BH_SUCCESS || m_eStatus == BH_STATUS.BH_FAILURE;
        }

        public bool IsRunning()
        {
            return m_eStatus == BH_STATUS.BH_RUNNING;
        }

        public BH_STATUS GetStatus()
        {
            return m_eStatus;
        }
    }
}