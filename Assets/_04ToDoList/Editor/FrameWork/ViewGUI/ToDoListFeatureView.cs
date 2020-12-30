using System;
using System.Collections.Generic;
using _04ToDoList.Editor.FrameWork.DataBinding;
using _04ToDoList.Editor.FrameWork.Drawer;
using _04ToDoList.Editor.FrameWork.Layout;
using _04ToDoList.Editor.FrameWork.Window;
using UnityEngine;

namespace _04ToDoList.Editor.FrameWork.ViewGUI
{
	public class ToDoListFeatureView : VerticalLayout
	{
		public bool isHeader;
		public int depth;
		public ToDoProduct product; //只有header 才有
		public ToDoFeature currentFeature;
		public ToDoListFeatureView parentView;
		public VerticalLayout featureDetailView;

		private FoldoutView featureFoldout;

		public ToDoListFeatureView(ToDoProduct todoProduct)
		{
			product = todoProduct;
			isHeader = true;
			depth = 0;
			Init();
		}

		public ToDoListFeatureView(ToDoFeature _currentFeature, ToDoListFeatureView _parentView)
		{
			isHeader = false;
			depth = _parentView.depth + 1;
			currentFeature = _currentFeature;
			parentView = _parentView;
			Init();
		}


		private void Init()
		{
			featureDetailView = new VerticalLayout("box");

			var hor = new HorizontalLayout();

			//string foldoutName = isHeader ? "功能:" : currentFeature.name + "	" + currentFeature.description;

			featureFoldout =
				new FoldoutView(false, string.Empty, hor)
					.FontSize(25)
					.FontBold().MarginLeft(15 + depth * 5).AddTo(this);

			RefreshFoldoutViewContent();

			new FlexibleSpaceView().AddTo(hor);

			new ImageButtonView(ImageButtonIcon.addIcon, OpenFeatureCreateSubWindow)
				.Height(25).Width(25).BackgroundColor(Color.yellow).AddTo(hor);

			if (!isHeader)
			{
				new ImageButtonView(ImageButtonIcon.editorIcon, OpenFeatureEditorSubWindow)
					.Height(25).Width(25).BackgroundColor(Color.red).AddTo(hor);
				
				new ImageButtonView(ImageButtonIcon.deleteIcon, DeleteFeature)
					.Height(25).Width(25).BackgroundColor(Color.red).AddTo(hor);
			}

			featureFoldout.AddFoldoutView(featureDetailView);

			foreach (var feature in isHeader ? product.features : currentFeature.childFeatures)
			{
				new ToDoListFeatureView(feature, this).AddTo(featureDetailView);
			}
		}

		private void OpenFeatureCreateSubWindow()
		{
			EnqueueCmd(() =>
			{
				if (isHeader)
				{
					ToDoListFeaturesSubWindow.Open(this, product);
				}
				else
				{
					ToDoListFeaturesSubWindow.Open(this, currentFeature, true);
				}
			});
		}
		
		private void OpenFeatureEditorSubWindow()
		{
			EnqueueCmd(() =>
			{
				ToDoListFeaturesSubWindow.Open(this, currentFeature, false);
			});
		}

		public void AddNewToView(ToDoFeature newFeature)
		{
			new ToDoListFeatureView(newFeature, this).AddTo(featureDetailView);
		}

		public void RefreshFoldoutViewContent()
		{
			string foldoutName = isHeader ? "功能:" : currentFeature.name + "	" + currentFeature.description;

			featureFoldout.Content = foldoutName;
		}

		private void DeleteFeature()
		{
			if (parentView.isHeader)
			{
				parentView.product.features.Remove(currentFeature);
			}
			else
			{
				parentView.currentFeature.childFeatures.Remove(currentFeature);
			}

			ToDoDataManager.Save();
			EnqueueCmd(() => parentView.featureDetailView.Remove(this));
		}
	}
}