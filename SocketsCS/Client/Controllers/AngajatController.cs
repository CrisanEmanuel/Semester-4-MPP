using System;
using Client.Events;
using Model;
using Services;

namespace Client.Controllers;

public class AngajatController: IObserver
{
    public event EventHandler<AgentieAngajatEventArgs> UpdateEvent; //controller calls it when it has received an update
    private readonly IService _server;
    private Angajat _currentAngajat;
    public AngajatController(IService server)
    {
        _server = server;
        _currentAngajat = null;
    }
    
    protected virtual void OnUserEvent(AgentieAngajatEventArgs e)
    {
        if (UpdateEvent == null) return;
        UpdateEvent(this, e);
        Console.WriteLine(@"Update Event called");
    }

    public void Login(string username, string password)
    {
        var angajat = new Angajat(username, password);
        _server.Login(angajat, this);
        Console.WriteLine(@"Login succeeded ....");
        _currentAngajat = angajat;
        Console.WriteLine(@"Current user {0}", angajat);
    }

    public void Logout()
    {
        _server.Logout(_currentAngajat);
        Console.WriteLine(@"Log out user {0}", _currentAngajat);
        _currentAngajat = null;
    }

    public void UpdateNumarLocuriExcursie(Excursie excursie)
    {
        var angajatArgs = new AgentieAngajatEventArgs(AgentieAngajatEvent.UpdatedNrLocuriExcursie, excursie);
        OnUserEvent(angajatArgs);
    }
}