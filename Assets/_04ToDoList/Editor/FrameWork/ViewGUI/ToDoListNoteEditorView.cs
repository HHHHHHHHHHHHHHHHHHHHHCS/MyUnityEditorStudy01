using _04ToDoList.Editor.FrameWork.DataBinding;
using _04ToDoList.Editor.FrameWork.Drawer;
using _04ToDoList.Editor.FrameWork.Layout;
using UnityEngine;

namespace _04ToDoList.Editor.FrameWork.ViewGUI
{
    public class ToDoListNoteEditorView : VerticalLayout
    {
        public ToDoListNoteEditorView()
        {
            Style = "box";

            new LabelView("笔记编辑器")
                .TheFontStyle(FontStyle.Bold)
                .FontSize(40)
                .AddTo(this);

            var textEditor = new TextAreaView(string.Empty).AddTo(this);

            new ButtonView("保存", () =>
                {
                    EnqueueCmd(() =>
                    {
                        var model = ToDoListCls.ModelData;
                        model.notes.Add(new ToDoNote(textEditor.Content.Val));
                        model.Save();
                        ParentRemoveThis();
                    });
                    
                }, true)
                .AddTo(this);
        }


    }
}