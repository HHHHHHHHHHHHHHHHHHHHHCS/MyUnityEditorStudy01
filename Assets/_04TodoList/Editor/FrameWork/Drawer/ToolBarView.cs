using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _04ToDoList.Editor.FrameWork.DataBinding;
using _04ToDoList.Editor.FrameWork.Drawer;
using UnityEngine;


namespace _04ToDoList.Editor.FrameWork.Drawer
{
    public class ToolBarView : View
    {
        public string style;
        private readonly List<string> menus;
        private readonly List<Action> actions;
        private readonly Property<int> selectIndex;

        public ToolBarView()
        {
            menus = new List<string>();
            actions = new List<Action>();
            selectIndex = new Property<int>(0, ClickAct);
            guiStyle = GUI.skin.button;
        }

        public ToolBarView(string[] _menus, Action[] _actions)
        {
            menus = _menus == null ? new List<string>() : _menus.ToList();
            actions = _actions == null ? new List<Action>() : _actions.ToList();
            selectIndex = new Property<int>(0, ClickAct);
            guiStyle = GUI.skin.button;
        }


        public ToolBarView(List<string> _menus, List<Action> _actions)
        {
            menus = _menus == null ? new List<string>() : _menus.ToList();
            actions = _actions == null ? new List<Action>() : _actions.ToList();
            selectIndex = new Property<int>(0, ClickAct);
            guiStyle = GUI.skin.button;
        }

        protected override void OnGUI()
        {
            bool haveStyle = !string.IsNullOrWhiteSpace(style);
            if (haveStyle)
            {
                GUILayout.BeginHorizontal(style);
            }

            if (menus.Count > 0)
            {
                selectIndex.Val = GUILayout.Toolbar(selectIndex.Val, menus.ToArray(), guiStyle, guiLayoutOptions);
            }

            if (haveStyle)
            {
                GUILayout.EndHorizontal();
            }
        }

        public ToolBarView AddMenu(string title, Action act = null)
        {
            var titleIndex = menus.IndexOf(title);
            if (titleIndex >= 0)
            {
                actions[titleIndex] = act;
            }
            else
            {
                menus.Add(title);
                actions.Add(act);
            }

            return this;
        }

        private void ClickAct(int index)
        {
            if (index >= 0 && index <= actions.Count)
            {
                actions[index]?.Invoke();
            }
        }
    }
}