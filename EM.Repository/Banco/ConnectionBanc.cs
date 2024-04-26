using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.Repository.banco
{
    public class ConnectionBanc
    {
        private const string ConnectionString = 
            @"Server=localhost; Port=3054;Database=C:\Workspace Weslei\ProjetoAluno\BancoDeDados\repositoriodealunos.fdb;User=SYSDBA;Password=masterkey;";

        public static string GetConnectionString() => ConnectionString;
    }
}
