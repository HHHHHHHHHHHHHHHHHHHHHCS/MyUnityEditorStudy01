using _04ToDoList.Editor.FrameWork.DataBinding;
using _04ToDoList.Editor.FrameWork.Drawer;
using _04ToDoList.Editor.FrameWork.Layout;
using _04ToDoList.Editor.FrameWork.Utils;
using _04ToDoList.Editor.FrameWork.Window;
using UnityEditor;
using UnityEngine;

namespace _04ToDoList.Editor.FrameWork.ViewGUI
{
    public class ToDoListCategoryListView : VerticalLayout
    {
        private static Texture2D editorIcon;

        private ToDoListCategorySubWindow _categorySubWindow;

        private ToDoListCategorySubWindow categorySubWindow
        {
            get
            {
                if (_categorySubWindow == null)
                {
                    _categorySubWindow = ToDoListMainWindow.CreateCategorySubWindow();
                }

                return _categorySubWindow;
            }
        }


        static ToDoListCategoryListView()
        {
            editorIcon = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/_04ToDoList/EditorIcons/Editor.png");
        }

        public ToDoListCategoryListView() : base("box")
        {
            new LabelView("this is category list view").AddTo(this);

            foreach (var item in ToDoListCls.ModelData.categoryList)
            {
                var layout = new HorizontalLayout("box").AddTo(this);
                new BoxView(item.name).BackgroundColor(item.color.ToColor()).AddTo(layout);
                new FlexibleSpaceView().AddTo(layout);

                new ImageButtonView(editorIcon, () =>
                    {

                        OpenSubWindow(item);
                    })
                    .Width(25).Height(25).BackgroundColor(Color.black).AddTo(layout);
            }

            new ButtonView("+", () => OpenSubWindow(), true).AddTo(this);
        }

        private void OpenSubWindow(ToDoData.TodoCategory item = null)
        {
            categorySubWindow.ShowWindow(item);
        }

        protected override void OnHide()
        {
            base.OnHide();
            categorySubWindow.Close();
        }
    }
}