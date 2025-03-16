Imports System.Data.SqlClient
Public Class استعراض_ملف_الفضلاء
    Dim query As String
    Dim connectionString As String

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Me.Hide()
        تقرير_ملف_الفضلاء.Show()
    End Sub

    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click
        Me.Hide()
        تقرير_ملف_الفضلاء.Show()
    End Sub


End Class
   