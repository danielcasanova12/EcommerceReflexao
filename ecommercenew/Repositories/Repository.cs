using System.Text;
using MySql.Data.MySqlClient;
using static System.Net.Mime.MediaTypeNames;

namespace Ecommercenew.Repositories
{
    public class Repository<T> : IRepository<T>
    {
        private readonly string _connectionString;
        private int precoUnitario = 11;

        public Repository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public List<T> RetornarTodos()
        {
            return RetornarTodos<T>();
        }

        public List<T> RetornarTodos<T>()
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var query = $"SELECT * FROM tb_{typeof(T).Name.ToLower()}";
                var command = new MySqlCommand(query, connection);

                var entities = new List<T>();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var entity = Activator.CreateInstance<T>();
                        var properties = typeof(T).GetProperties();

                        foreach (var property in properties)
                        {
                            if (property.Name == "Id")
                            {
                                var id = reader.GetInt32("PedidoId");
                                property.SetValue(entity, id);
                                continue;
                            }

                            var value = reader[property.Name];
                            if (value != DBNull.Value)
                            {
                                property.SetValue(entity, value);
                            }
                        }

                        entities.Add(entity);
                    }
                }

                return entities;
            }
        }
        public T GetById<T>(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var tableName = typeof(T).Name.ToLower();
                var idColumnName = tableName == "pedido" ? "PedidoId" : "ProdutoId";
                var query = $"SELECT * FROM tb_{tableName} WHERE {idColumnName} = @Id";
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

                            var columnName = property.Name;

                            if (columnName == "PedidoId" && idColumnName == "ProdutoId")
                            {
                                columnName = "ProdutoId";
                            }

                            var value = reader[columnName];
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
                var queryBuilder = new StringBuilder($"INSERT INTO tb_{typeof(T).Name} (");
                var valuesBuilder = new StringBuilder("VALUES (");
                var parameters = new List<MySqlParameter>();

                var properties = typeof(T).GetProperties();

                foreach (var property in properties)
                {
                    if (property.Name == "Id" || property.GetValue(entity) == null)
                        continue;

                    string columnName = property.Name.ToLower();
                    object value = property.GetValue(entity);
                    if (columnName == "produto")
                    {
                        columnName = "ProdutoId";
                        value = ((Ecommercenew.Models.Produto)value).ProdutoId;
                    }
                    if (columnName == "precounitario")
                    {
                        columnName = "preco_unitario";
                        value = precoUnitario;
                    }


                    if (columnName == "pedido")
                    {
                        columnName = "PedidoId";
                        value = ((Ecommercenew.Models.Pedido)value).PedidoId;
                    }
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
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();
                    var query = $"DELETE FROM tb_{typeof(T).Name} WHERE PedidoId = @Id";
                    var command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                }
                Console.WriteLine("Deletado com suseso");
            }
            catch
            {
                Console.WriteLine("erro ao Deletar");
            }

        }
    }
}
