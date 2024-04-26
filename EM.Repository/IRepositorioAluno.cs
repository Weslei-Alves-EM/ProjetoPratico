using EM.Domain.Interface;
using System.Linq.Expressions;

namespace EM.Repository
{
    public interface IRepositorioAluno<T> where T : IEntidade
    {
        public void Add(T obj);
        public void Remove(T obj);
        public void Update(T obj);
        public IEnumerable<T> GetAll();
        public IEnumerable<T> Get(Expression<Func<T, bool>> predicate);
    }
}
