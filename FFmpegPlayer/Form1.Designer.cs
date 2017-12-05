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
            this.label1 = new System.Windows.Forms.Label();
            this.btnAdStart = new System.Windows.Forms.Button();
            this.btnAdEnd = new System.Windows.Forms.Button();
            this.queueScroller = new System.Windows.Forms.HScrollBar();
            this.selectedFrameViewer = new AdDetectVideoPlayer.Components.VideoFrameViewer();
            this.pictureBoxVideo = new AdDetectVideoPlayer.Components.VideoFrameViewer();
            ((System.ComponentModel.ISupportInitialize)(this.selectedFrameViewer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxVideo)).BeginInit();
            this.SuspendLayout();
            // 
            // btnPlay
            // 
            this.btnPlay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPlay.Location = new System.Drawing.Point(611, 2);
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
            this.textBoxPath.Location = new System.Drawing.Point(2, 2);
            this.textBoxPath.Name = "textBoxPath";
            this.textBoxPath.Size = new System.Drawing.Size(603, 20);
            this.textBoxPath.TabIndex = 2;
            this.textBoxPath.Text = "http://a1.akamaized.net/app/823476.stream/chunks.m3u8";
            // 
            // thumbPanel
            // 
            this.thumbPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.thumbPanel.AutoScroll = true;
            this.thumbPanel.Location = new System.Drawing.Point(116, 28);
            this.thumbPanel.Name = "thumbPanel";
            this.thumbPanel.Size = new System.Drawing.Size(604, 95);
            this.thumbPanel.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(723, 130);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "(152) 0/5 ";
            // 
            // btnAdStart
            // 
            this.btnAdStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdStart.Location = new System.Drawing.Point(687, 2);
            this.btnAdStart.Name = "btnAdStart";
            this.btnAdStart.Size = new System.Drawing.Size(73, 20);
            this.btnAdStart.TabIndex = 5;
            this.btnAdStart.Text = "Mark Start";
            this.btnAdStart.UseVisualStyleBackColor = true;
            // 
            // btnAdEnd
            // 
            this.btnAdEnd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdEnd.Location = new System.Drawing.Point(766, 2);
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
            this.queueScroller.Location = new System.Drawing.Point(116, 130);
            this.queueScroller.Name = "queueScroller";
            this.queueScroller.Size = new System.Drawing.Size(604, 17);
            this.queueScroller.TabIndex = 9;
            this.queueScroller.Value = 50;
            // 
            // selectedFrameViewer
            // 
            this.selectedFrameViewer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.selectedFrameViewer.Location = new System.Drawing.Point(726, 28);
            this.selectedFrameViewer.Name = "selectedFrameViewer";
            this.selectedFrameViewer.Size = new System.Drawing.Size(108, 95);
            this.selectedFrameViewer.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.selectedFrameViewer.TabIndex = 0;
            this.selectedFrameViewer.TabStop = false;
            // 
            // pictureBoxVideo
            // 
            this.pictureBoxVideo.Location = new System.Drawing.Point(2, 28);
            this.pictureBoxVideo.Name = "pictureBoxVideo";
            this.pictureBoxVideo.Size = new System.Drawing.Size(108, 95);
            this.pictureBoxVideo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxVideo.TabIndex = 0;
            this.pictureBoxVideo.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(846, 183);
            this.Controls.Add(this.queueScroller);
            this.Controls.Add(this.btnAdEnd);
            this.Controls.Add(this.btnAdStart);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.thumbPanel);
            this.Controls.Add(this.textBoxPath);
            this.Controls.Add(this.btnPlay);
            this.Controls.Add(this.selectedFrameViewer);
            this.Controls.Add(this.pictureBoxVideo);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.selectedFrameViewer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxVideo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Components.VideoFrameViewer pictureBoxVideo;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.TextBox textBoxPath;
        private System.Windows.Forms.FlowLayoutPanel thumbPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAdStart;
        private System.Windows.Forms.Button btnAdEnd;
        private Components.VideoFrameViewer selectedFrameViewer;
        private System.Windows.Forms.HScrollBar queueScroller;
    }
}

