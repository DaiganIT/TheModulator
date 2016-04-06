using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheModulatorServerCommon
{
    public interface IModuleListener
    {
        void OnModuleLoad();
        void OnModuleUnload();
        void OnModuleCommand(string action);
    }
}
