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

    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        
        private string _connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
        public ProdutoRepository(string connectionString) : base(connectionString)
        {
        }
        public void ListarProdutos()
        {
            var entities = RetornarTodos<Produto>();
            Console.WriteLine(" ");
            foreach (var entity in entities)
            {
                Console.Write("Id: "+ entity.ProdutoId.ToString() );
                Console.Write(" Nome: " + entity.Nome.ToString() );
                Console.Write(" Preço: " + entity.Preco.ToString() );
                Console.WriteLine(" ");
            }
        }


    }

}
