using System.Data;
using Dapper;

namespace Clean.Infrastructure.Data;

public class DateOnlyHandler : SqlMapper.TypeHandler<DateOnly>
{
    public override void SetValue(IDbDataParameter parameter, DateOnly value)
    {
        // Store as DateTime so Postgres understands it
        parameter.Value = value.ToDateTime(TimeOnly.MinValue);
    }

    public override DateOnly Parse(object value)
    {
        return value switch
        {
            DateTime dt => DateOnly.FromDateTime(dt),
            string s => DateOnly.Parse(s),
            _ => throw new DataException("Cannot convert " + value.GetType() + " to DateOnly")
        };
    }
}
