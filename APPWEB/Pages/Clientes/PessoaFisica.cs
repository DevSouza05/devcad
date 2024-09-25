namespace APPWEB.Pages.Clientes
{
    public class PessoaFisica : IComparable<PessoaFisica>
    {
        public string ChaveNumerica { get; set; }
        public string NomeCompleto { get; set; }
        public string CPF { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Email { get; set; }

        public int CompareTo(PessoaFisica? other)
        {
            return other == null ? 1 : this.ChaveNumerica.CompareTo(other.ChaveNumerica);
        }
    }

}
