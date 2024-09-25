using APPWEB.Pages.Clientes;
using System.Text.RegularExpressions;

namespace APPWEB.Helpers
{
    public static class Validacoes
    {
        public static bool IsValidCPF(string cpf)
        {
            cpf = cpf.Replace(".", "").Replace("-", "");

            if (cpf.Length != 11 || !Regex.IsMatch(cpf, @"^\d{11}$"))
                return false;

            int[] multiplicador1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            int soma = 0;
            for (int i = 0; i < 9; i++)
                soma += int.Parse(cpf[i].ToString()) * multiplicador1[i];

            int resto = soma % 11;
            int digito1 = resto < 2 ? 0 : 11 - resto;

            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(cpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            int digito2 = resto < 2 ? 0 : 11 - resto;

            return cpf[9] == digito1.ToString()[0] && cpf[10] == digito2.ToString()[0];
        }

        public static bool IsValidEmail(string email)
        {
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern);
        }

        public static bool IsValidStringWithoutNumbers(string value)
        {
            return !string.IsNullOrWhiteSpace(value) && !Regex.IsMatch(value, @"\d");
        }

        public static bool AreFuncionarioFieldsValid(Funcionario funcionario, out string errorMessage)
        {
            errorMessage = "";

            if (!IsValidStringWithoutNumbers(funcionario.NomeCompleto))
            {
                errorMessage = "Nome completo não pode conter números ou estar vazio.";
                return false;
            }

            if (!IsValidStringWithoutNumbers(funcionario.cargo))
            {
                errorMessage = "Cargo não pode conter números ou estar vazio.";
                return false;
            }

            if (!IsValidStringWithoutNumbers(funcionario.nomeDoChefeImediato))
            {
                errorMessage = "Nome do chefe não pode conter números ou estar vazio.";
                return false;
            }

            if (!IsValidEmail(funcionario.Email))
            {
                errorMessage = "Email inválido.";
                return false;
            }

            if (!IsValidCPF(funcionario.CPF))
            {
                errorMessage = "CPF inválido.";
                return false;
            }

            if (!int.TryParse(funcionario.ChaveNumerica, out int chaveNumerica) || funcionario.ChaveNumerica.Length != 6)
            {
                errorMessage = "A Chave Numérica deve ter exatamente 6 dígitos.";
                return false;
            }

            if (funcionario.DataNascimento > DateTime.Now)
            {
                errorMessage = "A Data de Nascimento não pode estar no futuro.";
                return false;
            }

            
            if (funcionario.dataAdmissao > DateTime.Now)
            {
                errorMessage = "A Data de Admissão não pode estar no futuro.";
                return false;
            }

            if (funcionario.dataAdmissao < funcionario.DataNascimento)
            {
                errorMessage = "A Data de Admissão não pode ser anterior à Data de Nascimento.";
                return false;
            }

            return true;
        }


    }
}
