using System;
using System.Linq;
using _04ToDoList.Editor.FrameWork.DataBinding;
using _04ToDoList.Editor.FrameWork.Drawer;
using _04ToDoList.Editor.FrameWork.Layout;
using _04ToDoList.Editor.FrameWork.ViewGUI;

namespace _04ToDoList.Editor.FrameWork.Window
{
    public class ToDoListEditorSubWindow : SubWindow
    {
        public ToDoListItemView itemView;

        private TextAreaView contentTextArea;
        private PopupView enumPopupView;
        private ButtonView saveButton;


        public static ToDoListEditorSubWindow Open(ToDoListItemView itemView, string name = "ToDo Item Editor")
        {
            var window = Open<ToDoListEditorSubWindow>(name);
            window.itemView = itemView;
            return window;
        }

        private void Awake()
        {
            var verticalLayout = new VerticalLayout("box").AddTo(this);


            new LabelView("描述").FontSize(15).AddTo(verticalLayout);

            contentTextArea = new TextAreaView("")
                .Height(30).FontSize(20)
                .ExpandHeight(true).AddTo(verticalLayout);

            enumPopupView = new PopupView(-1, null).AddTo(verticalLayout);

            saveButton = new ButtonView("保存", _fullSize: true).AddTo(verticalLayout);

            new ButtonView("取消", () => { }, _fullSize: true).AddTo(verticalLayout);
        }

        public void ShowWindow(ToDoListItemView item)
        {
            Focus();
            ResetWindow(item.data);
            Show();
        }


        public void ResetWindow(ToDoData item)
        {
            var categoryList = ToDoDataManager.Data.categoryList;

            contentTextArea.Content.Val = item.content;

            if (categoryList.Count > 0)
            {
                var index = 0;

                if (item.category != null)
                {
                    index = categoryList.IndexOf(item.category);
                }

                enumPopupView.ValueProperty.Val = index < 0 ? 0 : index;
                enumPopupView.MenuArray = categoryList.Select(category => category.name).ToArray();
            }


            saveButton.OnClickEvent = () =>
            {
                item.content = contentTextArea.Content.Val;
                item.category = categoryList[enumPopupView.ValueProperty.Val];

                ToDoDataManager.Data.Save();
                itemView.UpdateItem();
                Close();
            };
        }
    }
}