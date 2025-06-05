using System.Text;
using DesafioProjetoHospedagem.Models;

Console.OutputEncoding = Encoding.UTF8;

// Desafio Projeto Hospedagem - Program.cs
// Menu Inicial
Console.WriteLine("Bem-vindo ao Desafio Projeto Hospedagem!");
Console.WriteLine("========================================");
Console.WriteLine("Este é um sistema de reservas de hospedagem.");
Console.WriteLine("Você pode cadastrar hóspedes e suítes, e calcular o valor da diária.");
Console.WriteLine("Pressione qualquer tecla para continuar...");
Console.ReadKey();

Console.WriteLine("\nIniciando o sistema...\n");

bool continuar = true;

// Lista global para armazenar reservas
List<Reserva> reservas = [];

while (continuar)
{
    Console.WriteLine(
        "O que você deseja fazer?\n1) Fazer reserva.\n2) Finalizar reserva.\n3) Sair do sistema."
    );

    // Aguarda a escolha do usuário
    string escolha = Console.ReadLine();

    switch (escolha)
    {
        case "1":
            Console.WriteLine("Cadastrando reserva.\n");
            Console.WriteLine("Digite o tipo da suíte:");
            string tipoSuite = Console.ReadLine();
            Console.WriteLine("Digite a capacidade da suíte:");
            int capacidade = int.Parse(Console.ReadLine() ?? "2");
            Console.WriteLine("Digite o valor da diária:");
            decimal valorDiaria = decimal.Parse(Console.ReadLine() ?? "10");
            Suite novaSuite = new(
                tipoSuite: tipoSuite,
                capacidade: capacidade,
                valorDiaria: valorDiaria
            );
            Console.WriteLine("Digite a quantidade de hóspedes:");
            int quantidadeHospedes = int.Parse(Console.ReadLine() ?? "1");
            List<Pessoa> hospedes = new();
            for (int i = 0; i < quantidadeHospedes; i++)
            {
                Console.WriteLine($"Digite o nome do hóspede {i + 1}:");
                string nomeHospede = Console.ReadLine();
                Console.WriteLine($"Digite o sobrenome do hóspede {i + 1}:");
                string sobrenomeHospede = Console.ReadLine();
                Pessoa novoHospede = new(nome: nomeHospede, sobrenome: sobrenomeHospede);
                hospedes.Add(novoHospede);
            }
            Console.WriteLine("Digite a quantidade de dias reservados:");
            int diasReservados = int.Parse(Console.ReadLine() ?? "1");
            Reserva novaReserva = new(diasReservados: diasReservados);
            novaReserva.CadastrarSuite(novaSuite);
            novaReserva.CadastrarHospedes(hospedes);
            reservas.Add(novaReserva); // Adiciona a nova reserva à lista global
            Console.WriteLine(
                $"Reserva feita com sucesso! Hóspedes: {novaReserva.ObterQuantidadeHospedes()}, Valor da diária: {novaReserva.CalcularValorDiaria():C2}"
            );
            break;

        case "2":
            Console.WriteLine("Checkout.\n");
            Console.WriteLine(
                "Digite o nome completo de um hóspede da reserva que deseja finalizar:"
            );
            string nomeHospedeFinalizar = Console.ReadLine();

            // Busca a reserva pelo nome do hóspede usando Find
            // A busca é feita considerando o nome completo do hóspede
            // e ignorando diferenças de maiúsculas/minúsculas
            Reserva reservaParaFinalizar = reservas.Find(r =>
                r.Hospedes.Any(h =>
                    h.NomeCompleto.Equals(nomeHospedeFinalizar, StringComparison.OrdinalIgnoreCase)
                )
            );

            if (reservaParaFinalizar != null) // Se a reserva foi encontrada
            {
                Console.WriteLine(
                    $"Reserva encontrada para {nomeHospedeFinalizar}.\n"
                        + $"Valor total da estadia: {reservaParaFinalizar.CalcularValorDiaria():C2}."
                );
                Console.WriteLine("Confirmar checkout? (s/n)");
                string confirmacao = Console.ReadLine()?.ToLower();

                while (confirmacao != "s" && confirmacao != "n")
                {
                    Console.WriteLine("Opção inválida. Digite 's' para sim ou 'n' para não:");
                    confirmacao = Console.ReadLine()?.ToLower();
                }

                if (confirmacao == "s")
                {
                    reservas.Remove(reservaParaFinalizar); // Remove a reserva da lista
                    Console.WriteLine("Reserva finalizada com sucesso!");
                }
                else
                {
                    Console.WriteLine("Checkout cancelado. Retornando ao menu principal.");
                }
            }
            else
            {
                Console.WriteLine("Nenhuma reserva encontrada para o hóspede informado.");
            }
            break;

        case "3":
            Console.WriteLine("Saindo do sistema...");
            continuar = false;
            break;

        default:
            Console.WriteLine("Opção inválida. Tente novamente.");
            break;
    }
}
Console.WriteLine("Obrigado por usar o sistema de reservas de hospedagem!");
Console.WriteLine("Até a próxima!");
// Fim do Desafio Projeto Hospedagem - Program.cs
