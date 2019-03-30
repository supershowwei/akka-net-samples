using System.Configuration;
using System.Data.SqlClient;
using Chef.Extensions.Dapper;
using Dapper;
using ImmutableObjectLab.Model.Data;
using ImmutableObjectLab.TypeHandlers;

namespace ImmutableObjectLab
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            TestInJsonNet();
            TestInDapper();
        }

        private static void TestInJsonNet()
        {
            
        }

        private static void TestInDapper()
        {
            SqlMapper.AddTypeHandler(new IntegerListTypeHandler());

            var sql = @"
SELECT
    --m.MemberNo AS Id
   --,
m.NickName AS [Name]
   ,m.Email
   --,m.UserName
   ,'1,2,3' AS Identifiers
FROM Member m WITH (NOLOCK)
WHERE m.MemberNo = 356";

            using (var db = new SqlConnection(ConfigurationManager.ConnectionStrings["WantGoo"].ConnectionString))
            {
                var a = db.QuerySingle<Member>(sql);
                var b = db.ImmutableQuerySingle<Member>(sql);
            }
        }
    }
}