namespace CustomEngine;

partial class CustomEngine
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
        this.OpenRenderView = new System.Windows.Forms.Button();
        this.SuspendLayout();
        // 
        // button1
        // 
        this.OpenRenderView.Location = new System.Drawing.Point(317, 229);
        this.OpenRenderView.Name = "button1";
        this.OpenRenderView.Size = new System.Drawing.Size(269, 169);
        this.OpenRenderView.TabIndex = 0;
        this.OpenRenderView.Text = "打开渲染窗口";
        this.OpenRenderView.UseVisualStyleBackColor = true;
        this.OpenRenderView.Click += new System.EventHandler(this.button1_Click);   
        
        this.Controls.Add(this.OpenRenderView);
        
        //主窗口部分的代码
        this.components = new System.ComponentModel.Container();
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(800, 450);
        this.Text = "CustomEngine";
    }
    private System.Windows.Forms.Button OpenRenderView;
    #endregion
}
