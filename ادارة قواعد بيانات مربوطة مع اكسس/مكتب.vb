Public Class مكتب

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'ادارة_ مكتب
        'المواعيد
        Dim frm As New باسورد_المواعيد
        frm.Show()  ' عرض الواجهة الثالثة
        Me.Hide()   ' إخفاء الواجهة الحالية

        Me.Close()
       
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'الضيوف
        Dim frm As New باسورد_الضيوف
        frm.Show()  ' عرض الواجهة الثالثة
        Me.Hide()   ' إخفاء الواجهة الحالية


        Me.Close()

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        'الاعلام
        Dim frm As New باسورد_الاعلام
        frm.Show()  ' عرض الواجهة الثالثة
        Me.Hide()   ' إخفاء الواجهة الحالية


        Me.Close()
    End Sub

    Private Sub Form5_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        Me.Hide()
        الادارات.Show()
    End Sub
End Class