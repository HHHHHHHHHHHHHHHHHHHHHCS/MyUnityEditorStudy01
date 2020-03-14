using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class CodeTemplate
{
    public static void Write(StreamWriter sw, string name)
    {
        sw.WriteLine("using UnityEngine;");
        sw.WriteLine();
        sw.WriteLine($"public class {name} : MonoBehaviour");
        sw.WriteLine("{");
        sw.WriteLine();
        sw.WriteLine("}");
    }
}