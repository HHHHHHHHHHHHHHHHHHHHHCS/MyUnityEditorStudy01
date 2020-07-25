using System;
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


        public static ToDoListCategorySubWindow Open(string name = "ToDoListCategorySubWindow")
        {
            var window = Open<ToDoListCategorySubWindow>(name);

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
            textAreaView = new TextAreaView(itemName, (s) => itemName = s)
                .BackgroundColor(Color.white).AddTo(verticalLayout);

            new LabelView("颜色").FontSize(15).AddTo(verticalLayout);

            Color itemColor = Color.black;

            colorView = new ColorView(itemColor, (c) => itemColor = c)
                .BackgroundColor(Color.white).AddTo(verticalLayout);

            changeButton = new ButtonView("添加", () => { }, true)
                .BackgroundColor(Color.white).AddTo(verticalLayout);

            new ButtonView("关闭", Close, true)
                .BackgroundColor(Color.white).AddTo(verticalLayout);
        }

        public void ShowWindow(ToDoData.TodoCategory item = null)
        {
            Focus();
            ResetWindow(item);
            Show();
        }

        public void ResetWindow(ToDoData.TodoCategory item = null)
        {
            if (item == null)
            {
                textAreaView.Content.Val = string.Empty;
                colorView.colorProperty.Val = Color.black;

                changeButton.text = "添加";
                changeButton.OnClickEvent = () =>
                {
                    ToDoListCls.ModelData.categoryList.Add(new ToDoData.TodoCategory(textAreaView.Content.Val,
                        colorView.colorProperty.Val.ToText()));
                    ToDoListCls.ModelData.Save();
                    listView.UpdateToDoItems();
                    Close();
                };
            }
            else
            {
                textAreaView.Content.Val = item.name;
                colorView.colorProperty.Val = item.color.ToColor();

                changeButton.text = "修改";
                changeButton.OnClickEvent = () =>
                {
                    item.name = textAreaView.Content.Val;
                    item.color = colorView.colorProperty.Val.ToText();
                    ToDoListCls.ModelData.Save();
                    listView.UpdateToDoItems();
                    Close();
                };
            }
        }
    }
}