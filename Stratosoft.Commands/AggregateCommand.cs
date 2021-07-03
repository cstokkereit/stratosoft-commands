using System.Collections.Generic;

namespace Stratosoft.Commands
{
    /// <summary>
    /// Holds a collection of commands that will be executed sequentially when this command is executed.
    /// </summary>
    public class AggregateCommand : ICommand
    {
        protected readonly List<ICommand> commands = new List<ICommand>(); // A list containing the commands to be executed.

        /// <summary>
        /// Initialises a new instance of the <see cref="AggregateCommand"/> class.
        /// </summary>
        /// <param name="commands">A collection containing the commands to be executed.</param>
        public AggregateCommand(IEnumerable<ICommand>commands)
        {
            if (commands != null) this.commands.AddRange(commands);
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="AggregateCommand"/> class.
        /// </summary>
        /// <param name="commands">The commands to be executed.</param>
        public AggregateCommand(params ICommand[] commands)
        {
            if (commands != null) this.commands.AddRange(commands);
        }

        /// <summary>
        /// Executes the commands sequentially.
        /// </summary>
        public virtual void Execute()
        {
            foreach (var command in commands)
            {
                command.Execute();
            }
        }
    }
}
