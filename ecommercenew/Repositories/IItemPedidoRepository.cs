using Ecommercenew.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommercenew.Repositories
{
    public interface IItemPedidoRepository : IRepository<ItemPedido>
    {
        List<ItemPedido> GetByPedido(int pedidoId);
         bool AdicionarItem(ItemPedido itemPedido);
    }

}
