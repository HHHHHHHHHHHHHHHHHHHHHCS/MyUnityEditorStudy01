using _04ToDoList.Editor.FrameWork.DataBinding;
using _04ToDoList.Editor.FrameWork.SystemComponent.Question;
using _04ToDoList.Editor.FrameWork.Window;
using UnityEditor;
using UnityEngine;

namespace _04ToDoList.Editor.FrameWork.SystemComponent.Logic
{
	public static partial class ToDoSplitChoiceEX
	{
		public static ProcessSystem ToDoSplitChoice(this ProcessSystem processSystem)
		{
			var first = string.Empty;
			var second = string.Empty;

			return processSystem.BeginChoice("拆解多步")
				.BeginQuestion()
				.SetTitle("先做什么?")
				.SetContext(string.Empty)
				.SetContextTextArea(string.Empty, (str1) => { first = str1; })
				.NewBtn("保存", () =>
				{
					ToDoDataManager.AddToDoItem(first);
					EditorGUI.FocusTextInControl(string.Empty);
				})
				.EndQuestion()
				//=================
				.BeginQuestion()
				.SetTitle("接着做什么?")
				.SetContext(string.Empty)
				.SetContextTextArea(string.Empty, (str2) => { second = str2; })
				.NewBtn("接着", () =>
				{
					ToDoDataManager.AddToDoItem(second);
					EditorGUI.FocusTextInControl(string.Empty);
					processSystem.RepeatThen();
				})
				.NewBtn("保存并结束", () =>
				{
					ToDoDataManager.AddToDoItem(second);
					EditorGUI.FocusTextInControl(string.Empty);
				})
				.EndQuestion()
				.EndChoice();
		}

		public static ProcessSystem RepeatThen(this ProcessSystem processSystem)
		{
			var context = string.Empty;
			
			return processSystem.BeginChoice("接着做什么")
				.BeginQuestion()
				.SetTitle("接着做什么?")
				.SetContext(string.Empty)
				.SetContextTextArea(string.Empty, (str) => { context = str; })
				.NewBtn("保存", () =>
				{
					ToDoDataManager.AddToDoItem(context);
					EditorGUI.FocusTextInControl(string.Empty);
				})
				.NewBtn("保存并结束", () =>
				{
					ToDoDataManager.AddToDoItem(context);
					EditorGUI.FocusTextInControl(string.Empty);
				})
				.EndQuestion()
				.EndChoice();
		}
	}
}