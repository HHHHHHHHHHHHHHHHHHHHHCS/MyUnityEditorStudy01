using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public static class CodeTemplate
{
    public static void DesignerWrite(string folder,string className, List<string> bindPath)
    {
        var scriptFile = folder + className + ".Designer.cs";
        using (var sw = File.CreateText(scriptFile))
        {
            sw.WriteLine("using UnityEngine;");
            sw.WriteLine();
            sw.WriteLine($"public partial class {className} : MonoBehaviour");
            sw.WriteLine("{");

            foreach (var item in bindPath)
            {
                var objName = string.IsNullOrWhiteSpace(item)
                    ? CreateComponentCode.ObjectName(className)
                    : item.Replace('/', '_');
                sw.WriteLine($" public Transform {objName};");
            }

            sw.WriteLine("}");
        }
    }

    public static void LogicWrite(string folder, string className)
    {
        var scriptFile = folder + className + ".cs";
        if (!File.Exists(scriptFile))
        {
            using (var sw = File.CreateText(scriptFile))
            {
                sw.WriteLine("using UnityEngine;");
                sw.WriteLine();
                sw.WriteLine($"public partial class {className} : MonoBehaviour");
                sw.WriteLine("{");

                sw.WriteLine("}");
            }
        }
    }
}