Public Class Form18
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim frm As New باسورد_متابعة_طلبات
        frm.Show()  ' عرض الواجهة التالية
        Me.Hide()   ' إخفاء الواجهة الحالية

        Me.Close()
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        Me.Hide()
        الادارات.Show()
    End Sub
End Class