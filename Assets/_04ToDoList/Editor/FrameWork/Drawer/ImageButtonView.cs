﻿using System;
using System.Collections.Generic;
using _04ToDoList.Editor.FrameWork.Drawer.Interface;
using _04ToDoList.Editor.FrameWork.Layout.Interface;
using UnityEditor;
using UnityEngine;

namespace _04ToDoList.Editor.FrameWork.Drawer
{
	public static class ImageButtonIcon
	{
		public const string openIcon = "Assets/_04ToDoList/EditorIcons/Open.png";
		public const string editorIcon = "Assets/_04ToDoList/EditorIcons/Editor.png";
		public const string deleteIcon = "Assets/_04ToDoList/EditorIcons/Delete.png";
		public const string addIcon = "Assets/_04ToDoList/EditorIcons/Add.png";
		public const string playIcon = "Assets/_04ToDoList/EditorIcons/Play.png";
		public const string finishIcon = "Assets/_04ToDoList/EditorIcons/Finish.png";
		public const string resetIcon = "Assets/_04ToDoList/EditorIcons/Reset.png";
	}

	public class ImageButtonView : View
	{
		private static Dictionary<string, Texture2D> imagesDict = new Dictionary<string, Texture2D>();


		public Texture2D Image { get; }

		public Action OnClickEvent { get; set; }

		public ImageButtonView(string imagePath, Action onClickEvent = null)
		{
			if (imagesDict.TryGetValue(imagePath, out Texture2D img))
			{
				Image = img;
			}

			if (Image == null)
			{
				img = AssetDatabase.LoadAssetAtPath<Texture2D>(imagePath);
				imagesDict.Add(imagePath, img);
				Image = img;
			}

			guiStyle = new GUIStyle(GUI.skin.button);
			OnClickEvent = onClickEvent;
		}

		public ImageButtonView(Texture2D _image, Action onClickEvent = null)
		{
			Image = _image;
			OnClickEvent = onClickEvent;
		}

		protected override void OnGUI()
		{
			bool isClick;

			if (guiLayoutOptions != null && guiLayoutOptions.Length > 0)
			{
				isClick = GUILayout.Button(Image ? Image : Texture2D.whiteTexture, guiStyle, guiLayoutOptions);
			}
			else
			{
				isClick = GUILayout.Button(Image ? Image : Texture2D.whiteTexture, guiStyle, GUILayout.Width(40));
			}


			if (isClick)
			{
				OnClickEvent?.Invoke();
			}
		}
	}
}