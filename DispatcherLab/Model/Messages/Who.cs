namespace DispatcherLab.Model.Messages
{
    public sealed class Who
    {
        public Who(string name)
        {
            this.Name = name;
        }

        public string Name { get; }
    }
}