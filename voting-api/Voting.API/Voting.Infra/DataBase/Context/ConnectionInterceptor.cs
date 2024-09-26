using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Voting.API.Voting.Infra.DataBase.Context
{
    public class ConnectionInterceptor : DbConnectionInterceptor
    {
        //public void ConnectionOpened(DbConnection connection, DbConnectionInterceptionContext interceptionContext)
        //{
        //    Console.WriteLine($"Conexão aberta: {connection.ConnectionString}");
        //}

        //public void ConnectionClosed(DbConnection connection, DbConnectionInterceptionContext interceptionContext)
        //{
        //    Console.WriteLine($"Conexão fechada: {connection.ConnectionString}");
        //}

        //public void ConnectionCreating(DbConnection connection, DbConnectionInterceptionContext interceptionContext)
        //{
        //    Console.WriteLine($"Criando Conexão: {connection.ConnectionString}");
        //}
    }

}
