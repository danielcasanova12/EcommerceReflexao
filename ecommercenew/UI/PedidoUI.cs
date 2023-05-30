using Ecommercenew.Models;
using Ecommercenew.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommercenew.UI
{
    public class PedidoUI
    {
        private readonly GerenciamentoDePedidos _gerenciador;

        public PedidoUI(GerenciamentoDePedidos gerenciamentoDePedidos)
        {
            this._gerenciador = gerenciamentoDePedidos;
        }

        public void ChamarCriarPedido()
        {
            CriarPedido();
        }
        public void ChamarAtualizarStatusPedido()
        {
            AtualizarStatusPedido();
        }
        public void ChamarRemoverPedido()
        {
            RemoverPedido();
        }
        public void ChamarListarPedidosPorCliente()
        {
            ListarPedidosPorCliente();
        }
        public void ChamarListarPedidosPorStatus()
        {
            ListarPedidosPorStatus();
        }
        public void ChamarListarPedidosPorData()
        {
            ListarPedidosPorData();
        }
        public void ChamarCalcularValorTotalPedido()
        {
            CalcularValorTotalPedido();
        }

        private void CriarPedido()
        {
            //try
            //{

                Console.WriteLine("Digite o nome do cliente:");
                var nomeCliente = Console.ReadLine();
                var novoPedido = new Pedido
                {
                    Cliente = nomeCliente,
                    Status = "Em aberto",
                    DataPedido = DateTime.Now
                };
                Console.Clear();
                _gerenciador.CriarPedido(novoPedido);
                Console.WriteLine("Pedido criado com sucesso.");

            //}
            //catch
            //{
            //    Console.Clear();
            //    Console.WriteLine("Erro ao Criar pedido");
            //}
        }


        private void AtualizarStatusPedido()
        {
            try
            {
                Console.WriteLine("Digite o ID do pedido:");
                var pedidoId = int.Parse(Console.ReadLine());
                Console.WriteLine("Digite o novo status:");
                var novoStatus = Console.ReadLine();
                _gerenciador.AtualizarStatusPedido(pedidoId, novoStatus);
                Console.WriteLine("Status do pedido atualizado com sucesso.");
            }
            catch
            {
                Console.Clear();
                Console.WriteLine("Erro ao atualizar o status do pedido.");
            }
        }
        private void RemoverPedido()
        {
            try
            {
                Console.WriteLine("Digite o ID do pedido:");
                int id = int.Parse(Console.ReadLine());

                _gerenciador.RemoverPedido(id);
            }
            catch
            {
                Console.Clear();
                Console.WriteLine("Erro ao remover pedido.");
            }
        }

        private void ListarPedidosPorCliente()
        {
            try
            {
                Console.WriteLine("Digite o nome do cliente:");
                string nomeCliente = Console.ReadLine();

                var pedidos = _gerenciador.ListarPedidosPorCliente(nomeCliente);
                if (pedidos.Count == 0)
                {
                    Console.WriteLine("Nenhum pedido encontrado para o cliente informado.");
                    return;
                }

                Console.WriteLine($"Pedidos do cliente {nomeCliente}:");
                foreach (var pedido in pedidos)
                {
                    Console.WriteLine($"ID: {pedido.PedidoId} | Data do pedido: {pedido.DataPedido} | Status: {pedido.Status}");
                }
            }
            catch
            {
                Console.Clear();
                Console.WriteLine("Erro ao listar pedidos por cliente.");
            }
        }

        private void ListarPedidosPorStatus()
        {
            try { 
                Console.WriteLine("Digite o status:");
                string status = Console.ReadLine();

                var pedidos = _gerenciador.ListarPedidosPorStatus(status);
                if (pedidos.Count == 0)
                {
                    Console.WriteLine("Nenhum pedido encontrado para o status informado.");
                    return;
                }

                Console.WriteLine($"Pedidos com status {status}:");
                foreach (var pedido in pedidos)
                {
                    Console.WriteLine($"ID: {pedido.PedidoId} | Data do pedido: {pedido.DataPedido} | Cliente: {pedido.Cliente}");
                }
            }
            catch
            {
                Console.Clear();
                Console.WriteLine("Erro ao listar pedidos por status.");
            }
}

        private void ListarPedidosPorData()
        {
            try { 
                DateTime exemploData = new DateTime(2023, 06, 01);
                Console.WriteLine($"Digite a data inicial no formato dd/MM/yyyy (exemplo: {exemploData.ToString("dd/MM/yyyy")}):");
                DateTime dataInicial = DateTime.Parse(Console.ReadLine());
                Console.WriteLine("Digite a data final (dd/MM/yyyy):");
                DateTime dataFinal = DateTime.Parse(Console.ReadLine());

                if (dataFinal < dataInicial)
                {
                    Console.WriteLine("A data final deve ser posterior à data inicial.");
                    return;
                }

                var pedidos = _gerenciador.ListarPedidosPorData(dataInicial);
                if (pedidos.Count == 0)
                {
                    Console.WriteLine("Nenhum pedido encontrado para o período informado.");
                    return;
                }

                Console.WriteLine($"Pedidos no período de {dataInicial:dd/MM/yyyy} a {dataFinal:dd/MM/yyyy}:");
                foreach (var pedido in pedidos)
                {
                    Console.WriteLine($"ID: {pedido.PedidoId} | Cliente: {pedido.Cliente} | Status: {pedido.Status}");
                }
            }
            catch
            {
                Console.Clear();
                Console.WriteLine("Erro ao listar pedidos por data.");
            }

}

        private void CalcularValorTotalPedido()
        {
            try 
            { 
                Console.WriteLine("Digite o ID do pedido:");
                int id = int.Parse(Console.ReadLine());

                decimal valorTotal = _gerenciador.CalcularValorTotalPedido(id);
                Console.WriteLine($"Valor total do pedido: R$ {valorTotal}");
            }
            catch
            {
                Console.Clear();
                Console.WriteLine("Erro ao listar pedidos por data.");
            }
        }
        public void mostrarClientes(List<Pedido> lista)
        {
            foreach (var item in lista)
            {
                Console.WriteLine(item.PedidoId);
                Console.WriteLine(item.Cliente);
            }
        }

        //private void AdicionarItensAoPedido()
        //{
        //    Console.WriteLine("Digite o ID do pedido:");
        //    var pedidoId = int.Parse(Console.ReadLine());
        //    Console.WriteLine("Digite o ID do produto:");
        //    var produtoId = int.Parse(Console.ReadLine());
        //    Console.WriteLine("Digite a quantidade:");
        //    var quantidade = int.Parse(Console.ReadLine());
        //    var pedido = _gerenciamentoDePedidos.BuscarPorID(pedidoId);
        //    var produto = _gerenciamentoDePedidos.ObterProdutoPorId(produtoId);
        //    var itemPedido = new ItemPedido
        //    {
        //        Produto = produtoId,
        //        Quantidade = quantidade,
        //        PrecoUnitario = 55 // aqui você pode alterar para o preço real do produto, se tiver essa informação disponível
        //    };
        //    _gerenciador.AdicionarItemAoPedido(pedidoId, itemPedido);
        //    Console.WriteLine("Item adicionado ao pedido com sucesso.");
        //}
    }
}
