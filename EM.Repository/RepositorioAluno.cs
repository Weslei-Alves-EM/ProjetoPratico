using EM.Domain;
using EM.Domain.Utilitarios;
using EM.Domain.Enuns;
using EM.Repository.banco;
using FirebirdSql.Data.FirebirdClient;
using System.Data.Common;
using System.Linq.Expressions;

namespace EM.Repository
{
    public class RepositorioAluno : IRepositorioGeral<Aluno>, IRepositorioAluno<Aluno>
    {
        public void Add(Aluno aluno)
        {            
            using (DbConnection connection = new FbConnection(ConnectionBanc.GetConnectionString()))
            {
                connection.Open();
                using (DbCommand command = connection.CreateCommand())
                {
                    command.CommandText = "INSERT INTO Alunos (matricula, nome, CPF, nascimento, sexo, id_cidade) " +
                            "VALUES (@Matricula, @Nome, @CPF, @Nascimento, @Sexo, @id_Cidade)";
                    
                    command.Parameters.CreateParameter("@Matricula", Extensoes.FormatarNumeroMatricula(aluno.Matricula));
                    command.Parameters.CreateParameter("@Nome", aluno.Nome.ToUpper());
                    command.Parameters.CreateParameter("@CPF", aluno.CPF.ToUpper());
                    command.Parameters.CreateParameter("@Nascimento", aluno.Nascimento);
                    command.Parameters.CreateParameter("@Sexo", aluno.Sexo);
                    command.Parameters.CreateParameter("@id_Cidade", aluno.Cidade.Id_cidade);
                    command.ExecuteNonQuery();
                }
            }
        }

        

        public IEnumerable<Aluno> GetAll()
        {
            List<Aluno> alunos = [];

            using (DbConnection connection = new FbConnection(ConnectionBanc.GetConnectionString()))
            {
                connection.Open();
                using (DbCommand command = connection.CreateCommand())
                {
                    command.CommandText = @"SELECT A.Id_Alunos, A.Matricula, A.Nome, A.Sexo, A.Nascimento, A.CPF, C.UF
                                    FROM Alunos A
                                    INNER JOIN Cidades C ON A.Id_cidade = C.Id_cidade";

                    using (DbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Aluno aluno = new Aluno
                            {
                                Id_Alunos = Convert.ToInt32(reader["Id_Alunos"]),
                                Matricula = Convert.ToInt32(reader["Matricula"]),
                                Nome = reader["Nome"].ToString(),
                                Sexo = (EnumeradorSexo)reader.GetInt32(reader.GetOrdinal("Sexo")),
                                Nascimento = Convert.ToDateTime(reader["Nascimento"]),
                                CPF = reader["CPF"].ToString(),
                                Cidade = new Cidade { UF = reader["UF"].ToString() }
                            };

                            alunos.Add(aluno);
                        }
                    }
                }
            }

            return alunos;
        }


        public void Remove(Aluno aluno)
        {
            using (DbConnection connection = new FbConnection(ConnectionBanc.GetConnectionString()))
            {
                connection.Open();
                using (DbCommand command = connection.CreateCommand())
                {
                    command.CommandText = "DELETE FROM Alunos WHERE Id_Alunos = @Id_Alunos";
                    command.Parameters.CreateParameter("@Id_Alunos", aluno.Id_Alunos);
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
                    command.CommandText = "UPDATE Alunos SET  Nome = @Nome, CPF = @CPF, Nascimento = @Nascimento," +
                        "Sexo = @Sexo, Id_Cidade = @Id_Cidade WHERE Id_Alunos = @Id_Alunos";

                    // command.Parameters.CreateParameter("@Matricula", Extensoes.FormatarNumeroMatricula(aluno.Matricula)) Matricula = @Matricula,;
                    command.Parameters.CreateParameter("@Nome", aluno.Nome.ToUpper());
                    command.Parameters.CreateParameter("@CPF", aluno.CPF.ToUpper());
                    command.Parameters.CreateParameter("@Nascimento", aluno.Nascimento);
                    command.Parameters.CreateParameter("@Sexo", aluno.Sexo);
                    command.Parameters.CreateParameter("@Id_Cidade", aluno.Cidade.Id_cidade);
                    command.Parameters.CreateParameter("@Id_Alunos", aluno.Id_Alunos);
                    command.ExecuteNonQuery();
                }
            }
        }
        public IEnumerable<Aluno> Get(Expression<Func<Aluno, bool>> predicate) => GetAll().Where(predicate.Compile());

        public Aluno GetByMatricula(int matricula) => GetAll().First(mt => mt.Matricula == matricula);
        
        public IEnumerable<Aluno> GetByContendoNoNome(string parteDoNome) => GetAll().Where(a => a.Nome.IndexOf(parteDoNome, StringComparison.OrdinalIgnoreCase) >= 0);

        public IEnumerable<Aluno> GetByEstado(string uf) => GetAll().Where(a => a.Cidade != null && a.Cidade.UF != null && a.Cidade.UF.Equals(uf, StringComparison.OrdinalIgnoreCase));
    }
}
