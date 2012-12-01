using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public static class WebControlsExtensions
	{
		public static void AddCssClass(this WebControl control, string cssClass)
		{
			List<string> classes = control.CssClass.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();

			if (!classes.Contains(cssClass.ToLower().Trim()))
				classes.Add(cssClass);

			control.CssClass = classes.ToDelimitedString(" ");
		}

		public static void RemoveCssClass(this WebControl control, string cssClass)
		{
			List<string> classes = control.CssClass.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();

			classes.Remove(cssClass);

			control.CssClass = classes.ToDelimitedString(" ");
		}

		public static void AddCssClass(this HtmlControl control, string cssClass)
		{
			List<string> classes = new List<string>();
			if(control.Attributes["class"] != null)
			{
				classes = control.Attributes["class"].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
			}

			if (!classes.Contains(cssClass.ToLower().Trim()))
				classes.Add(cssClass);
			control.Attributes.Add("class", classes.ToDelimitedString(" "));
		}

		public static void RemoveCssClass(this HtmlControl control, string cssClass)
		{
			List<string> classes = new List<string>();
			if (control.Attributes["class"] != null)
			{
				classes = control.Attributes["class"].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
			}
			classes.Remove(cssClass);
			control.Attributes.Add("class", classes.ToDelimitedString(" "));
		}
	}

	static class StringExtensions
	{
		public static string ToDelimitedString(this IEnumerable<string> list, string delimiter)
		{
			StringBuilder sb = new StringBuilder();
			foreach (string item in list)
			{
				if (sb.Length > 0)
					sb.Append(delimiter);

				sb.Append(item);
			}

			return sb.ToString();
		}
	}