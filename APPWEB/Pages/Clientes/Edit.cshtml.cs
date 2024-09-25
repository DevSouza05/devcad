using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using APPWEB.Helpers; 

namespace APPWEB.Pages.Clientes
{
    public class EditModel : PageModel
    {
        public Funcionario Funcionario { get; set; } = new Funcionario();
        public string errorMessage { get; set; } = "";
        public string successMessage { get; set; } = "";
        private string caminhoArquivo = Path.Combine(Directory.GetCurrentDirectory(), "funcionarios.json");

        public void OnGet(string chaveNumerica)
        {
            if (System.IO.File.Exists(caminhoArquivo))
            {
                string jsonString = System.IO.File.ReadAllText(caminhoArquivo);
                var funcionarios = JsonSerializer.Deserialize<List<Funcionario>>(jsonString) ?? new List<Funcionario>();
                Funcionario = funcionarios.Find(f => f.ChaveNumerica == chaveNumerica);

                if (Funcionario == null)
                {
                    errorMessage = "Funcionário não encontrado.";
                }
            }
        }

        public void OnPost()
        {
            Funcionario.ChaveNumerica = Request.Form["ChaveNumerica"];
            Funcionario.NomeCompleto = Request.Form["NomeCompleto"];
            Funcionario.cargo = Request.Form["Cargo"];
            Funcionario.CPF = Request.Form["CPF"];
            Funcionario.nomeDoChefeImediato = Request.Form["NomeDoChefeImediato"];
            Funcionario.DataNascimento = DateTime.Parse(Request.Form["DataNascimento"]);
            Funcionario.Email = Request.Form["Email"];
            Funcionario.dataAdmissao = DateTime.Parse(Request.Form["DataAdmissao"]);

            // Validações
            if (!Validacoes.AreFuncionarioFieldsValid(Funcionario, out string validationError))
            {
                errorMessage = validationError;
                return; 
            }

            var funcionarios = new List<Funcionario>();
            if (System.IO.File.Exists(caminhoArquivo))
            {
                string jsonString = System.IO.File.ReadAllText(caminhoArquivo);
                funcionarios = JsonSerializer.Deserialize<List<Funcionario>>(jsonString) ?? new List<Funcionario>();
            }

            var index = funcionarios.FindIndex(f => f.ChaveNumerica == Funcionario.ChaveNumerica);
            if (index >= 0)
            {
                funcionarios[index] = Funcionario;
                System.IO.File.WriteAllText(caminhoArquivo, JsonSerializer.Serialize(funcionarios));
                successMessage = "Funcionário atualizado com sucesso!";
            }
            else
            {
                errorMessage = "Funcionário não encontrado para atualização.";
            }
        }
    }
}
