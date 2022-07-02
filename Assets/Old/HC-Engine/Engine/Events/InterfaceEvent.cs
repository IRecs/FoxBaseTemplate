﻿using System.Collections.Generic;
using System;

namespace Engine.Events
{
    public class InterfaceEvent<T> : IEvent<T> where T : class
    {
        internal List<T> m_Subscribes = new List<T>();

        public virtual void Subscribe(T eventObject)
        {
            if (eventObject == null) throw new ArgumentNullException();

            m_Subscribes.Add(eventObject);
        }

        public virtual void Unsubscribe(T eventObject)
        {
            if (eventObject == null) throw new ArgumentNullException();

            m_Subscribes.Remove(eventObject);
        }

        public void UnsubscribeAll()
        {
            m_Subscribes.Clear();
        }

        public void CleanNulls()
        {
            m_Subscribes.RemoveAll(item => item == null || item.Equals(null));
        }

        public virtual void Invoke(Action<T> invoke)
        {
            for (int i = 0; i < m_Subscribes.Count; i++)
            {
                if (m_Subscribes[i] != null && !m_Subscribes[i].Equals(null))
                    invoke.Invoke(m_Subscribes[i]);
            }
        }
    }
}