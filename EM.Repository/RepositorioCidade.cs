using EM.Domain;
using EM.Repository.banco;
using EM.Repository.Utilitarios;
using FirebirdSql.Data.FirebirdClient;
using System.Data.Common;
using System.Linq.Expressions;

namespace EM.Repository
{
    public class RepositorioCidade : IRepositorioCidade<Cidade>
    {
        public void Add(Cidade cidade)
        {

            using (DbConnection connection = new FbConnection(ConnectionBanc.GetConnectionString()))
            {
                connection.Open();
                using (DbCommand command = connection.CreateCommand())
                {
                    command.CommandText = "INSERT INTO Cidades (nome, UF) VALUES (@Nome, @UF)";

                    command.Parameters.CreateParameter("@Nome", cidade.Nome.ToUpper());
                    command.Parameters.CreateParameter("@UF", cidade.UF.ToUpper());
                    command.ExecuteNonQuery();
                }
            }
        }

        public IEnumerable<Cidade> Get(Expression<Func<Cidade, bool>> predicate)
        {
            List<Cidade> cidades = new List<Cidade>();

            using (DbConnection connection = new FbConnection(ConnectionBanc.GetConnectionString()))
            {
                connection.Open();
                using (DbCommand command = connection.CreateCommand())
                {
                    // Construir a consulta SQL com base no predicado
                    string sql = "SELECT * FROM Cidades WHERE ";
                    var expression = (UnaryExpression)predicate.Body;
                    var lambdaExpression = (LambdaExpression)expression.Operand;
                    var binaryExpression = (BinaryExpression)lambdaExpression.Body;
                    var leftExpression = (MemberExpression)binaryExpression.Left;
                    var rightExpression = (ConstantExpression)binaryExpression.Right;

                    sql += leftExpression.Member.Name + " = @" + leftExpression.Member.Name;

                    command.CommandText = sql;
                    command.Parameters.Add(new FbParameter("@" + leftExpression.Member.Name, rightExpression.Value));

                    using (DbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Cidade cidade = new Cidade
                            {
                                Id_Cidade = Convert.ToInt32(reader["Id_Cidade"]),
                                Nome = reader["Nome"].ToString(),
                                UF = reader["UF"].ToString()
                            };

                            cidades.Add(cidade);
                        }
                    }
                }
            }

            return cidades;
        }

        public IEnumerable<Cidade> GetAll()
        {
            List<Cidade> cidades = new List<Cidade>();

            using (DbConnection connection = new FbConnection(ConnectionBanc.GetConnectionString()))
            {
                connection.Open();
                using (DbCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM Cidades";

                    using (DbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Cidade cidade = new Cidade
                            {
                                Id_Cidade = Convert.ToInt32(reader["Id_Cidade"]),
                                Nome = reader["Nome"].ToString(),
                                UF = reader["UF"].ToString()
                            };

                            cidades.Add(cidade);
                        }
                    }
                }
            }

            return cidades;
        }

        public void Update(Cidade cidade)
        {
            using (DbConnection connection = new FbConnection(ConnectionBanc.GetConnectionString()))
            {
                using (DbCommand command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = "UPDATE Cidades SET Nome = @Nome, UF = @UF WHERE Id_Cidade = @Id_Cidade";

                    command.Parameters.CreateParameter("@Nome", cidade.Nome.ToUpper());
                    command.Parameters.CreateParameter("@UF", cidade.UF.ToUpper());
                    command.Parameters.CreateParameter("@Id_Cidade", cidade.Id_Cidade);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
