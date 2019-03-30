using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;

namespace ImmutableObjectLab.TypeHandlers
{
    public class IntegerListTypeHandler : SqlMapper.TypeHandler<List<int>>
    {
        public override void SetValue(IDbDataParameter parameter, List<int> value)
        {
            parameter.Value = value == null || value.Count == 0 ? (object)DBNull.Value : string.Join(",", value);
            parameter.DbType = DbType.String;
        }

        public override List<int> Parse(object value)
        {
            return ((string)value).Split(',').Select(x => int.Parse(x)).ToList();
        }
    }
}