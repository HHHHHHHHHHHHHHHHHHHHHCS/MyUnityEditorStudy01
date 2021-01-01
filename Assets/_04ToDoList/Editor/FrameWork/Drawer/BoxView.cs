using UnityEngine;

namespace _04ToDoList.Editor.FrameWork.Drawer
{
	public class BoxView : View
	{
		public string Context { get; set; }

		public BoxView(string context)
		{
			Context = context;
			guiStyle = new GUIStyle(GUI.skin.box);
		}

		protected override void OnGUI()
		{
			GUILayout.Box(Context, guiStyle,guiLayoutOptions);
		}
	}
}