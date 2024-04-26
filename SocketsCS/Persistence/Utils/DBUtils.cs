using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using Npgsql;

namespace Persistence.Utils;

public static class DbUtils
{
    private static IDbConnection _instance;


    public static IDbConnection GetConnection(IDictionary<String,string> props)
    {
        if (_instance == null || _instance.State == ConnectionState.Closed)
        {
            _instance = GetNewConnection(props);
            _instance.Open();
        }
        return _instance;
    }

    private static IDbConnection GetNewConnection(IDictionary<string,string> props)
    {
        return ConnectionFactory.GetInstance().CreateConnection(props);
    }
}

public abstract class ConnectionFactory
{
    protected ConnectionFactory() {}

    private static ConnectionFactory _instance;

    public static ConnectionFactory GetInstance()
    {
        if (_instance != null) return _instance;
        var assem = Assembly.GetExecutingAssembly();
        var types = assem.GetTypes();
        foreach (var type in types)
        {
            if (type.IsSubclassOf(typeof(ConnectionFactory)))
                _instance = (ConnectionFactory)Activator.CreateInstance(type);
        }
        return _instance;
    }

    public abstract  IDbConnection CreateConnection(IDictionary<string,string> props);
}

public class PostgresqlConnectionFactory: ConnectionFactory
{
    public override IDbConnection CreateConnection(IDictionary<string, string> props)
    {
        var connectionString = props["ConnectionString"];
        Console.WriteLine(@"PostgreSQL ---Se deschide o conexiune la  ... {0}", connectionString);
        return new NpgsqlConnection(connectionString);
    }
}