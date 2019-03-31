using System.Collections.Generic;
using System.Collections.Immutable;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using AutoMapper;
using Chef.Extensions.Assembly;
using Chef.Extensions.Dapper;
using Dapper;
using ImmutableObjectLab.Extensions;
using ImmutableObjectLab.Model.Data;
using ImmutableObjectLab.TypeHandlers;
using LiteDB;
using Newtonsoft.Json;

namespace ImmutableObjectLab
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            TestInLiteDB();
            TestInAutoMapper();
            TestInJsonNet();
            TestInDapper();
        }

        private static void TestInLiteDB()
        {
            var dbFile = Path.Combine(Assembly.GetExecutingAssembly().GetCurrentDirectory(), "test.db");

            using (var db = new LiteDatabase(dbFile))
            {
                var members = db.GetCollection("member");

                members.Upsert(
                    new Member(356, "玩股小俏妞", "service@wantgoo.com", new List<int> { 1, 2, 3 }).ToBsonDocument());
            }

            Member member;
            using (var db = new LiteDatabase(dbFile))
            {
                var members = db.GetCollection("member");

                member = members.FindById(356).ToImmutability<Member>();
                //member = members.FindById(356);
            }
        }

        private static void TestInAutoMapper()
        {
            Mapper.Initialize(
                cfg => cfg.CreateMap<User, Member>().ConstructUsing(u => new Member(u.Id, u.Name, u.Email, null)));

            var user = new User { Id = 356, Name = "玩股小俏妞", Email = "service@wantgoo.com" };

            var member = Mapper.Map<Member>(user);
        }

        private static void TestInJsonNet()
        {
            var json = "{\"Email\": \"service@wantgoo.com\",\"Name\": \"玩股小俏妞\",\"Identifiers\": [1,2,3]}";

            var obj = JsonConvert.DeserializeObject<Member>(json);
        }

        private static void TestInDapper()
        {
            SqlMapper.AddTypeHandler(new ReadOnlyIntegerCollectionTypeHandler());

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