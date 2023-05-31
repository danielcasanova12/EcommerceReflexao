using Ecommercenew.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommercenew.Repositories
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        public void ListarProdutos();
    }

}
