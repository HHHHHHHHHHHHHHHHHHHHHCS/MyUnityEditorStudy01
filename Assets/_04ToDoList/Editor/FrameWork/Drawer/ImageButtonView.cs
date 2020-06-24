using System;
using System.Collections.Generic;
using _04ToDoList.Editor.FrameWork.Drawer.Interface;
using _04ToDoList.Editor.FrameWork.Layout.Interface;
using UnityEditor;
using UnityEngine;

namespace _04ToDoList.Editor.FrameWork.Drawer
{
    public class ImageButtonView : View
    {
        public Texture2D image { get; set; }

        public Action OnClickEvent { get; set; }

        public ImageButtonView(string imagePath, Action onClickEvent = null)
        {
            image = AssetDatabase.LoadAssetAtPath<Texture2D>(imagePath);
            OnClickEvent = onClickEvent;
        }

        public ImageButtonView(Texture2D _image, Action onClickEvent = null)
        {
            image = _image;
            OnClickEvent = onClickEvent;
        }

        protected override void OnGUI()
        {


            bool isClick;

            if (guiLayoutOptions != null && guiLayoutOptions.Length > 0)
            {
                isClick = GUILayout.Button(image ? image : Texture2D.whiteTexture, guiLayoutOptions);
            }
            else
            {
                isClick = GUILayout.Button(image ? image : Texture2D.whiteTexture, GUILayout.Width(40));
            }


            if (isClick)
            {
                OnClickEvent?.Invoke();
            }

        }
    }
}