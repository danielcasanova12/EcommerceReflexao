using Ecommercenew.Models;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace Ecommercenew.Repositories
{
    public class ItemPedidoRepository : Repository<ItemPedido>, IItemPedidoRepository
    {
        public ItemPedidoRepository(string connectionString) : base(connectionString)
        {
        }
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
        public List<ItemPedido> GetByPedido(int pedidoId)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var query = $"SELECT * FROM tb_ItemPedido WHERE PedidoId = @PedidoId";
                var command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@PedidoId", pedidoId);

                var itensPedido = new List<ItemPedido>();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var itemPedido = new ItemPedido
                        {
                            Id = reader.GetInt32("ItemPedidoId"),
                            Quantidade = reader.GetInt32("quantidade"),
                            PrecoUnitario = reader.GetDecimal("preco_unitario")
                        };

                        var pedidoRepository = new PedidoRepository(_connectionString);
                        var pedido = pedidoRepository.GetById(pedidoId);
                        itemPedido.Pedido = pedido;

                        var produtoId = reader.GetInt32("ProdutoId");
                        var produtoRepository = new PedidoRepository(_connectionString);
                        var produto = produtoRepository.GetProductById(produtoId);
                        itemPedido.Produto = produto;

                        itensPedido.Add(itemPedido);
                    }
                }

                return itensPedido;
            }
        }
        public bool AdicionarItem(ItemPedido itemPedido)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();
                    var query = "INSERT INTO tb_itempedido (ItemPedidoId, ProdutoId, Quantidade, preco_unitario, PedidoId) VALUES (@ItemPedidoId, @ProdutoId, @Quantidade, @PrecoUnitario, @PedidoId);";
                    var command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ItemPedidoId", itemPedido.Id);
                    command.Parameters.AddWithValue("@ProdutoId", itemPedido.Produto.ProdutoId);
                    command.Parameters.AddWithValue("@Quantidade", itemPedido.Quantidade);
                    command.Parameters.AddWithValue("@PrecoUnitario", itemPedido.Produto.Preco);
                    command.Parameters.AddWithValue("@PedidoId", itemPedido.Pedido.PedidoId);
                    command.ExecuteNonQuery();
                }

                Console.Clear();
                Console.WriteLine("Produto adicionado ao pedido");
                return true;
            }
            catch
            {
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine("Erro: ID do pedido ou produto não existe");
                Console.WriteLine();
                return false;
            }
        }



    }
}


