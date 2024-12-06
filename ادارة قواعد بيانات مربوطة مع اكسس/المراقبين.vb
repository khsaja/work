Public Class المراقبين

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim frm As New باسورد_المراقبين
        frm.Show()  ' عرض الواجهة التالبة
        Me.Hide()   ' إخفاء الواجهة الحالية

        Me.Close()

    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        Me.Hide()
        الادارات.Show()
    End Sub
End Class