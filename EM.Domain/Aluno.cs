using EM.Domain.Enuns;
using EM.Domain.Interface;
using System.ComponentModel.DataAnnotations;

namespace EM.Domain
{
    public class Aluno : IEntidade
    {
      
        public int Id_Alunos { get; set; }
        [Required]
        public int Matricula { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public DateTime Nascimento { get; set; }
        public EnumeradorSexo Sexo { get; set; }
        public Cidade Cidade { get; set; }
    }
}
