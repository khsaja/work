Public Class الماكنة
    'الماكنة الانتخابية 
    'الجمهور الخدمي 
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim frm As New باسورد_الخدمي
        frm.Show()  ' عرض الواجهة الثانية
        Me.Hide()   ' إخفاء الواجهة الحالية

        Me.Close()
    End Sub
    'الجمهور العقائدي

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim frm As New باسورد_العقائدي
        frm.Show()  ' عرض الواجهة الثانية
        Me.Hide()   ' إخفاء الواجهة الحالية

        Me.Close()

    End Sub
    'تحديث بطائق الناخبين 

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim frm As New باسورد_بطائق
        frm.Show()  ' عرض الواجهة الثانية
        Me.Hide()   ' إخفاء الواجهة الحالية
        Me.Close()
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        Me.Hide()
        الادارات.Show()
    End Sub
End Class