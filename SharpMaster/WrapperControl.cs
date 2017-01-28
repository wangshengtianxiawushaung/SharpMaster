﻿
using System;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SharpMaster
{
	/// <summary>
	/// Description of WrapperControl.
	/// </summary>
	public partial class WrapperControl : UserControl
	{
		private readonly Control payload;
		private readonly Action action;
		
		public WrapperControl(Control payload, Action action)
		{
			this.payload = payload;
			this.action = action;
			
			InitializeComponent();
			
			panelContainer.Controls.Add(payload);
			labelTitle.Text = SplitTitle(payload.GetType().Name);
			Height = payload.Height + panelTop.Height;
			Width = payload.Width;
		}
		
		public Control Payload {
			get { return payload; }
		}
		
		private string SplitTitle(string text)
		{
			var title = text.Replace("Control", "");
			var sb = new StringBuilder();
			foreach (var c in title) {
				if (char.IsUpper(c))
					sb.Append(" ");
				sb.Append(c);
			}
			return sb.ToString();
		}
		
		void LinkLabelRemoveLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Parent.Controls.Remove(this);
			action();
		}
		
		void LabelDragMouseDown(object sender, MouseEventArgs e)
		{
			DoDragDrop(this, DragDropEffects.Move);
		}
	}
}