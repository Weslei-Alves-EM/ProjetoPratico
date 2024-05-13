using EM.Domain;
using EM.Domain.Interface;
using System.Linq.Expressions;

namespace EM.Repository
{
    public interface IRepositorioAluno<T> where T : IEntidade
    {

        public void Remove(T obj);
        public Aluno GetByMatricula(int matricula);
        public IEnumerable<Aluno> GetByContendoNoNome(string parteDoNome);
        public IEnumerable<Aluno> GetByEstado(string uf);

    }
}
