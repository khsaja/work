Public Class باسورد_الرياضة

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim password As String = "1234" ' كلمة المرور الصحيحة
        If TextBox1.Text = password Then
            ' فتح الواجهة رقم 14
            Dim form14 As New الرياضة()
            form14.Show()
            Me.Hide() ' إخفاء الواجهة الحالية
        Else
            MessageBox.Show("كلمة المرور غير صحيحة!", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If

    End Sub
    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs)
        TextBox1.UseSystemPasswordChar = True
    End Sub

End Class