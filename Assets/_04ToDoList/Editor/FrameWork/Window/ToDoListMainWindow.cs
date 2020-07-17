using System;
using System.Collections.Generic;
using _04ToDoList.Editor.FrameWork.DataBinding;
using _04ToDoList.Editor.FrameWork.Drawer;
using _04ToDoList.Editor.FrameWork.Drawer.Interface;
using _04ToDoList.Editor.FrameWork.Layout;
using _04ToDoList.Editor.FrameWork.ViewController;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace _04ToDoList.Editor.FrameWork.Window
{
    public class ToDoListMainWindow : AbsWindow
    {
        public static ToDoListMainWindow instance;

        public SubWindow subWindow;

        private ToDoListController toDoListController;


        [MenuItem("ToDoList/MainWindow %#t")]
        public static ToDoListMainWindow Open()
        {
            instance = Open<ToDoListMainWindow>(true, "ToDoLists", true);
            return instance;
        }

        //一定确保instance是存在的
        public SubWindow CreateSubWindow(string name = "SubWindow")
        {
            if (subWindow == null)
            {
                subWindow = SubWindow.Open(name);
            }
            else
            {
                subWindow.name = name;
            }
            return subWindow;
        }

        protected override void OnInit()
        {
            toDoListController = CreateViewController<ToDoListController>();
        }

        protected override void Disable()
        {
            toDoListController?.OnDisable();
        }

        protected override void Dispose()
        {
            if (subWindow)
            {
                subWindow.Close();
                subWindow = null;
            }
            instance = null;
        }

        protected override void OnGUI()
        {
            toDoListController.Draw();
        }
    }
}