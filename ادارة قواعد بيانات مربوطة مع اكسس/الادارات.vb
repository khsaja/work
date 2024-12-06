Imports System.IO

Public Class الادارات
    'اضهار الادارات
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'الماكنة
        ' إظهار الفورم التالية
        Dim frm As New الماكنة
        frm.Show()  ' عرض الواجهة التالبة
        Me.Hide()   ' إخفاء الواجهة الحالية

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'ادارةمكتب
        Dim frm As New مكتب
        frm.Show()  ' عرض الواجهة التالية
        Me.Hide()   ' إخفاء الواجهة الحالية

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        'ادارة الطلبات
        Dim frm As New Form18
        frm.Show()  ' عرض الواجهة الخامسة
        Me.Hide()   ' إخفاء الواجهة الحالية

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        'ادارةالتنضيمات
        Dim frm As New التنظيمات
        frm.Show()  ' عرض الواجهة الخامسة
        Me.Hide()   ' إخفاء الواجهة الحالية
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        'ادارة  المراقبين
        Dim frm As New باسورد_المراقبين
        frm.Show()  ' عرض الواجهة الخامسة
        Me.Hide()   ' إخفاء الواجهة الحالية

    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        Me.Hide()
        الواجهة.Show()
    End Sub
End Class
   