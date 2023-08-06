using ADS_ED2_20230810.Controllers;
using ADS_ED2_20230810.Models;

namespace ADS_ED2_20230810
{
    internal class Program
    {
        static void Main(string[] args)
        {
            VendedoresController vendedoresController = new();

            void handleCadastrarVendedor()
            {
                Console.WriteLine("--- Adicionar vendedor ---");
                Console.WriteLine();

                Console.Write("Digite o nome do vendedor: ");
                string nome = Console.ReadLine();
                Console.WriteLine();

                Console.Write("Digite a porcentagem do vendedor (ex: 0.15): ");
                double percComissao = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine();

                VendedorController vendedor = new(nome, percComissao);

                bool isSuccess = vendedoresController.AddVendedor(vendedor);

                Console.WriteLine(isSuccess ? "Vendedor adicionado." : "Operação não permitida, limite de vendedores atingido.");

                Console.WriteLine("\n--- Fim da adição de vendedor ---\n");
            }

            void handleConsultarVendedor()
            {
                Console.WriteLine("--- Consultar vendedor ---");
                Console.WriteLine();

                Console.Write("Digite o ID do vendedor: ");
                int id = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine();

                VendedorController vendedor = new(id);

                VendedorController foundVendedor = vendedoresController.SearchVendedor(vendedor);

                if (foundVendedor.Id > -1)
                {
                    Console.WriteLine($"ID: {foundVendedor.Id}");
                    Console.WriteLine($"Nome: {foundVendedor.Nome}");
                    Console.WriteLine($"Valor total das vendas: R${foundVendedor.ValorVendas()}");
                    Console.WriteLine($"Valor total da comissão devida: R${foundVendedor.ValorComissao()} ({foundVendedor.PercComissao * 100}%)");

                    double sum = 0.0;
                    int registeredDays = 0;

                    for (int i = 0; i < foundVendedor.AsVendas.Length; i++)
                    {
                        VendaModel venda = foundVendedor.AsVendas[i];

                        if (venda.Valor > 0.0 && venda.Qtde > 0)
                        {
                            sum += venda.Valor; 
                            registeredDays++;
                        }
                    }

                    if (registeredDays > 0)
                    {
                        Console.WriteLine($"Valor médio das vendas diárias: R${sum / registeredDays} (Total das vendas / {registeredDays} dia(s))");
                    } else
                    {
                        Console.WriteLine("Valor médio das vendas diárias: R$0");
                    }

                } else
                {
                    Console.WriteLine("Vendedor não encontrado.");
                }

                Console.WriteLine("\n--- Fim da consulta de vendedor ---\n");
            }

            void handleExcluirVendedor()
            {
                Console.WriteLine("--- Deletar vendedor ---");
                Console.WriteLine();

                Console.Write("Digite o ID do vendedor: ");
                int id = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine();

                VendedorController vendedor = new(id);

                bool isSuccess = vendedoresController.DelVendedor(vendedor);

                Console.WriteLine(isSuccess ? "Vendedor excluído." : "Exclusão não permitida, vendedor não encontrado ou há vendas associadas a este vendedor.");

                Console.WriteLine("\n--- Fim da exclusão de vendedor ---\n");
            }

            void handleRegistrarVenda()
            {
                Console.WriteLine("--- Registrar venda ---");
                Console.WriteLine();

                Console.Write("Digite o ID do vendedor: ");
                int id = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine();

                VendedorController vendedor = new(id);

                VendedorController foundVendedor = vendedoresController.SearchVendedor(vendedor);

                if (foundVendedor.Id > -1)
                {
                    Console.Write("Digite o dia da venda: ");
                    int dia = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine();

                    Console.Write("Digite o valor da venda: ");
                    double valor = Convert.ToDouble(Console.ReadLine());
                    Console.WriteLine();

                    Console.Write("Digite a quantidade da venda: ");
                    int qtde = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine();

                    VendaModel venda = new(qtde, valor);

                    foundVendedor.RegistrarVenda(dia, venda);

                    Console.WriteLine("Venda registrada.");
                } else
                {
                    Console.WriteLine("Vendedor não encontrado");
                }

                Console.WriteLine("\n--- Fim do registro de venda ---\n");
            }

            void handleListarVendedores()
            {
                double totalVendas = 0.0, totalComissao = 0.0;

                Console.WriteLine("--- Listagem de vendedores ---");
                Console.WriteLine();

                foreach(VendedorController vendedor in vendedoresController.OsVendedores)
                {
                    Console.WriteLine($"ID: {vendedor.Id}");
                    Console.WriteLine($"Nome: {vendedor.Nome}");
                    Console.WriteLine($"Valor total das vendas: R${vendedor.ValorVendas()}");
                    Console.WriteLine($"Valor total da comissão devida: R${vendedor.ValorComissao()} ({vendedor.PercComissao*100}%)");

                    totalVendas += vendedor.ValorVendas();
                    totalComissao += vendedor.ValorComissao();

                    Console.WriteLine();
                }

                Console.WriteLine("---\n");

                Console.WriteLine($"Somatória das vendas de todos vendedores: R${totalVendas}");
                Console.WriteLine($"Somatória das comissões de todos vendedores: R${totalComissao}");


                Console.WriteLine("\n--- Fim da listagem de vendedores ---\n");
            }

            while (true)
            {
                int option = -1;

                Console.WriteLine("0. Sair");
                Console.WriteLine("1. Cadastrar vendedor");
                Console.WriteLine("2. Consultar vendedor");
                Console.WriteLine("3. Excluir vendedor");
                Console.WriteLine("4. Registrar venda");
                Console.WriteLine("5. Listar vendedores");

                Console.WriteLine();

                option = Convert.ToInt32(Console.ReadLine());

                Console.Clear();

                if (option == 0) break;
                if (option == 1) handleCadastrarVendedor();
                if (option == 2) handleConsultarVendedor();
                if (option == 3) handleExcluirVendedor();
                if (option == 4) handleRegistrarVenda();
                if (option == 5) handleListarVendedores();
            }

        }
    }
}