using SQLite;

namespace StandUpPersonPicker.Domain.Models;

[Table("statistic")]
public class StatisticDao
{
	[PrimaryKey, AutoIncrement, Column("id")]
	public int Id { get; set; }

	[Column("total_number_of_people")]
	public int TotalNumberOfPeople { get; set; }

	[Column("session_date_time")]
	public DateTimeOffset SessionDateTime { get; set; }
}