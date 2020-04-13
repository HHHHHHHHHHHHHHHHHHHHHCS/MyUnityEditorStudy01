using System;
using System.Collections.Generic;
using _04TodoList.Editor.FrameWork.DataBinding;
using _04TodoList.Editor.FrameWork.Drawer;
using _04TodoList.Editor.FrameWork.Drawer.Interface;
using _04TodoList.Editor.FrameWork.Layout;
using _04TodoList.Editor.FrameWork.ViewController;
using UnityEditor;
using UnityEngine;

namespace _04TodoList.Editor.FrameWork.Window
{
    public class ToDoListMainAbsWindow : AbsWindow
    {
        private ToDoListController toDoListController;

        [MenuItem("TodoList/MainWindow %#t")]
        public static void Open()
        {
            OnOpen<ToDoListMainAbsWindow>(true, "ToDoLists", true);
        }

        protected override void OnInit()
        {
            toDoListController = new ToDoListController();
        }

        protected override void Disable()
        {
            toDoListController?.OnDisable();
        }

        protected override void Dispose()
        {
        }

        protected override void OnGUI()
        {
            toDoListController.Draw();
        }
    }
}