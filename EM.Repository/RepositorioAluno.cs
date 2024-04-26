using EM.Domain;
using EM.Repository.banco;
using EM.Repository.Utilitarios;
using FirebirdSql.Data.FirebirdClient;
using System.Data.Common;
using System.Linq.Expressions;

namespace EM.Repository
{
    public class RepositorioAluno : IRepositorioAluno<Aluno>
    {
        public void Add(Aluno aluno)
        {
            using (DbConnection connection = new FbConnection(ConnectionBanc.GetConnectionString()))
            {
                connection.Open();
                using (DbCommand command = connection.CreateCommand())
                {
                    command.CommandText = "INSERT INTO Alunos (matricula, nome, CPF, nascimento, sexo, cidade) " +
                               "VALUES (@Matricula, @Nome, @CPF, @Nascimento, @Sexo, @Cidade)";

                    command.Parameters.CreateParameter("@Matricula", aluno.Matricula);
                    command.Parameters.CreateParameter("@Nome", aluno.Nome.ToUpper());
                    command.Parameters.CreateParameter("@CPF", aluno.CPF.ToUpper());
                    command.Parameters.CreateParameter("@Nascimento", aluno.Nascimento);
                    command.Parameters.CreateParameter("@Sexo", aluno.Sexo);
                    command.Parameters.CreateParameter("@Cidade", aluno.Cidade.Nome);
                    command.Parameters.CreateParameter("@Cidade", aluno.Cidade.UF);
                    
                    command.ExecuteNonQuery();
                }
            }
        }

        public IEnumerable<Aluno> Get(Expression<Func<Aluno, bool>> predicate)
        {
            List<Aluno> alunos = new List<Aluno>();

            using (DbConnection connection = new FbConnection(ConnectionBanc.GetConnectionString()))
            {
                connection.Open();
                using (DbCommand command = connection.CreateCommand())
                {
                    // Construir a consulta SQL com base no predicado
                    string sql = "SELECT * FROM Alunos WHERE ";
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
                            Aluno aluno= new()
                            {
                                Matricula = Convert.ToInt32(reader["Matricula"]),
                                Nome = reader["Nome"].ToString(),
                                CPF = reader["CPF"].ToString(),
                                Nascimento = Convert.ToDateTime(reader["Nascimento"]),
                                Sexo = Convert.ToInt32(reader["Sexo"])

                            };

                            alunos.Add(aluno);
                        }
                    }
                }
            }

            return alunos;
        }

        public IEnumerable<Aluno> GetAll()
        {

            List<Aluno> alunos = new List<Aluno>();

            using (DbConnection connection = new FbConnection(ConnectionBanc.GetConnectionString()))
            {
                connection.Open();
                using (DbCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM Alunos";

                    using (DbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Aluno aluno = new()
                            {
                                Matricula = Convert.ToInt32(reader["Matricula"]),
                                Nome = reader["Nome"].ToString(),
                                Sexo = Convert.ToInt32(reader["Sexo"]),
                                
                                Nascimento = Convert.ToDateTime(reader["Nascimento"]),
                                CPF = reader["CPF"].ToString()
                            };

                            alunos.Add(aluno);
                        }
                    }
                }
                return alunos;
            }
        }

        public void Remove(Aluno aluno)
        {
            using (DbConnection connection = new FbConnection(ConnectionBanc.GetConnectionString()))
            {
                connection.Open();
                using (DbCommand command = connection.CreateCommand())
                {
                    command.CommandText = "DELETE FROM Alunos WHERE Id_Aluno = @Id_Aluno";
                    command.Parameters.CreateParameter("@Id_Aluno", aluno.Id_Alunos);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Update(Aluno aluno)
        {
            using (DbConnection connection = new FbConnection(ConnectionBanc.GetConnectionString()))
            {
                using (DbCommand command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = "INSERT INTO Alunos (matricula, nome, CPF, nascimento, sexo, cidade) " +
                               "VALUES (@Matricula, @Nome, @CPF, @Nascimento, @Sexo, @Cidade)";

                    command.Parameters.CreateParameter("@Matricula", aluno.Matricula);
                    command.Parameters.CreateParameter("@Nome", aluno.Nome.ToUpper());
                    command.Parameters.CreateParameter("@CPF", aluno.CPF.ToUpper());
                    command.Parameters.CreateParameter("@Nascimento", aluno.Nascimento);
                    command.Parameters.CreateParameter("@Sexo", aluno.Sexo);
                    command.Parameters.CreateParameter("@Cidade", aluno.Cidade.Nome);
                    command.Parameters.CreateParameter("@Cidade", aluno.Cidade.UF);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
