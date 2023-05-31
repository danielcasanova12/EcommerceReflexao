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
        
        public PedidoRepository(string connectionString) : base(connectionString)
        {
        }







        public Pedido GetById(int pedidoId)
        {
            return GetById<Pedido>(pedidoId);
        }




        public void Adicionar(Pedido pedido)
        {
            Insert(pedido);
            Console.WriteLine("Seu id é : " + ObterUltimoIdPedido());
        }

        public int ObterUltimoIdPedido()
        {
            var pedidos = RetornarTodos<Pedido>();
            if (pedidos.Count > 0)
            {
                pedidos.Sort((p1, p2) => p2.PedidoId.CompareTo(p1.PedidoId));
                return pedidos[0].PedidoId;
            }
            else
            {
                return 0; // ou algum valor padrão que você deseje retornar caso não haja pedidos
            }
        }
        public List<Pedido> GetByCliente(string cliente)
        {
            return RetornarTodos<Pedido>().Where(p => p.Cliente == cliente).ToList();
        }

        public List<Pedido> GetByStatus(string status)
        {
            return RetornarTodos<Pedido>().Where(p => p.Status == status).ToList();
        }

        public List<Pedido> GetByData(DateTime data)
        {
            return RetornarTodos<Pedido>().Where(p => p.DataPedido == data).ToList();
        }
        public Produto GetProductById(int produtoId)
        {
            return GetById<Produto>(produtoId);
        }
        public void DeletePedido(int pedidoId)
        {
            Delete(pedidoId);
        }
    }

}
