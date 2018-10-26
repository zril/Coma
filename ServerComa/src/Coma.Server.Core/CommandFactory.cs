using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Coma.Common;
using Coma.Server.Core.Command;
using UnconstrainedMelody;

namespace Coma.Server.Core
{
    public static class CommandFactory
    {
        static CommandFactory()
        {
            COMMANDS = new Dictionary<string, ICommand>();

            var commands = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.Namespace == "Coma.Server.Core.Command" && t.Name != "CommandFactory").ToList();

            foreach (CommandType command in Enum.GetValues(typeof(CommandType)))
            {
                var description = Enums.GetDescription(command);

                var commandType = commands.Single(t => (t.GetCustomAttributes(typeof(DescriptionAttribute)) as DescriptionAttribute[])[0].Description == description);

                COMMANDS.Add(description, Activator.CreateInstance(commandType) as ICommand);
            }
        }

        private static Dictionary<string, ICommand> COMMANDS;

        public static ICommand GetCommand(string cmd)
        {
            return COMMANDS[cmd];
        }
    }
}
