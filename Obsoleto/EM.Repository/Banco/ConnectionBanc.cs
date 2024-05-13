using FirebirdSql.Data.FirebirdClient;

namespace EM.Repository.banco
{
    public class ConnectionBanc
    {
        static string _onnectionString =
            @"Server=localhost; Port=3054;Database=C:\Workspace Weslei\ProjetoAluno\BancoDeDados\repositoriodealunos.fdb;User=SYSDBA;Password=masterkey;";
        private static FbConnection? connection = null;

        public static FbConnection GetConnectionString()
        {
            if (connection == null || connection.State != System.Data.ConnectionState.Open)
            {
                FbConnection.ClearAllPools();
                connection = new FbConnection(_onnectionString);
                connection.Open();
            }

            return connection;
        }
    }
}

