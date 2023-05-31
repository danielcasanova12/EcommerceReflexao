using Ecommercenew.Models;
using Ecommercenew.UI;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Reflection.PortableExecutable;
using System.Text;

namespace Ecommercenew.Repositories
{
    public class ItemPedidoRepository : Repository<ItemPedido>, IItemPedidoRepository
    {
        public ItemPedidoRepository(string connectionString) : base(connectionString)
        {
        }
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

        public bool AdicionarItem(ItemPedido itemPedido)
        {
            try
            {
                Insert(itemPedido);

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


