using EM.Domain.Enuns;
using EM.Domain.Interface;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace EM.Domain
{
    public class Aluno : IEntidade
    {
      
        public int? Id_Alunos { get; set; }
        public int Matricula { get; set; }
        public string? Nome { get; set; }

        [AllowNull]
        public string? CPF { get; set; }
        public DateTime? Nascimento { get; set; }
        public EnumeradorSexo? Sexo { get; set; }
        public Cidade? Cidade { get; set; }
    }
}
