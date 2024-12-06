Imports System.Data.OleDb
Public Class بطائق
    ' سلسلة الاتصال بقاعدة بيانات Access
    Private connectionString As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\NSR\Documents\alliance.accdb;"
    Private dataAdapter As OleDbDataAdapter
    Private commandBuilder As OleDbCommandBuilder
    Private dataTable As DataTable

    ' تحميل البيانات من قاعدة البيانات عند تحميل الفورم
    Private Sub Form8_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadData()
    End Sub
    ' دالة لتحميل البيانات من قاعدة البيانات إلى DataGridView
    Private Sub LoadData()
        Try
            ' استعلام لجلب البيانات من جدول Users
            Dim query As String = "SELECT * FROM بطائق"
            dataAdapter = New OleDbDataAdapter(query, connectionString)
            commandBuilder = New OleDbCommandBuilder(dataAdapter) ' يقوم بإنشاء أوامر التحديث التلقائي
            dataTable = New DataTable()

            ' ملء DataTable بالبيانات من قاعدة البيانات
            dataAdapter.Fill(dataTable)
            ' ربط DataGridView بالبيانات
            DataGridView1.DataSource = dataTable
        Catch ex As Exception
            MessageBox.Show("حدث خطأ أثناء تحميل البيانات: " & ex.Message)
        End Try
    End Sub
    ' دالة لحفظ التعديلات من DataGridView إلى قاعدة البيانات
    Private Sub Button1_Click(sender As Object, e As EventArgs)
        Try
            ' تحديث قاعدة البيانات باستخدام التعديلات الموجودة في DataTable
            dataAdapter.Update(dataTable)
            MessageBox.Show("تم حفظ البيانات بنجاح!")
        Catch ex As Exception
            MessageBox.Show("حدث خطأ أثناء حفظ البيانات: " & ex.Message)
        End Try

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs)
        Dim frm As New تقرير_الناخبين
        frm.Show()  ' عرض الواجهة التالية
        Me.Hide()   ' إخفاء الواجهة الحالية

        Me.Close()
    End Sub

    
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
      
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
       
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
       
    End Sub

    
    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click
        Me.Hide()
        الماكنة.Show()
    End Sub
End Class