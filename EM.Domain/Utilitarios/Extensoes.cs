using FirebirdSql.Data.FirebirdClient;

using System.Data.Common;
using System.Reflection.Metadata;
using System.Runtime.Intrinsics.X86;


namespace EM.Domain.Utilitarios
{
    public static class Extensoes
    {
        /*
            public static void CreateParameter(this DbParameterCollection dbParameter, string parameterName, object value): Esta linha define a assinatura da extensão de método.O método é chamado CreateParameter,
            e ele estende o tipo DbParameterCollection.Ele recebe dois parâmetros além do this: parameterName: Uma string que representa o nome do parâmetro.
            value: O valor a ser associado ao parâmetro.
            =>: Este é um operador de expressão lambda que introduz o corpo do método.
            dbParameter.Add(new FbParameter(parameterName, value)): Este é o corpo do método.Ele adiciona um novo parâmetro à coleção dbParameter.O parâmetro é criado usando a classe FbParameter,
            que é uma classe específica para parâmetros em bancos de dados Firebird (Fb).
            FbParameter(parameterName, value): Aqui, estamos criando um novo objeto FbParameter com o parameterName e value fornecidos.
            dbParameter.Add(...): Este método adiciona o parâmetro recém-criado à coleção dbParameter.
        */

        public static void CreateParameter(this DbParameterCollection dbParameter, string parameterName, object value) =>
            dbParameter.Add(new FbParameter(parameterName, value));

        public static string FormatarNumeroMatricula(this int numero) => $"{new Random().Next(10, 1000):D3}" + DateTime.Now.ToString("yyyy");
    }
}
