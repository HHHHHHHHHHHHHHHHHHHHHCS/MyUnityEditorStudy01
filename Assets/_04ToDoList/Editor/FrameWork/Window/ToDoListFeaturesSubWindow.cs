using System;
using _04ToDoList.Editor.FrameWork.DataBinding;
using _04ToDoList.Editor.FrameWork.Drawer;
using _04ToDoList.Editor.FrameWork.Layout;
using UnityEditor;
using UnityEngine;

namespace _04ToDoList.Editor.FrameWork.Window
{
	public class ToDoListFeaturesSubWindow : SubWindow
	{
		private bool isCreate;
		private ToDoFeature featureParent;
		private ToDoProduct productParent;

		private TextFieldView nameInputView, descInputView;
		private ButtonView saveCreateBtn;


		public static ToDoListFeaturesSubWindow Open(ToDoFeature _featureParent, bool _isCreate,
			string name = "ToDo Feature Editor")
		{
			var window = Open<ToDoListFeaturesSubWindow>(name);
			window.Init(_featureParent, null, _isCreate);
			return window;
		}

		public static ToDoListFeaturesSubWindow Open(ToDoProduct _productParent, string name = "ToDo Feature Editor")
		{
			var window = Open<ToDoListFeaturesSubWindow>(name);
			window.Init(null, _productParent, false);
			return window;
		}

		private void Awake()
		{
			var verticalLayout = new VerticalLayout().AddTo(this);

			new LabelView("功能名:")
				.FontBold()
				.TextMiddleCenter()
				.FontSize(15)
				.AddTo(verticalLayout);

			nameInputView = new TextFieldView(string.Empty).FontSize(20).AddTo(verticalLayout);

			new SpaceView(8f).AddTo(verticalLayout);
			
			new LabelView("描述:")
				.FontBold()
				.TextMiddleCenter()
				.FontSize(15)
				.AddTo(verticalLayout);

			descInputView = new TextFieldView(string.Empty).FontSize(20).AddTo(verticalLayout);
			
			new SpaceView(8f).AddTo(verticalLayout);

			saveCreateBtn = new ButtonView("保存", SaveOrCreateEvent, true).AddTo(verticalLayout);
		}

		private void Init(ToDoFeature _featureParent, ToDoProduct _todoProduct, bool _isCreate)
		{
			isCreate = _isCreate;
			featureParent = _featureParent;
			productParent = _todoProduct;
			EditorGUI.FocusTextInControl(null);

			if (isCreate)
			{
				saveCreateBtn.Text = "创建";
			}
			else
			{
				saveCreateBtn.Text = "保存";
				nameInputView.Content.Val = featureParent.name;
				descInputView.Content.Val = featureParent.description;
			}
		}

		private void SaveOrCreateEvent()
		{
			Debug.Log(2);
			//Save 之后 反馈回去  
		}
	}
}