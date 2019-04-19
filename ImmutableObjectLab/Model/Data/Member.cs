using System.Collections.Generic;

namespace ImmutableObjectLab.Model.Data
{
    public sealed class Member
    {
        public Member(
            int id,
            string name,
            string email,
            IReadOnlyCollection<int> identifiers,
            IReadOnlyDictionary<int, string> goods)
        {
            this.Id = id;
            this.Name = name;
            this.Email = email;
            this.Identifiers = identifiers;
            this.Goods = goods;
        }

        public int Id { get; }

        public string Name { get; }

        public string Email { get; }

        public IReadOnlyCollection<int> Identifiers { get; }

        public IReadOnlyDictionary<int, string> Goods { get; }
    }

    public class MemberReadWrite
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public List<int> Identifiers { get; set; }

        public Dictionary<int, string> Goods { get; set; }
    }
}