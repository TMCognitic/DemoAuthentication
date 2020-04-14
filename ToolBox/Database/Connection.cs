using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ToolBox.Database
{
    public class Connection : IConnection
    {
        private readonly string _connectionString;

        public Connection(string connectionString)
        {
            _connectionString = connectionString;

            //using (SqlConnection connection = CreateConnection())
            //{
            //    connection.Open();
            //}
        }

        public IEnumerable<TResult> ExecuteReader<TResult>(Command command, Func<IDataRecord, TResult> selector)
        {
            if (selector is null)
                throw new ArgumentNullException();

            using (SqlConnection connection = CreateConnection())
            {
                using (SqlCommand sqlCommand = CreateCommand(command, connection))
                {
                    connection.Open();

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            yield return selector(reader);
                        }
                    }
                }
            }
        }

        public int ExecuteNonQuery(Command command)
        {
            using (SqlConnection connection = CreateConnection())
            {
                using (SqlCommand sqlCommand = CreateCommand(command, connection))
                {
                    connection.Open();
                    return sqlCommand.ExecuteNonQuery();
                }
            }
        }

        public object ExecuteScalar(Command command)
        {
            using (SqlConnection connection = CreateConnection())
            {
                using (SqlCommand sqlCommand = CreateCommand(command, connection))
                {
                    connection.Open();
                    object o = sqlCommand.ExecuteScalar();
                    return (o is DBNull) ? null : o;
                }
            }
        }

        private SqlCommand CreateCommand(Command command, SqlConnection connection)
        {
            SqlCommand sqlCommand = connection.CreateCommand();
            sqlCommand.CommandText = command.Query;

            if (command.IsStoredProcedure)
                sqlCommand.CommandType = CommandType.StoredProcedure;

            foreach (KeyValuePair<string, object> kvp in command.Parameters)
            {
                SqlParameter parameter = sqlCommand.CreateParameter();
                parameter.ParameterName = kvp.Key;
                parameter.Value = kvp.Value;

                sqlCommand.Parameters.Add(parameter);
            }

            return sqlCommand;
        }

        private SqlConnection CreateConnection()
        {
            return new SqlConnection()
            {
                ConnectionString = _connectionString
            };
        }
    }
}
