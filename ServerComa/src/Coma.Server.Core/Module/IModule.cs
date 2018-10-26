using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coma.Server.Core.Module
{
    public interface IModule
    {
        long Interval { get; }
        DateTime LastUpdate { get; }

        void Update(TimeSpan elapsed);

        void Start();

        void Stop();
    }
}
