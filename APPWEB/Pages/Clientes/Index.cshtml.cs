using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace APPWEB.Pages.Clientes
{
    public class IndexModel : PageModel
    {
        public string SearchTerm { get; set; } = string.Empty;
        public List<Funcionario> ListFuncionarios { get; private set; } = new List<Funcionario>();
        private readonly string caminhoArquivo;

        public IndexModel()
        {
            caminhoArquivo = Path.Combine(Directory.GetCurrentDirectory(), "funcionarios.json");
        }

        public void OnGet()
        {
            
            if (!Directory.Exists(Path.GetDirectoryName(caminhoArquivo)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(caminhoArquivo));
            }

            if (System.IO.File.Exists(caminhoArquivo))
            {
                string jsonString = System.IO.File.ReadAllText(caminhoArquivo);
                ListFuncionarios = JsonSerializer.Deserialize<List<Funcionario>>(jsonString) ?? new List<Funcionario>();
            }

            
            System.Diagnostics.Debug.WriteLine($"SearchTerm: {SearchTerm}");

        
            if (!string.IsNullOrWhiteSpace(SearchTerm))
            {
                ListFuncionarios = ListFuncionarios
                    .Where(f => f.CPF.Contains(SearchTerm))
                    .ToList();
            }
        }
    }
}
