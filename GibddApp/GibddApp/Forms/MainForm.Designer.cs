using GibddApp.Db;
using GibddApp.Forms;
using System.ComponentModel;

namespace GibddApp
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("Автомобили", 0);
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("Водители", 0);
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("Протоколы", 0);
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem("Нарушения", 0);
            System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem("Пользователи", 1);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.mainFormListView = new System.Windows.Forms.ListView();
            this.MenuList = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // mainFormListView
            // 
            this.mainFormListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainFormListView.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3,
            listViewItem4,
            listViewItem5});
            this.mainFormListView.LargeImageList = this.MenuList;
            this.mainFormListView.Location = new System.Drawing.Point(0, 0);
            this.mainFormListView.MultiSelect = false;
            this.mainFormListView.Name = "mainFormListView";
            this.mainFormListView.Size = new System.Drawing.Size(800, 450);
            this.mainFormListView.SmallImageList = this.MenuList;
            this.mainFormListView.TabIndex = 0;
            this.mainFormListView.UseCompatibleStateImageBehavior = false;            
            this.mainFormListView.DoubleClick += new System.EventHandler(this.MainFormListView_DoubleClick);
            // 
            // MenuList
            // 
            this.MenuList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.MenuList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("MenuList.ImageStream")));
            this.MenuList.TransparentColor = System.Drawing.Color.Transparent;
            this.MenuList.Images.SetKeyName(0, "DataTable_32x.jpg");
            this.MenuList.Images.SetKeyName(1, "GroupOfUsers_16x.jpg");
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.mainFormListView);
            this.Name = "MainForm";
            this.Text = "База ГИБДД";
            this.ResumeLayout(false);

        }

        private void MainFormListView_DoubleClick(object sender, EventArgs e)
        {
            FormBase form = null;

            switch(mainFormListView.SelectedItems[0].Index)
            {
                case 0: // CAR
                    form = new CarForm();
                    break;
                case 1: // DRIVER
                    form = new DriverForm();
                    break; 
                case 2: // PROTOCOL
                    form = new ProtocolForm();
                    break;
                case 3: // VIOLATION
                    form = new ViolationForm();
                    break;
                case 4: // USERS
                    if (!LoginInfo.IsSysDba)
                    {
                        Messages.ShowErrorMessage("Работа с пользователями разрешена только SYSDBA!");
                        return;
                    }

                    form = new UserForm();
                    break;
            }

            form.ShowDialog();
        }

        #endregion

        private ListView mainFormListView;
        private ImageList MenuList;
    }
}