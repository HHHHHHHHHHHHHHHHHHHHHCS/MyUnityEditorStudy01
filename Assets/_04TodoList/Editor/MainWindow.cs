using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MainWindow : EditorWindow
{
    private bool isShow;

    [MenuItem("TodoList/MainWindow %#t")]
    public static void Open()
    {
        var window = GetWindow<MainWindow>();
        if (window.isShow)
        {
            window.Show();
            window.isShow = true;
        }
        else
        {
            window.Close();
            window.isShow = false;
        }
    }
}