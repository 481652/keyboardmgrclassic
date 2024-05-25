<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form3
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。  
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form3))
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.新建ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.打开ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.保存ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.操作ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.添加新行AToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton2 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripButton3 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton4 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton5 = New System.Windows.Forms.ToolStripButton()
        Me.ListBox1 = New System.Windows.Forms.ListBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.MenuStrip1.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.BackColor = System.Drawing.Color.LightCyan
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem, Me.操作ToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(470, 25)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ToolStripMenuItem
        '
        Me.ToolStripMenuItem.BackColor = System.Drawing.Color.LightCyan
        Me.ToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.新建ToolStripMenuItem, Me.打开ToolStripMenuItem, Me.保存ToolStripMenuItem})
        Me.ToolStripMenuItem.Name = "ToolStripMenuItem"
        Me.ToolStripMenuItem.Size = New System.Drawing.Size(44, 21)
        Me.ToolStripMenuItem.Text = "文件"
        '
        '新建ToolStripMenuItem
        '
        Me.新建ToolStripMenuItem.Image = Global.keyboardmgr.My.Resources.Resources.AddFile_16x
        Me.新建ToolStripMenuItem.Name = "新建ToolStripMenuItem"
        Me.新建ToolStripMenuItem.Size = New System.Drawing.Size(182, 22)
        Me.新建ToolStripMenuItem.Text = "新建列表连发（&N）"
        '
        '打开ToolStripMenuItem
        '
        Me.打开ToolStripMenuItem.Image = Global.keyboardmgr.My.Resources.Resources.Open_16x
        Me.打开ToolStripMenuItem.Name = "打开ToolStripMenuItem"
        Me.打开ToolStripMenuItem.Size = New System.Drawing.Size(182, 22)
        Me.打开ToolStripMenuItem.Text = "打开列表连发（&O）"
        '
        '保存ToolStripMenuItem
        '
        Me.保存ToolStripMenuItem.Image = Global.keyboardmgr.My.Resources.Resources.Save_24x
        Me.保存ToolStripMenuItem.Name = "保存ToolStripMenuItem"
        Me.保存ToolStripMenuItem.Size = New System.Drawing.Size(182, 22)
        Me.保存ToolStripMenuItem.Text = "保存列表连发（&S）"
        '
        '操作ToolStripMenuItem
        '
        Me.操作ToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.添加新行AToolStripMenuItem})
        Me.操作ToolStripMenuItem.Name = "操作ToolStripMenuItem"
        Me.操作ToolStripMenuItem.Size = New System.Drawing.Size(44, 21)
        Me.操作ToolStripMenuItem.Text = "操作"
        '
        '添加新行AToolStripMenuItem
        '
        Me.添加新行AToolStripMenuItem.Name = "添加新行AToolStripMenuItem"
        Me.添加新行AToolStripMenuItem.Size = New System.Drawing.Size(140, 22)
        Me.添加新行AToolStripMenuItem.Text = "添加新行(&A)"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 25)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Button1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.TextBox1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.ToolStrip1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.ListBox1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.Label1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Button2)
        Me.SplitContainer1.Size = New System.Drawing.Size(470, 340)
        Me.SplitContainer1.SplitterDistance = 273
        Me.SplitContainer1.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(16, 232)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(44, 17)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "未选择"
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.PaleTurquoise
        Me.Button1.FlatAppearance.BorderColor = System.Drawing.Color.SkyBlue
        Me.Button1.FlatAppearance.BorderSize = 2
        Me.Button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.PaleTurquoise
        Me.Button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PaleTurquoise
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Font = New System.Drawing.Font("微软雅黑", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Button1.Location = New System.Drawing.Point(16, 128)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(96, 56)
        Me.Button1.TabIndex = 7
        Me.Button1.Text = "修改该列" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & " →"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(8, 32)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(116, 17)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "在这里编辑单行内容"
        '
        'TextBox1
        '
        Me.TextBox1.BackColor = System.Drawing.Color.PaleTurquoise
        Me.TextBox1.Location = New System.Drawing.Point(8, 56)
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.TextBox1.Size = New System.Drawing.Size(128, 56)
        Me.TextBox1.TabIndex = 5
        '
        'ToolStrip1
        '
        Me.ToolStrip1.BackColor = System.Drawing.Color.LightCyan
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton1, Me.ToolStripButton2, Me.ToolStripSeparator1, Me.ToolStripButton3, Me.ToolStripButton4, Me.ToolStripButton5})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(470, 25)
        Me.ToolStrip1.TabIndex = 1
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton1.Image = Global.keyboardmgr.My.Resources.Resources.Add_16x
        Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButton1.Text = "ToolStripButton1"
        Me.ToolStripButton1.ToolTipText = "在所选项后插入空白行"
        '
        'ToolStripButton2
        '
        Me.ToolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton2.Image = Global.keyboardmgr.My.Resources.Resources.Delete_16x
        Me.ToolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton2.Name = "ToolStripButton2"
        Me.ToolStripButton2.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButton2.Text = "ToolStripButton2"
        Me.ToolStripButton2.ToolTipText = "删除列"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'ToolStripButton3
        '
        Me.ToolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton3.Image = Global.keyboardmgr.My.Resources.Resources.Save_24x
        Me.ToolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton3.Name = "ToolStripButton3"
        Me.ToolStripButton3.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButton3.Text = "ToolStripButton3"
        Me.ToolStripButton3.ToolTipText = "保存列表连发"
        '
        'ToolStripButton4
        '
        Me.ToolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton4.Image = Global.keyboardmgr.My.Resources.Resources.Open_16x
        Me.ToolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton4.Name = "ToolStripButton4"
        Me.ToolStripButton4.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButton4.Text = "ToolStripButton4"
        Me.ToolStripButton4.ToolTipText = "打开列表连发"
        '
        'ToolStripButton5
        '
        Me.ToolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton5.Image = Global.keyboardmgr.My.Resources.Resources.AddFile_16x
        Me.ToolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton5.Name = "ToolStripButton5"
        Me.ToolStripButton5.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButton5.Text = "ToolStripButton5"
        Me.ToolStripButton5.ToolTipText = "新建列表连发"
        '
        'ListBox1
        '
        Me.ListBox1.BackColor = System.Drawing.Color.PaleTurquoise
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.HorizontalScrollbar = True
        Me.ListBox1.ItemHeight = 17
        Me.ListBox1.Location = New System.Drawing.Point(136, 32)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.ScrollAlwaysVisible = True
        Me.ListBox1.Size = New System.Drawing.Size(320, 225)
        Me.ListBox1.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(8, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(44, 17)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "操作区"
        '
        'Button2
        '
        Me.Button2.BackColor = System.Drawing.Color.PaleTurquoise
        Me.Button2.FlatAppearance.BorderColor = System.Drawing.Color.SkyBlue
        Me.Button2.FlatAppearance.BorderSize = 2
        Me.Button2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.PaleTurquoise
        Me.Button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PaleTurquoise
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button2.Font = New System.Drawing.Font("微软雅黑", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Button2.Location = New System.Drawing.Point(352, 8)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(96, 40)
        Me.Button2.TabIndex = 5
        Me.Button2.Text = "运行"
        Me.Button2.UseVisualStyleBackColor = False
        '
        'ToolTip1
        '
        Me.ToolTip1.AutoPopDelay = 1000
        Me.ToolTip1.BackColor = System.Drawing.Color.LightCyan
        Me.ToolTip1.InitialDelay = 500
        Me.ToolTip1.IsBalloon = True
        Me.ToolTip1.ReshowDelay = 0
        Me.ToolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.[Error]
        Me.ToolTip1.ToolTipTitle = "错误"
        '
        'SaveFileDialog1
        '
        Me.SaveFileDialog1.DefaultExt = "lcslst"
        Me.SaveFileDialog1.Filter = "LCS列表连发文件|*.lcslst"
        Me.SaveFileDialog1.Title = "保存列表连发文件"
        '
        'Form3
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 17.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LightCyan
        Me.ClientSize = New System.Drawing.Size(470, 365)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Font = New System.Drawing.Font("微软雅黑", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "Form3"
        Me.ShowInTaskbar = False
        Me.Text = "[列表未保存]列表连发编辑器"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents 新建ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents 打开ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents 保存ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ListBox1 As ListBox
    Friend WithEvents 操作ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents ToolStripButton1 As ToolStripButton
    Friend WithEvents ToolStripButton2 As ToolStripButton
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents ToolStripButton3 As ToolStripButton
    Friend WithEvents ToolStripButton4 As ToolStripButton
    Friend WithEvents ToolStripButton5 As ToolStripButton
    Friend WithEvents Button2 As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Button1 As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents SaveFileDialog1 As SaveFileDialog
    Friend WithEvents 添加新行AToolStripMenuItem As ToolStripMenuItem
End Class
