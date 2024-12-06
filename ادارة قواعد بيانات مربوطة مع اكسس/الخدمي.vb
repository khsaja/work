Imports System.Data.OleDb
Public Class الخدمي
   
    Private connectionString As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\SK\Desktop\app\ادارة قواعد بيانات مربوطة مع اكسس\alliance.accdb;"
    Private connection As OleDbConnection
    Private adapter As OleDbDataAdapter
    Private dataSet As DataSet
    Private bindingSource As BindingSource
  

    ' تحميل البيانات عند فتح النموذج
    Private Sub الخدمي_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'AllianceDataSet2.الكسب_للخدمي' table. You can move, or remove it, as needed.
        Me.الكسب_للخدميTableAdapter.Fill(Me.AllianceDataSet2.الكسب_للخدمي)
        LoadData()

    End Sub

    ' تحميل البيانات إلى DataGridView
    Private Sub LoadData()
        Try
            ' 1. إنشاء اتصال بقاعدة البيانات
            connection = New OleDbConnection(connectionString)

            ' 2. إنشاء أمر SQL لجلب البيانات من الجدول
            Dim query As String = "SELECT * FROM khadami" ' استبدل YourTableName باسم الجدول

            ' 3. إعداد DataAdapter لجلب البيانات
            adapter = New OleDbDataAdapter(query, connection)

            ' 4. ملء DataSet بالبيانات
            dataSet = New DataSet()
            adapter.Fill(dataSet, "khadami")

            ' 5. إعداد BindingSource وربطه بالبيانات
            bindingSource = New BindingSource()
            bindingSource.DataSource = dataSet.Tables("khadami")

            ' 6. ربط DataGridView بمصدر البيانات
            DataGridView1.DataSource = bindingSource

            ' 7. ربط BindingNavigator بمصدر البيانات
            BindingNavigator1.BindingSource = bindingSource

        Catch ex As Exception
            ' معالجة الأخطاء
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            ' 1. فتح الاتصال
            connection.Open()

            ' 2. إعداد أمر SQL لإدخال بيانات جديدة
            Dim query As String = "INSERT INTO khadami (ID, Name, Phone1,Phone2,Address,Tributary,Birthdate,JoiningDate,Attribute,NumberOfFamilyMembers,ReferencePoint,today,data) VALUES (@ID, @Name, @Phone1,@Phone2,@Address,@Tributary,@Birthdate,@JoiningDate,@Attribute,@NumberOfFamilyMembers,@ReferencePoint,@today,@data)"
            Dim command As New OleDbCommand(query, connection)

            ' 3. إضافة القيم من TextBox إلى المعلمات
            command.Parameters.AddWithValue("@ID", TextBox14.Text)
            command.Parameters.AddWithValue("@Name", TextBox2.Text)
            command.Parameters.AddWithValue("@Phone1", TextBox3.Text)
            command.Parameters.AddWithValue("@Phone2", TextBox4.Text)
            command.Parameters.AddWithValue("@Address", TextBox5.Text)
            command.Parameters.AddWithValue("@Tributary", TextBox6.Text)
            command.Parameters.AddWithValue("@Birthdate", TextBox7.Text)
            command.Parameters.AddWithValue("@JoiningDate", TextBox8.Text)
            command.Parameters.AddWithValue("@Attribute", TextBox9.Text)
            command.Parameters.AddWithValue("@NumberOfFamilyMembers", TextBox10.Text)
            command.Parameters.AddWithValue("@ReferencePoint", TextBox11.Text)
            command.Parameters.AddWithValue("@today", TextBox12.Text)
            command.Parameters.AddWithValue("@data", TextBox13.Text)

            ' 4. تنفيذ الأمر
            Dim rowsAffected As Integer = command.ExecuteNonQuery()

            ' 5. تحديث DataSet وDataGridView بعد الإضافة
            If rowsAffected > 0 Then
                MessageBox.Show("تمت الإضافة بنجاح!")
                dataSet.Tables("khadami").Clear()
                adapter.Fill(dataSet, "khadami")
            End If

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        Finally
            ' 6. إغلاق الاتصال
            connection.Close()
        End Try
    End Sub


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            ' التأكد من أن المستخدم قد أدخل ID للسجل الذي يريد حذفه
            If String.IsNullOrEmpty(TextBox14.Text) Then
                MessageBox.Show("من فضلك أدخل ID السجل الذي تريد حذفه.")
                Return
            End If

            ' 1. فتح الاتصال
            connection.Open()

            ' 2. إعداد أمر SQL لحذف السجل بناءً على ID
            Dim query As String = "DELETE FROM khadami WHERE ID = @ID"
            Dim command As New OleDbCommand(query, connection)

            ' 3. إضافة المعلمة الخاصة بالـ ID
            command.Parameters.AddWithValue("@ID", TextBox14.Text)

            ' 4. تنفيذ الأمر
            Dim rowsAffected As Integer = command.ExecuteNonQuery()

            ' 5. التأكد من نجاح عملية الحذف
            If rowsAffected > 0 Then
                MessageBox.Show("تم الحذف بنجاح!")
                LoadData() ' إعادة تحميل البيانات بعد الحذف
            Else
                MessageBox.Show("لم يتم العثور على السجل.")
            End If

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        Finally
            ' 6. إغلاق الاتصال
            connection.Close()
        End Try
    End Sub



    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Try
            ' التأكد من أن المستخدم قد أدخل ID للسجل الذي يريد تعديله
            If String.IsNullOrEmpty(TextBox14.Text) Then
                MessageBox.Show("من فضلك أدخل ID السجل الذي تريد تعديله.")
                Return
            End If
            Dim query As String = "INSERT INTO khadami (ID, Name, Phone1,Phone2,Address,Tributary,Birthdate,JoiningDate,Attribute,NumberOfFamilyMembers,ReferencePoint,today,data) VALUES (@ID, @Name, @Phone1,@Phone2,@Address,@Tributary,@Birthdate,@JoiningDate,@Attribute,@NumberOfFamilyMembers,@ReferencePoint,@today,@data)"
            Dim command As New OleDbCommand(query, connection)

            ' 3. إضافة القيم من TextBox إلى المعلمات
            command.Parameters.AddWithValue("@ID", TextBox14.Text)
            command.Parameters.AddWithValue("@Name", TextBox2.Text)
            command.Parameters.AddWithValue("@Phone1", TextBox3.Text)
            command.Parameters.AddWithValue("@Phone2", TextBox4.Text)
            command.Parameters.AddWithValue("@Address", TextBox5.Text)
            command.Parameters.AddWithValue("@Tributary", TextBox6.Text)
            command.Parameters.AddWithValue("@Birthdate", TextBox7.Text)
            command.Parameters.AddWithValue("@JoiningDate", TextBox8.Text)
            command.Parameters.AddWithValue("@Attribute", TextBox9.Text)
            command.Parameters.AddWithValue("@NumberOfFamilyMembers", TextBox10.Text)
            command.Parameters.AddWithValue("@ReferencePoint", TextBox11.Text)
            command.Parameters.AddWithValue("@today", TextBox12.Text)
            command.Parameters.AddWithValue("@data", TextBox13.Text)

            Dim rowsAffected As Integer = command.ExecuteNonQuery()

            ' 5. تحديث DataSet وDataGridView بعد التحديث
            If rowsAffected > 0 Then
                MessageBox.Show("تم التحديث بنجاح!")
                LoadData() ' إعادة تحميل البيانات بعد التحديث
            End If

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        Finally
            ' 6. إغلاق الاتصال
            connection.Close()
        End Try
            ' 1. فتح الاتصال
            connection.Open()
    End Sub
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim query As String = "SELECT * FROM khadami WHERE ID LIKE @SearchTerm"
        Dim command As New OleDbCommand(query, connection)

        ' 4. إضافة قيمة البحث إلى المعلمة
        command.Parameters.AddWithValue("@SearchTerm", "%" & TextBox14.Text & "%") ' فرضًا لديك TextBoxSearch للبحث

        ' 5. استخدام DataAdapter لإعادة تحميل البيانات بناءً على استعلام البحث
        adapter = New OleDbDataAdapter(command)
        dataSet = New DataSet()
        adapter.Fill(dataSet, "khadami")

        ' 6. تحديث DataGridView
        DataGridView1.DataSource = dataSet.Tables("khadami")

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        Finally
            ' 7. إغلاق الاتصال
            connection.Close()
        End Try
    End Sub


   

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim frm As New تقرير_الخدمي
        frm.Show()  ' عرض الواجهة الثانية
        Me.Hide()   ' إخفاء الواجهة الحالية

        Me.Close()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim frm As New الطباعة
        frm.Show()  ' عرض الواجهة الثانية
        Me.Hide()   ' إخفاء الواجهة الحالية

        Me.Close()

    End Sub


    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click
        Me.Hide()   ' إخفاء الواجهة الحالية
        الماكنة.Show()
    End Sub
  
    Private Sub Label18_Click(sender As Object, e As EventArgs) Handles Label18.Click

    End Sub
End Class