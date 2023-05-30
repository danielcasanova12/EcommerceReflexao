using Ecommercenew.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommercenew.Repositories
{
    public interface IPedidoRepository : IRepository<Pedido>
    {
        List<Pedido> GetByCliente(string cliente);
        List<Pedido> GetByStatus(string status);
        List<Pedido> GetByData(DateTime data);
        void Adicionar(Pedido pedido);
        public Produto GetProductById(int produtoId);
        public Pedido GetById(int pedidoId);
        public void DeletePedido(int pedidoId);
    }

}
