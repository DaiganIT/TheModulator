using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using log4net;
using Newtonsoft.Json.Linq;

[assembly: WebActivatorEx.PostApplicationStartMethod(typeof(TheModulator.App_Start.ModulatorModuleFinder), "Start")]

namespace TheModulator.App_Start
{
    public static class ModulatorModuleFinder
    {

        //basic structure is
        // - /Modules
        // - - /ModuleName
        // - - - ModuleName.info
        // - - - ModuleName.dll
        // - - /Module2
        // - - - Module2.info
        // - - - Module2.dll

        private static string ModulesDirectory
        {
            get
            {
                var dir = ConfigurationManager.AppSettings["ModulesDirectory"];
                if (String.IsNullOrEmpty(dir))
                {
                    return "\\Modules";
                }

                return dir;
            }
        }

        public static void Start()
        {
            if (!Directory.Exists(ModulesDirectory))
            {
                Directory.CreateDirectory(ModulesDirectory);
            }

            var logger = LogManager.GetLogger("TheModulatorLogger");
            logger.Info("Loading modules");
            var modulesDirectories = Directory.GetDirectories(ModulesDirectory);
            logger.Info(string.Format("Found {0} modules", modulesDirectories.Length));
            foreach (var dir in modulesDirectories)
            {
                var moduleName = dir.Replace(ModulesDirectory + "\\", "");
                var infoFilePath = dir + "\\" + moduleName + ".info";
                //search for the info file
                if (!File.Exists(infoFilePath))
                {
                    logger.Warn("Error loading info file for module: " + moduleName);
                    continue;
                }

                //open and parse the file
                try
                {
                    var infoFile = JObject.Parse(File.ReadAllText(infoFilePath));
                }
                catch (Exception e)
                {
                    //parsing of the file...
                    logger.Warn("Error parsing info file for module: " + moduleName + "\n" + e);
                }

                var dllFilePath = dir + "\\" + moduleName + ".dll";
                if (!File.Exists(dllFilePath))
                {
                    logger.Warn("Dll file not found for module: " + moduleName);
                    continue;
                }

                try
                {
                    Assembly.LoadFile(dllFilePath);
                }
                catch (Exception e)
                {
                    logger.Warn("Error while loading the module dll: " + moduleName + "\n" + e);
                }

                logger.Info("Successfully loaded module: " + moduleName);
            }
        }

    }
}