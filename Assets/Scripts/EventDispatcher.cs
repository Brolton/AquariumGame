using System;
using System.Collections.Generic;

namespace Nekki.Events
{
    public class EventDispatcher<T> : IEventDispatcher<T>
    {
        Dictionary<int, Action<T>> _events;

        public EventDispatcher()
        {
            _events = new Dictionary<int, Action<T>>();
        }

        public int AddEventListener(int name, Action<T> dlg)
        {
            if (dlg == null)
            {
                return -1;
            }

            if (_events.ContainsKey(name)) {
                _events[name] += dlg;
                return 0;
            }

            _events.Add(name, dlg);

            return 1;
        }

        public int RemoveEventListener(int name, Action<T> dlg)
        {
            if (dlg == null)
            {
                return -1;
            }

            if (_events.ContainsKey(name)) {
                _events[name] -= dlg;

				if (_events [name] == null) {
					RemoveEvent (name);
				}

                return 0;
            }

            return 1;
        }

        public int RemoveAllEventListener()
        {
            _events.Clear();
            return 0;
        }

        public int RemoveEvent(int name)
        {
            _events.Remove(name);

            return 1;
        }

        public int CallEvent(int name, T arg)
        {
            if (_events.ContainsKey(name)) {
                _events[name](arg);
                return 0;
            }

            return 1;
        }
    }     
}