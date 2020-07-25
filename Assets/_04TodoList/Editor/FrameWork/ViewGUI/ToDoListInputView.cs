using System;
using System.Linq;
using _04ToDoList.Editor.FrameWork.DataBinding;
using _04ToDoList.Editor.FrameWork.Drawer;
using _04ToDoList.Editor.FrameWork.Layout;
using UnityEditor;
using UnityEngine;

namespace _04ToDoList.Editor.FrameWork.ViewGUI
{
    public class ToDoListInputView : HorizontalLayout
    {
        private static Texture2D addIcon;

        private string todoName = string.Empty;

        private PopupView popupView;

        public int PopupIndex => popupView?.ValueProperty.Val ?? -1;


        static ToDoListInputView()
        {
            addIcon = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/_04ToDoList/EditorIcons/Add.png");
        }

        public ToDoListInputView(Action<string> onInputClick)
            : base("box")
        {
            popupView = new PopupView(0,
                    ToDoListCls.ModelData.categoryList.Select(x => x.name).ToArray(),
                    (index) => { Debug.Log(index); })
                .Width(100f).Height(20).AddTo(this);

            var inputTextArea = new TextAreaView(todoName).Height(20).FontSize(15);
            inputTextArea.Content.Bind(x => todoName = x);
            Add(inputTextArea);

            var addBtn = new ImageButtonView(addIcon, () =>
            {
                if (!string.IsNullOrEmpty(todoName))
                {
                    onInputClick(todoName);
                    inputTextArea.Content.Val = string.Empty;
                    GUI.FocusControl(null);
                }
            }).Width(30).Height(20).BackgroundColor(Color.yellow);

            Add(addBtn);
        }
    }
}