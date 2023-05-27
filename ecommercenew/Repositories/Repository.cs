using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Ecommercenew.Models;
using Ecommercenew.UI;
using MySql.Data.MySqlClient;
using static System.Net.Mime.MediaTypeNames;

namespace Ecommercenew.Repositories
{
    public class Repository<T> : IRepository<T>
    {
        private readonly string _connectionString;

        public Repository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public T GetById(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var query = $"SELECT * FROM tb_{typeof(T).Name.ToLower()} WHERE PedidoId = @Id";
                var command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        var entity = Activator.CreateInstance<T>();
                        var properties = typeof(T).GetProperties();

                        foreach (var property in properties)
                        {
                            if (property.Name == "Id")
                            {
                                property.SetValue(entity, id);
                                continue;
                            }

                            var value = reader[property.Name];
                            if (value != DBNull.Value)
                            {
                                property.SetValue(entity, value);
                            }
                        }

                        return entity;
                    }
                }
            }

            return default;
        }

        public void Insert(T entity)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var queryBuilder = new StringBuilder($"INSERT INTO {typeof(T).Name} (");
                var valuesBuilder = new StringBuilder("VALUES (");
                var parameters = new List<MySqlParameter>();

                var properties = typeof(T).GetProperties();

                foreach (var property in properties)
                {
                    if (property.Name == "Id" || property.GetValue(entity) == null)
                        continue;

                    string columnName = property.Name.ToLower();
                    object value = property.GetValue(entity);

                    queryBuilder.Append($"{columnName}, ");
                    valuesBuilder.Append($"@{columnName}, ");

                    var parameter = new MySqlParameter($"@{columnName}", value);
                    parameters.Add(parameter);
                }

                queryBuilder.Remove(queryBuilder.Length - 2, 2);
                valuesBuilder.Remove(valuesBuilder.Length - 2, 2);

                queryBuilder.Append(") ");
                valuesBuilder.Append(")");

                var query = queryBuilder.ToString() + valuesBuilder.ToString();

                var command = new MySqlCommand(query, connection);
                command.Parameters.AddRange(parameters.ToArray());
                command.ExecuteNonQuery();
            }
        }

        public void Update(T entity)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var queryBuilder = new StringBuilder($"UPDATE tb_{typeof(T).Name} SET ");
                var parameters = new List<MySqlParameter>();

                var properties = typeof(T).GetProperties();

                foreach (var property in properties)
                {
                    if (property.Name == "Id" || property.GetValue(entity) == null)
                        continue;

                    string columnName = property.Name.ToLower();
                    object value = property.GetValue(entity);

                    queryBuilder.Append($"{columnName} = @{columnName}, ");

                    var parameter = new MySqlParameter($"@{columnName}", value);
                    parameters.Add(parameter);
                }

                queryBuilder.Remove(queryBuilder.Length - 2, 2);
                queryBuilder.Append(" WHERE PedidoId = @Id");

                var query = queryBuilder.ToString();

                var command = new MySqlCommand(query, connection);
                command.Parameters.AddRange(parameters.ToArray());
                command.Parameters.AddWithValue("@Id", properties.First(p => p.Name == "PedidoId").GetValue(entity));

                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var query = $"DELETE FROM {typeof(T).Name} WHERE PedidoId = @Id";
                var command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);
                command.ExecuteNonQuery();
            }
        }
    }

   


}
