using EM.Domain.Interface;
using System.Linq.Expressions;

namespace EM.Repository
{
    public interface IRepositorioGeral<T> where T : IEntidade
    {
        void Add(T obj);
        void Update(T obj);
        IEnumerable<T> GetAll();
        IEnumerable<T> Get(Expression<Func<T, bool>> predicate);

    }
}
