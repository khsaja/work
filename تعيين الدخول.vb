Public Class تعيين_الدخول

    Private Sub تعيين_الدخول_Load(sender As Object, e As EventArgs)
        'Me.WindowState = FormWindowState.Maximized
    End Sub

    

    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click
        Me.Hide()
        الواجهة.Show()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Hide()
        الملفات.Show()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Hide()
        الدخول__بصفة_مسؤول.Show()

    End Sub
End Class