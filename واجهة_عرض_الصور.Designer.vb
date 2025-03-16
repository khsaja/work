<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class واجهة_عرض_الصور
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.PictureBoxshow = New System.Windows.Forms.PictureBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        CType(Me.PictureBoxshow, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PictureBoxshow
        '
        Me.PictureBoxshow.BackColor = System.Drawing.Color.White
        Me.PictureBoxshow.Location = New System.Drawing.Point(249, 107)
        Me.PictureBoxshow.Name = "PictureBoxshow"
        Me.PictureBoxshow.Size = New System.Drawing.Size(1061, 659)
        Me.PictureBoxshow.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBoxshow.TabIndex = 0
        Me.PictureBoxshow.TabStop = False
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("Times New Roman", 16.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Location = New System.Drawing.Point(836, 30)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(382, 42)
        Me.Button1.TabIndex = 1
        Me.Button1.Text = "تفريغ الصورة"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Font = New System.Drawing.Font("Times New Roman", 16.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.Location = New System.Drawing.Point(359, 30)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(382, 42)
        Me.Button2.TabIndex = 2
        Me.Button2.Text = "العودة للصورة السابقة"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'واجهة_عرض_الصور
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.ClientSize = New System.Drawing.Size(1547, 802)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.PictureBoxshow)
        Me.Name = "واجهة_عرض_الصور"
        Me.Text = "واجهة_عرض_الصور"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.PictureBoxshow, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PictureBoxshow As System.Windows.Forms.PictureBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
End Class
