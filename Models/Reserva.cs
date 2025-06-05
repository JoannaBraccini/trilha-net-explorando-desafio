namespace DesafioProjetoHospedagem.Models
{
    public class Reserva
    {
        public List<Pessoa> Hospedes { get; set; }
        public Suite Suite { get; set; }
        public int DiasReservados { get; set; }

        public Reserva() { }

        public Reserva(int diasReservados)
        {
            DiasReservados = diasReservados;
        }

        public void CadastrarHospedes(List<Pessoa> hospedes)
        {
            if (Suite != null && hospedes.Count <= Suite.Capacidade)
            {
                Hospedes = hospedes;
            }
            else
            {
                throw new Exception("Quantidade de hóspedes excede a capacidade da suíte.");
            }
        }

        public void CadastrarSuite(Suite suite)
        {
            Suite = suite;
        }

        public int ObterQuantidadeHospedes()
        {
            if (Hospedes != null)
            {
                return Hospedes.Count;
            }
            else
            {
                return 0;
            }
        }

        public decimal CalcularValorDiaria()
        {
            if (DiasReservados > 0 && Suite != null)
            {
                decimal valor = DiasReservados * Suite.ValorDiaria;

                // Regra: Caso os dias reservados forem maior ou igual a 10, conceder um desconto de 10%
                if (DiasReservados >= 10)
                {
                    valor *= 0.9m; // Aplicando 10% de desconto ao multiplicar por 90% do valor original
                }

                // Converter para formato moeda (R$)
                string valorFormatado = valor.ToString("C2");

                Console.WriteLine($"Valor total da diária: {valorFormatado}");

                return valor;
            }
            else
            {
                throw new Exception(
                    "Não é possível calcular o valor da diária. Verifique os dados da reserva."
                );
            }
        }
    }
}
