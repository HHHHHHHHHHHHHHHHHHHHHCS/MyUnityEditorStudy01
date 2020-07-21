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

        public static ToDoListCategorySubWindow categorySubWindow;

        private ToDoListController toDoListController;


        [MenuItem("ToDoList/MainWindow %#t")]
        public static ToDoListMainWindow Open()
        {
            instance = Open<ToDoListMainWindow>(true, "ToDoLists", true);
            return instance;
        }

        public static ToDoListCategorySubWindow CreateCategorySubWindow(string name = "ToDoListCategorySubWindow")
        {
            //close 会自动destroy  所以都要重新new
            categorySubWindow = ToDoListCategorySubWindow.Open(name);
            return categorySubWindow;
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
            if (categorySubWindow)
            {
                categorySubWindow.Close();
                categorySubWindow = null;
            }
            instance = null;
        }

        protected override void OnGUI()
        {
            toDoListController.Draw();
        }
    }
}