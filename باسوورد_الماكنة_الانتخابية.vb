Public Class باسوورد_الماكنة_الانتخابية

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim password As String = "1234" ' كلمة المرور الصحيحة
        If TextBox1.Text = password Then
            ' فتح الواجهة 
            Dim form13 As New ملف_الماكنة_الانتخابية()
            form13.Show()
            Me.Hide() ' إخفاء الواجهة الحالية
        Else
            MessageBox.Show("كلمة المرور غير صحيحة!", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        TextBox1.UseSystemPasswordChar = True

    End Sub
End Class