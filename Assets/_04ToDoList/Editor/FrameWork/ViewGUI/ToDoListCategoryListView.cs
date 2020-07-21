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
        private static readonly Texture2D editorIcon;

        private ToDoListCategorySubWindow _categorySubWindow;

        private bool isDirty;

        private ToDoListCategorySubWindow categorySubWindow
        {
            get
            {
                if (_categorySubWindow == null)
                {
                    _categorySubWindow = ToDoListMainWindow.CreateCategorySubWindow();
                    _categorySubWindow.listView = this;
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
        }


        public void OnUpdate()
        {
            if (isDirty)
            {
                isDirty = false;
                Rebuild();
            }
        }

        public void UpdateToDoItems()
        {
            isDirty = true;

            //如果不focus 则会不刷新
            ToDoListMainWindow.instance.Focus();
        }


        private void Rebuild()
        {
            Clear();

            foreach (var item in ToDoListCls.ModelData.categoryList)
            {
                var layout = new HorizontalLayout("box").AddTo(this);
                new BoxView(item.name).BackgroundColor(item.color.ToColor()).AddTo(layout);
                new FlexibleSpaceView().AddTo(layout);

                new ImageButtonView(editorIcon, () => { OpenSubWindow(item); })
                    .Width(25).Height(25).BackgroundColor(Color.black).AddTo(layout);
            }

            new FlexibleSpaceView().AddTo(this);

            new ButtonView("+", () => OpenSubWindow(), true).AddTo(this);
        }

        private void OpenSubWindow(ToDoData.TodoCategory item = null)
        {
            categorySubWindow.ShowWindow(item);
        }

        protected override void OnShow()
        {
            isDirty = true;
        }

        protected override void OnHide()
        {
            base.OnHide();
            //_categorySubWindow 可能是空的 不一定要创建
            if (_categorySubWindow != null)
            {
                _categorySubWindow.Close();
            }
        }
    }
}