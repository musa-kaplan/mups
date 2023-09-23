using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MusaUtils
{
    public enum EventState
    {
        OnEnable, 
        Awake,
        Start,
        Update,
        OnDisable
    }
    public class UnityEventHandler : MonoBehaviour
    {
        public List<CurrentEvents> _events;

        private void OnEnable() { InvokeEvent(EventState.OnEnable);}

        private void Awake() { InvokeEvent(EventState.Awake); }

        private void Start() { InvokeEvent(EventState.Start); }
        
        private void Update() { InvokeEvent(EventState.Update); }

        private void OnDisable() { InvokeEvent(EventState.OnDisable); }

        private void InvokeEvent(EventState a)
        {
            foreach (var e in _events)
            {
                if (e._state.Equals(a))
                {
                    e._event.Invoke();
                }
            }
        }
    }

    [Serializable]
    public class CurrentEvents
    {
        public UnityEvent _event;
        public EventState _state;
    }
}