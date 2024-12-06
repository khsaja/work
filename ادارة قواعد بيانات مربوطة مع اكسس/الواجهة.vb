Public Class الواجهة

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim frm As New الادارات
        frm.Show()  ' عرض الواجهة الثانية
        Me.Hide()   ' إخفاء الواجهة الحالية

    End Sub

    
End Class
