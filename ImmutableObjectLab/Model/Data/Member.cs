using System.Collections.Generic;
using Newtonsoft.Json;

namespace ImmutableObjectLab.Model.Data
{
    public sealed class Member
    {
        [JsonConstructor]
        public Member(int id, string name, string email, IReadOnlyCollection<int> identifiers)
        {
            this.Id = id;
            this.Name = name;
            this.Email = email;
            this.Identifiers = identifiers;
        }

        public Member()
        {
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public IReadOnlyCollection<int> Identifiers { get; set; }
    }
}