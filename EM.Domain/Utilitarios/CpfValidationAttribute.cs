using System.ComponentModel.DataAnnotations;

namespace EM.Domain.Utilitarios
{
    public class CpfValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            string cpf = value.ToString();

            // Use o método CPFValidado para validar o CPF
            if (!Validacoes.CPFValidado(cpf))
            {
                return new ValidationResult("CPF inválido.");
            }

            return ValidationResult.Success;
        }
    }
}
