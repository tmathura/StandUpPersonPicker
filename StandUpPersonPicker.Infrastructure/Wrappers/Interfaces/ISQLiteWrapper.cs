using System.Linq.Expressions;

namespace StandUpPersonPicker.Infrastructure.Wrappers.Interfaces;

/// <summary>
/// Represents a wrapper interface for SQLite operations.
/// </summary>
/// <typeparam name="T">The type of the record.</typeparam>
public interface ISQLiteWrapper<T> where T : new()
{
    /// <summary>
    /// Creates a new record in the SQLite database.
    /// </summary>
    /// <param name="record">The record to create.</param>
    /// <returns>The created record.</returns>
    Task<T> Create(T record);
    
    /// <summary>
    /// Reads records from the SQLite database based on the specified expression.
    /// </summary>
    /// <param name="expression">The expression to filter the records.</param>
    /// <returns>The list of read records.</returns>
    Task<List<T>> Read(Expression<Func<T, bool>>? expression = null);
}
