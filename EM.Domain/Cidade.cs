
using EM.Domain.Interface;

namespace EM.Domain
{
    public class Cidade : IEntidade
    {
        public int Id_Cidade { get; set; }
        public string Nome { get; set; }
        public string UF { get; set; }
    }
}
