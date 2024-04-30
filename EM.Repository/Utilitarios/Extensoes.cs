using FirebirdSql.Data.FirebirdClient;

using System.Data.Common;


namespace EM.Repository.Utilitarios
{
    public static class Extensoes
    {
        public static void CreateParameter(this DbParameterCollection dbParameter, string parameterName, object value) => 
            dbParameter.Add(new FbParameter(parameterName, value));

        public static string FormatarNumeroMatricula(this int numero) => $"{new Random().Next(10, 1000):D3}2024";
    }
}
