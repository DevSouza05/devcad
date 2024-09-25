using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace APPWEB.Pages.Clientes
{
    public class DeleteModel : PageModel
    {
        public string ChaveNumerica { get; set; }
        public string NomeCompleto { get; set; }
        public string errorMessage { get; set; } = "";
        public string successMessage { get; set; } = "";

        private string caminhoArquivo = Path.Combine(Directory.GetCurrentDirectory(), "funcionarios.json");

        public void OnGet(string chaveNumerica)
        {
            ChaveNumerica = chaveNumerica;

           
            var funcionarios = new List<Funcionario>();
            if (System.IO.File.Exists(caminhoArquivo))
            {
                string jsonString = System.IO.File.ReadAllText(caminhoArquivo);
                funcionarios = JsonSerializer.Deserialize<List<Funcionario>>(jsonString) ?? new List<Funcionario>();
            }

            var funcionario = funcionarios.Find(f => f.ChaveNumerica == chaveNumerica);
            if (funcionario != null)
            {
                NomeCompleto = funcionario.NomeCompleto;
            }
        }

        public IActionResult OnPost(string chaveNumerica)
        {
            if (string.IsNullOrEmpty(chaveNumerica))
            {
                errorMessage = "Chave numérica não fornecida.";
                return Page();
            }

            var funcionarios = new List<Funcionario>();
            if (System.IO.File.Exists(caminhoArquivo))
            {
                string jsonString = System.IO.File.ReadAllText(caminhoArquivo);
                funcionarios = JsonSerializer.Deserialize<List<Funcionario>>(jsonString) ?? new List<Funcionario>();
            }

            var funcionarioToRemove = funcionarios.Find(f => f.ChaveNumerica == chaveNumerica);
            if (funcionarioToRemove != null)
            {
                funcionarios.Remove(funcionarioToRemove);
                System.IO.File.WriteAllText(caminhoArquivo, JsonSerializer.Serialize(funcionarios));
                successMessage = "Funcionário excluído com sucesso!";
                return RedirectToPage("Index");
            }
            else
            {
                errorMessage = "Funcionário não encontrado.";
            }

            return Page();
        }
    }
}
