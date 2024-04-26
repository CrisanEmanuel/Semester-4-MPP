using System;

namespace Client.Events;

public enum AgentieAngajatEvent
{
    UpdatedNrLocuriExcursie
}

public class AgentieAngajatEventArgs : EventArgs
{
    private readonly AgentieAngajatEvent _userEvent;
    private readonly object _data;

    public AgentieAngajatEventArgs(AgentieAngajatEvent userEvent, object data)
    {
        _userEvent = userEvent;
        _data = data;
    }

    public AgentieAngajatEvent AngajatEventType
    {
        get { return _userEvent; }
    }

    public object Data
    {
        get { return _data; }
    }
}