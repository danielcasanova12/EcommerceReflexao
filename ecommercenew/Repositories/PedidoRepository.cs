using Ecommercenew.Models;
using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Configuration;
using Ecommercenew.UI;
using static System.Net.Mime.MediaTypeNames;

namespace Ecommercenew.Repositories
{
    public class PedidoRepository : Repository<Pedido>, IPedidoRepository
    {
        private string _connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
        public PedidoRepository(string connectionString) : base(connectionString)
        {
        }
        public void ListarProdutos()
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var query = "SELECT * FROM tb_produto";
                var command = new MySqlCommand(query, connection);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var produtoId = reader.GetInt32("ProdutoId");
                        var nome = reader.GetString("Nome");
                        var preco = reader.GetDecimal("Preco");

                        Console.WriteLine($"ID: {produtoId}, Nome: {nome}, Preço: {preco}");
                    }
                }
            }
        }



        public Produto GetProductById(int produtoId)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var query = $"SELECT * FROM tb_produto WHERE ProdutoId = @ProdutoId";
                var command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@ProdutoId", produtoId);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        var produto = new Produto();
                        produto.ProdutoId = reader.GetInt32("ProdutoId");
                        produto.Nome = reader.GetString("Nome");
                        produto.Preco = reader.GetDecimal("Preco");
                        // Defina outros atributos do produto conforme necessário

                        return produto;
                    }
                }
            }

            return null;
        }

        public Pedido GetById(int pedidoId)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var query = $"SELECT * FROM tb_pedido WHERE PedidoId = @PedidoId";
                var command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@PedidoId", pedidoId);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        var pedido = new Pedido();
                        pedido.PedidoId = reader.GetInt32("PedidoId");
                        pedido.DataPedido = reader.GetDateTime("DataPEdido");
                        pedido.Cliente = reader.GetString("Cliente");
                        pedido.Status = reader.GetString("Status");
                        // Defina outros atributos do pedido conforme necessário

                        return pedido;
                    }
                }
            }

            return null;
        }




        public void Adicionar(Pedido pedido)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var query = "INSERT INTO tb_pedido (DataPedido, Cliente, Status) VALUES (@DataPedido, @Cliente, @StatusPedido); SELECT LAST_INSERT_ID()";
                var command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@DataPedido", pedido.DataPedido);
                command.Parameters.AddWithValue("@Cliente", pedido.Cliente);
                command.Parameters.AddWithValue("@StatusPedido", pedido.Status);
                var id = Convert.ToInt32(command.ExecuteScalar());
                pedido.PedidoId = id;
            }
            Console.WriteLine("Seu id é : "+ pedido.PedidoId);
        }




        public List<Pedido> GetByCliente(string cliente)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var query = $"SELECT * FROM tb_pedido WHERE Cliente = @Cliente";
                var command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Cliente", cliente);

                var pedidos = new List<Pedido>();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var pedido = new Pedido
                        {
                            PedidoId = reader.GetInt32("PedidoId"),
                            DataPedido = reader.GetDateTime("DataPedido"),
                            Cliente = reader.GetString("Cliente"),
                            Status = reader.GetString("Status")
                        };

                        pedidos.Add(pedido);
                    }
                }

                return pedidos;
            }
        }

        public List<Pedido> GetByStatus(string status)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var query = $"SELECT * FROM tb_pedido WHERE StatusPedido = @Status";
                var command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Status", status);

                var pedidos = new List<Pedido>();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var pedido = new Pedido
                        {
                            PedidoId = reader.GetInt32("PedidoId"),
                            DataPedido = reader.GetDateTime("DataPedido"),
                            Cliente = reader.GetString("Cliente"),
                            Status = reader.GetString("Status")
                        };

                        pedidos.Add(pedido);
                    }
                }

                return pedidos;
            }
        }

        public List<Pedido> GetByData(DateTime data)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var query = $"SELECT * FROM tb_pedido WHERE DataPedido = @Data";
                var command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Data", data);

                var pedidos = new List<Pedido>();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var pedido = new Pedido
                        {
                            PedidoId = reader.GetInt32("PedidoId"),
                            DataPedido = reader.GetDateTime("DataPedido"),
                            Cliente = reader.GetString("Cliente"),
                            Status = reader.GetString("Status")
                        };

                        pedidos.Add(pedido);
                    }
                }

                return pedidos;
            }
        }
    }

}
