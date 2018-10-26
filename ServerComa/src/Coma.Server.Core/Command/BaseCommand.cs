﻿using Coma.Common.Parameter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Coma.Server.Model.Entity;
using Coma.Server.Core.Command;

namespace Coma.Server.Core.Command
{
    abstract class BaseCommand<T> : ICommand
        where T : BaseParam, new()
    {
        public virtual bool CanRun(Player player, string args, out object param)
        {
            var parameter = new T();
            parameter.DeserializeArguments(args);

            if (parameter.IsValid)
            {
                param = parameter;
                return true;
            }

            param = null;
            return false;
        }

        public void Run(Player player, object param)
        {
            this.RunWithCast(player, param as T);
        }

        public abstract void RunWithCast(Player player, T param);
    }
}
