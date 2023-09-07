using System;

public class EventManager : GenericSingleton<EventManager>
{
    internal Action<int> onPlayerMove;
    internal Action onLevelComplete;
}
