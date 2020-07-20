using System;
using _04ToDoList.Editor.FrameWork.DataBinding;
using _04ToDoList.Editor.FrameWork.Drawer;
using _04ToDoList.Editor.FrameWork.Utils;
using UnityEngine;

namespace _04ToDoList.Editor.FrameWork.Window
{
    public class ToDoListCategorySubWindow : SubWindow
    {
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
            new SpaceView(4).AddTo(this);

            new LabelView("名字").AddTo(this);

            string itemName = string.Empty;

            //加.BackgroundColor(Color.white)  的原因 
            //因为按钮事件的时候 background 还是在被更改的期间
            //所以 需要一个颜色 来显示自己的颜色
            textAreaView = new TextAreaView(itemName, (s) => itemName = s)
                .BackgroundColor(Color.white).AddTo(this);

            new LabelView("颜色").AddTo(this);

            Color itemColor = Color.black;

            colorView = new ColorView(itemColor, (c) => itemColor = c)
                .BackgroundColor(Color.white).AddTo(this);

            changeButton = new ButtonView("添加", () => { }, true)
                .BackgroundColor(Color.white).AddTo(this);

            new ButtonView("关闭", Close, true)
                .BackgroundColor(Color.white).AddTo(this);
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
                    Close();
                };
            }
        }
    }
}