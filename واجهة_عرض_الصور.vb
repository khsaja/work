Public Class واجهة_عرض_الصور
    Public Property ReceivedImagePath As String ' متغير لاستقبال مسار الصورة

    Private Sub واجهة_عرض_الصور_Load(sender As Object, e As EventArgs) Handles MyBase.Load
      
    End Sub

   
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        PictureBoxshow.Image = Nothing  ' 🔹 تنظيف الصورة عند الإغلاق
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Hide()
        فيض_الزهراء.Show()
    End Sub
End Class