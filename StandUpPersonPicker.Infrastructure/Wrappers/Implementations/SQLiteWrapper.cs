using System.Linq.Expressions;
using log4net;
using Microsoft.Extensions.Configuration;
using SQLite;
using StandUpPersonPicker.Infrastructure.Wrappers.Interfaces;

namespace StandUpPersonPicker.Infrastructure.Wrappers.Implementations;

/// <summary>
/// Wrapper class for SQLite database operations.
/// </summary>
/// <typeparam name="T">The type of the table.</typeparam>
public class SQLiteWrapper<T> : ISQLiteWrapper<T> where T : new()
{
    private readonly SQLiteAsyncConnection _database;
    private readonly ILog _logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod()?.DeclaringType);

    /// <summary>
    /// Initializes a new instance of the <see cref="SQLiteWrapper{T}"/> class.
    /// </summary>
    /// <param name="configuration">The configuration object.</param>
    public SQLiteWrapper(IConfiguration configuration)
    {
        var databaseName = configuration["Database:Name"];

        if (string.IsNullOrWhiteSpace(databaseName))
        {
            throw new Exception("Error code: no database name set");
        }

        var databasePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, databaseName);

        try
        {
            _logger.Info($"Connecting to database for table {nameof(T)}.");

            _database = new SQLiteAsyncConnection(databasePath);
            _database.CreateTableAsync<T>().Wait();

            _logger.Info($"Connection established to database for table {nameof(T)}.");
        }
        catch (Exception exception)
        {
            _logger.Error($"{exception.Message} - {exception.StackTrace}");
            throw;
        }
    }

    /// <inheritdoc />
    public async Task<T> Create(T record)
    {
        try
        {
            await _database.InsertAsync(record);

            return record;
        }
        catch (Exception exception)
        {
            _logger.Error($"{exception.Message} - {exception.StackTrace}");
            throw;
        }
    }

    /// <inheritdoc />
    public async Task<List<T>> Read(Expression<Func<T, bool>>? expression = null)
    {
        try
        {
            var tableQuery = _database.Table<T>();

            if (expression != null)
            {
                tableQuery = tableQuery.Where(expression);
            }

            var records = await tableQuery.ToListAsync();

            return records;
        }
        catch (Exception exception)
        {
            _logger.Error($"{exception.Message} - {exception.StackTrace}");
            throw;
        }
    }
}
