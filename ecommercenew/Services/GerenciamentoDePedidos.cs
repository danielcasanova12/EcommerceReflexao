using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommercenew.Models;
using Ecommercenew.Repositories;
namespace Ecommercenew.Services
{
    public class GerenciamentoDePedidos
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IItemPedidoRepository _itemPedidoRepository;

        public GerenciamentoDePedidos(IPedidoRepository pedidoRepository, IItemPedidoRepository itemPedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
            _itemPedidoRepository = itemPedidoRepository;
        }

        public void CriarNovoPedido(Pedido pedido)
        {
            _pedidoRepository.Insert(pedido);
        }

        public void AdicionarItemAoPedido(ItemPedido itemPedido)
        {
            _itemPedidoRepository.Insert(itemPedido);
        }

        public void AtualizarStatusPedido(int pedidoId, string novoStatus)
        {
            var pedido = _pedidoRepository.GetById(pedidoId);
            if (pedido != null)
            {
                pedido.Status = novoStatus;
                _pedidoRepository.Update(pedido);
            }
        }

        public void RemoverPedido(int pedidoId)
        {
            _pedidoRepository.Delete(pedidoId);
        }

        public List<Pedido> ListarPedidosPorCliente(string cliente)
        {
            return _pedidoRepository.GetByCliente(cliente);
        }

        public List<Pedido> ListarPedidosPorStatus(string status)
        {
            return _pedidoRepository.GetByStatus(status);
        }

        public List<Pedido> ListarPedidosPorData(DateTime data)
        {
            return _pedidoRepository.GetByData(data);
        }
        public void CriarPedido(Pedido pedido)
        {
            _pedidoRepository.Adicionar(pedido);
        }
        public void ListarTodosOsProdutos()
        {
            _pedidoRepository.ListarProdutos();
        }
        public decimal CalcularValorTotalPedido(int pedidoId)
        {
            var itensPedido = _itemPedidoRepository.GetByPedido(pedidoId);
            decimal valorTotal = 0;

            foreach (var itemPedido in itensPedido)
            {
                valorTotal += itemPedido.Quantidade * itemPedido.PrecoUnitario;
            }

            return valorTotal;
        }


        public Pedido BuscarPorID(int pedidoId)
        {
            return _pedidoRepository.GetById(pedidoId);
        }

        public Produto ObterProdutoPorId(int produtoId)
        {
            return _pedidoRepository.GetProductById(produtoId);
        }

        public bool AdicionarItemPedido(ItemPedido itemPedido)
        {
            return _itemPedidoRepository.AdicionarItem(itemPedido);
        }
    }







}
