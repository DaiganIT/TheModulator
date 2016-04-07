using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TheModulatorServerCommon.Interfaces;

namespace TheModulatorServerCommon.Services
{
    public class ModuleDetectorService : IModuleDetector
    {
        public List<Module> GetModulesList()
        {
            var modules = (from domainAssembly in AppDomain.CurrentDomain.GetAssemblies()
                            from assemblyType in domainAssembly.GetTypes().Where(myType => myType.IsClass && !myType.IsAbstract && myType.Name != "SimpleModule")
                            where typeof(Module).IsAssignableFrom(assemblyType)
                            select assemblyType).ToArray();

            return modules.Select(module => new SimpleModule
            {
                ModuleDescription = "", ModuleName = module.FullName, ModuleStatus = "Disabled", ModuleVendor = "Pietro"
            }).Cast<Module>().ToList();
        }
    }
}
