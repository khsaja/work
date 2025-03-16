Imports System.Data.OleDb
Public Class واجهة_عرض_طلبات_استرداد_كلمة_المرور


    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click
        Me.Hide()
        الدخول__بصفة_مسؤول.Show()
    End Sub

    Dim conn As OleDbConnection
    Dim da As OleDbDataAdapter
    Dim dt As DataTable

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' تحديد مسار قاعدة البيانات
        Dim dbPath As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\SK\Documents\white_hand.accdb"
        Dim connString As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & dbPath & ";"

        ' إنشاء الاتصال
        conn = New OleDbConnection(connString)

        ' تحميل البيانات عند فتح الفورم
        LoadData()
    End Sub

    Private Sub LoadData()
        Try
            conn.Open()
            Dim query As String = "SELECT * FROM reset" ' استبدلي table_name باسم الجدول
            da = New OleDbDataAdapter(query, conn)
            dt = New DataTable()
            da.Fill(dt)

            ' عرض البيانات في DataGridView
            DataGridView1.DataSource = dt
        Catch ex As Exception
            MessageBox.Show("خطأ في الاتصال: " & ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub


End Class