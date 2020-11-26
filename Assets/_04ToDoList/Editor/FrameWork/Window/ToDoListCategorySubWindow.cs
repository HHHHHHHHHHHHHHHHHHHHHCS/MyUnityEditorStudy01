using System;
using System.Linq;
using _04ToDoList.Editor.FrameWork.DataBinding;
using _04ToDoList.Editor.FrameWork.Drawer;
using _04ToDoList.Editor.FrameWork.Layout;
using _04ToDoList.Editor.FrameWork.Utils;
using _04ToDoList.Editor.FrameWork.ViewGUI;
using UnityEngine;

namespace _04ToDoList.Editor.FrameWork.Window
{
    public class ToDoListCategorySubWindow : SubWindow
    {
        public ToDoListCategoryListView listView;

        private TextAreaView textAreaView;

        private ColorView colorView;

        private ButtonView changeButton;


        public static ToDoListCategorySubWindow Open(ToDoListCategoryListView listView,
            string name = "ToDoListCategorySubWindow")
        {
            var window = Open<ToDoListCategorySubWindow>(name);
            window.listView = listView;
            return window;
        }

        private void Awake()
        {
            var verticalLayout = new VerticalLayout("box").AddTo(this);

            new SpaceView(4).AddTo(verticalLayout);

            new LabelView("名字").FontSize(15).AddTo(verticalLayout);

            string itemName = string.Empty;

            //加.BackgroundColor(Color.white)  的原因 
            //因为按钮事件的时候 background 还是在被更改的期间
            //所以 需要一个颜色 来显示自己的颜色
            //不过后面用cmdQueue延迟加载了所以无所谓
            textAreaView = new TextAreaView(itemName, (s) => itemName = s)
                .AddTo(verticalLayout);

            new LabelView("颜色").FontSize(15).AddTo(verticalLayout);

            Color itemColor = Color.black;

            colorView = new ColorView(itemColor, (c) => itemColor = c)
                .AddTo(verticalLayout);

            changeButton = new ButtonView("添加", () => { }, true)
                .AddTo(verticalLayout);

            new ButtonView("关闭", Close, true)
                .AddTo(verticalLayout);
        }

        public void ShowWindow(ToDoCategory item = null)
        {
            Focus();
            ResetWindow(item);
            Show();
        }

        public void ResetWindow(ToDoCategory item = null)
        {
            if (item == null)
            {
                textAreaView.Content.Val = string.Empty;
                colorView.ColorProperty.Val = Color.black;

                changeButton.Text = "添加";
                changeButton.OnClickEvent = () =>
                {
                    var data = ToDoDataManager.Data;

                    if (data.categoryList.All(x => x.name != textAreaView.Content.Val))
                    {
                        ToDoDataManager.AddToDoCategory(textAreaView.Content.Val,
                            colorView.ColorProperty.Val.ToText());
                        listView.UpdateToDoItems();
                        Close();
                    }
                };
            }
            else
            {
                textAreaView.Content.Val = item.name;
                colorView.ColorProperty.Val = item.color.ToColor();

                changeButton.Text = "修改";
                changeButton.OnClickEvent = () =>
                {
                    item.name = textAreaView.Content.Val;
                    item.color = colorView.ColorProperty.Val.ToText();
                    ToDoDataManager.Data.Save();
                    listView.UpdateToDoItems();
                    Close();
                };
            }
        }
    }
}