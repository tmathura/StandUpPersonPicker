using System.Linq.Expressions;
using StandUpPersonPicker.Domain.Models;

namespace StandUpPersonPicker.Infrastructure.Dals.Interfaces;

/// <summary>
/// Represents the data access layer interface for the Statistic entity.
/// </summary>
public interface IStatisticDal
{
	/// <summary>
	/// Creates a new statistic.
	/// </summary>
	/// <param name="statistic">The statistic to create.</param>
	/// <returns>The ID of the created statistic.</returns>
	Task<int> Create(StatisticDao statistic);

	/// <summary>
	/// Reads a list of statistics based on the specified expression.
	/// </summary>
	/// <param name="expression">The expression to filter the statistics.</param>
	/// <returns>A list of statistics.</returns>
	Task<List<StatisticDao>> Read(Expression<Func<StatisticDao, bool>>? expression = null);
}
