using UnityEngine;

namespace _04ToDoList.Editor.FrameWork.Drawer
{
	public class LabelView : View
	{
		public string Content { get; set; }

		public LabelView(string _content)
		{
			Content = _content;
			guiStyle = new GUIStyle(GUI.skin.label);
		}

		public void SetText(string _content)
		{
			Content = _content;
		}

		protected override void OnGUI()
		{
			GUILayout.Label(Content, guiStyle, guiLayoutOptions);
		}
	}
}