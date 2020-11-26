using System;
using System.Linq;
using _04ToDoList.Editor.FrameWork.DataBinding;
using _04ToDoList.Editor.FrameWork.Drawer;
using _04ToDoList.Editor.FrameWork.Layout;
using _04ToDoList.Editor.FrameWork.ViewGUI;

namespace _04ToDoList.Editor.FrameWork.Window
{
    public class ToDoListItemEditorSubWindow : SubWindow
    {
        public ToDoListItemView itemView;

        private ButtonView showHideButton;
        private TextAreaView contentTextArea;
        private PopupView enumPopupView;
        private ButtonView saveButton;


        public static ToDoListItemEditorSubWindow Open(ToDoListItemView itemView, string name = "ToDo Item Editor")
        {
            var window = Open<ToDoListItemEditorSubWindow>(name);
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

            showHideButton = new ButtonView("", _fullSize: true).AddTo(verticalLayout);

            saveButton = new ButtonView("保存", _fullSize: true).AddTo(verticalLayout);

            new ButtonView("取消", Close, _fullSize: true).AddTo(verticalLayout);
        }

        public void ShowWindow(ToDoListItemView item)
        {
            Focus();
            ResetWindow(item.data);
            Show();
        }


        public void ResetWindow(ToDoData item)
        {
            var data = ToDoDataManager.Data;

            contentTextArea.Content.Val = item.content;

            bool isHide = item.isHide;

            showHideButton.Text = isHide ? "显示" : "隐藏";
            showHideButton.OnClickEvent = () =>
            {
                isHide = !isHide;
                showHideButton.Text = isHide ? "显示" : "隐藏";
                //itemView.UpdateItem();
                //Close();
            };

            if (data.categoryList.Count > 0)
            {
                var index =  ToDoDataManager.ToDoCategoryIndexOf(item.category);

                enumPopupView.ValueProperty.Val = index < 0 ? 0 : index;
                enumPopupView.MenuArray = data.categoryList.Select(category => category.name).ToArray();
            }


            saveButton.OnClickEvent = () =>
            {
                item.isHide = isHide;
                item.content = contentTextArea.Content.Val;
                item.category = data.categoryList[enumPopupView.ValueProperty.Val];

                ToDoDataManager.Save();
                itemView.UpdateItem();
                Close();
            };
        }
    }
}