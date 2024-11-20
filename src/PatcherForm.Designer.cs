using System;

namespace Oxide.Patcher
{
    partial class PatcherForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PatcherForm));
            this.mainmenu = new System.Windows.Forms.MenuStrip();
            this.filemenu = new System.Windows.Forms.ToolStripMenuItem();
            this.newproject = new System.Windows.Forms.ToolStripMenuItem();
            this.openproject = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.recentprojects = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exit = new System.Windows.Forms.ToolStripMenuItem();
            this.maintoolbar = new System.Windows.Forms.ToolStrip();
            this.newprojecttool = new System.Windows.Forms.ToolStripButton();
            this.openprojecttool = new System.Windows.Forms.ToolStripButton();
            this.generateDocsButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.patchtool = new System.Windows.Forms.ToolStripButton();
            this.mainstatusbar = new System.Windows.Forms.StatusStrip();
            this.statuslabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitter = new System.Windows.Forms.SplitContainer();
            this.objectview = new System.Windows.Forms.TreeView();
            this.imagelist = new System.Windows.Forms.ImageList(this.components);
            this.tabview = new System.Windows.Forms.TabControl();
            this.tabviewcontextmenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.closeTabToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeOtherTabsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openprojectdialog = new System.Windows.Forms.OpenFileDialog();
            this.unloadedassemblymenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addtoproject = new System.Windows.Forms.ToolStripMenuItem();
            this.loadedassemblymenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.removefromproject = new System.Windows.Forms.ToolStripMenuItem();
            this.hooksmenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.FlagMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.UnflagMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.FlagAllItem = new System.Windows.Forms.ToolStripMenuItem();
            this.UnflagAllItem = new System.Windows.Forms.ToolStripMenuItem();
            this.categorymenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.FlagCategory = new System.Windows.Forms.ToolStripMenuItem();
            this.UnflagCategory = new System.Windows.Forms.ToolStripMenuItem();
            this.hookmenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.imagelistDragDrop = new System.Windows.Forms.ImageList(this.components);
            this.modifiersMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.flagAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unflagAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainmenu.SuspendLayout();
            this.maintoolbar.SuspendLayout();
            this.mainstatusbar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitter)).BeginInit();
            this.splitter.Panel1.SuspendLayout();
            this.splitter.Panel2.SuspendLayout();
            this.splitter.SuspendLayout();
            this.tabviewcontextmenu.SuspendLayout();
            this.unloadedassemblymenu.SuspendLayout();
            this.loadedassemblymenu.SuspendLayout();
            this.hooksmenu.SuspendLayout();
            this.categorymenu.SuspendLayout();
            this.hookmenu.SuspendLayout();
            this.modifiersMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainmenu
            // 
            this.mainmenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.filemenu});
            this.mainmenu.Location = new System.Drawing.Point(0, 0);
            this.mainmenu.Name = "mainmenu";
            this.mainmenu.Size = new System.Drawing.Size(1264, 24);
            this.mainmenu.TabIndex = 0;
            this.mainmenu.Text = "menuStrip1";
            // 
            // filemenu
            // 
            this.filemenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newproject,
            this.openproject,
            this.toolStripSeparator1,
            this.recentprojects,
            this.toolStripSeparator2,
            this.exit});
            this.filemenu.Name = "filemenu";
            this.filemenu.Size = new System.Drawing.Size(37, 20);
            this.filemenu.Text = "File";
            // 
            // newproject
            // 
            this.newproject.Image = global::Oxide.Patcher.Properties.Resources.book_add;
            this.newproject.Name = "newproject";
            this.newproject.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newproject.Size = new System.Drawing.Size(194, 22);
            this.newproject.Text = "New Project";
            this.newproject.Click += new System.EventHandler(this.newproject_Click);
            // 
            // openproject
            // 
            this.openproject.Image = global::Oxide.Patcher.Properties.Resources.book_go;
            this.openproject.Name = "openproject";
            this.openproject.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openproject.Size = new System.Drawing.Size(194, 22);
            this.openproject.Text = "Open Project";
            this.openproject.Click += new System.EventHandler(this.openproject_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(191, 6);
            // 
            // recentprojects
            // 
            this.recentprojects.Name = "recentprojects";
            this.recentprojects.Size = new System.Drawing.Size(194, 22);
            this.recentprojects.Text = "Recent Projects";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(191, 6);
            // 
            // exit
            // 
            this.exit.Image = global::Oxide.Patcher.Properties.Resources.door_in;
            this.exit.Name = "exit";
            this.exit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.exit.Size = new System.Drawing.Size(194, 22);
            this.exit.Text = "Exit";
            this.exit.Click += new System.EventHandler(this.exit_Click);
            // 
            // maintoolbar
            // 
            this.maintoolbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newprojecttool,
            this.openprojecttool,
            this.generateDocsButton,
            this.toolStripSeparator3,
            this.patchtool});
            this.maintoolbar.Location = new System.Drawing.Point(0, 24);
            this.maintoolbar.Name = "maintoolbar";
            this.maintoolbar.Size = new System.Drawing.Size(1264, 25);
            this.maintoolbar.TabIndex = 1;
            this.maintoolbar.Text = "toolStrip1";
            // 
            // newprojecttool
            // 
            this.newprojecttool.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.newprojecttool.Image = global::Oxide.Patcher.Properties.Resources.book_add;
            this.newprojecttool.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newprojecttool.Name = "newprojecttool";
            this.newprojecttool.Size = new System.Drawing.Size(23, 22);
            this.newprojecttool.Text = "New Project";
            this.newprojecttool.Click += new System.EventHandler(this.newprojecttool_Click);
            // 
            // openprojecttool
            // 
            this.openprojecttool.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.openprojecttool.Image = global::Oxide.Patcher.Properties.Resources.book_go;
            this.openprojecttool.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openprojecttool.Name = "openprojecttool";
            this.openprojecttool.Size = new System.Drawing.Size(23, 22);
            this.openprojecttool.Text = "Open Project";
            this.openprojecttool.Click += new System.EventHandler(this.openprojecttool_Click);
            // 
            // generateDocsButton
            // 
            this.generateDocsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.generateDocsButton.Image = global::Oxide.Patcher.Resources.Icons.lightning;
            this.generateDocsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.generateDocsButton.Name = "generateDocsButton";
            this.generateDocsButton.Size = new System.Drawing.Size(23, 22);
            this.generateDocsButton.Text = "Generate Docs";
            this.generateDocsButton.Click += new System.EventHandler(this.generateDocsButton_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // patchtool
            // 
            this.patchtool.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.patchtool.Enabled = false;
            this.patchtool.Image = global::Oxide.Patcher.Properties.Resources.wand;
            this.patchtool.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.patchtool.Name = "patchtool";
            this.patchtool.Size = new System.Drawing.Size(23, 22);
            this.patchtool.Text = "Patch";
            this.patchtool.Click += new System.EventHandler(this.patchtool_Click);
            // 
            // mainstatusbar
            // 
            this.mainstatusbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statuslabel});
            this.mainstatusbar.Location = new System.Drawing.Point(0, 659);
            this.mainstatusbar.Name = "mainstatusbar";
            this.mainstatusbar.Size = new System.Drawing.Size(1264, 22);
            this.mainstatusbar.TabIndex = 3;
            this.mainstatusbar.Text = "statusStrip1";
            // 
            // statuslabel
            // 
            this.statuslabel.Name = "statuslabel";
            this.statuslabel.Size = new System.Drawing.Size(0, 17);
            // 
            // splitter
            // 
            this.splitter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitter.Location = new System.Drawing.Point(0, 49);
            this.splitter.Name = "splitter";
            // 
            // splitter.Panel1
            // 
            this.splitter.Panel1.Controls.Add(this.objectview);
            // 
            // splitter.Panel2
            // 
            this.splitter.Panel2.Controls.Add(this.tabview);
            this.splitter.Size = new System.Drawing.Size(1264, 610);
            this.splitter.SplitterDistance = 268;
            this.splitter.TabIndex = 4;
            // 
            // objectview
            // 
            this.objectview.AllowDrop = true;
            this.objectview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.objectview.ImageIndex = 0;
            this.objectview.ImageList = this.imagelist;
            this.objectview.Location = new System.Drawing.Point(0, 0);
            this.objectview.Name = "objectview";
            this.objectview.SelectedImageIndex = 0;
            this.objectview.Size = new System.Drawing.Size(268, 610);
            this.objectview.TabIndex = 0;
            this.objectview.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.objectview_AfterLabelEdit);
            this.objectview.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.objectview_ItemDrag);
            this.objectview.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.objectview_NodeMouseClick);
            this.objectview.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.objectview_NodeMouseDoubleClick);
            this.objectview.DragDrop += new System.Windows.Forms.DragEventHandler(this.objectview_DragDrop);
            this.objectview.DragEnter += new System.Windows.Forms.DragEventHandler(this.objectview_DragEnter);
            this.objectview.DragOver += new System.Windows.Forms.DragEventHandler(this.objectview_DragOver);
            this.objectview.DragLeave += new System.EventHandler(this.objectview_DragLeave);
            this.objectview.MouseDown += new System.Windows.Forms.MouseEventHandler(this.objectview_MouseDown);
            // 
            // imagelist
            // 
            this.imagelist.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imagelist.ImageStream")));
            this.imagelist.TransparentColor = System.Drawing.Color.Transparent;
            this.imagelist.Images.SetKeyName(0, "book_add.png");
            this.imagelist.Images.SetKeyName(1, "book_go.png");
            this.imagelist.Images.SetKeyName(2, "cog_edit.png");
            this.imagelist.Images.SetKeyName(3, "cross.png");
            this.imagelist.Images.SetKeyName(4, "door_in.png");
            this.imagelist.Images.SetKeyName(5, "folder.png");
            this.imagelist.Images.SetKeyName(6, "lightning.png");
            this.imagelist.Images.SetKeyName(7, "accept.png");
            this.imagelist.Images.SetKeyName(8, "Namespace.png");
            this.imagelist.Images.SetKeyName(9, "Class_489.png");
            this.imagelist.Images.SetKeyName(10, "Class-Friend_491.png");
            this.imagelist.Images.SetKeyName(11, "Class-Private_493.png");
            this.imagelist.Images.SetKeyName(12, "Class-Protected_492.png");
            this.imagelist.Images.SetKeyName(13, "Class-Sealed_490.png");
            this.imagelist.Images.SetKeyName(14, "Class-Shortcut_494.png");
            this.imagelist.Images.SetKeyName(15, "script_error.png");
            this.imagelist.Images.SetKeyName(16, "Interface_612.png");
            this.imagelist.Images.SetKeyName(17, "Interface-Friend_614.png");
            this.imagelist.Images.SetKeyName(18, "Interface-Private_616.png");
            this.imagelist.Images.SetKeyName(19, "Interface-Protected_615.png");
            this.imagelist.Images.SetKeyName(20, "Interface-Sealed_613.png");
            this.imagelist.Images.SetKeyName(21, "Interface-Shortcut_617.png");
            this.imagelist.Images.SetKeyName(22, "error.png");
            this.imagelist.Images.SetKeyName(23, "script_lightning.png");
            this.imagelist.Images.SetKeyName(24, "folder_flagged.png");
            // 
            // tabview
            // 
            this.tabview.AllowDrop = true;
            this.tabview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabview.ItemSize = new System.Drawing.Size(110, 18);
            this.tabview.Location = new System.Drawing.Point(0, 0);
            this.tabview.Name = "tabview";
            this.tabview.SelectedIndex = 0;
            this.tabview.Size = new System.Drawing.Size(992, 610);
            this.tabview.TabIndex = 0;
            this.tabview.DragOver += new System.Windows.Forms.DragEventHandler(this.tabview_DragOver);
            this.tabview.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tabview_MouseClick);
            this.tabview.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tabview_MouseDown);
            this.tabview.MouseMove += new System.Windows.Forms.MouseEventHandler(this.tabview_MouseMove);
            this.tabview.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tabview_MouseUp);
            // 
            // tabviewcontextmenu
            // 
            this.tabviewcontextmenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeTabToolStripMenuItem,
            this.closeOtherTabsMenuItem});
            this.tabviewcontextmenu.Name = "tabviewcontextmenu";
            this.tabviewcontextmenu.Size = new System.Drawing.Size(163, 48);
            // 
            // closeTabToolStripMenuItem
            // 
            this.closeTabToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.closeTabToolStripMenuItem.Name = "closeTabToolStripMenuItem";
            this.closeTabToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.closeTabToolStripMenuItem.Text = "Close Tab";
            this.closeTabToolStripMenuItem.Click += new System.EventHandler(this.closetab_Click);
            // 
            // closeOtherTabsMenuItem
            // 
            this.closeOtherTabsMenuItem.Name = "closeOtherTabsMenuItem";
            this.closeOtherTabsMenuItem.Size = new System.Drawing.Size(162, 22);
            this.closeOtherTabsMenuItem.Text = "Close Other Tabs";
            this.closeOtherTabsMenuItem.Click += new System.EventHandler(this.closeothertabs_Click);
            // 
            // openprojectdialog
            // 
            this.openprojectdialog.DefaultExt = "opj";
            this.openprojectdialog.Filter = "Oxide project|*.opj";
            this.openprojectdialog.Title = "Open Project";
            // 
            // unloadedassemblymenu
            // 
            this.unloadedassemblymenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addtoproject});
            this.unloadedassemblymenu.Name = "unloadedassemblymenu";
            this.unloadedassemblymenu.Size = new System.Drawing.Size(151, 26);
            this.unloadedassemblymenu.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.unloadedassemblymenu_ItemClicked);
            // 
            // addtoproject
            // 
            this.addtoproject.Name = "addtoproject";
            this.addtoproject.Size = new System.Drawing.Size(150, 22);
            this.addtoproject.Text = "Add to Project";
            // 
            // loadedassemblymenu
            // 
            this.loadedassemblymenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.removefromproject});
            this.loadedassemblymenu.Name = "unloadedassemblymenu";
            this.loadedassemblymenu.Size = new System.Drawing.Size(187, 26);
            this.loadedassemblymenu.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.loadedassemblymenu_ItemClicked);
            // 
            // removefromproject
            // 
            this.removefromproject.Name = "removefromproject";
            this.removefromproject.Size = new System.Drawing.Size(186, 22);
            this.removefromproject.Text = "Remove from Project";
            // 
            // hooksmenu
            // 
            this.hooksmenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FlagMenuItem,
            this.UnflagMenuItem,
            this.toolStripSeparator4,
            this.FlagAllItem,
            this.UnflagAllItem});
            this.hooksmenu.Name = "hooksmenu";
            this.hooksmenu.Size = new System.Drawing.Size(127, 98);
            // 
            // FlagMenuItem
            // 
            this.FlagMenuItem.Name = "FlagMenuItem";
            this.FlagMenuItem.Size = new System.Drawing.Size(126, 22);
            this.FlagMenuItem.Text = "Flag";
            this.FlagMenuItem.Click += new System.EventHandler(this.flag_Click);
            // 
            // UnflagMenuItem
            // 
            this.UnflagMenuItem.Name = "UnflagMenuItem";
            this.UnflagMenuItem.Size = new System.Drawing.Size(126, 22);
            this.UnflagMenuItem.Text = "Unflag";
            this.UnflagMenuItem.Click += new System.EventHandler(this.unflag_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(123, 6);
            // 
            // FlagAllItem
            // 
            this.FlagAllItem.Name = "FlagAllItem";
            this.FlagAllItem.Size = new System.Drawing.Size(126, 22);
            this.FlagAllItem.Text = "Flag All";
            this.FlagAllItem.Click += new System.EventHandler(this.flagall_Click);
            // 
            // UnflagAllItem
            // 
            this.UnflagAllItem.Name = "UnflagAllItem";
            this.UnflagAllItem.Size = new System.Drawing.Size(126, 22);
            this.UnflagAllItem.Text = "Unflag All";
            this.UnflagAllItem.Click += new System.EventHandler(this.unflagall_Click);
            // 
            // categorymenu
            // 
            this.categorymenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem3,
            this.toolStripSeparator5,
            this.FlagCategory,
            this.UnflagCategory});
            this.categorymenu.Name = "hooksmenu";
            this.categorymenu.Size = new System.Drawing.Size(127, 98);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(126, 22);
            this.toolStripMenuItem1.Text = "Rename";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.renamecategory_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(126, 22);
            this.toolStripMenuItem3.Text = "Remove";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.removecategory_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(123, 6);
            // 
            // FlagCategory
            // 
            this.FlagCategory.Name = "FlagCategory";
            this.FlagCategory.Size = new System.Drawing.Size(126, 22);
            this.FlagCategory.Text = "Flag All";
            this.FlagCategory.Click += new System.EventHandler(this.FlagCategory_Click);
            // 
            // UnflagCategory
            // 
            this.UnflagCategory.Name = "UnflagCategory";
            this.UnflagCategory.Size = new System.Drawing.Size(126, 22);
            this.UnflagCategory.Text = "Unflag All";
            this.UnflagCategory.Click += new System.EventHandler(this.UnflagCategory_Click);
            // 
            // hookmenu
            // 
            this.hookmenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem4,
            this.toolStripSeparator6,
            this.toolStripMenuItem5,
            this.toolStripMenuItem6});
            this.hookmenu.Name = "hooksmenu";
            this.hookmenu.Size = new System.Drawing.Size(148, 76);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(147, 22);
            this.toolStripMenuItem4.Text = "Add Category";
            this.toolStripMenuItem4.Click += new System.EventHandler(this.addcategory_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(144, 6);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(147, 22);
            this.toolStripMenuItem5.Text = "Flag All";
            this.toolStripMenuItem5.Click += new System.EventHandler(this.flagall_Click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(147, 22);
            this.toolStripMenuItem6.Text = "Unflag All";
            this.toolStripMenuItem6.Click += new System.EventHandler(this.unflagall_Click);
            // 
            // imagelistDragDrop
            // 
            this.imagelistDragDrop.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imagelistDragDrop.ImageSize = new System.Drawing.Size(16, 16);
            this.imagelistDragDrop.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // modifiersMenu
            // 
            this.modifiersMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.flagAllToolStripMenuItem,
            this.unflagAllToolStripMenuItem});
            this.modifiersMenu.Name = "modifiersMenu";
            this.modifiersMenu.Size = new System.Drawing.Size(127, 48);
            // 
            // flagAllToolStripMenuItem
            // 
            this.flagAllToolStripMenuItem.Name = "flagAllToolStripMenuItem";
            this.flagAllToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.flagAllToolStripMenuItem.Text = "Flag All";
            this.flagAllToolStripMenuItem.Click += new System.EventHandler(this.ModifiersFlagAll);
            // 
            // unflagAllToolStripMenuItem
            // 
            this.unflagAllToolStripMenuItem.Name = "unflagAllToolStripMenuItem";
            this.unflagAllToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.unflagAllToolStripMenuItem.Text = "Unflag All";
            this.unflagAllToolStripMenuItem.Click += new System.EventHandler(this.ModifiersUnflagAll);
            // 
            // PatcherForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.splitter);
            this.Controls.Add(this.mainstatusbar);
            this.Controls.Add(this.maintoolbar);
            this.Controls.Add(this.mainmenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mainmenu;
            this.Name = "PatcherForm";
            this.Text = "Oxide Patcher - Version {0}";
            this.mainmenu.ResumeLayout(false);
            this.mainmenu.PerformLayout();
            this.maintoolbar.ResumeLayout(false);
            this.maintoolbar.PerformLayout();
            this.mainstatusbar.ResumeLayout(false);
            this.mainstatusbar.PerformLayout();
            this.splitter.Panel1.ResumeLayout(false);
            this.splitter.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitter)).EndInit();
            this.splitter.ResumeLayout(false);
            this.tabviewcontextmenu.ResumeLayout(false);
            this.unloadedassemblymenu.ResumeLayout(false);
            this.loadedassemblymenu.ResumeLayout(false);
            this.hooksmenu.ResumeLayout(false);
            this.categorymenu.ResumeLayout(false);
            this.hookmenu.ResumeLayout(false);
            this.modifiersMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.ContextMenuStrip modifiersMenu;
        private System.Windows.Forms.ToolStripMenuItem flagAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unflagAllToolStripMenuItem;

        private System.Windows.Forms.ToolStripButton generateDocsButton;

        #endregion

        private System.Windows.Forms.MenuStrip mainmenu;
        private System.Windows.Forms.ToolStripMenuItem filemenu;
        private System.Windows.Forms.ToolStrip maintoolbar;
        private System.Windows.Forms.StatusStrip mainstatusbar;
        private System.Windows.Forms.SplitContainer splitter;
        private System.Windows.Forms.TreeView objectview;
        private System.Windows.Forms.TabControl tabview;
        private System.Windows.Forms.ToolStripMenuItem newproject;
        private System.Windows.Forms.ToolStripMenuItem openproject;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem recentprojects;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem exit;
        private System.Windows.Forms.ToolStripButton newprojecttool;
        private System.Windows.Forms.OpenFileDialog openprojectdialog;
        private System.Windows.Forms.ImageList imagelist;
        private System.Windows.Forms.ToolStripButton openprojecttool;
        private System.Windows.Forms.ToolStripStatusLabel statuslabel;
        private System.Windows.Forms.ContextMenuStrip unloadedassemblymenu;
        private System.Windows.Forms.ToolStripMenuItem addtoproject;
        private System.Windows.Forms.ContextMenuStrip loadedassemblymenu;
        private System.Windows.Forms.ToolStripMenuItem removefromproject;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton patchtool;
        private System.Windows.Forms.ContextMenuStrip tabviewcontextmenu;
        private System.Windows.Forms.ToolStripMenuItem closeTabToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip hooksmenu;
        private System.Windows.Forms.ToolStripMenuItem FlagAllItem;
        private System.Windows.Forms.ToolStripMenuItem UnflagAllItem;
        public System.Windows.Forms.ToolStripMenuItem FlagMenuItem;
        public System.Windows.Forms.ToolStripMenuItem UnflagMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem closeOtherTabsMenuItem;
        private System.Windows.Forms.ContextMenuStrip categorymenu;
        public System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ContextMenuStrip hookmenu;
        public System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem FlagCategory;
        private System.Windows.Forms.ToolStripMenuItem UnflagCategory;
        private System.Windows.Forms.ImageList imagelistDragDrop;
    }
}
