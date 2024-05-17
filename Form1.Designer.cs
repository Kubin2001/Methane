namespace MethaneNew
{
    partial class Form1
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
            BtnClassCreate = new Button();
            BtnSkeletonCreate = new Button();
            button1 = new Button();
            ClassDialog = new SaveFileDialog();
            textBoxClass = new TextBox();
            textBoxSkeleton = new TextBox();
            SkeletonDialog = new SaveFileDialog();
            BtnLink = new Button();
            SDLDialog = new FolderBrowserDialog();
            SDLOpenDialog = new OpenFileDialog();
            checkBox32 = new CheckBox();
            checkBox64 = new CheckBox();
            checkBoxImage = new CheckBox();
            checkBoxMixer = new CheckBox();
            SuspendLayout();
            // 
            // BtnClassCreate
            // 
            BtnClassCreate.Font = new Font("Consolas", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            BtnClassCreate.Location = new Point(48, 92);
            BtnClassCreate.Margin = new Padding(3, 2, 3, 2);
            BtnClassCreate.Name = "BtnClassCreate";
            BtnClassCreate.Size = new Size(179, 70);
            BtnClassCreate.TabIndex = 0;
            BtnClassCreate.Text = "CreateClass";
            BtnClassCreate.UseVisualStyleBackColor = true;
            BtnClassCreate.Click += BtnClassCreate_Click;
            // 
            // BtnSkeletonCreate
            // 
            BtnSkeletonCreate.Font = new Font("Consolas", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            BtnSkeletonCreate.Location = new Point(444, 91);
            BtnSkeletonCreate.Margin = new Padding(3, 2, 3, 2);
            BtnSkeletonCreate.Name = "BtnSkeletonCreate";
            BtnSkeletonCreate.Size = new Size(179, 70);
            BtnSkeletonCreate.TabIndex = 1;
            BtnSkeletonCreate.Text = "CreateSkeleton";
            BtnSkeletonCreate.UseVisualStyleBackColor = true;
            BtnSkeletonCreate.Click += BtnSkeletonCreate_Click;
            // 
            // button1
            // 
            button1.Enabled = false;
            button1.Location = new Point(269, 42);
            button1.Margin = new Padding(3, 2, 3, 2);
            button1.Name = "button1";
            button1.Size = new Size(138, 22);
            button1.TabIndex = 2;
            button1.Text = "METHANE v0.6";
            button1.UseVisualStyleBackColor = true;
            // 
            // textBoxClass
            // 
            textBoxClass.Location = new Point(48, 166);
            textBoxClass.Margin = new Padding(3, 2, 3, 2);
            textBoxClass.Name = "textBoxClass";
            textBoxClass.Size = new Size(180, 23);
            textBoxClass.TabIndex = 3;
            // 
            // textBoxSkeleton
            // 
            textBoxSkeleton.Location = new Point(444, 166);
            textBoxSkeleton.Margin = new Padding(3, 2, 3, 2);
            textBoxSkeleton.Name = "textBoxSkeleton";
            textBoxSkeleton.Size = new Size(180, 23);
            textBoxSkeleton.TabIndex = 4;
            // 
            // BtnLink
            // 
            BtnLink.Font = new Font("Consolas", 13.2000008F, FontStyle.Bold, GraphicsUnit.Point);
            BtnLink.Location = new Point(246, 92);
            BtnLink.Margin = new Padding(3, 2, 3, 2);
            BtnLink.Name = "BtnLink";
            BtnLink.Size = new Size(179, 70);
            BtnLink.TabIndex = 5;
            BtnLink.Text = "Link SDL to project";
            BtnLink.UseVisualStyleBackColor = true;
            BtnLink.Click += BtnLink_Click;
            // 
            // SDLOpenDialog
            // 
            SDLOpenDialog.FileName = "openFileDialog1";
            // 
            // checkBox32
            // 
            checkBox32.AutoSize = true;
            checkBox32.Location = new Point(246, 168);
            checkBox32.Margin = new Padding(3, 2, 3, 2);
            checkBox32.Name = "checkBox32";
            checkBox32.Size = new Size(38, 19);
            checkBox32.TabIndex = 6;
            checkBox32.Text = "32";
            checkBox32.UseVisualStyleBackColor = true;
            checkBox32.CheckedChanged += checkBox32_CheckedChanged;
            // 
            // checkBox64
            // 
            checkBox64.AutoSize = true;
            checkBox64.Location = new Point(384, 167);
            checkBox64.Margin = new Padding(3, 2, 3, 2);
            checkBox64.Name = "checkBox64";
            checkBox64.Size = new Size(38, 19);
            checkBox64.TabIndex = 7;
            checkBox64.Text = "64";
            checkBox64.UseVisualStyleBackColor = true;
            checkBox64.CheckedChanged += checkBox64_CheckedChanged;
            // 
            // checkBoxImage
            // 
            checkBoxImage.AutoSize = true;
            checkBoxImage.Location = new Point(246, 200);
            checkBoxImage.Margin = new Padding(3, 2, 3, 2);
            checkBoxImage.Name = "checkBoxImage";
            checkBoxImage.Size = new Size(110, 19);
            checkBoxImage.TabIndex = 8;
            checkBoxImage.Text = "InstallSDLImage";
            checkBoxImage.UseVisualStyleBackColor = true;
            // 
            // checkBoxMixer
            // 
            checkBoxMixer.AutoSize = true;
            checkBoxMixer.Location = new Point(246, 224);
            checkBoxMixer.Name = "checkBoxMixer";
            checkBoxMixer.Size = new Size(107, 19);
            checkBoxMixer.TabIndex = 9;
            checkBoxMixer.Text = "installSDLMixer";
            checkBoxMixer.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(700, 338);
            Controls.Add(checkBoxMixer);
            Controls.Add(checkBoxImage);
            Controls.Add(checkBox64);
            Controls.Add(checkBox32);
            Controls.Add(BtnLink);
            Controls.Add(textBoxSkeleton);
            Controls.Add(textBoxClass);
            Controls.Add(button1);
            Controls.Add(BtnSkeletonCreate);
            Controls.Add(BtnClassCreate);
            Margin = new Padding(3, 2, 3, 2);
            Name = "Form1";
            Text = "Methane";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button BtnClassCreate;
        private Button BtnSkeletonCreate;
        private Button button1;
        private SaveFileDialog ClassDialog;
        private TextBox textBoxClass;
        private TextBox textBoxSkeleton;
        private SaveFileDialog SkeletonDialog;
        private Button BtnLink;
        private FolderBrowserDialog SDLDialog;
        private OpenFileDialog SDLOpenDialog;
        private CheckBox checkBox32;
        private CheckBox checkBox64;
        private CheckBox checkBoxImage;
        private CheckBox checkBoxMixer;
    }
}
