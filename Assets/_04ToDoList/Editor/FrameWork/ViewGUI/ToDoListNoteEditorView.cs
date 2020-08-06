using System;
using _04ToDoList.Editor.FrameWork.DataBinding;
using _04ToDoList.Editor.FrameWork.Drawer;
using _04ToDoList.Editor.FrameWork.Layout;
using UnityEditor;
using UnityEngine;

namespace _04ToDoList.Editor.FrameWork.ViewGUI
{
    public class ToDoListNoteEditorView : VerticalLayout
    {
        private TextAreaView textEditor;

        public ToDoListNoteEditorView(Action saveAction)
        {
            Style = "box";

            new LabelView("笔记编辑器")
                .TextMiddleCenter()
                .TheFontStyle(FontStyle.Bold)
                .FontSize(40)
                .AddTo(this);

            textEditor = new TextAreaView(string.Empty)
                .ExpandHeight(true)
                .AddTo(this);

            new ButtonView("保存", () =>
                {
                    EditorGUI.FocusTextInControl(null);
                    var model = ToDoListCls.ModelData;
                    model.notes.Add(new ToDoNote(textEditor.Content.Val));
                    model.Save();
                    saveAction();
                }, true)
                .AddTo(this);
        }

        protected override void OnShow()
        {
            textEditor.Content.Val = string.Empty;
        }
    }
}