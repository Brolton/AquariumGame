using System;

namespace Nekki.Events
{
    public interface IEventDispatcher<T>
    {
        int AddEventListener(int name, Action<T> dlg);
        int RemoveEventListener(int name, Action<T> dlg);
        int RemoveAllEventListener();

        int RemoveEvent(int name);
        int CallEvent(int name, T arg);
    }
}