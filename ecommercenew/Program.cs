using Ecommercenew.Models;
using Ecommercenew.Repositories;
using Ecommercenew.Services;
using Ecommercenew.UI;
using System.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;

var connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

var services = new ServiceCollection();

services.AddTransient<IPedidoRepository>(provider => new PedidoRepository(connectionString));
services.AddTransient<IItemPedidoRepository>(provider => new ItemPedidoRepository(connectionString));
services.AddTransient<IProdutoRepository>(provider => new ProdutoRepository(connectionString));
services.AddTransient<GerenciamentoDePedidos>();

var serviceProvider = services.BuildServiceProvider();
var gerenciamentoDePedidos = serviceProvider.GetService<GerenciamentoDePedidos>();
var pedidoUI = new PedidoUI(gerenciamentoDePedidos);
var itenspedidoUI = new ItemPedidoUI(gerenciamentoDePedidos);
while (true)
{
    Console.WriteLine("Selecione uma opção:");
    Console.WriteLine("1. Criar um novo pedido");
    Console.WriteLine("2. Adicionar itens a um pedido");
    Console.WriteLine("3. Atualizar o status de um pedido");
    Console.WriteLine("4. Remover um pedido");
    Console.WriteLine("5. Listar pedidos por cliente, status ou data");
    Console.WriteLine("6. Calcular o valor total de um pedido");
    Console.WriteLine("0. Sair");

    var opcao = Console.ReadLine();

    switch (opcao)
    {
        case "1":
            Console.Clear();
            pedidoUI.ChamarCriarPedido();
            break;
        case "2":
            Console.Clear();
            itenspedidoUI.ChamarAdicionarItensPedido();

            break;
        case "3":
            Console.Clear();
            pedidoUI.ChamarAtualizarStatusPedido();
            break;
        case "4":
            Console.Clear();
            pedidoUI.ChamarRemoverPedido();
            break;
        case "5":
            Console.Clear();
            Console.WriteLine("Selecione uma opção:");
            Console.WriteLine("1. Listar por cliente");
            Console.WriteLine("2. Listar por status");
            Console.WriteLine("3. Listar por data");
            var opcaoListagem = Console.ReadLine();
            switch (opcaoListagem)
            {
                case "1":
                    pedidoUI.ChamarListarPedidosPorCliente();
                    break;
                case "2":
                    pedidoUI.ChamarListarPedidosPorStatus();
                    break;
                case "3":
                    pedidoUI.ChamarListarPedidosPorData();
                    break;
                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    break;
            }
            break;
        case "6":
            Console.Clear();
            pedidoUI.ChamarCalcularValorTotalPedido();
            break;
        case "0":
            return;
        default:
            Console.WriteLine("Opção inválida. Tente novamente.");
            break;
    }

    Console.WriteLine();
}