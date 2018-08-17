namespace AdDetectVideoPlayer
{
    partial class Form1
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
            this.btnPlay = new System.Windows.Forms.Button();
            this.textBoxPath = new System.Windows.Forms.TextBox();
            this.thumbPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.btnAdStart = new System.Windows.Forms.Button();
            this.btnAdEnd = new System.Windows.Forms.Button();
            this.queueScroller = new System.Windows.Forms.HScrollBar();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblQueueInfo = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.pictureBoxVideo = new AdDetectVideoPlayer.Components.FrameViewer();
            this.selectedFrameViewer = new AdDetectVideoPlayer.Components.FrameViewer();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnPlay
            // 
            this.btnPlay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPlay.Location = new System.Drawing.Point(685, 18);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(70, 20);
            this.btnPlay.TabIndex = 1;
            this.btnPlay.Text = "Play";
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // textBoxPath
            // 
            this.textBoxPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPath.Location = new System.Drawing.Point(6, 19);
            this.textBoxPath.Name = "textBoxPath";
            this.textBoxPath.Size = new System.Drawing.Size(673, 20);
            this.textBoxPath.TabIndex = 2;
            this.textBoxPath.Text = "https://devimages.apple.com.edgekey.net/streaming/examples/bipbop_16x9/bipbop_16x" +
    "9_variant.m3u8";
            // 
            // thumbPanel
            // 
            this.thumbPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.thumbPanel.Location = new System.Drawing.Point(6, 19);
            this.thumbPanel.Name = "thumbPanel";
            this.thumbPanel.Size = new System.Drawing.Size(635, 112);
            this.thumbPanel.TabIndex = 3;
            this.thumbPanel.WrapContents = false;
            // 
            // btnAdStart
            // 
            this.btnAdStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdStart.Location = new System.Drawing.Point(761, 18);
            this.btnAdStart.Name = "btnAdStart";
            this.btnAdStart.Size = new System.Drawing.Size(73, 20);
            this.btnAdStart.TabIndex = 5;
            this.btnAdStart.Text = "Mark Start";
            this.btnAdStart.UseVisualStyleBackColor = true;
            // 
            // btnAdEnd
            // 
            this.btnAdEnd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdEnd.Location = new System.Drawing.Point(840, 18);
            this.btnAdEnd.Name = "btnAdEnd";
            this.btnAdEnd.Size = new System.Drawing.Size(68, 20);
            this.btnAdEnd.TabIndex = 6;
            this.btnAdEnd.Text = "Mark End";
            this.btnAdEnd.UseVisualStyleBackColor = true;
            // 
            // queueScroller
            // 
            this.queueScroller.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.queueScroller.Location = new System.Drawing.Point(3, 134);
            this.queueScroller.Name = "queueScroller";
            this.queueScroller.Size = new System.Drawing.Size(641, 20);
            this.queueScroller.SmallChange = 2;
            this.queueScroller.TabIndex = 9;
            this.queueScroller.Value = 50;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pictureBoxVideo);
            this.groupBox1.Location = new System.Drawing.Point(6, 45);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(122, 181);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Video";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.selectedFrameViewer);
            this.groupBox2.Location = new System.Drawing.Point(788, 45);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(121, 181);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Selected frame";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.lblQueueInfo);
            this.groupBox3.Controls.Add(this.thumbPanel);
            this.groupBox3.Controls.Add(this.queueScroller);
            this.groupBox3.Location = new System.Drawing.Point(134, 45);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(647, 181);
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Frames";
            // 
            // lblQueueInfo
            // 
            this.lblQueueInfo.AutoSize = true;
            this.lblQueueInfo.Location = new System.Drawing.Point(7, 158);
            this.lblQueueInfo.Name = "lblQueueInfo";
            this.lblQueueInfo.Size = new System.Drawing.Size(60, 13);
            this.lblQueueInfo.TabIndex = 10;
            this.lblQueueInfo.Text = "Queue Info";
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox4.Controls.Add(this.groupBox1);
            this.groupBox4.Controls.Add(this.btnAdEnd);
            this.groupBox4.Controls.Add(this.groupBox3);
            this.groupBox4.Controls.Add(this.btnAdStart);
            this.groupBox4.Controls.Add(this.groupBox2);
            this.groupBox4.Controls.Add(this.textBoxPath);
            this.groupBox4.Controls.Add(this.btnPlay);
            this.groupBox4.Location = new System.Drawing.Point(8, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(914, 237);
            this.groupBox4.TabIndex = 13;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Player";
            // 
            // pictureBoxVideo
            // 
            this.pictureBoxVideo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxVideo.Location = new System.Drawing.Point(3, 16);
            this.pictureBoxVideo.Name = "pictureBoxVideo";
            this.pictureBoxVideo.Size = new System.Drawing.Size(116, 162);
            this.pictureBoxVideo.TabIndex = 0;
            this.pictureBoxVideo.TabStop = false;
            // 
            // selectedFrameViewer
            // 
            this.selectedFrameViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.selectedFrameViewer.Location = new System.Drawing.Point(3, 16);
            this.selectedFrameViewer.Name = "selectedFrameViewer";
            this.selectedFrameViewer.Size = new System.Drawing.Size(115, 162);
            this.selectedFrameViewer.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(934, 555);
            this.Controls.Add(this.groupBox4);
            this.Name = "Form1";
            this.Text = "Video Player";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Components.FrameViewer pictureBoxVideo;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.TextBox textBoxPath;
        private System.Windows.Forms.FlowLayoutPanel thumbPanel;
        private System.Windows.Forms.Button btnAdStart;
        private System.Windows.Forms.Button btnAdEnd;
        private System.Windows.Forms.HScrollBar queueScroller;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private Components.FrameViewer selectedFrameViewer;
        private System.Windows.Forms.Label lblQueueInfo;
    }
}

