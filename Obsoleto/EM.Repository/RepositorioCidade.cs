using EM.Domain;
using EM.Repository.banco;
using EM.Domain.Utilitarios;
using FirebirdSql.Data.FirebirdClient;
using System.Data.Common;
using System.Linq.Expressions;

namespace EM.Repository
{
    public class RepositorioCidade : IRepositorioGeral<Cidade>
    {
        public void Add(Cidade cidade)
        {
            using DbConnection connect = ConnectionBanc.GetConnectionString();
            using DbCommand command = connect.CreateCommand();

            command.CommandText = "INSERT INTO Cidades (nome, UF) VALUES (@Nome, @UF)";

            command.Parameters.CreateParameter("@Nome", cidade.Nome.ToUpper());
            command.Parameters.CreateParameter("@UF", cidade.UF.ToUpper());
            command.ExecuteNonQuery();
        }

        public IEnumerable<Cidade> Get(Expression<Func<Cidade, bool>> predicate) => GetAll().Where(predicate.Compile());


        public IEnumerable<Cidade> GetAll()
        {
            List<Cidade> cidades = new List<Cidade>();

            using DbConnection connect = ConnectionBanc.GetConnectionString();
            using DbCommand command = connect.CreateCommand();

            command.CommandText = "SELECT * FROM Cidades order by UF";

            using (DbDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Cidade cidade = new()
                    {
                        Id_cidade = Convert.ToInt32(reader["Id_cidade"]),
                        Nome = reader["Nome"].ToString(),
                        UF = reader["UF"].ToString()
                    };
                    cidades.Add(cidade);
                }
            }
            return cidades;
        }

        public void Update(Cidade cidade)
        {
            using DbConnection connect = ConnectionBanc.GetConnectionString();
            using DbCommand command = connect.CreateCommand();

            command.CommandText = "UPDATE Cidades SET Nome = @Nome, UF = @UF WHERE Id_Cidade = @Id_Cidade";

            command.Parameters.CreateParameter("@Nome", cidade.Nome.ToUpper());
            command.Parameters.CreateParameter("@UF", cidade.UF.ToUpper());
            command.Parameters.CreateParameter("@Id_Cidade", cidade.Id_cidade);
            command.ExecuteNonQuery();

        }
    }
}
