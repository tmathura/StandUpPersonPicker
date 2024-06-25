using System.Linq.Expressions;
using StandUpPersonPicker.Domain.Models;
using StandUpPersonPicker.Infrastructure.Dals.Interfaces;
using StandUpPersonPicker.Infrastructure.Wrappers.Interfaces;

namespace StandUpPersonPicker.Infrastructure.Dals.Implementations;

/// <summary>
/// Represents the implementation of the data access layer for the Statistic entity.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="StatisticDal"/> class.
/// </remarks>
/// <param name="database">The database wrapper.</param>
public class StatisticDal(ISQLiteWrapper<StatisticDao> database) : IStatisticDal
{
    /// <inheritdoc />
    public async Task<int> Create(StatisticDao group)
    {
        await database.Create(group);

        return group.Id;
    }

    /// <inheritdoc />
    public async Task<List<StatisticDao>> Read(Expression<Func<StatisticDao, bool>>? expression = null)
    {
        var records = await database.Read(expression);

        return records;
    }
}
