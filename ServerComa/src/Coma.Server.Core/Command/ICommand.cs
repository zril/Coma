using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Coma.Server.Model.Entity;

namespace Coma.Server.Core.Command
{
    public interface ICommand
    {
        #region methods

        bool CanRun(Player player, string args, out Object param);
        void Run(Player player, Object param);

        #endregion
    }
}
