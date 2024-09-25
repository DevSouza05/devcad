using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using System.Text.RegularExpressions;
using APPWEB.Helpers; // Importa o namespace onde Validacoes está definido

namespace APPWEB.Pages.Clientes
{
    public class Index1Model : PageModel
    {
        public Funcionario Funcionario = new Funcionario();
        public string errorMessage = "";
        public string successMessage = "";

        private string caminhoArquivo = Path.Combine(Directory.GetCurrentDirectory(), "funcionarios.json");

        public void OnGet()
        {
        }

        public void OnPost()
        {
            Funcionario.NomeCompleto = Request.Form["NomeCompleto"];
            Funcionario.CPF = Request.Form["CPF"];
            Funcionario.cargo = Request.Form["cargo"];
            Funcionario.DataNascimento = DateTime.Parse(Request.Form["DataNascimento"]);
            Funcionario.ChaveNumerica = Request.Form["ChaveNumerica"];
            Funcionario.nomeDoChefeImediato = Request.Form["NomeDoChefeImediato"];
            Funcionario.Email = Request.Form["Email"];
            Funcionario.dataAdmissao = DateTime.Parse(Request.Form["dataAdmissao"]);

           
            if (!Validacoes.AreFuncionarioFieldsValid(Funcionario, out string validationError))
            {
                errorMessage = validationError;
                return; 
            }

            try
            {
                List<Funcionario> funcionarios = new List<Funcionario>();
                if (System.IO.File.Exists(caminhoArquivo))
                {
                    string jsonString = System.IO.File.ReadAllText(caminhoArquivo);
                    funcionarios = JsonSerializer.Deserialize<List<Funcionario>>(jsonString) ?? new List<Funcionario>();
                }

                if (funcionarios.Any(f => f.CPF == Funcionario.CPF || f.ChaveNumerica == Funcionario.ChaveNumerica))
                {
                    errorMessage = "Um Funcionário com o mesmo CPF ou Chave Numérica já existe.";
                    return;
                }

                funcionarios.Add(Funcionario);

                string newJsonString = JsonSerializer.Serialize(funcionarios, new JsonSerializerOptions { WriteIndented = true });
                System.IO.File.WriteAllText(caminhoArquivo, newJsonString);

                successMessage = "Novo Funcionário adicionado com sucesso.";
                Funcionario = new Funcionario();
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }
    }
}
