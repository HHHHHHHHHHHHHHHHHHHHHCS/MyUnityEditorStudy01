using System;
using _04ToDoList.Editor.FrameWork.DataBinding;
using _04ToDoList.Editor.FrameWork.Drawer;
using _04ToDoList.Editor.FrameWork.Layout;
using _04ToDoList.Editor.FrameWork.ViewGUI;
using UnityEditor;
using UnityEngine;

namespace _04ToDoList.Editor.FrameWork.Window
{
	public class ToDoListFeaturesSubWindow : SubWindow
	{
		public static ToDoListFeaturesSubWindow instance { get; private set; }

		private ToDoListFeatureView inputView;
		private bool isCreate;
		private ToDoFeature featureOrParent;
		private ToDoProduct productParent;

		private TextFieldView nameInputView, descInputView;
		private ButtonView saveCreateBtn;


		public static ToDoListFeaturesSubWindow Open(ToDoListFeatureView _inputView, ToDoFeature _featureOrParent,
			bool _isCreate, string name = "ToDo Feature Editor")
		{
			var window = Open<ToDoListFeaturesSubWindow>(name);
			window.Init(_inputView, _featureOrParent, null, _isCreate);
			instance = window;
			window.Show();
			return window;
		}

		public static ToDoListFeaturesSubWindow Open(ToDoListFeatureView _inputView, ToDoProduct _productParent,
			string name = "ToDo Feature Editor")
		{
			var window = Open<ToDoListFeaturesSubWindow>(name);
			window.Init(_inputView, null, _productParent, true);
			instance = window;
			window.Show();
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

		private void Init(ToDoListFeatureView _inputView, ToDoFeature _featureParent, ToDoProduct _todoProduct,
			bool _isCreate)
		{
			inputView = _inputView;
			isCreate = _isCreate;
			featureOrParent = _featureParent;
			productParent = _todoProduct;
			EditorGUI.FocusTextInControl(null);

			if (isCreate)
			{
				nameInputView.Content.Val = string.Empty;
				descInputView.Content.Val = string.Empty;
				saveCreateBtn.Text = "创建";
			}
			else
			{
				nameInputView.Content.Val = featureOrParent.name;
				descInputView.Content.Val = featureOrParent.description;
				saveCreateBtn.Text = "保存";
			}
		}

		private void OnDisable()
		{
			instance = null;
		}

		private void SaveOrCreateEvent()
		{
			ToDoListMainWindow.instance.Focus();

			if (isCreate)
			{
				ToDoFeature feature = new ToDoFeature(nameInputView.Content, descInputView.Content);

				if (productParent != null)
				{
					productParent.features.Add(feature);
				}
				else
				{
					featureOrParent.childFeatures.Add(feature);
				}
				
				inputView.AddNewToView(feature);
			}
			else
			{
				featureOrParent.name = nameInputView.Content.Val;
				featureOrParent.description = descInputView.Content.Val;
				inputView.RefreshFoldoutViewContent();
			}

			ToDoDataManager.Save();
		}
	}
}