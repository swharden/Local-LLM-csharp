namespace ChatWinForms;

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
        rtbOutput = new RichTextBox();
        tbInput = new TextBox();
        btnSend = new Button();
        nudTemperature = new NumericUpDown();
        label1 = new Label();
        label2 = new Label();
        ((System.ComponentModel.ISupportInitialize)nudTemperature).BeginInit();
        SuspendLayout();
        // 
        // rtbOutput
        // 
        rtbOutput.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        rtbOutput.BackColor = SystemColors.Control;
        rtbOutput.BorderStyle = BorderStyle.None;
        rtbOutput.Font = new Font("Consolas", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
        rtbOutput.Location = new Point(24, 97);
        rtbOutput.Name = "rtbOutput";
        rtbOutput.ReadOnly = true;
        rtbOutput.Size = new Size(626, 235);
        rtbOutput.TabIndex = 0;
        rtbOutput.Text = "";
        // 
        // tbInput
        // 
        tbInput.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        tbInput.Location = new Point(24, 44);
        tbInput.Name = "tbInput";
        tbInput.Size = new Size(411, 23);
        tbInput.TabIndex = 1;
        tbInput.Text = "What are the names of the planets?";
        // 
        // btnSend
        // 
        btnSend.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnSend.Location = new Point(575, 26);
        btnSend.Name = "btnSend";
        btnSend.Size = new Size(75, 49);
        btnSend.TabIndex = 3;
        btnSend.Text = "Send";
        btnSend.UseVisualStyleBackColor = true;
        // 
        // nudTemperature
        // 
        nudTemperature.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        nudTemperature.DecimalPlaces = 1;
        nudTemperature.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
        nudTemperature.Location = new Point(475, 45);
        nudTemperature.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
        nudTemperature.Name = "numericUpDown1";
        nudTemperature.Size = new Size(58, 23);
        nudTemperature.TabIndex = 5;
        nudTemperature.Value = new decimal(new int[] { 6, 0, 0, 65536 });
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Location = new Point(24, 26);
        label1.Name = "label1";
        label1.Size = new Size(47, 15);
        label1.TabIndex = 6;
        label1.Text = "Prompt";
        // 
        // label2
        // 
        label2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        label2.AutoSize = true;
        label2.Location = new Point(467, 26);
        label2.Name = "label2";
        label2.Size = new Size(73, 15);
        label2.TabIndex = 7;
        label2.Text = "Temperature";
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(676, 358);
        Controls.Add(label2);
        Controls.Add(label1);
        Controls.Add(btnSend);
        Controls.Add(tbInput);
        Controls.Add(nudTemperature);
        Controls.Add(rtbOutput);
        Name = "Form1";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Form1";
        ((System.ComponentModel.ISupportInitialize)nudTemperature).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private RichTextBox rtbOutput;
    private TextBox tbInput;
    private Button btnSend;
    private NumericUpDown nudTemperature;
    private Label label1;
    private Label label2;
}
