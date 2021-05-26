using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Autodesk.AutoCAD.Runtime;
using System.IO;

namespace PlugReload
{
    public class Reload
    {
        private Action load;
        public Reload()
        {
            ReloadPlug();
        }
        [CommandMethod("ReloadPlug")]
        public void ReloadPlug()
        {
            var adapterFileInfo = new FileInfo(Assembly.GetExecutingAssembly().Location);
            var targetFilePath = Path.Combine(adapterFileInfo.DirectoryName, "LogisticPlanPlug.dll");
            var targetAssembly = Assembly.Load(File.ReadAllBytes(targetFilePath));
            var targetType = targetAssembly.GetType("LogisticPlanPlug.Program");
            var targetMethod = targetType.GetMethod("LoadPlug");
            var targetObject = Activator.CreateInstance(targetType);
            load = () => targetMethod.Invoke(targetObject, null);
        }

        [CommandMethod("LoadPlug")]
        public void LoadPlug()
        {

        }
    }
}
