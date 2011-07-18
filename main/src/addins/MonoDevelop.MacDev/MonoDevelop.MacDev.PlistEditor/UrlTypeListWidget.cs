// 
// UrlTypeListWidget.cs
//  
// Author:
//       Michael Hutchinson <mhutch@xamarin.com>
// 
// Copyright (c) 2011 Xamarin Inc. (http://xamarin.com)
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using MonoDevelop.Projects;
using MonoDevelop.Core;


namespace MonoDevelop.MacDev.PlistEditor
{
	public class UrlTypeListWidget : ExpanderList
	{
		PDictionary dictionary;
		Project project;
		
		public UrlTypeListWidget (Project project, PDictionary dictionary)
			: base (GettextCatalog.GetString ("No URL Types"), GettextCatalog.GetString ("Add URL Type"))
		{
			this.dictionary = dictionary;
			this.project = project;
			dictionary.Changed += delegate {
				Update ();
			};
			Update ();
		}
		
		void Update ()
		{
			var urlTypes = dictionary.Get<PArray> ("CFBundleURLTypes");
			Clear ();
			if (urlTypes == null)
				return;
			foreach (var pObject in urlTypes) {
				var dict = (PDictionary)pObject;
				if (dict == null)
					continue;
				string name = GettextCatalog.GetString ("Untitled");
				var dtw = new URLTypeWidget (project, dict);
				dtw.Expander = AddListItem (name, dtw, dict);
			}
		}
		
		protected override void OnCreateNew (EventArgs e)
		{
			base.OnCreateNew (e);
			
			var dict = dictionary.Get<PArray> ("CFBundleURLTypes");
			if (dict == null) {
				dictionary["CFBundleURLTypes"] = dict = new PArray ();
				dictionary.QueueRebuild ();
			}
			var newEntry = new PDictionary ();
			dict.Add (newEntry);
			dict.QueueRebuild ();
		}
	}
}