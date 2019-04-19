using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using AutoMapper;
using Chef.Extensions.Assembly;
using Chef.Extensions.Dapper;
using Chef.Extensions.LiteDB;
using Dapper;
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
            //TestInLiteDB();
            //TestInAutoMapper();
            TestInJsonNet();
            //TestInDapper();
        }

        private static void TestInLiteDB()
        {
            var dbFile = Path.Combine(Assembly.GetExecutingAssembly().GetCurrentDirectory(), "test.db");

            using (var db = new LiteDatabase(dbFile))
            {
                var collection = db.GetCollection<Member>("member");

                collection.Upsert(
                    new Member(
                        356,
                        "玩股小俏妞",
                        "service@wantgoo.com",
                        new List<int> { 1, 2, 3 },
                        new Dictionary<int, string> { [1] = "111", [2] = "222", [3] = "333" }));
            }

            using (var db = new LiteDatabase(dbFile))
            {
                var readWriteCollection = db.GetCollection<MemberReadWrite>("member");

                for (int i = 0; i < 10; i++)
                {
                    var stopwatch = Stopwatch.StartNew();

                    for (int j = 0; j < 10000; j++)
                    {
                        //readWriteCollection.Find(x => x.Id.Equals(356)).ToList();
                        readWriteCollection.FindById(356);
                    }

                    stopwatch.Stop();
                    Console.WriteLine(stopwatch.ElapsedMilliseconds);
                }

                Console.WriteLine();
                Console.WriteLine();

                var collection = db.GetCollection<Member>("member");

                for (int i = 0; i < 10; i++)
                {
                    var stopwatch = Stopwatch.StartNew();

                    for (int j = 0; j < 10000; j++)
                    {
                        //collection.FindAsImmutability(x => x.Id.Equals(356)).ToList();
                        collection.FindAsImmutabilityById(356);
                    }

                    stopwatch.Stop();
                    Console.WriteLine(stopwatch.ElapsedMilliseconds);
                }
            }
        }

        private static void TestInAutoMapper()
        {
            Mapper.Initialize(
                cfg => cfg.CreateMap<User, Member>()
                    .ConstructUsing(u => new Member(u.Id, u.Name, u.Email, null, null)));

            var user = new User { Id = 356, Name = "玩股小俏妞", Email = "service@wantgoo.com" };

            var member = Mapper.Map<Member>(user);
        }

        private static void TestInJsonNet()
        {
            var json = "{\"Email\": \"service@wantgoo.com\",\"Name\": \"玩股小俏妞\",\"Identifiers\": [1,2,3]}";

            // 沒有 [JsonConstructor] 的話，預設使用第一個。
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
                var b = db.ImmutableQuerySingle<Member>(sql);
            }
        }
    }
}