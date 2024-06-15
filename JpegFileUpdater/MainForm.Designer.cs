namespace JpegFileUpdater;

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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
        FileListTextBox = new TextBox();
        ClearButton = new Button();
        SortButton = new Button();
        GroupBox1 = new GroupBox();
        label2 = new Label();
        SecondsTextBox = new TextBox();
        BaseDateTimePicker = new DateTimePicker();
        UpdateButton = new Button();
        label3 = new Label();
        ExifToolPathTextBox = new TextBox();
        statusStrip1 = new StatusStrip();
        StatusLabel = new ToolStripStatusLabel();
        groupBox2 = new GroupBox();
        RenameButton = new Button();
        label6 = new Label();
        label5 = new Label();
        StartNumberTextBox = new TextBox();
        DigitTextBox = new TextBox();
        label4 = new Label();
        BaseFileNameTextBox = new TextBox();
        label1 = new Label();
        GroupBox1.SuspendLayout();
        statusStrip1.SuspendLayout();
        groupBox2.SuspendLayout();
        SuspendLayout();
        // 
        // FileListTextBox
        // 
        FileListTextBox.Location = new Point(12, 12);
        FileListTextBox.Multiline = true;
        FileListTextBox.Name = "FileListTextBox";
        FileListTextBox.ScrollBars = ScrollBars.Vertical;
        FileListTextBox.Size = new Size(610, 396);
        FileListTextBox.TabIndex = 0;
        FileListTextBox.TextChanged += FileListTextBox_TextChanged;
        // 
        // ClearButton
        // 
        ClearButton.Location = new Point(628, 12);
        ClearButton.Name = "ClearButton";
        ClearButton.Size = new Size(160, 23);
        ClearButton.TabIndex = 1;
        ClearButton.Text = "クリア";
        ClearButton.UseVisualStyleBackColor = true;
        ClearButton.Click += ClearButton_Click;
        // 
        // SortButton
        // 
        SortButton.Location = new Point(628, 41);
        SortButton.Name = "SortButton";
        SortButton.Size = new Size(160, 23);
        SortButton.TabIndex = 2;
        SortButton.Text = "ソート";
        SortButton.UseVisualStyleBackColor = true;
        SortButton.Click += SortButton_Click;
        // 
        // GroupBox1
        // 
        GroupBox1.Controls.Add(label2);
        GroupBox1.Controls.Add(SecondsTextBox);
        GroupBox1.Controls.Add(BaseDateTimePicker);
        GroupBox1.Controls.Add(UpdateButton);
        GroupBox1.Location = new Point(628, 284);
        GroupBox1.Name = "GroupBox1";
        GroupBox1.Size = new Size(160, 124);
        GroupBox1.TabIndex = 4;
        GroupBox1.TabStop = false;
        GroupBox1.Text = "新しい日付";
        // 
        // label2
        // 
        label2.AutoSize = true;
        label2.Location = new Point(112, 54);
        label2.Name = "label2";
        label2.Size = new Size(43, 15);
        label2.TabIndex = 2;
        label2.Text = "秒間隔";
        // 
        // SecondsTextBox
        // 
        SecondsTextBox.Location = new Point(6, 51);
        SecondsTextBox.Name = "SecondsTextBox";
        SecondsTextBox.Size = new Size(100, 23);
        SecondsTextBox.TabIndex = 1;
        SecondsTextBox.TextAlign = HorizontalAlignment.Right;
        SecondsTextBox.TextChanged += SecondsTextBox_TextChanged;
        // 
        // BaseDateTimePicker
        // 
        BaseDateTimePicker.CustomFormat = "yyyy-MM-dd HH:mm:ss";
        BaseDateTimePicker.Format = DateTimePickerFormat.Custom;
        BaseDateTimePicker.Location = new Point(6, 22);
        BaseDateTimePicker.Name = "BaseDateTimePicker";
        BaseDateTimePicker.Size = new Size(148, 23);
        BaseDateTimePicker.TabIndex = 0;
        BaseDateTimePicker.Value = new DateTime(2024, 1, 1, 12, 0, 0, 0);
        BaseDateTimePicker.ValueChanged += BaseDateTimePicker_ValueChanged;
        // 
        // UpdateButton
        // 
        UpdateButton.Enabled = false;
        UpdateButton.Location = new Point(6, 95);
        UpdateButton.Name = "UpdateButton";
        UpdateButton.Size = new Size(148, 23);
        UpdateButton.TabIndex = 3;
        UpdateButton.Text = "ファイル日付更新";
        UpdateButton.UseVisualStyleBackColor = true;
        UpdateButton.Click += UpdateButton_Click;
        // 
        // label3
        // 
        label3.AutoSize = true;
        label3.Location = new Point(12, 417);
        label3.Name = "label3";
        label3.Size = new Size(77, 15);
        label3.TabIndex = 5;
        label3.Text = "ExifToolのパス";
        // 
        // ExifToolPathTextBox
        // 
        ExifToolPathTextBox.Location = new Point(12, 435);
        ExifToolPathTextBox.Name = "ExifToolPathTextBox";
        ExifToolPathTextBox.Size = new Size(776, 23);
        ExifToolPathTextBox.TabIndex = 6;
        ExifToolPathTextBox.TextChanged += ExifToolPathTextBox_TextChanged;
        // 
        // statusStrip1
        // 
        statusStrip1.Items.AddRange(new ToolStripItem[] { StatusLabel });
        statusStrip1.Location = new Point(0, 469);
        statusStrip1.Name = "statusStrip1";
        statusStrip1.Size = new Size(800, 22);
        statusStrip1.SizingGrip = false;
        statusStrip1.TabIndex = 7;
        statusStrip1.Text = "statusStrip1";
        // 
        // StatusLabel
        // 
        StatusLabel.Name = "StatusLabel";
        StatusLabel.Size = new Size(0, 17);
        // 
        // groupBox2
        // 
        groupBox2.Controls.Add(RenameButton);
        groupBox2.Controls.Add(label6);
        groupBox2.Controls.Add(label5);
        groupBox2.Controls.Add(StartNumberTextBox);
        groupBox2.Controls.Add(DigitTextBox);
        groupBox2.Controls.Add(label4);
        groupBox2.Controls.Add(BaseFileNameTextBox);
        groupBox2.Controls.Add(label1);
        groupBox2.Location = new Point(629, 95);
        groupBox2.Name = "groupBox2";
        groupBox2.Size = new Size(159, 183);
        groupBox2.TabIndex = 3;
        groupBox2.TabStop = false;
        groupBox2.Text = "新しいファイル名";
        // 
        // RenameButton
        // 
        RenameButton.Location = new Point(6, 154);
        RenameButton.Name = "RenameButton";
        RenameButton.Size = new Size(147, 23);
        RenameButton.TabIndex = 7;
        RenameButton.Text = "リネーム";
        RenameButton.UseVisualStyleBackColor = true;
        RenameButton.Click += RenameButton_Click;
        // 
        // label6
        // 
        label6.AutoSize = true;
        label6.Location = new Point(112, 113);
        label6.Name = "label6";
        label6.Size = new Size(26, 15);
        label6.TabIndex = 6;
        label6.Text = "から";
        // 
        // label5
        // 
        label5.AutoSize = true;
        label5.Location = new Point(112, 84);
        label5.Name = "label5";
        label5.Size = new Size(19, 15);
        label5.TabIndex = 4;
        label5.Text = "桁";
        // 
        // StartNumberTextBox
        // 
        StartNumberTextBox.Location = new Point(6, 110);
        StartNumberTextBox.Name = "StartNumberTextBox";
        StartNumberTextBox.Size = new Size(100, 23);
        StartNumberTextBox.TabIndex = 5;
        StartNumberTextBox.TextChanged += StartNumberTextBox_TextChanged;
        // 
        // DigitTextBox
        // 
        DigitTextBox.Location = new Point(6, 81);
        DigitTextBox.Name = "DigitTextBox";
        DigitTextBox.Size = new Size(100, 23);
        DigitTextBox.TabIndex = 3;
        DigitTextBox.TextChanged += DigitTextBox_TextChanged;
        // 
        // label4
        // 
        label4.AutoSize = true;
        label4.Location = new Point(6, 63);
        label4.Name = "label4";
        label4.Size = new Size(31, 15);
        label4.TabIndex = 2;
        label4.Text = "連番";
        // 
        // BaseFileNameTextBox
        // 
        BaseFileNameTextBox.Location = new Point(6, 37);
        BaseFileNameTextBox.Name = "BaseFileNameTextBox";
        BaseFileNameTextBox.Size = new Size(147, 23);
        BaseFileNameTextBox.TabIndex = 1;
        BaseFileNameTextBox.TextChanged += BaseFileNameTextBox_TextChanged;
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Location = new Point(6, 19);
        label1.Name = "label1";
        label1.Size = new Size(58, 15);
        label1.TabIndex = 0;
        label1.Text = "ベース名称";
        // 
        // MainForm
        // 
        AllowDrop = true;
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(800, 491);
        Controls.Add(groupBox2);
        Controls.Add(statusStrip1);
        Controls.Add(ExifToolPathTextBox);
        Controls.Add(label3);
        Controls.Add(GroupBox1);
        Controls.Add(SortButton);
        Controls.Add(ClearButton);
        Controls.Add(FileListTextBox);
        Icon = (Icon)resources.GetObject("$this.Icon");
        Name = "MainForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "JPEGファイル更新ツール";
        Load += MainForm_Load;
        DragDrop += MainForm_DragDrop;
        DragEnter += MainForm_DragEnter;
        GroupBox1.ResumeLayout(false);
        GroupBox1.PerformLayout();
        statusStrip1.ResumeLayout(false);
        statusStrip1.PerformLayout();
        groupBox2.ResumeLayout(false);
        groupBox2.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private TextBox FileListTextBox;
    private Button ClearButton;
    private Button SortButton;
    private GroupBox GroupBox1;
    private Button UpdateButton;
    private DateTimePicker BaseDateTimePicker;
    private Label label2;
    private TextBox SecondsTextBox;
    private Label label3;
    private TextBox ExifToolPathTextBox;
    private StatusStrip statusStrip1;
    private ToolStripStatusLabel StatusLabel;
    private GroupBox groupBox2;
    private TextBox BaseFileNameTextBox;
    private Label label1;
    private Button RenameButton;
    private Label label6;
    private Label label5;
    private TextBox StartNumberTextBox;
    private TextBox DigitTextBox;
    private Label label4;
}
