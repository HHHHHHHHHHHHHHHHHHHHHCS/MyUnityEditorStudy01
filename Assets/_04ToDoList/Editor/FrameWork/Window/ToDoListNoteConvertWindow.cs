﻿using _04ToDoList.Editor.FrameWork.DataBinding;
using _04ToDoList.Editor.FrameWork.Drawer;
using _04ToDoList.Editor.FrameWork.Layout;
using _04ToDoList.Editor.FrameWork.SystemComponent;
using _04ToDoList.Editor.FrameWork.SystemComponent.Question;
using _04ToDoList.Editor.FrameWork.ViewGUI;
using UnityEngine;

namespace _04ToDoList.Editor.FrameWork.Window
{
    public class ToDoListNoteConvertWindow : SubWindow
    {
        private ToDoListNoteView listView;
        private ToDoNote note;

        public static ToDoListNoteConvertWindow Open(ToDoListNoteView _todoListNoteView, ToDoNote _note,
            string name = "ToDo Note Convert")
        {
            var window = Open<ToDoListNoteConvertWindow>(name);
            window.listView = _todoListNoteView;
            window.note = _note;
            window.Init();
            return window;
        }

        private void Init()
        {
            //避免重复打开
            Clear();


            var verticalLayout = new VerticalLayout().AddTo(this);


            // ButtonView finishedBtn = new ButtonView("转换", () =>
            // {
            //     listView.UpdateList();
            //     Close();
            //     ToDoListMainWindow.instance.Focus();
            // }, true).AddTo(verticalLayout);
            //
            // finishedBtn.Hide();

            ProcessSystem.CreateQuestions()
                //------------------------
                .BeginQuestion()
                .SetTitle(note.content)
                .SetContext("这是什么?")
                .NewBtn("目标")
                .NewBtn("参考/阅读资料")
                .NewBtn("想法/Idea")
                .NewChoice("事项/事件", "事项")
                //------------------------
                .EndQuestion()
                .BeginQuestion()
                .SetTitle("是否可以拆解为多步")
                .NewChoice("是", "拆解多步")
                .NewBtn("否", () => { })
                .EndQuestion()
                //------------------------
                .BeginChoice("事项")
                .BeginQuestion()
                .SetTitle("现在是否可以执行!")
                .NewBtn(0, "是", () => ConvertToDoNote(note, false))
                .NewBtn(1, "否", () => ConvertToDoNote(note, true))
                .EndQuestion()
                .EndQuestion()
                //------------------------
                .BeginChoice("拆解多步")
                .BeginQuestion()
                .SetTitle("先做什么?")
                .SetContextTextArea("什么")
                .NewBtn("保存", () => { })
                .EndQuestion()
                //=================
                .BeginQuestion()
                .SetTitle("再做什么?")
                .SetContextTextArea("什么")
                .NewBtn("保存", () => { })
                .NewBtn("保存并结束", () => { })
                .EndQuestion()
                .EndQuestion()
                .AddTo(verticalLayout)
                .StartProcess(Close);
        }

        private void ConvertToDoNote(ToDoNote note, bool isHide)
        {
            listView.UpdateList();
            ToDoDataManager.ConvertToDoNote(note, isHide);
        }
    }
}