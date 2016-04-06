using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheModulatorServerCommon
{
    public abstract class Module : IModuleListener
    {
        public string ModuleName { get; set; }
        public string ModuleDescription { get; set; }
        public string ModuleVendor { get; set; }
        public string ModuleStatus { get; set; }

        public void OnModuleLoad()
        {
            throw new NotImplementedException();
        }

        public void OnModuleUnload()
        {
            throw new NotImplementedException();
        }

        public void OnModuleCommand(string action)
        {
            throw new NotImplementedException();
        }
    }
}
