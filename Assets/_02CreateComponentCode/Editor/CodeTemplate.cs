using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

public static class CodeTemplate
{
    public static int indentLevel = 0;
    public static StringBuilder sb;


    public static void WriteLine(StreamWriter sw, string data = null, params object[] obj)
    {
        if (indentLevel == 0)
        {
            sw.WriteLine(data);
            return;
        }

        if (sb == null)
        {
            sb = new StringBuilder();
        }

        sb.Clear();


        for (int i = 0; i < indentLevel; i++)
        {
            sb.Append('\t');
        }

        if (data == null)
        {
            sw.WriteLine("");
        }

        if (obj == null || obj.Length == 0)
        {
            sb.Append(data);
        }
        else
        {
            sb.AppendFormat(data, obj);
        }

        sw.WriteLine(sb.ToString());
    }


    public static void DesignerWrite(string folder, string className, List<string> bindPath)
    {
        var scriptFile = folder + className + ".Designer.cs";
        using (var sw = File.CreateText(scriptFile))
        {
            WriteLine(sw, "using UnityEngine;");
            WriteLine(sw);

            if (CreateComponentCode.IsDefaultNamespace)
            {
                WriteLine(sw, "//请在菜单 EditorMenu/Namespace setting 里面设置命名空间");
                WriteLine(sw, "//修改命名空间后需要手动修改逻辑代码(非Designer.cs)的Namespace");
            }

            WriteLine(sw, $"namespace {CreateComponentCode.CodeNamespace}");
            WriteLine(sw, "{");

            indentLevel++;

            WriteLine(sw, $"public partial class {className} : MonoBehaviour");
            WriteLine(sw, "{");

            indentLevel++;

            foreach (var item in bindPath)
            {
                var objName = string.IsNullOrWhiteSpace(item)
                    ? CreateComponentCode.ObjectName(className)
                    : item.Replace('/', '_');
                WriteLine(sw, $"public Transform {objName};");
            }

            indentLevel--;

            WriteLine(sw, "}");

            indentLevel--;

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
                WriteLine(sw, "using UnityEngine;");
                WriteLine(sw);

                if (CreateComponentCode.IsDefaultNamespace)
                {
                    WriteLine(sw, "//请在菜单 EditorMenu/Namespace setting 里面设置命名空间");
                    WriteLine(sw, "//修改命名空间后需要手动修改逻辑代码(非Designer.cs)的Namespace");
                }

                WriteLine(sw, $"namespace {CreateComponentCode.CodeNamespace}");
                WriteLine(sw, "{");

                indentLevel++;

                WriteLine(sw, $"public partial class {className} : MonoBehaviour");
                WriteLine(sw, "{");
                WriteLine(sw, "}");

                indentLevel--;

                WriteLine(sw, "}");
            }
        }
    }
}