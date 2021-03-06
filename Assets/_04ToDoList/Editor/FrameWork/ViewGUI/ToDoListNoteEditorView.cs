﻿using System;
using _04ToDoList.Editor.FrameWork.DataBinding;
using _04ToDoList.Editor.FrameWork.Drawer;
using _04ToDoList.Editor.FrameWork.Layout;
using UnityEditor;
using UnityEngine;

namespace _04ToDoList.Editor.FrameWork.ViewGUI
{
	public class ToDoListNoteEditorView : VerticalLayout
	{
		private ToDoNote note;

		private TextAreaView textEditor;

		public ToDoListNoteEditorView(Action saveAction, Action closeAction)
		{
			Style = "box";

			new LabelView("笔记编辑器")
				.TextMiddleCenter()
				.FontBold()
				.FontSize(40)
				.AddTo(this);

			textEditor = new TextAreaView(string.Empty)
				.Height(130)
				.ExpandHeight(true)
				.AddTo(this);

			new ButtonView("保存", () =>
				{
					if (note == null)
					{
						ToDoDataManager.AddToDoNote(textEditor.Content.Val);
					}
					else
					{
						note.content = textEditor.Content.Val;
						ToDoDataManager.Save();
					}

					EditorGUI.FocusTextInControl(null);
					saveAction();
					note = null;
				}, true)
				.AddTo(this);


			new ButtonView("关闭", () =>
				{
					EditorGUI.FocusTextInControl(null);
					closeAction();
					note = null;
				}, true)
				.AddTo(this);
		}

		public void ReInit(ToDoNote _note)
		{
			note = _note;
			if (note == null)
			{
				textEditor.Content.Val = string.Empty;
			}
			else
			{
				textEditor.Content.Val = note.content;
			}
		}

		protected override void OnHide()
		{
			note = null;
		}
	}
}