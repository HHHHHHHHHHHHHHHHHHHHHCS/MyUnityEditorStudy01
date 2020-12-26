using _04ToDoList.Editor.FrameWork.DataBinding;
using _04ToDoList.Editor.FrameWork.Drawer;
using _04ToDoList.Editor.FrameWork.Layout;
using UnityEngine;

namespace _04ToDoList.Editor.FrameWork.ViewGUI
{
	public class ToDoListFeatureView : VerticalLayout
	{
		public ToDoListFeatureView(ToDoFeature todoFeature, int depth = 1)
		{
			var featureDetailView = new VerticalLayout("box");

			var createFeatureBtn = new ButtonView("添加功能", () =>
				{
					var newFeature = new ToDoFeature("AA", "BB");
					todoFeature.childFeatures.Add(newFeature);
					ToDoDataManager.Save();
					new ToDoListFeatureView(newFeature, depth + 1).AddTo(featureDetailView);
				}, true)
				.Height(20).Width(100).FontSize(15).TextMiddleCenter();

			var featureFoldout =
				new FoldoutView(false, todoFeature.name + "	" + todoFeature.description, createFeatureBtn)
					.FontSize(25)
					.FontBold().MarginLeft(15 + depth * 5).AddTo(this);

			featureFoldout.AddFoldoutView(featureDetailView);

			foreach (var feature in todoFeature.childFeatures)
			{
				new ToDoListFeatureView(feature).AddTo(featureDetailView);
			}
		}
	}
}