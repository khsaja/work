Public Class ملفات_المراقبين

   
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim frm As New الطباعة
        frm.Show()  ' عرض الواجهة التالبة
        Me.Hide()   ' إخفاء الواجهة الحالية

        Me.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim frm As New تقرير_المراقبين
        frm.Show()  ' عرض الواجهة التالبة
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
        المراقبين.Show()
    End Sub
End Class