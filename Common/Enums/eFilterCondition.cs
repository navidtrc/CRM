using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Enums
{
    public enum eFilterCondition
    {
        Contains,
        Equals,
        StartsWith,
        EndsWith,
        NotEquals,
        GreaterThan,
        GreaterThanOrEqual,
        LessThan,
        LessThanOrEqual,
    }
    public enum eFilterValueType
    {
        String,
        Number,
        DateTime
    }
    public enum eSortType
    {
        Asc,
        Desc,
    }
}
