Imports System.Data.OleDb
Public Class واجهة_استعادة_كلمة_المرور
    Dim con As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\SK\Documents\white_hand.accdb;")

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        If TextBox1.Text = "" Or TextBox4.Text = "" Then
            MsgBox("يرجى إدخال جميع المعلومات", vbExclamation, "تنبيه")
            Exit Sub
        End If

        Try
            con.Open()
            Dim cmd As New OleDbCommand("INSERT INTO reset ( user_name , name_file,type_file ) VALUES (@user_name, @name_file, @type_file)", con)
            ', RequestDate, RequestStatus
            ', @date, 
            cmd.Parameters.AddWithValue("@user_name", TextBox1.Text)
            cmd.Parameters.AddWithValue("@name_file", TextBox4.Text)
            cmd.Parameters.AddWithValue("@type_file", TextBox2.Text)

            'cmd.Parameters.AddWithValue("@date", Now)
            'cmd.Parameters.AddWithValue("@status", "بانتظار المعالجة")
            cmd.ExecuteNonQuery()
            con.Close()

            MsgBox("تم إرسال الطلب إلى المسؤول", vbInformation, "نجاح")
            Me.Close()

        Catch ex As Exception
            MsgBox("حدث خطأ أثناء إرسال الطلب: " & ex.Message, vbCritical, "خطأ")
        Finally
            con.Close()
            الواجهة.Show()
        End Try
    End Sub

    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click
        Me.Hide()
        الواجهة.Show()
    End Sub

  
End Class
   