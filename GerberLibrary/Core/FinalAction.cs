using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerberLibrary.Core
{

    internal sealed class FinalAction : IDisposable
    {
        private readonly Action finalAction;

        public FinalAction(Action finalAction)
        {
            this.finalAction = finalAction ?? throw new ArgumentNullException(nameof(finalAction));
        }

        public void Dispose()
        {
            this.finalAction();
        }
    }
}
