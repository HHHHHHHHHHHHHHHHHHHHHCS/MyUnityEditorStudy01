using System;
using System.Collections.Generic;
using _04ToDoList.Editor.FrameWork.DataBinding;
using _04ToDoList.Editor.FrameWork.Drawer;
using _04ToDoList.Editor.FrameWork.Drawer.Interface;
using _04ToDoList.Editor.FrameWork.Layout;
using _04ToDoList.Editor.FrameWork.ViewController;
using UnityEditor;
using UnityEngine;

namespace _04ToDoList.Editor.FrameWork.Window
{
    public class ToDoListMainWindow : AbsWindow
    {
        private ToDoListController toDoListController;

        [MenuItem("ToDoList/MainWindow %#t")]
        public static ToDoListMainWindow Open()
        {
            return Open<ToDoListMainWindow>(true, "ToDoLists", true);
        }

        protected override void OnInit()
        {
            toDoListController =  CreateViewController<ToDoListController>();
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