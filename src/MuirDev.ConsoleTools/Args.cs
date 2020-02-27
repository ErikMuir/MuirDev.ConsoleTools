using System.Collections.Generic;

namespace MuirDev.ConsoleTools
{
    public class Token
    {
        public Token() {}
        public Token(string name) { Name = name; }
        public Token(string name, string alias)
        {
            Name = name;
            Alias = alias;
        }
        public string Name { get; set; }
        public string Alias { get; set; }
    }

    public class Argument : Token
    {
        public Argument() : base() { }
        public Argument(string name) : base(name) { }
        public Argument(string name, string alias) : base(name, alias) { }
        public bool IsRequired { get; set; }
    }

    public class Option : Token
    {
        public Option() : base() { }
        public Option(string name) : base(name) { }
        public Option(string name, string alias) : base(name, alias) { }
    }

    public class Command : Token
    {
        public Command() : base() { }
        public Command(string name) : base(name) { }
        public Command(string name, string alias) : base(name, alias) { }
        public List<Command> Subcommands { get; set; } = new List<Command>();
        public List<Argument> Arguments { get; set; } = new List<Argument>();
        public List<Option> Options { get; set; } = new List<Option>();
    }

    public class Args
    {
        public Args(string[] args)
        {

        }

        public List<Command> Subcommands { get; set; } = new List<Command>();
        public List<Argument> Arguments { get; set; } = new List<Argument>();
        public List<Option> Options { get; set; } = new List<Option>();
    }
}
