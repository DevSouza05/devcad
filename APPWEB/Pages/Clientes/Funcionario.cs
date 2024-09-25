namespace APPWEB.Pages.Clientes
{
    public class Funcionario:PessoaFisica
    {

         public string cargo { get; set; }
        public  DateTime dataAdmissao { get; set; }
        public string nomeDoChefeImediato { get; set; }
    }
}
