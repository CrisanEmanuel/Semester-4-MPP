using System;
using System.Collections.Generic;
using System.Data;

namespace AgentieTurismCS.Utils
{
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
}