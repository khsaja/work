Imports System.Data.SqlClient
Imports System.Text
Public Class تقرير_ملف_الفضلاء
    ' تعريف الاتصال بحيث يكون متاح بكل الدوال داخل الفورم
    Dim sqlcon As New SqlConnection("Server=DESKTOP-08SGMQ2\SQLEXPRESS;Database=white_hand;Integrated Security=True;")
    Dim filterQuery As String = "" ' تأكدي من تعريفه كمتغير نصي قبل أي استخدام

    Private Sub PictureBox3_Click_1(sender As Object, e As EventArgs) Handles PictureBox3.Click
        Me.Hide()
        الفضلاء.Show()
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs)

        Me.Hide()
        Dim form2 As New استعراض_ملف_الفضلاء("")
        form2.Show()
    End Sub


    Private Sub Button6_Click(sender As Object, e As EventArgs)
        Me.Hide()
        الفضلاء.Show()
    End Sub

  

    Private Sub تقرير_ملف_الفضلاء_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ComboBox8.Enabled = False
        ComboBox9.Enabled = False
        ComboBox10.Enabled = False
        ComboBox11.Enabled = False

        ComboBox1.Enabled = False
        ComboBox3.Enabled = False
        ComboBox4.Enabled = False
        ComboBox5.Enabled = False
        ComboBox6.Enabled = False
        ComboBox7.Enabled = False
        ComboBox8.Enabled = False
        ComboBox9.Enabled = False
        ComboBox10.Enabled = False
        ComboBox11.Enabled = False

    
    End Sub

  

    Private Sub Button10_Click_1(sender As Object, e As EventArgs) Handles Button10.Click
        ' إنشاء قائمة لحفظ الاستعلامات المحددة
        Dim SelectedFields As New List(Of String)()
        'الانتخابي

        If CheckBox1.Checked Then
            SelectedFields.Add("(SELECT COUNT(*) FROM voter_fodalaa) + (SELECT COUNT(*) FROM AXIS_fodalaa) AS [عدد الناخبين]")
        End If

        If CheckBox2.Checked Then
            SelectedFields.Add("(SELECT COUNT(*) FROM AXIS_fodalaa) AS [عدد المحور]")

        End If

        If CheckBox3.Checked Then
            SelectedFields.Add("(SELECT COUNT(*) FROM voter_fodalaa) AS [عدد الكسب]")

        End If

        'الفني
        If CheckBox4.Checked Then
            SelectedFields.Add("(SELECT COUNT(*) FROM AXIS_fodalaa WHERE axle_type = 'ملف الشباب')  AS [محاور ملف الشباب]")
        End If

        If CheckBox7.Checked Then
            SelectedFields.Add("(SELECT COUNT(*) FROM AXIS_fodalaa WHERE axle_type = 'ملف الرياضة')  AS [محاور ملف الرياضة]")
        End If

        If CheckBox6.Checked Then
            SelectedFields.Add("(SELECT COUNT(*) FROM AXIS_fodalaa WHERE axle_type = 'ملف العشائر')  AS [محاور ملف العشائر]")
        End If

        If CheckBox5.Checked Then
            SelectedFields.Add("(SELECT COUNT(*) FROM AXIS_fodalaa WHERE axle_type = 'الملف النسوي')  AS [محاور ملف النسوي]")
        End If

        If CheckBox9.Checked Then
            SelectedFields.Add("(SELECT COUNT(*) FROM AXIS_fodalaa WHERE axle_type = 'الملف المهني')  AS [محاور ملف المهني]")
        End If


        'التنفيذي
        If CheckBox10.Checked Then
            SelectedFields.Add("(SELECT COUNT(*) FROM AXIS_fodalaa WHERE axle_type = 'ملف المراقبين')  AS [محاور ملف المراقبين]")
        End If

        If CheckBox11.Checked Then
            SelectedFields.Add("(SELECT COUNT(*) FROM AXIS_fodalaa WHERE axle_type = 'ملف المرشدين')  AS [محاور ملف المرشدين]")
        End If


        'التنفيذي
        If CheckBox13.Checked Then
            SelectedFields.Add("(SELECT COUNT(*) FROM AXIS_fodalaa WHERE axlecardstatus = 'محدثة')  AS [عدد البطائق  محدثة]")
        End If

        If CheckBox12.Checked Then
            SelectedFields.Add("(SELECT COUNT(*) FROM AXIS_fodalaa WHERE axlecardstatus = 'غير محدثة')  AS [عدد البطائق الغير محدثة]")
        End If
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'احتياط

        'If CheckBox50.Checked Then
        '    SelectedFields.Add("(SELECT COUNT(*) FROM voter_fodalaa) AS Nominate_mentor")
        'End If
        'If CheckBox10.Checked Then
        '    SelectedFields.Add("(SELECT COUNT(*) FROM AXIS_fodalaa WHERE axle_type = 'المراقبين')  AS [عدد المرشحين في المراقبين]")
        'End If
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''


        ' التأكد من وجود حقول محددة
        If SelectedFields.Count = 0 Then
            MessageBox.Show("يرجى تحديد الحقول التي تريد استعراضها!", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' تكوين الاستعلام النهائي
        Dim finalQuery As String = "SELECT " & String.Join(", ", SelectedFields)

        ' إخفاء الفورم الحالي وفتح الفورم الجديد مع تمرير الاستعلام
        Me.Hide()
        Dim form2 As New استعراض_ملف_الفضلاء(finalQuery)
        form2.Show()
        '''''''''''''''''''''''''''''''''''''''''''''''

    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        CheckBox1.Checked = Nothing
        CheckBox2.Checked = Nothing
        CheckBox3.Checked = Nothing
        CheckBox4.Checked = Nothing
        CheckBox5.Checked = Nothing
        CheckBox6.Checked = Nothing
        CheckBox7.Checked = Nothing
        CheckBox9.Checked = Nothing
        CheckBox10.Checked = Nothing
        CheckBox11.Checked = Nothing
        CheckBox12.Checked = Nothing
        CheckBox13.Checked = Nothing

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Hide()
        الفضلاء.Show()
    End Sub

    Private Sub Button5_Click_1(sender As Object, e As EventArgs) Handles Button5.Click
        ' إنشاء قائمة لحفظ الاستعلامات المحددة
        Dim SelectedFields As New List(Of String)()
        Dim Filters As New List(Of String)()

        ' ✅ بدلًا من COUNT(*), نخلي أسماء الحقول الحقيقية لعرض البيانات الفعلية
        If CheckBox86.Checked Then
            SelectedFields.Add("voter_name AS [اسم الناخب]")
        End If

        If CheckBox75.Checked Then
            SelectedFields.Add("voter_phone AS [هاتف الناخب]")
        End If
        If CheckBox68.Checked Then
            SelectedFields.Add("votercardnumber AS [رقم بطاقةالناخب]")
        End If
        If CheckBox43.Checked Then
            SelectedFields.Add("Filter_files AS [ترشيح الملفات]")
        End If
        If CheckBox46.Checked Then
            SelectedFields.Add("Nominate_observer AS [ترشيح المراقبين]")
        End If
        If CheckBox50.Checked Then
            SelectedFields.Add("Nominate_mentor AS [ترشيح المرشدين]")
        End If

        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''


        ' إضافة الحقول الخاصة بالفلاتر
        If CheckBox79.Checked Then SelectedFields.Add("degree_kinship AS [درجة القرابة]")
        If CheckBox67.Checked Then SelectedFields.Add("voter_address AS [العنوان]")
        If CheckBox77.Checked Then SelectedFields.Add("Polling_Center_Name AS [اسم مركز الانتخاب]")
        If CheckBox66.Checked Then SelectedFields.Add("votercardstatus AS [حالة بطاقة الناخب]")

        ' التأكد من تحديد الحقول
        If SelectedFields.Count = 0 Then
            MessageBox.Show("يرجى تحديد الحقول التي تريد استعراضها!", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' إضافة الفلاتر عند تفعيل الـ CheckBox الخاص بها
        If CheckBox79.Checked AndAlso ComboBox8.SelectedIndex <> -1 Then
            Filters.Add("degree_kinship = '" & ComboBox8.SelectedItem.ToString() & "'")
        End If
        If CheckBox67.Checked AndAlso ComboBox9.SelectedIndex <> -1 Then
            Filters.Add("voter_address = '" & ComboBox9.SelectedItem.ToString() & "'")
        End If
        If CheckBox77.Checked AndAlso ComboBox10.SelectedIndex <> -1 Then
            Filters.Add("Polling_Center_Name = '" & ComboBox10.SelectedItem.ToString() & "'")
        End If
        If CheckBox66.Checked AndAlso ComboBox11.SelectedIndex <> -1 Then
            Filters.Add("votercardstatus = '" & ComboBox11.SelectedItem.ToString() & "'")
        End If
        ''''''''''''''''''''''''''''''''''''''''''''''''
        ' ✅ إضافة اسم المحور عند تحديده
        If CheckBox72.Checked Then  ' استبدل CheckBox99 بجك بوكس الخاص باسم المحور
            SelectedFields.Add("p.NAME AS [اسم المحور]")
        End If



        '''''''''''''''''''''''''''''''''''''''''''''''
        ' ✅ تكوين الاستعلام النهائي مع الربط بين الجدولين وإضافة الفلاتر إذا كانت موجودة
        Dim finalQuery As String = "SELECT " & String.Join(", ", SelectedFields) &
                                   " FROM voter_fodalaa v " &
                                   " INNER JOIN AXIS_fodalaa p ON v.ID_AXIS_FAEDZAHRAA = p.ID"

        ' إعداد الأمر مع المعاملات المطلوبة
        Dim cmd As New SqlCommand(finalQuery, sqlcon)
        cmd.Parameters.AddWithValue("@NAME", TextBox14.Text)


        ' ✅ إضافة الفلاتر إذا كانت موجودة
        If Filters.Count > 0 Then
            finalQuery &= " WHERE " & String.Join(" AND ", Filters)
        End If

        '' ✅ تكوين الاستعلام النهائي مع الربط بين الجدولين
        'Dim finalQuery As String = "SELECT " & String.Join(", ", SelectedFields) &
        '                     " FROM voter_fodalaa v " &
        '                     " INNER JOIN AXIS_fodalaa p ON v.ID_AXIS_FAEDZAHRAA = p.ID"
        ' '' '' تكوين الاستعلام النهائي
        '' ''Dim finalQuery As String = "SELECT " & String.Join(", ", SelectedFields) & " FROM voter_fodalaa"
        '' ''If Filters.Count > 0 Then
        '' ''    finalQuery &= " WHERE " & String.Join(" AND ", Filters)
        '' ''End If

        '' '' التأكد من وجود حقول محددة
        If SelectedFields.Count = 0 Then
            MessageBox.Show("يرجى تحديد الحقول التي تريد استعراضها!", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

       
        ' فتح الفورم الثاني وتمرير الاستعلام
        Dim form2 As New استعراض_ملف_الفضلاء(finalQuery)
        form2.Show()
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        CheckBox86.Checked = Nothing
        CheckBox72.Checked = Nothing
        CheckBox75.Checked = Nothing
        CheckBox68.Checked = Nothing
        CheckBox50.Checked = Nothing
        CheckBox43.Checked = Nothing
        CheckBox46.Checked = Nothing
        CheckBox54.Checked = Nothing
        CheckBox77.Checked = Nothing
        CheckBox66.Checked = Nothing
        CheckBox67.Checked = Nothing
        CheckBox79.Checked = Nothing
    End Sub

    Private Sub Button4_Click_1(sender As Object, e As EventArgs) Handles Button4.Click
        Me.Hide()
        الفضلاء.Show()
    End Sub



    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        CheckBox8.Checked = Nothing
        CheckBox14.Checked = Nothing
        CheckBox15.Checked = Nothing
        CheckBox16.Checked = Nothing
        CheckBox17.Checked = Nothing
        CheckBox18.Checked = Nothing
        CheckBox19.Checked = Nothing
        CheckBox20.Checked = Nothing
        CheckBox21.Checked = Nothing
        CheckBox22.Checked = Nothing
        CheckBox23.Checked = Nothing
        CheckBox24.Checked = Nothing
        CheckBox25.Checked = Nothing
        CheckBox26.Checked = Nothing
        CheckBox27.Checked = Nothing
        CheckBox28.Checked = Nothing
        CheckBox29.Checked = Nothing
        CheckBox31.Checked = Nothing
        CheckBox32.Checked = Nothing
        CheckBox33.Checked = Nothing
        CheckBox34.Checked = Nothing
        CheckBox35.Checked = Nothing
        CheckBox36.Checked = Nothing
        CheckBox37.Checked = Nothing
        CheckBox38.Checked = Nothing
        CheckBox39.Checked = Nothing
        CheckBox40.Checked = Nothing
        CheckBox41.Checked = Nothing
        CheckBox42.Checked = Nothing
        CheckBox44.Checked = Nothing
        CheckBox45.Checked = Nothing
        CheckBox46.Checked = Nothing
        CheckBox47.Checked = Nothing
        CheckBox49.Checked = Nothing
        CheckBox50.Checked = Nothing
        CheckBox51.Checked = Nothing
        CheckBox52.Checked = Nothing
        CheckBox43.Checked = Nothing


    End Sub

    Private Sub Button8_Click_1(sender As Object, e As EventArgs) Handles Button8.Click
        ''''''''''''''''''''''''''''

        ' إنشاء قوائم لتجميع الحقول والفلاتر
        Dim SelectedFields As New List(Of String)()
        Dim Filters As New List(Of String)()

        '========================================================
        ' بيانات المحور الشخصية (من جدول المحاور)
        '========================================================
        If CheckBox14.Checked Then
            SelectedFields.Add("af.NAME AS [الاسم]")
        End If
        If CheckBox16.Checked Then
            SelectedFields.Add("af.FATHER AS [اسم الاب]")
        End If
        If CheckBox18.Checked Then
            SelectedFields.Add("af.GRANDFATHER AS [اسم الجد]")
        End If
        If CheckBox19.Checked Then
            SelectedFields.Add("af.great_father AS [اب الجد]")
        End If
        If CheckBox20.Checked Then
            SelectedFields.Add("af.Title AS [اللقــــب]")
        End If
        If CheckBox15.Checked Then
            SelectedFields.Add("af.Socialname AS [الاسم الاجتماعي]")
        End If
        If CheckBox17.Checked Then
            SelectedFields.Add("af.birthDate AS [تاريخ الميلاد]")
        End If
        If CheckBox28.Checked Then
            SelectedFields.Add("af.pointrefernce AS [نقطة الدلالة]")
        End If
        If CheckBox24.Checked Then
            SelectedFields.Add("af.familymembers AS [افراد الاسرة]")
        End If
        If CheckBox25.Checked Then
            SelectedFields.Add("af.phone1 AS [رقم الهاتف الاول]")
        End If
        If CheckBox26.Checked Then
            SelectedFields.Add("af.phone2 AS [رقم الهاتف الثاني]")
        End If
        If CheckBox27.Checked Then
            SelectedFields.Add("af.joiningdate AS [تاريخ الانضمام]")
        End If
        If CheckBox55.Checked Then
            SelectedFields.Add("af.axle_type AS [ملفات جميع المحاور]")
        End If

        '========================================================
        ' بيانات الفلاتر الخاصة بالمحور
        '========================================================
        If CheckBox21.Checked AndAlso ComboBox4.SelectedIndex <> -1 Then
            Filters.Add("af.Gender = '" & ComboBox4.SelectedItem.ToString() & "'")
        End If
        If CheckBox23.Checked AndAlso ComboBox1.SelectedIndex <> -1 Then
            Filters.Add("af.address = '" & ComboBox1.SelectedItem.ToString() & "'")
        End If
        If CheckBox22.Checked AndAlso ComboBox3.SelectedIndex <> -1 Then
            Filters.Add("af.martialstatus = '" & ComboBox3.SelectedItem.ToString() & "'")
        End If

        '========================================================
        ' بيانات تنظيمية وانتخابية من جدول المحور
        '========================================================
        If CheckBox31.Checked Then
            SelectedFields.Add("af.adjective AS [صفة المحور]")
        End If
        If CheckBox32.Checked Then
            SelectedFields.Add("af.tributary AS [الرافد]")
        End If
        If CheckBox33.Checked Then
            SelectedFields.Add("af.axlecardnumber AS [رقم بطاقة الناخب للمحور]")
        End If
        If CheckBox36.Checked Then
            SelectedFields.Add("af.Voted_2013 AS [التصويت في سنة 2013]")
        End If
        If CheckBox8.Checked Then
            SelectedFields.Add("af.Voted_2021 AS [التصويت في سنة 2021]")
        End If
        If CheckBox48.Checked Then
            SelectedFields.Add("af.Voted_2023 AS [التصويت في سنة 2023]")
        End If

        '========================================================
        ' المستمسكات الرسمية من جدول المحور
        '========================================================
        If CheckBox40.Checked Then
            SelectedFields.Add("af.Residencecardnumber AS [رقم البطاقة السكن]")
        End If
        If CheckBox41.Checked Then
            SelectedFields.Add("af.Rationcardnumber AS [رقم البطاقة التموينية]")
        End If
        If CheckBox42.Checked Then
            SelectedFields.Add("af.Unifiedcardnumber AS [رقم البطاقة الموحدة]")
        End If

        '========================================================
        ' فلاتر إضافية تتعلق بنوع الملف (محاور ملف الشباب، الرياضة، ... إلخ)
        '========================================================
        If CheckBox53.Checked Then
            SelectedFields.Add("af.axle_type AS [محاور ملف الشباب]")
            Filters.Add("af.axle_type = 'ملف الشباب'")
        End If
        If CheckBox52.Checked Then
            SelectedFields.Add("af.axle_type AS [محاور ملف الرياضة]")
            Filters.Add("af.axle_type = 'ملف الرياضة'")
        End If
        If CheckBox51.Checked Then
            SelectedFields.Add("af.axle_type AS [محاور ملف العشائر]")
            Filters.Add("af.axle_type = 'ملف العشائر'")
        End If
        If CheckBox49.Checked Then
            SelectedFields.Add("af.axle_type AS [محاور ملف النسوي]")
            Filters.Add("af.axle_type = 'الملف النسوي'")
        End If
        If CheckBox47.Checked Then
            SelectedFields.Add("af.axle_type AS [محاور ملف المهني]")
            Filters.Add("af.axle_type = 'الملف المهني'")
        End If

        '========================================================
        ' الحقول الخاصة بالترشيحات والإحصائيات (بيانات من جدول الناخبين)
        '========================================================
        ' ملاحظة: هنا جميع الحقول الحسابية تعتمد على جدول الناخبين (vf)
        If CheckBox29.Checked Then
            SelectedFields.Add("COUNT(vf.voter_name) AS [عدد الكسب]")
        End If
        If CheckBox37.Checked Then
            SelectedFields.Add("COUNT(vf.Filter_files) AS [ترشيح الملفات]")
        End If
        If CheckBox39.Checked Then
            SelectedFields.Add("COUNT(vf.Nominate_observer) AS [ترشيح المراقبين]")
        End If
        If CheckBox38.Checked Then
            SelectedFields.Add("COUNT(vf.Nominate_mentor) AS [ترشيح المرشدين]")
        End If
        If CheckBox44.Checked Then
            SelectedFields.Add("COUNT(CASE WHEN vf.votercardstatus = 'محدثة' THEN 1 END) AS [عدد البطاقات المحدثة]")
        End If
        If CheckBox45.Checked Then
            SelectedFields.Add("COUNT(CASE WHEN vf.votercardstatus = 'غير محدثة' THEN 1 END) AS [عدد البطاقات الغيرالمحدثة]")
        End If

        '========================================================
        ' الفلاتر الخاصة بالمحور من الجدول (فلترة حسب النوع، حالة البطاقة، العنوان المركز الانتخابي)
        '========================================================
        If CheckBox30.Checked AndAlso ComboBox5.SelectedIndex <> -1 Then
            Filters.Add("af.Type = '" & ComboBox5.SelectedItem.ToString() & "'")
        End If
        If CheckBox35.Checked AndAlso ComboBox7.SelectedIndex <> -1 Then
            Filters.Add("af.axlecardstatus = '" & ComboBox7.SelectedItem.ToString() & "'")
        End If
        If CheckBox34.Checked AndAlso ComboBox6.SelectedIndex <> -1 Then
            Filters.Add("af.Pollingcenteraddress = '" & ComboBox6.SelectedItem.ToString() & "'")
        End If

        ' التأكد من اختيار حقول
        If SelectedFields.Count = 0 Then
            MessageBox.Show("يرجى تحديد الحقول التي تريد استعراضها!", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        '========================================================
        ' بناء الاستعلام النهائي
        '========================================================
        Dim usingJoin As Boolean = False
        ' إذا تم تحديد أي من الحقول الحسابية (التي تستخدم COUNT)، فنستخدم JOIN بين المحاور والناخبين
        If CheckBox29.Checked Or CheckBox37.Checked Or CheckBox39.Checked Or CheckBox38.Checked Or CheckBox44.Checked Or CheckBox45.Checked Then
            usingJoin = True
        End If

        Dim baseQuery As String = ""
        If usingJoin Then
            baseQuery = "FROM AXIS_fodalaa af LEFT JOIN voter_fodalaa vf ON af.ID = vf.ID_AXIS_FAEDZAHRAA "
        Else
            baseQuery = "FROM AXIS_fodalaa af "
        End If
        Dim finalQuery As New StringBuilder("SELECT " & String.Join(", ", SelectedFields) & " " & baseQuery)

        ' إضافة الفلاتر إن وجدت
        If Filters.Count > 0 Then
            finalQuery.Append(" WHERE " & String.Join(" AND ", Filters))
        End If

        ' إذا كان الاستعلام يحتوي على حقول حسابية (aggregates) يجب GROUP BY
        If usingJoin Then
            ' نختار جميع الحقول غير الحسابية من SelectedFields
            Dim groupByColumns As New List(Of String)()
            For Each field As String In SelectedFields
                ' إذا لم يحتوي النص على COUNT( أي دالة تجميع، نعتبره حقلاً عاديًا
                If Not field.ToUpper().Contains("COUNT(") Then
                    ' نفترض أن الحقل مكتوب بالشكل: expression AS [Alias]
                    Dim parts() As String = field.Split(New String() {" AS "}, StringSplitOptions.None)
                    If parts.Length > 0 Then
                        groupByColumns.Add(parts(0).Trim())
                    End If
                End If
            Next
            If groupByColumns.Count > 0 Then
                finalQuery.Append(" GROUP BY " & String.Join(", ", groupByColumns))
            End If
        End If

        '========================================================
        ' فتح الفورمة الثانية وعرض البيانات
        '========================================================
        Try
            Dim connString As String = "Server=DESKTOP-08SGMQ2\SQLEXPRESS;Database=white_hand;Integrated Security=True"
            Using conn As New SqlConnection(connString)
                Using cmd As New SqlCommand(finalQuery.ToString(), conn)
                    conn.Open()
                    Dim dt As New DataTable()
                    Dim adapter As New SqlDataAdapter(cmd)
                    adapter.Fill(dt)
                    If dt.Rows.Count = 0 Then
                        MessageBox.Show("لم يتم العثور على أي بيانات!", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Else
                        Dim form2 As New استعراض_ملف_الفضلاء(finalQuery.ToString())
                        form2.Show()
                        form2.DataGridView1.DataSource = dt
                    End If
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("حدث خطأ: " & ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub CheckBox23_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox23.CheckedChanged
        ComboBox1.Enabled = CheckBox23.Checked

    End Sub

    Private Sub CheckBox21_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox21.CheckedChanged
        ComboBox4.Enabled = CheckBox21.Checked

    End Sub

    Private Sub CheckBox22_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox22.CheckedChanged
        ComboBox3.Enabled = CheckBox22.Checked

    End Sub

    Private Sub CheckBox30_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox30.CheckedChanged
        ComboBox5.Enabled = CheckBox30.Checked

    End Sub

    Private Sub CheckBox35_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox35.CheckedChanged
        ComboBox7.Enabled = CheckBox35.Checked

    End Sub

    Private Sub CheckBox34_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox34.CheckedChanged
        ComboBox6.Enabled = CheckBox34.Checked

    End Sub

 
    Private Sub CheckBox79_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox79.CheckedChanged
        ComboBox8.Enabled = CheckBox79.Checked

    End Sub

    Private Sub CheckBox77_CheckedChanged_1(sender As Object, e As EventArgs) Handles CheckBox77.CheckedChanged
        ComboBox10.Enabled = CheckBox77.Checked
    End Sub

    Private Sub CheckBox67_CheckedChanged_1(sender As Object, e As EventArgs) Handles CheckBox67.CheckedChanged
        ComboBox9.Enabled = CheckBox67.Checked

    End Sub

    Private Sub CheckBox66_CheckedChanged_1(sender As Object, e As EventArgs) Handles CheckBox66.CheckedChanged
        ComboBox11.Enabled = CheckBox66.Checked

    End Sub

  
    Private Sub TextBox14_TextChanged(sender As Object, e As EventArgs) Handles TextBox14.TextChanged
      
        ListBox1.Items.Clear() ' تفريغ النتائج السابقة

        ' هنا، نحن نبحث عن اسم الناخب فقط
        Dim searchQuery As String = "SELECT voter_name FROM voter_fodalaa WHERE voter_name LIKE @name + '%'"

        Using conn As New SqlConnection("Server=DESKTOP-08SGMQ2\SQLEXPRESS;Database=white_hand;Integrated Security=True"),
              cmd As New SqlCommand(searchQuery, conn)
            cmd.Parameters.AddWithValue("@name", TextBox14.Text.Trim()) ' تأكد من إزالة الفراغات الزائدة
            conn.Open()
            Dim reader As SqlDataReader = cmd.ExecuteReader()

            If reader.HasRows Then
                While reader.Read()
                    ListBox1.Items.Add(reader("voter_name").ToString())
                End While
            Else
                MessageBox.Show("لا توجد نتائج مطابقة.")
            End If
        End Using

    End Sub
    
    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        If ListBox1.SelectedItem IsNot Nothing Then
            TextBox14.Text = ListBox1.SelectedItem.ToString()
        End If
    End Sub

    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click
        
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        Dim SelectedFields As New List(Of String)()
        Dim Filters As New List(Of String)()

        ' إضافة الحقول المحددة من الجيك بوكسات
        If CheckBox86.Checked Then SelectedFields.Add("voter_name AS [اسم الناخب]")
        If CheckBox72.Checked Then SelectedFields.Add("p.NAME + ' ' + p.FATHER + ' ' + p.GRANDFATHER + ' ' + p.great_father + ' ' + p.Title AS [اسم المحور]")
        If CheckBox75.Checked Then SelectedFields.Add("voter_phone AS [هاتف الناخب]")
        If CheckBox68.Checked Then SelectedFields.Add("votercardnumber AS [رقم بطاقة الناخب]")
        If CheckBox43.Checked Then SelectedFields.Add("Filter_files AS [ترشيح الملفات]")
        If CheckBox46.Checked Then SelectedFields.Add("Nominate_observer AS [ترشيح المراقبين]")
        If CheckBox50.Checked Then SelectedFields.Add("Nominate_mentor AS [ترشيح المرشدين]")

        ' إضافة الحقول الخاصة بالفلاتر
        If CheckBox79.Checked Then SelectedFields.Add("degree_kinship AS [درجة القرابة]")
        If CheckBox67.Checked Then SelectedFields.Add("voter_address AS [العنوان]")
        If CheckBox77.Checked Then SelectedFields.Add("Polling_Center_Name AS [اسم مركز الانتخاب]")
        If CheckBox66.Checked Then SelectedFields.Add("votercardstatus AS [حالة بطاقة الناخب]")

        ' التأكد من تحديد الحقول
        If SelectedFields.Count = 0 Then
            MessageBox.Show("يرجى تحديد الحقول التي تريد استعراضها!", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' إضافة الفلاتر عند تفعيل الـ CheckBox الخاص بها
        If CheckBox79.Checked AndAlso ComboBox8.SelectedIndex <> -1 Then
            Filters.Add("degree_kinship = '" & ComboBox8.SelectedItem.ToString() & "'")
        End If
        If CheckBox67.Checked AndAlso ComboBox9.SelectedIndex <> -1 Then
            Filters.Add("voter_address = '" & ComboBox9.SelectedItem.ToString() & "'")
        End If
        If CheckBox77.Checked AndAlso ComboBox10.SelectedIndex <> -1 Then
            Filters.Add("Polling_Center_Name = '" & ComboBox10.SelectedItem.ToString() & "'")
        End If
        If CheckBox66.Checked AndAlso ComboBox11.SelectedIndex <> -1 Then
            Filters.Add("votercardstatus = '" & ComboBox11.SelectedItem.ToString() & "'")
        End If


        ' إنشاء الاستعلام الأساسي مع الربط الصحيح
        'Dim query As String = "SELECT " & String.Join(", ", SelectedFields) &
        '                      " FROM AXIS_fodalaa p " &
        '                      " INNER JOIN voter_fodalaa v ON p.ID = v.ID_AXIS_FAEDZAHRAA " &
        '                      " WHERE p.ID = @AxisID"

        '' إضافة الفلاتر (إذا تم تحديد أي منها)
        'If Filters.Count > 0 Then
        '    query &= " WHERE " & String.Join(" AND ", Filters)
        'End If

        '' إضافة الفلتر للبحث بناءً على اسم الناخب
        'query &= " AND v.voter_name = @name"
        Dim query As String = "SELECT " & String.Join(", ", SelectedFields) &
                      " FROM AXIS_fodalaa p " &
                      " INNER JOIN voter_fodalaa v ON p.ID = v.ID_AXIS_FAEDZAHRAA "

        ' إضافة الفلاتر (إذا تم تحديد أي منها)
        If Filters.Count > 0 Then
            query &= " WHERE " & String.Join(" AND ", Filters)
        End If

        ' إضافة الفلتر للبحث بناءً على اسم الناخب
        If Filters.Count > 0 Then
            query &= " AND v.voter_name = @name"
        Else
            query &= " WHERE v.voter_name = @name"
        End If

        ' تعريف الاتصال والاستعلام
        Dim connString As String = "Server=DESKTOP-08SGMQ2\SQLEXPRESS;Database=white_hand;Integrated Security=True"

        Using conn As New SqlConnection(connString),
              cmd As New SqlCommand(query, conn)

            ' تعريف المتغير @name وربطه بالقيمة المدخلة في TextBox14
            cmd.Parameters.AddWithValue("@name", TextBox14.Text.Trim()) ' إضافة المعامل بشكل صحيح

            ' فتح الاتصال
            conn.Open()

            ' تنفيذ الاستعلام
            Dim reader As SqlDataReader = cmd.ExecuteReader()

            ' تحميل النتائج في DataTable
            Dim dt As New DataTable()
            dt.Load(reader)

            ' التحقق من وجود بيانات
            If dt.Rows.Count = 0 Then
                MessageBox.Show("لم يتم العثور على أي بيانات!", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                ' فتح الفورمة الثانية وعرض البيانات
                Dim form2 As New استعراض_ملف_الفضلاء(query)
                form2.Show()
                ' عرض البيانات في DataGridView
                form2.DataGridView1.DataSource = dt
            End If
        End Using

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        ListBox2.Items.Clear() ' تفريغ النتائج السابقة

        Dim searchQuery As String = "SELECT p.NAME + ' ' + p.FATHER + ' ' + p.GRANDFATHER + ' ' + p.great_father + ' ' + p.Title AS fullName " &
                                  "FROM AXIS_fodalaa p WHERE (p.NAME + ' ' + p.FATHER + ' ' + p.GRANDFATHER + ' ' + p.great_father + ' ' + p.Title) LIKE @fullName"

        Using conn As New SqlConnection("Server=DESKTOP-08SGMQ2\SQLEXPRESS;Database=white_hand;Integrated Security=True"),
              cmd As New SqlCommand(searchQuery, conn)
            conn.Open()

            cmd.Parameters.AddWithValue("@fullName", "%" & TextBox1.Text.Trim() & "%") ' أضفنا % للبحث الجزئي
            Dim reader As SqlDataReader = cmd.ExecuteReader()

            While reader.Read()
                ListBox2.Items.Add(reader("fullName").ToString()) ' إضافة الاسم الكامل للمحور
            End While
        End Using
    End Sub

    Private Sub ListBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox2.SelectedIndexChanged
        If ListBox2.SelectedItem IsNot Nothing Then
            TextBox1.Text = ListBox2.SelectedItem.ToString()
        End If
    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click

        ' تعريف المتغيرات
        Dim SelectedFields As New List(Of String)()
        Dim Filters As New List(Of String)()
        Dim pivotFullName As String = TextBox1.Text.Trim()

        ' إضافة الحقول المختارة من الجيك بوكسات

        If CheckBox14.Checked Then SelectedFields.Add("af.NAME AS [اسم المحور الاول]")
        If CheckBox16.Checked Then SelectedFields.Add("af.FATHER AS [اسم الاب]")
        If CheckBox18.Checked Then SelectedFields.Add("af.GRANDFATHER AS [اسم الجد]")
        If CheckBox19.Checked Then SelectedFields.Add("af.great_father AS [اسم الجد الاعظم]")
        If CheckBox20.Checked Then SelectedFields.Add("af.Title AS [اسم اللقب]")
        If CheckBox15.Checked Then SelectedFields.Add("af.Socialname AS [الاسم الاجتماعي]")
        If CheckBox17.Checked Then SelectedFields.Add("af.birthDate AS [تاريخ الميلاد]")
        If CheckBox28.Checked Then SelectedFields.Add("af.pointrefernce AS [نقطة الدلالة]")
        If CheckBox24.Checked Then SelectedFields.Add("af.familymembers AS [افراد الاسرة]")
        If CheckBox25.Checked Then SelectedFields.Add("af.phone1 AS [رقم الهاتف الاول]")
        If CheckBox26.Checked Then SelectedFields.Add("af.phone2 AS [رقم الهاتف الثاني]")
        If CheckBox27.Checked Then SelectedFields.Add("af.joiningdate AS [تاريخ الانضمام]")
        If CheckBox55.Checked Then SelectedFields.Add("af.axle_type AS [محاور جميع الملفات]")
        If CheckBox21.Checked Then SelectedFields.Add("af.Gender AS [الجنس]")
        If CheckBox23.Checked Then SelectedFields.Add("af.address AS [العنوان]")
        If CheckBox22.Checked Then SelectedFields.Add("af.martialstatus AS [الحالة الاجتماعية]")
        If CheckBox31.Checked Then SelectedFields.Add("af.adjective AS [صفة المحور]")
        If CheckBox32.Checked Then SelectedFields.Add("af.tributary AS [الرافد]")
        If CheckBox33.Checked Then SelectedFields.Add("af.axlecardnumber AS [رقم بطاقة الناخب للمحور]")
        If CheckBox36.Checked Then SelectedFields.Add("af.Voted_2013 AS [التصويت في سنة 2013]")
        If CheckBox8.Checked Then SelectedFields.Add("af.Voted_2021 AS [التصويت في سنة 2021]")
        If CheckBox48.Checked Then SelectedFields.Add("af.Voted_2023 AS [التصويت في سنة 2023]")
        If CheckBox40.Checked Then SelectedFields.Add("af.Residencecardnumber AS [رقم بطاقة السكن]")
        If CheckBox41.Checked Then SelectedFields.Add("af.Rationcardnumber AS [رقم البطاقة التموينية]")
        If CheckBox42.Checked Then SelectedFields.Add("af.Unifiedcardnumber AS [رقم البطاقة الموحدة]")
        If CheckBox30.Checked Then SelectedFields.Add("af.Type AS [النوع]")
        If CheckBox35.Checked Then SelectedFields.Add("af.axlecardstatus AS [حالة بطاقة الناخب]")
        If CheckBox34.Checked Then SelectedFields.Add("af.Pollingcenteraddress AS [العنوان المركز الانتخابي]")


        ' الفلاتر الإضافية
        If CheckBox53.Checked Then
            SelectedFields.Add("axle_type AS [محاور ملف الشباب]")
            Filters.Add("axle_type = 'ملف الشباب'")
        End If
        If CheckBox52.Checked Then
            SelectedFields.Add("axle_type AS [محاور ملف الرياضة]")
            Filters.Add("axle_type = 'ملف الرياضة'")
        End If
        If CheckBox51.Checked Then
            SelectedFields.Add("axle_type AS [محاور ملف العشائر]")
            Filters.Add("axle_type = 'ملف العشائر'")
        End If
        If CheckBox49.Checked Then
            SelectedFields.Add("axle_type AS [محاور ملف النسوي]")
            Filters.Add("axle_type = 'الملف النسوي'")
        End If
        If CheckBox47.Checked Then
            SelectedFields.Add("axle_type AS [محاور ملف المهني]")
            Filters.Add("axle_type = 'الملف المهني'")
        End If


        If CheckBox29.Checked Then
            SelectedFields.Add("COUNT(vf.voter_name) AS [عدد الكسب]") ' حساب عدد الناخبين التابعين للمحور
        End If

        If CheckBox37.Checked Then
            SelectedFields.Add("COUNT(vf.Filter_files) AS [ترشيح الملفات]") ' حساب عدد الناخبين التابعين للمحور
        End If

        If CheckBox39.Checked Then
            SelectedFields.Add("COUNT(vf.Nominate_observer) AS [ترشيح المراقبين]") ' حساب عدد الناخبين التابعين للمحور
        End If

        If CheckBox38.Checked Then
            SelectedFields.Add("COUNT(vf.Nominate_mentor) AS [ترشيح المرشدين]") ' حساب عدد الناخبين التابعين للمحور
        End If

        If CheckBox44.Checked Then
            SelectedFields.Add("COUNT(CASE WHEN vf.votercardstatus = 'محدثة' THEN 1 END) AS [عدد البطاقات المحدثة]") ' حساب عدد البطاقات المحدثة فقط
        End If

        If CheckBox45.Checked Then
            SelectedFields.Add("COUNT(CASE WHEN vf.votercardstatus = 'غير محدثة' THEN 1 END) AS [عدد البطاقات الغيرالمحدثة]") ' حساب عدد البطاقات المحدثة فقط
        End If

        ' إضافة الفلاتر
        If CheckBox21.Checked AndAlso ComboBox4.SelectedIndex <> -1 Then
            Filters.Add("af.Gender = '" & ComboBox4.SelectedItem.ToString() & "'")
        End If
        If CheckBox23.Checked AndAlso ComboBox1.SelectedIndex <> -1 Then
            Filters.Add("af.address = '" & ComboBox1.SelectedItem.ToString() & "'")
        End If
        If CheckBox22.Checked AndAlso ComboBox3.SelectedIndex <> -1 Then
            Filters.Add("af.martialstatus = '" & ComboBox3.SelectedItem.ToString() & "'")
        End If
        If CheckBox30.Checked AndAlso ComboBox5.SelectedIndex <> -1 Then
            Filters.Add("af.Type = '" & ComboBox5.SelectedItem.ToString() & "'")
        End If
        If CheckBox35.Checked AndAlso ComboBox7.SelectedIndex <> -1 Then
            Filters.Add("af.axlecardstatus = '" & ComboBox7.SelectedItem.ToString() & "'")
        End If
        If CheckBox34.Checked AndAlso ComboBox6.SelectedIndex <> -1 Then
            Filters.Add("af.Pollingcenteraddress = '" & ComboBox6.SelectedItem.ToString() & "'")
        End If
        ' التحقق من الحقول المحددة
        If SelectedFields.Count = 0 Then
            MessageBox.Show("يرجى تحديد الحقول التي تريد استعراضها!", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' بناء الاستعلام النهائي

        Dim query As String = "SELECT " & String.Join(", ", SelectedFields) &
                      " FROM voter_fodalaa vf " &
                      " INNER JOIN AXIS_fodalaa af ON af.ID = vf.ID_AXIS_FAEDZAHRAA " &
                      " WHERE (af.NAME + ' ' + af.FATHER + ' ' + af.GRANDFATHER + ' ' + af.great_father + ' ' + af.Title) = @FullName"


        ' إضافة GROUP BY إذا كان هناك حقول غير مجمعة
        If CheckBox29.Checked Or CheckBox44.Checked Then
            query &= " GROUP BY af.ID, af.NAME, af.FATHER, af.GRANDFATHER, af.great_father, af.Title"
        End If

        ' إضافة الفلاتر إذا كانت موجودة
        If Filters.Count > 0 Then
            query &= " AND (" & String.Join(" OR ", Filters) & ")"
        End If

        ' تعريف الاتصال بقاعدة البيانات
        Dim connString As String = "Server=DESKTOP-08SGMQ2\SQLEXPRESS;Database=white_hand;Integrated Security=True"

        Try
            Using conn As New SqlConnection(connString),
                  cmd As New SqlCommand(query, conn)

                cmd.Parameters.AddWithValue("@FullName", pivotFullName)

                conn.Open()
                Using reader As SqlDataReader = cmd.ExecuteReader()
                    Dim dt As New DataTable()
                    dt.Load(reader)

                    If dt.Rows.Count = 0 Then
                        MessageBox.Show("لم يتم العثور على أي بيانات!", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Else
                        ' فتح الفورمة الثانية وعرض البيانات
                        Dim form2 As New استعراض_ملف_الفضلاء(query)
                        form2.Show()
                        ' عرض البيانات في DataGridView
                        form2.DataGridView1.DataSource = dt
                    End If
                End Using
            End Using
        Catch ex As Exception
            'MessageBox.Show("حدث خطأ: " & ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        ''''''''''''''''''''''''''''''''''''''''''''''''

    End Sub

   
End Class
