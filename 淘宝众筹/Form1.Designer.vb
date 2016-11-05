<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
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
        Me.WebBrowser2 = New System.Windows.Forms.WebBrowser()
        Me.WebBrowser1 = New System.Windows.Forms.WebBrowser()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Button2 = New System.Windows.Forms.Button()
        Me.WebBrowser3 = New System.Windows.Forms.WebBrowser()
        Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
        Me.RadioButton1 = New System.Windows.Forms.RadioButton()
        Me.RadioButton2 = New System.Windows.Forms.RadioButton()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Button3 = New System.Windows.Forms.Button()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'WebBrowser2
        '
        Me.WebBrowser2.Location = New System.Drawing.Point(981, 30)
        Me.WebBrowser2.MinimumSize = New System.Drawing.Size(20, 20)
        Me.WebBrowser2.Name = "WebBrowser2"
        Me.WebBrowser2.ScriptErrorsSuppressed = True
        Me.WebBrowser2.Size = New System.Drawing.Size(628, 419)
        Me.WebBrowser2.TabIndex = 10
        Me.WebBrowser2.Url = New System.Uri("http://wan.meizu.com/", System.UriKind.Absolute)
        '
        'WebBrowser1
        '
        Me.WebBrowser1.Location = New System.Drawing.Point(5, 38)
        Me.WebBrowser1.MinimumSize = New System.Drawing.Size(20, 20)
        Me.WebBrowser1.Name = "WebBrowser1"
        Me.WebBrowser1.ScriptErrorsSuppressed = True
        Me.WebBrowser1.Size = New System.Drawing.Size(970, 709)
        Me.WebBrowser1.TabIndex = 9
        Me.WebBrowser1.Url = New System.Uri("http://z.jd.com/bigger/search.html?categoryId=10", System.UriKind.Absolute)
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.Crimson
        Me.Button1.Enabled = False
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.ForeColor = System.Drawing.Color.White
        Me.Button1.Location = New System.Drawing.Point(1441, 1)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 8
        Me.Button1.Text = "开始填表"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'TextBox1
        '
        Me.TextBox1.BackColor = System.Drawing.Color.Crimson
        Me.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBox1.ForeColor = System.Drawing.Color.White
        Me.TextBox1.Location = New System.Drawing.Point(159, 6)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(1276, 14)
        Me.TextBox1.TabIndex = 7
        Me.TextBox1.Text = "Input the URL."
        '
        'Timer1
        '
        Me.Timer1.Interval = 1000
        '
        'Button2
        '
        Me.Button2.BackColor = System.Drawing.Color.Crimson
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button2.ForeColor = System.Drawing.Color.White
        Me.Button2.Location = New System.Drawing.Point(1517, 1)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(75, 23)
        Me.Button2.TabIndex = 11
        Me.Button2.Text = "登录"
        Me.Button2.UseVisualStyleBackColor = False
        '
        'WebBrowser3
        '
        Me.WebBrowser3.Location = New System.Drawing.Point(981, 461)
        Me.WebBrowser3.MinimumSize = New System.Drawing.Size(20, 20)
        Me.WebBrowser3.Name = "WebBrowser3"
        Me.WebBrowser3.ScriptErrorsSuppressed = True
        Me.WebBrowser3.Size = New System.Drawing.Size(628, 286)
        Me.WebBrowser3.TabIndex = 12
        Me.WebBrowser3.Url = New System.Uri("http://wan.meizu.com", System.UriKind.Absolute)
        '
        'Timer2
        '
        Me.Timer2.Interval = 1000
        '
        'RadioButton1
        '
        Me.RadioButton1.AutoSize = True
        Me.RadioButton1.BackColor = System.Drawing.Color.Crimson
        Me.RadioButton1.Checked = True
        Me.RadioButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.RadioButton1.ForeColor = System.Drawing.Color.White
        Me.RadioButton1.Location = New System.Drawing.Point(5, 5)
        Me.RadioButton1.Name = "RadioButton1"
        Me.RadioButton1.Size = New System.Drawing.Size(70, 16)
        Me.RadioButton1.TabIndex = 13
        Me.RadioButton1.TabStop = True
        Me.RadioButton1.Text = "淘宝众筹"
        Me.RadioButton1.UseVisualStyleBackColor = False
        '
        'RadioButton2
        '
        Me.RadioButton2.AutoSize = True
        Me.RadioButton2.BackColor = System.Drawing.Color.Crimson
        Me.RadioButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.RadioButton2.ForeColor = System.Drawing.Color.White
        Me.RadioButton2.Location = New System.Drawing.Point(82, 5)
        Me.RadioButton2.Name = "RadioButton2"
        Me.RadioButton2.Size = New System.Drawing.Size(70, 16)
        Me.RadioButton2.TabIndex = 14
        Me.RadioButton2.Text = "京东众筹"
        Me.RadioButton2.UseVisualStyleBackColor = False
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.Crimson
        Me.PictureBox1.Location = New System.Drawing.Point(-10, -23)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(1623, 48)
        Me.PictureBox1.TabIndex = 15
        Me.PictureBox1.TabStop = False
        '
        'Button3
        '
        Me.Button3.BackColor = System.Drawing.Color.Crimson
        Me.Button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button3.ForeColor = System.Drawing.Color.White
        Me.Button3.Location = New System.Drawing.Point(1593, 1)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(19, 23)
        Me.Button3.TabIndex = 16
        Me.Button3.Text = "X"
        Me.Button3.UseVisualStyleBackColor = False
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1614, 759)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.RadioButton2)
        Me.Controls.Add(Me.RadioButton1)
        Me.Controls.Add(Me.WebBrowser3)
        Me.Controls.Add(Me.WebBrowser2)
        Me.Controls.Add(Me.WebBrowser1)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.PictureBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "淘宝众筹自动填表"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents WebBrowser2 As WebBrowser
    Friend WithEvents WebBrowser1 As WebBrowser
    Friend WithEvents Button1 As Button
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Timer1 As Timer
    Friend WithEvents Button2 As Button
    Friend WithEvents WebBrowser3 As WebBrowser
    Friend WithEvents Timer2 As Timer
    Friend WithEvents RadioButton1 As RadioButton
    Friend WithEvents RadioButton2 As RadioButton
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Button3 As Button
End Class
