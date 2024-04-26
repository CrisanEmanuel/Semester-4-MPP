using System;
using System.Collections.Generic;
using System.Data;
using Npgsql;

namespace AgentieTurismCS.Utils
{
    public class PostgresqlConnectionFactory: ConnectionFactory
    {
        public override IDbConnection CreateConnection(IDictionary<string, string> props)
        {
            var connectionString = props["ConnectionString"];
            Console.WriteLine(@"PostgreSQL ---Se deschide o conexiune la  ... {0}", connectionString);
            return new NpgsqlConnection(connectionString);
        }
    }
}