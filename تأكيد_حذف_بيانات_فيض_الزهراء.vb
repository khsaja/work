Public Class تأكيد_حذف_بيانات_فيض_الزهراء


    ' هذا المتغير سيكون متاحًا للفورم الآخر
    Public Property IsPasswordCorrect As Boolean = False

    Private Sub TextBox1_TextChanged_1(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        TextBox1.UseSystemPasswordChar = True

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim enteredPassword As String = TextBox1.Text
        Dim correctPassword As String = "1234" ' يفضل جلبها من قاعدة البيانات

        If enteredPassword = correctPassword Then
            IsPasswordCorrect = True
            Me.Close() ' إغلاق الفورم بعد التأكيد
        Else
            MessageBox.Show("كلمة المرور غير صحيحة", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
            IsPasswordCorrect = False ' نتاكد من إرجاع القيمة الصحيحة
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Hide()
        فيض_الزهراء.Show()
    End Sub
End Class