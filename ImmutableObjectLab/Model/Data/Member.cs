using System.Collections.Generic;

namespace ImmutableObjectLab.Model.Data
{
    public sealed class Member
    {
        public Member(int id, string name, string email, List<int> identifiers)
        {
            this.Id = id;
            this.Name = name;
            this.Email = email;
            this.Identifiers = identifiers;
        }

        public Member()
        {
        }

        public int Id { get; }

        public string Name { get; }

        public string Email { get; }

        public List<int> Identifiers { get; }
    }
}