
// This file has been generated by the GUI designer. Do not modify.
namespace MonoDevelop.EmmetPlugin.Dialogs
{
	public partial class EmmetSettingsWidget
	{
		private global::Gtk.Table table1;
		private global::Gtk.Label label1;
		private global::Gtk.Entry nodeJSPathEntry;
		
		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget MonoDevelop.EmmetPlugin.Dialogs.EmmetSettingsWidget
			global::Stetic.BinContainer.Attach (this);
			this.Name = "MonoDevelop.EmmetPlugin.Dialogs.EmmetSettingsWidget";
			// Container child MonoDevelop.EmmetPlugin.Dialogs.EmmetSettingsWidget.Gtk.Container+ContainerChild
			this.table1 = new global::Gtk.Table (((uint)(1)), ((uint)(2)), false);
			this.table1.Name = "table1";
			this.table1.RowSpacing = ((uint)(6));
			this.table1.ColumnSpacing = ((uint)(6));
			// Container child table1.Gtk.Table+TableChild
			this.label1 = new global::Gtk.Label ();
			this.label1.Name = "label1";
			this.label1.LabelProp = global::Mono.Unix.Catalog.GetString ("NodeJS path:");
			this.table1.Add (this.label1);
			global::Gtk.Table.TableChild w1 = ((global::Gtk.Table.TableChild)(this.table1 [this.label1]));
			w1.XOptions = ((global::Gtk.AttachOptions)(4));
			w1.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.nodeJSPathEntry = new global::Gtk.Entry ();
			this.nodeJSPathEntry.CanFocus = true;
			this.nodeJSPathEntry.Name = "nodeJSPathEntry";
			this.nodeJSPathEntry.IsEditable = true;
			this.nodeJSPathEntry.InvisibleChar = '●';
			this.table1.Add (this.nodeJSPathEntry);
			global::Gtk.Table.TableChild w2 = ((global::Gtk.Table.TableChild)(this.table1 [this.nodeJSPathEntry]));
			w2.LeftAttach = ((uint)(1));
			w2.RightAttach = ((uint)(2));
			w2.YOptions = ((global::Gtk.AttachOptions)(4));
			this.Add (this.table1);
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.Hide ();
		}
	}
}