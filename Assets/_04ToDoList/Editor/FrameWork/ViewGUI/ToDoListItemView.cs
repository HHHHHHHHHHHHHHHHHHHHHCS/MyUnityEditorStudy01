using System;
using System.Collections.Generic;
using _04ToDoList.Editor.Component;
using _04ToDoList.Editor.FrameWork.DataBinding;
using _04ToDoList.Editor.FrameWork.Drawer;
using _04ToDoList.Editor.FrameWork.Drawer.Interface;
using _04ToDoList.Editor.FrameWork.Layout;
using _04ToDoList.Editor.FrameWork.Layout.Interface;
using _04ToDoList.Editor.FrameWork.Window;
using UnityEditor;
using UnityEngine;

namespace _04ToDoList.Editor.FrameWork.ViewGUI
{
	public class ToDoListItemView : VerticalLayout
	{
		private HorizontalLayout container;

		public ToDoData data;
		public Action<ToDoListItemView> changeProperty;
		public Action<ToDoData> deleteAct;
		public bool showTime = false;

		private bool needFresh;
		private ToDoListItemEditorSubWindow itemEditorSubWindow;


		public ToDoListItemView(ToDoData _item, Action<ToDoListItemView> _changeProperty, bool _showTime = false,
			Action<ToDoData> _deleteAct = null)
			: base()
		{
			this.data = _item;
			this.changeProperty = _changeProperty;
			this.showTime = _showTime;
			this.deleteAct = _deleteAct;
			container = new HorizontalLayout("box");
			Add(container);
			Add(new SpaceView(4));
			BuildItem();
		}

		protected override void OnRefresh()
		{
			if (needFresh)
			{
				needFresh = false;
				changeProperty?.Invoke(this);
				BuildItem();
			}
		}

		public void BuildItem()
		{
			container.Clear();


			if (data.state == ToDoData.ToDoState.NoStart)
			{
				var startBtn = new ImageButtonView(ImageButtonIcon.playIcon, () =>
				{
					data.startTime = DateTime.Now;
					data.state.Val = ToDoData.ToDoState.Started; //改变了state会自动储存
					UpdateItem();
				}).Height(20).Width(40).BackgroundColor(Color.green);
				container.Add(startBtn);
			}
			else if (data.state == ToDoData.ToDoState.Started)
			{
				var finishedBtn = new ImageButtonView(ImageButtonIcon.finishIcon, () =>
				{
					data.finishTime = DateTime.Now;
					data.state.Val = ToDoData.ToDoState.Done;
					UpdateItem();
				}).Height(20).Width(40).BackgroundColor(Color.green);
				container.Add(finishedBtn);
			}
			else if (data.state == ToDoData.ToDoState.Done)
			{
				var resetBtn = new ImageButtonView(ImageButtonIcon.resetIcon, () =>
				{
					data.createTime = DateTime.Now;
					data.state.Val = ToDoData.ToDoState.NoStart;
					UpdateItem();
				}).Height(20).Width(40).BackgroundColor(Color.grey);
				container.Add(resetBtn);
			}

			var boxView = new BoxView("无").AddTo(container)
				.TextMiddleCenter().Width(20).FontSize(12)
				.FontColor(Color.white).FontBold();

			new CategoryComponent(data.category).AddTo(container);

			var contentLabel = new LabelView(data.content).Height(20).FontSize(15).TextMiddleCenter();
			container.Add(contentLabel);

			if (showTime)
			{
				new LabelView(data.finishTime.ToString("完成于 HH:mm:ss"))
					.Height(20).Width(80).TextMiddleLeft().FontBold();
				new LabelView(data.UsedTimeText)
					.Height(20).Width(100).TextMiddleLeft().AddTo(container);
			}

			var priorityVal = data.priority.Val;
			Color priorityColor = Color.clear;
			switch (priorityVal)
			{
				case ToDoData.ToDoPriority.A:
					boxView.Context = "A";
					priorityColor = Color.red;
					boxView.BackgroundColor(Color.red);
					break;
				case ToDoData.ToDoPriority.B:
					boxView.Context = "B";
					priorityColor = Color.yellow;
					boxView.BackgroundColor(Color.yellow);
					break;
				case ToDoData.ToDoPriority.C:
					boxView.Context = "C";
					priorityColor = Color.cyan;
					boxView.BackgroundColor(Color.cyan);
					break;
				case ToDoData.ToDoPriority.D:
					boxView.Context = "D";
					priorityColor = Color.blue;
					boxView.BackgroundColor(Color.blue);
					break;
				case ToDoData.ToDoPriority.None:
					boxView.Context = "无";
					priorityColor = Color.gray;
					boxView.BackgroundColor(Color.gray);
					break;
			}


			var priority = new EnumPopupView<ToDoData.ToDoPriority>(priorityVal)
				.Width(30).Height(20).BackgroundColor(priorityColor).AddTo(container);

			priority.ValueProperty.RegisterValueChanged((val) =>
			{
				data.priority.Val = val;

				switch (val)
				{
					case ToDoData.ToDoPriority.A:
						boxView.Context = "A";
						priority.BackgroundColor(Color.red);
						boxView.BackgroundColor(Color.red);
						break;
					case ToDoData.ToDoPriority.B:
						boxView.Context = "B";
						priority.BackgroundColor(Color.yellow);
						boxView.BackgroundColor(Color.yellow);
						break;
					case ToDoData.ToDoPriority.C:
						boxView.Context = "C";
						priority.BackgroundColor(Color.cyan);
						boxView.BackgroundColor(Color.cyan);
						break;
					case ToDoData.ToDoPriority.D:
						boxView.Context = "D";
						priority.BackgroundColor(Color.blue);
						boxView.BackgroundColor(Color.blue);
						break;
					case ToDoData.ToDoPriority.None:
						boxView.Context = "无";
						priority.BackgroundColor(Color.gray);
						boxView.BackgroundColor(Color.gray);
						break;
				}

				UpdateItem();
			});

			new ImageButtonView(ImageButtonIcon.editorIcon, () => { OpenSubWindow(); })
				.Width(25).Height(25).BackgroundColor(Color.black).AddTo(container);

			var deleteBtn = new ImageButtonView(ImageButtonIcon.deleteIcon, () =>
			{
				data.finished.ClearValueChanged();
				ToDoDataManager.RemoveToDoItem(data);
				deleteAct?.Invoke(data);
				UpdateItem();
			}).Height(25).Width(25).BackgroundColor(Color.red);
			container.Add(deleteBtn);
		}

		public void UpdateItem()
		{
			needFresh = true;

			//如果不focus 则会不刷新
			ToDoListMainWindow.instance.Focus();
		}

		protected override void OnHide()
		{
			base.OnHide();
			if (itemEditorSubWindow != null)
			{
				itemEditorSubWindow.Close();
			}
		}


		private void OpenSubWindow()
		{
			EnqueueCmd(() =>
				{
					itemEditorSubWindow = ToDoListItemEditorSubWindow.Open(this, "ToDo Editor");
					itemEditorSubWindow.ShowWindow(this);
				}
			);
		}
	}
}