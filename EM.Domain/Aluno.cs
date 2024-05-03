using EM.Domain.Interface;
using System;
using System.ComponentModel.DataAnnotations;
using EM.Domain.Enuns;
using EM.Domain.Utilitarios;

namespace EM.Domain
{
    public class Aluno : IEntidade
    {
        public int Id_Alunos { get; set; }
        public int Matricula { get; set; }

        [Required(ErrorMessage = "O nome do usuário é obrigatório", AllowEmptyStrings = false)]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O tamanho do nome deve estar entre 3 e 100 caracteres.")]
        public string Nome { get; set; }

        [CpfValidation]
        [StringLength(14)]
        public string? CPF { get; set; }

        [Required(ErrorMessage = "A data de nascimento do usuário é obrigatório")]
        [Display(Name = "Nascimento")]
        [MinIdade(3, ErrorMessage = "O usuário deve ter pelo menos 3 anos de idade.")]
        public DateTime Nascimento { get; set; }

        [Required(ErrorMessage = "O genero do usuário é obrigatório")]
        [Display(Name = "Sexo")]
        [EnumDataType(typeof(EnumeradorSexo), ErrorMessage = "Valor inválido para o sexo.")]
        public EnumeradorSexo Sexo { get; set; }

        [Required(ErrorMessage = "A cidade do usuário é obrigatório")]
        [Display(Name = "Cidade")]
        public Cidade Cidade { get; set; }
    }
}
