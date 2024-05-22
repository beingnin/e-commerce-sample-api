using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Sinks.MSSqlServer;
using System.Collections.ObjectModel;
using System.Data;

namespace ECommerce.Logger
{
    public class DbLogger:Infra.Abstracts.ICustomLogger
    {
        public Guid Error(Exception exception)
        {
            var identifier = Guid.NewGuid();
            Log.Error(exception, "{Identifier} : {Message}", identifier, exception.Message);
            return identifier;
        }

        public Guid Info(string message)
        {
            throw new NotImplementedException();
        }

        public void Initialize(string connectionString)
        {
            var columnOptions = new ColumnOptions
            {
                AdditionalColumns = new Collection<SqlColumn>
               {
                   new SqlColumn("UserId", SqlDbType.Int),
                   new SqlColumn("Identifier", SqlDbType.UniqueIdentifier),
               }
            };

            Log.Logger = new LoggerConfiguration()
               .Enrich.FromLogContext()
               .Enrich.WithExceptionDetails()
               .WriteTo.MSSqlServer(connectionString, sinkOptions: new MSSqlServerSinkOptions { TableName = "Logs" }
               , null, null, LogEventLevel.Information, null, columnOptions: columnOptions, null, null)
               .CreateLogger();
        }
    }
}
