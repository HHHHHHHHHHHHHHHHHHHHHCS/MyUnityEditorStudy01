using _04ToDoList.Editor.FrameWork.Drawer;
using _04ToDoList.Editor.FrameWork.Drawer.Interface;
using UnityEngine;

namespace _04ToDoList.Editor.FrameWork
{
	public static class ViewExtension
	{
		public static T Width<T>(this T view, float width) where T : IView
		{
			view.AddGUILayouts(GUILayout.Width(width));
			return view;
		}

		public static T Height<T>(this T view, float height) where T : IView
		{
			view.AddGUILayouts(GUILayout.Height(height));
			return view;
		}

		public static T MaxHeight<T>(this T view, float maxHeight) where T : IView
		{
			view.AddGUILayouts(GUILayout.MaxHeight(maxHeight));
			return view;
		}

		public static T ExpandHeight<T>(this T view, bool expandHeight) where T : IView
		{
			view.AddGUILayouts(GUILayout.ExpandHeight(expandHeight));
			return view;
		}


		public static T FontSize<T>(this T view, int fontSize) where T : View
		{
			view.guiStyle.fontSize = fontSize;
			return view;
		}

		public static T TextPosition<T>(this T view, TextAnchor textAnchor) where T : View
		{
			view.guiStyle.alignment = textAnchor;
			return view;
		}

		public static T TextMiddleCenter<T>(this T view) where T : View
		{
			return TextPosition(view, TextAnchor.MiddleCenter);
		}

		public static T TextMiddleLeft<T>(this T view) where T : View
		{
			return TextPosition(view, TextAnchor.MiddleLeft);
		}

		public static T TextMiddleRight<T>(this T view) where T : View
		{
			return TextPosition(view, TextAnchor.MiddleRight);
		}

		public static T TextLowCenter<T>(this T view) where T : View
		{
			return TextPosition(view, TextAnchor.LowerCenter);
		}

		public static T TextLowRight<T>(this T view) where T : View
		{
			return TextPosition(view, TextAnchor.LowerRight);
		}

		public static T BackgroundColor<T>(this T view, Color color) where T : View
		{
			view.backgroundColor = color;
			return view;
		}

		//用于boxview 等过暗的情况
		public static T MulBackgroundColor<T>(this T view, Color color, float v) where T : View
		{
			view.backgroundColor = color * v;
			return view;
		}

		public static T FontColor<T>(this T view, Color color) where T : View
		{
			view.guiStyle.normal.textColor = color;

			return view;
		}

		public static T TheFontStyle<T>(this T view, FontStyle fontStyle) where T : View
		{
			view.guiStyle.fontStyle = fontStyle;

			return view;
		}

		public static T FontBold<T>(this T view) where T : View
		{
			return TheFontStyle(view, FontStyle.Bold);
		}

		public static T PaddingLeft<T>(this T view, int val) where T : View
		{
			view.guiStyle.padding.left = val;

			return view;
		}

		public static T MarginLeft<T>(this T view, int val) where T : View
		{
			view.guiStyle.margin.left = val;

			return view;
		}

		public static ButtonView IsFill(this ButtonView button, bool isFill)
		{
			button.fullSize = isFill;

			return button;
		}
	}
}