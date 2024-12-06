Public Class التنظيمات

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        'ادارة التنضيمات
        'ملف الكفاءات
        Dim frm As New باسورد_الكفاءات
        frm.Show()  ' عرض الواجهة الثانية
        Me.Hide()   ' إخفاء الواجهة الحالية

        Me.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'ملف الشباب
       
        Dim frm As New باسورد_الشباب
        frm.Show()  ' عرض الواجهة الثانية
        Me.Hide()   ' إخفاء الواجهة الحالية

        Me.Close()

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
       
        Dim frm As New باسورد_الرياضة
        frm.Show()  ' عرض الواجهة الثانية
        Me.Hide()   ' إخفاء الواجهة الحالية

        Me.Close()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim frm As New باسورد_العشائر
        frm.Show()  ' عرض الواجهة الثانية
        Me.Hide()   ' إخفاء الواجهة الحالية

        Me.Close()

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
      
        Dim frm As New باسورد_النسوي
        frm.Show()  ' عرض الواجهة الثانية
        Me.Hide()   ' إخفاء الواجهة الحالية

        Me.Close()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
       

        Dim frm As New باسورد_عكبة
        frm.Show()  ' عرض الواجهة الثانية
        Me.Hide()   ' إخفاء الواجهة الحالية

        Me.Close()
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        Me.Hide()
        الادارات.Show()
    End Sub
End Class