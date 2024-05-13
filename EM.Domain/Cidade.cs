
using EM.Domain.Interface;
using System.ComponentModel.DataAnnotations;

namespace EM.Domain
{

    public class Cidade : IEntidade
    {
        public Cidade()
        {
        }

        public Cidade(int id_cidade, string? nome, string? uF)
        {
            Id_cidade = id_cidade;
            Nome = nome;
            UF = uF;
        }


        public int Id_cidade { get; set; }
        [Required]
        public string? Nome { get; set; }

        [Required]
        public string? UF { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is Cidade cidade &&
                Id_cidade == cidade.Id_cidade &&
                Nome == cidade.Nome &&
                UF == cidade.UF;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id_cidade, Nome, UF);

        }

        public override string? ToString()
        {
            return $"Nome: {Nome} - {UF}";
        }
    }
}
