using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Data;
using System.Linq;
using Dapper;

namespace ImmutableObjectLab.TypeHandlers
{
    public class ReadOnlyIntegerCollectionTypeHandler : SqlMapper.TypeHandler<IReadOnlyCollection<int>>
    {
        public override void SetValue(IDbDataParameter parameter, IReadOnlyCollection<int> value)
        {
            parameter.Value = value == null || value.Count == 0 ? (object)DBNull.Value : string.Join(",", value);
            parameter.DbType = DbType.String;
        }

        public override IReadOnlyCollection<int> Parse(object value)
        {
            return ((string)value).Split(',').Select(x => int.Parse(x)).ToList();
        }
    }
}