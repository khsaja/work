Imports System.Data.SqlClient

Public Class الفضلاء
    Dim sqlcon As New SqlConnection("Server=DESKTOP-08SGMQ2\SQLEXPRESS;Database=white_hand;Integrated Security=True;")

    Private Sub الفضلاء_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ' 🔹 ضبط DataGridView لعرض البيانات تلقائيًا
        DataGridView1.AutoGenerateColumns = True

        ' 🔹 التأكد من إغلاق أي اتصال مفتوح
        If sqlcon.State = ConnectionState.Open Then
            sqlcon.Close()
        End If

        ' 🔹 تحميل البيانات
        LoadData()
        تحديث_جميع_الإحصائيات()
        تحديث_إحصائيات_الكلمات()
    End Sub

    ' 🔹 تحميل البيانات في DataGridView
    Private Sub LoadData()
        Try
            ' 🔹 التأكد من إغلاق الاتصال قبل فتحه
            If sqlcon.State = ConnectionState.Open Then
                sqlcon.Close()
            End If

            sqlcon.Open()

            ' 🔹 استعلام SQL لتحديد الأعمدة المطلوبة فقط
            Dim query As String = "SELECT ID_AXIS_FAEDZAHRAA,voter_name, degree_kinship, voter_phone, voter_address, votercardnumber, votercardstatus, Filter_files, Nominate_observer, Nominate_mentor, Polling_center_number,ID FROM voter_fodalaa"
            Dim adapter As New SqlDataAdapter(query, sqlcon)
            Dim dt As New DataTable()
            adapter.Fill(dt)

            ' 🔹 عرض البيانات في DataGridView
            DataGridView1.DataSource = dt

            ' 🔹 تعيين عناوين الأعمدة باللغة العربية
            Dim columnHeaders As New Dictionary(Of String, String) From {{"ID_AXIS_FAEDZAHRAA", "معرف المحور"}, {"ID", "المعرف"}, {"voter_name", "الاسم الكامل"}, {"degree_kinship", "درجة القرابة"}, {"voter_phone", "رقم الهاتف"}, {"voter_address", "العنوان"}, {"votercardnumber", "رقم بطاقة الناخب"}, {"votercardstatus", "حالة بطاقة الناخب"}, {"Filter_files", "ترشيح الملفات"}, {"Nominate_observer", "ترشيح كمراقب"}, {"Nominate_mentor", "ترشيح كمرشد"}, {"Polling_center_number", "رقم مركز الانتخاب"}}

            ' 🔹 ضبط أسماء الأعمدة وعرضها
            For Each col As DataGridViewColumn In DataGridView1.Columns
                If columnHeaders.ContainsKey(col.Name) Then
                    col.HeaderText = columnHeaders(col.Name)
                    col.Width = 150 ' ضبط عرض الأعمدة
                End If
            Next

            ' 🔹 تخصيص عرض عمود رقم مركز الانتخاب
            DataGridView1.Columns("Polling_center_number").Width = 200

            ' 🔹 تلوين الصفوف
            DataGridView1.RowTemplate.Height = 30
            ColorRows()

        Catch ex As Exception
            MessageBox.Show("خطأ أثناء تحميل البيانات: " & ex.Message)
        Finally
            ' 🔹 إغلاق الاتصال بعد الانتهاء
            If sqlcon.State = ConnectionState.Open Then
                sqlcon.Close()
            End If
        End Try
    End Sub

    ' 🔹 دالة لتلوين الصفوف بالتناوب بين الأبيض والرمادي
    Private Sub ColorRows()
        If DataGridView1.Rows.Count > 0 Then
            For i As Integer = 0 To DataGridView1.Rows.Count - 1
                DataGridView1.Rows(i).DefaultCellStyle.BackColor = If(i Mod 2 = 0, Color.White, Color.LightGray)
            Next
        End If
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        ColorRows()

        DataGridView1.AutoGenerateColumns = True

        If e.RowIndex < 0 Then Exit Sub ' التأكد من أن المستخدم لم ينقر على عنوان العمود

        Try
            Dim selectedRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)

            ' 🔹 التأكد أن العمود موجود في DataGridView قبل محاولة الوصول إليه
            If Not DataGridView1.Columns.Contains("Polling_center_number") Then
                MessageBox.Show("عمود 'Polling_center_number' غير موجود في الجدول!", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            ' 🔹 التحقق من أن القيمة ليست NULL قبل تحويلها
            Dim centerNumber As String = If(selectedRow.Cells("Polling_center_number").Value IsNot Nothing, selectedRow.Cells("Polling_center_number").Value.ToString(), "")

            If String.IsNullOrWhiteSpace(centerNumber) Then
                MessageBox.Show("رقم المركز غير متوفر!", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            Dim query As String = "SELECT  ID_AXIS_FAEDZAHRAA ,Voter_governorate_name, New_judiciary, New_side, Supply_number, Supply_center_name, Registration_Center_Code, Registration_Center_Name, Polling_Center_Name, Polling_station_address, Number_stations , voter_name ,degree_kinship,voter_phone,voter_address,votercardnumber,votercardstatus,Filter_files,Nominate_observer,Nominate_mentor,Polling_center_number FROM voter_fodalaa WHERE Polling_center_number = @CenterNumber"

            Using conn As New SqlConnection("Server=DESKTOP-08SGMQ2\SQLEXPRESS;Database=white_hand;Integrated Security=True;"),
                  cmd As New SqlCommand(query, conn)

                cmd.Parameters.AddWithValue("@CenterNumber", centerNumber)
                conn.Open()

                Using reader As SqlDataReader = cmd.ExecuteReader()
                    If reader.Read() Then
                        ' 🔹 تعبئة البيانات في الـ TextBoxes مباشرة بدون استبدال القيم الفارغة بـ "لا يوجد"
                        TextBox55.Text = If(IsDBNull(reader("Voter_governorate_name")), "", reader("Voter_governorate_name").ToString().Trim())
                        TextBox56.Text = If(IsDBNull(reader("New_judiciary")), "", reader("New_judiciary").ToString().Trim())
                        TextBox57.Text = If(IsDBNull(reader("New_side")), "", reader("New_side").ToString().Trim())
                        TextBox58.Text = If(IsDBNull(reader("Supply_number")), "", reader("Supply_number").ToString().Trim())
                        TextBox61.Text = If(IsDBNull(reader("Supply_center_name")), "", reader("Supply_center_name").ToString().Trim())
                        TextBox62.Text = If(IsDBNull(reader("Registration_Center_Code")), "", reader("Registration_Center_Code").ToString().Trim())
                        TextBox63.Text = If(IsDBNull(reader("Registration_Center_Name")), "", reader("Registration_Center_Name").ToString().Trim())
                        TextBox64.Text = If(IsDBNull(reader("Polling_Center_Name")), "", reader("Polling_Center_Name").ToString().Trim())
                        TextBox65.Text = If(IsDBNull(reader("Polling_station_address")), "", reader("Polling_station_address").ToString().Trim())
                        TextBox66.Text = If(IsDBNull(reader("Number_stations")), "", reader("Number_stations").ToString().Trim())
                        'جدول الكسب 
                        TextBox54.Text = If(IsDBNull(reader("ID_AXIS_FAEDZAHRAA")), "", reader("ID_AXIS_FAEDZAHRAA").ToString().Trim())
                        TextBox19.Text = If(IsDBNull(reader("voter_name")), "", reader("voter_name").ToString().Trim())
                        ComboBox8.Text = If(IsDBNull(reader("degree_kinship")), "", reader("degree_kinship").ToString().Trim())
                        TextBox69.Text = If(IsDBNull(reader("voter_phone")), "", reader("voter_phone").ToString().Trim())
                        ComboBox9.Text = If(IsDBNull(reader("voter_address")), "", reader("voter_address").ToString().Trim())
                        TextBox71.Text = If(IsDBNull(reader("votercardnumber")), "", reader("votercardnumber").ToString().Trim())
                        ComboBox10.Text = If(IsDBNull(reader("votercardstatus")), "", reader("votercardstatus").ToString().Trim())
                        ComboBox5.Text = If(IsDBNull(reader("Filter_files")), "", reader("Filter_files").ToString().Trim())
                        CheckBox4.Text = If(IsDBNull(reader("Nominate_observer")), "", reader("Nominate_observer").ToString().Trim())
                        CheckBox5.Text = If(IsDBNull(reader("Nominate_mentor")), "", reader("Nominate_mentor").ToString().Trim())
                        TextBox74.Text = If(IsDBNull(reader("Polling_center_number")), "", reader("Polling_center_number").ToString().Trim())

                    Else
                        MessageBox.Show("لم يتم العثور على البيانات!", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    End If
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("خطأ أثناء جلب البيانات: " & ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)

            DataGridView1.RowTemplate.Height = 30
            For i As Integer = 0 To DataGridView1.Rows.Count - 1
                If i Mod 2 = 0 Then
                    DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.White
                Else
                    DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.LightGray
                End If
            Next

        End Try
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click

        ' مسح الصور من PictureBox المحددة
        PictureBox5.Image = Nothing
        PictureBox6.Image = Nothing
        PictureBox7.Image = Nothing
        PictureBox8.Image = Nothing
        ' التأكد أن التاب كنترول موجود
        If TabControl1 IsNot Nothing Then
            ' مسح الأدوات داخل جميع التبويبات
            For Each tab As TabPage In TabControl1.TabPages
                ClearControls(tab)
            Next
        End If

        ' مسح الأدوات داخل الفورم ككل (لو عندك أدوات خارج التاب كنترول)
        ClearControls(Me)
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click

        Try
            ' 1) فتح الاتصال
            sqlcon.Open()

            ' 2) بدء معاملة لضمان إدخال البيانات أو التراجع في حال حدوث خطأ
            Dim transaction As SqlTransaction = sqlcon.BeginTransaction()


            Dim query As String = "INSERT INTO voter_fodalaa  ( voter_name,voter_phone,degree_kinship,  voter_address, votercardnumber, votercardstatus, Filter_files, Nominate_observer, Nominate_mentor, Polling_center_number,Voter_governorate_name,New_judiciary,New_side,Supply_number,Supply_center_name,Registration_Center_Code,Registration_Center_Name,Polling_Center_Name,Polling_station_address ,Number_stations ,ID_AXIS_FAEDZAHRAA) VALUES(@voter_name,@voter_phone, @degree_kinship,  @voter_address, @votercardnumber, @votercardstatus, @Filter_files, @Nominate_observer, @Nominate_mentor, @Polling_center_number,@Voter_governorate_name,@New_judiciary,@New_side,@Supply_number,@Supply_center_name,@Registration_Center_Code,@Registration_Center_Name,@Polling_Center_Name,@Polling_station_address,@Number_stations, @ID_AXIS_FAEDZAHRAA);"


            ' 6) إنشاء أمر الإدخال وربطه بالمعاملة
            Using cmd As New SqlCommand(query, sqlcon, transaction)


                ' بيانات الكسب "الجدول الثاني" 
                cmd.Parameters.AddWithValue("@voter_name", If(String.IsNullOrWhiteSpace(TextBox19.Text), "لا يوجد", TextBox19.Text))
                cmd.Parameters.AddWithValue("@degree_kinship", If(String.IsNullOrWhiteSpace(ComboBox8.Text), "لا يوجد", ComboBox8.Text))
                cmd.Parameters.AddWithValue("@voter_phone", If(String.IsNullOrWhiteSpace(TextBox69.Text), "لا يوجد", TextBox69.Text))
                cmd.Parameters.AddWithValue("@voter_address", If(String.IsNullOrWhiteSpace(ComboBox9.Text), "لا يوجد", ComboBox9.Text))
                cmd.Parameters.AddWithValue("@votercardnumber", If(String.IsNullOrWhiteSpace(TextBox71.Text), "لا يوجد", TextBox71.Text))
                cmd.Parameters.AddWithValue("@votercardstatus", If(String.IsNullOrWhiteSpace(ComboBox10.Text), "لا يوجد", ComboBox10.Text))
                cmd.Parameters.AddWithValue("@Filter_files", If(String.IsNullOrWhiteSpace(ComboBox5.Text), "لا يوجد", ComboBox5.Text))
                cmd.Parameters.AddWithValue("@ID_AXIS_FAEDZAHRAA", TextBox54.Text)

                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''


                ' '' تعيين قيم تجريبية مؤقتة لاختبار الشيفرة (يجب استبدالها بالقيم الحقيقية لاحقًا)
                'Dim totalObservers As Integer = 0 ' استبدلها بالقيمة الفعلية من قاعدة البيانات أو عداد معين
                'Dim totalMentors As Integer = 0 ' استبدلها بالقيمة الفعلية من قاعدة البيانات أو عداد معين
                'Dim observerValue As Integer = If(CheckBox4.Checked AndAlso totalObservers Mod 5 = 0, 1, 0)
                'Dim mentorValue As Integer = If(CheckBox5.Checked AndAlso totalMentors Mod 10 = 0, 1, 0)
                'cmd.Parameters.AddWithValue("@Nominate_observer", observerValue)
                'cmd.Parameters.AddWithValue("@Nominate_mentor", mentorValue)
                'MessageBox.Show("Nominate_observer: " & observerValue & vbCrLf & "Nominate_mentor: " & mentorValue)
                cmd.Parameters.AddWithValue("@Nominate_observer", If(CheckBox4.Checked, 1, 0))
                cmd.Parameters.AddWithValue("@Nominate_mentor", If(CheckBox5.Checked, 1, 0))


                ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                cmd.Parameters.AddWithValue("@Polling_center_number", If(String.IsNullOrWhiteSpace(TextBox74.Text), "لا يوجد", TextBox74.Text))

                ' بيانات "جدول التفاصيل"
                cmd.Parameters.AddWithValue("@Voter_governorate_name", If(String.IsNullOrWhiteSpace(TextBox55.Text), "لا يوجد", TextBox55.Text))
                cmd.Parameters.AddWithValue("@New_judiciary", If(String.IsNullOrWhiteSpace(TextBox56.Text), "لا يوجد", TextBox56.Text))
                cmd.Parameters.AddWithValue("@New_side", If(String.IsNullOrWhiteSpace(TextBox57.Text), "لا يوجد", TextBox57.Text))
                cmd.Parameters.AddWithValue("@Supply_number", If(String.IsNullOrWhiteSpace(TextBox58.Text), "لا يوجد", TextBox58.Text))
                cmd.Parameters.AddWithValue("@Supply_center_name", If(String.IsNullOrWhiteSpace(TextBox61.Text), "لا يوجد", TextBox61.Text))
                cmd.Parameters.AddWithValue("@Registration_Center_Code", If(String.IsNullOrWhiteSpace(TextBox62.Text), "لا يوجد", TextBox62.Text))
                cmd.Parameters.AddWithValue("@Registration_Center_Name", If(String.IsNullOrWhiteSpace(TextBox63.Text), "لا يوجد", TextBox63.Text))
                cmd.Parameters.AddWithValue("@Polling_Center_Name", If(String.IsNullOrWhiteSpace(TextBox64.Text), "لا يوجد", TextBox64.Text))
                cmd.Parameters.AddWithValue("@Polling_station_address", If(String.IsNullOrWhiteSpace(TextBox65.Text), "لا يوجد", TextBox65.Text))
                cmd.Parameters.AddWithValue("@Number_stations", If(String.IsNullOrWhiteSpace(TextBox66.Text), "لا يوجد", TextBox66.Text))


                ' 7) تنفيذ الإدخال
                cmd.ExecuteNonQuery()

                ' 8) تأكيد العملية
                transaction.Commit()
            End Using

            ' رسالة نجاح
            MessageBox.Show("تمت إضافة البيانات بنجاح!", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information)

            ' تحديث DataGridView
            LoadData()
            DataGridView1.Refresh()

        Catch ex As Exception
            ' في حالة الخطأ، التراجع عن العملية
            MessageBox.Show("خطأ أثناء الحفظ: " & ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            ' إغلاق الاتصال
            If sqlcon.State = ConnectionState.Open Then
                sqlcon.Close()
            End If
        End Try
    End Sub
    Private Sub DeleteRecord()
        Try
            Using sqlcon As New SqlConnection("Server=DESKTOP-08SGMQ2\SQLEXPRESS;Database=white_hand;Integrated Security=True")
                sqlcon.Open()
                Dim query As String = "DELETE FROM voter_fodalaa WHERE voter_name = @voter_name"
                Using cmd As New SqlCommand(query, sqlcon)
                    cmd.Parameters.AddWithValue("@voter_name", TextBox19.Text)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("حدث خطأ أثناء الحذف: " & ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click

        ' تأكيد الحذف
        Dim result As DialogResult = MessageBox.Show("هل أنت متأكد من الحذف؟", "تأكيد الحذف", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)

        If result = DialogResult.Yes Then
            ' فتح نافذة إدخال كلمة المرور
            Dim passwordForm As New تأكيد_حذف_بيانات_فيض_الزهراء()
            passwordForm.ShowDialog()

            ' إذا كانت كلمة المرور صحيحة، قم بتنفيذ الحذف
            If passwordForm.IsPasswordCorrect Then
                DeleteRecord() ' استدعاء دالة الحذف
                MessageBox.Show("تم حذف البيانات بنجاح!", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("لم يتم الحذف! كلمة المرور غير صحيحة.", "إلغاء", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        End If
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click

        Try


            ' فتح الاتصال بقاعدة البيانات
            sqlcon.Open()

            ' استعلام لجلب كافة تفاصيل الشخص بناءً على الاسم المدخل في TextBox14
            Dim cmd As New SqlCommand("SELECT * FROM voter_fodalaa WHERE voter_name= @voter_name", sqlcon)
            cmd.Parameters.AddWithValue("@voter_name", TextBox19.Text)

            ' تنفيذ الاستعلام
            Dim reader As SqlDataReader = cmd.ExecuteReader()

            ' التحقق إذا كان تم العثور على الشخص
            If reader.Read() Then
                ' تعبئة التكست بوكسات بالبيانات التي تم جلبها من قاعدة البيانات
                TextBox54.Text = reader("ID_AXIS_FAEDZAHRAA").ToString()
                TextBox19.Text = reader("voter_name").ToString()
                ComboBox8.Text = reader("degree_kinship").ToString()
                TextBox69.Text = reader("voter_phone").ToString()
                ComboBox9.Text = reader("voter_address").ToString()
                TextBox71.Text = reader("votercardnumber").ToString()
                ComboBox10.Text = reader("votercardstatus").ToString()
                ComboBox5.Text = reader("Filter_files").ToString()

                ' تعبئة CheckBox مع فحص القيم الفارغة
                CheckBox4.Checked = CBool(reader("Nominate_observer"))
                CheckBox5.Checked = CBool(reader("Nominate_mentor"))

                TextBox74.Text = reader("Polling_center_number").ToString()

                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''




                ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''


            Else
            MessageBox.Show("الشخص غير موجود.")
            End If
            reader.Close()

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        Finally

            sqlcon.Close()
        End Try

    End Sub


    ''مسح كل ما تم ادخاله في الادوات لادخال غيره
    'Private Sub ClearControls(ctrl As Control)
    '    For Each c As Control In ctrl.Controls
    '        If TypeOf c Is TextBox Then
    '            DirectCast(c, TextBox).Clear()
    '        ElseIf TypeOf c Is ComboBox Then
    '            DirectCast(c, ComboBox).SelectedIndex = -1
    '        ElseIf TypeOf c Is CheckBox Then
    '            DirectCast(c, CheckBox).Checked = False
    '        End If
    '        ' البحث داخل الحاويات الداخلية
    '        If c.HasChildren Then
    '            ClearControls(c)
    '        End If
    '    Next
    'End Sub
    Private Sub ClearControls(ctrl As Control)
        ' قائمة تحتوي على أسماء الـ TextBox التي لا تريد مسحها
        Dim excludedTextBoxes As String() = {"TextBox44", "TextBox45", "TextBox46", "TextBox47", "TextBox48", "TextBox49", "TextBox50", "TextBox51", "TextBox52", "TextBox53", "TextBox59", "TextBox60", "TextBox9", "TextBox35", "TextBox36"}

        For Each c As Control In ctrl.Controls
            If TypeOf c Is TextBox AndAlso Not excludedTextBoxes.Contains(c.Name) Then
                DirectCast(c, TextBox).Clear()
            ElseIf TypeOf c Is ComboBox Then
                DirectCast(c, ComboBox).SelectedIndex = -1
            ElseIf TypeOf c Is CheckBox Then
                DirectCast(c, CheckBox).Checked = False
            End If

            ' البحث داخل الحاويات الداخلية
            If c.HasChildren Then
                ClearControls(c)
            End If
        Next
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' مسح الصور من PictureBox المحددة
        PictureBox5.Image = Nothing
        PictureBox6.Image = Nothing
        PictureBox7.Image = Nothing
        PictureBox8.Image = Nothing
        ' التأكد أن التاب كنترول موجود
        If TabControl1 IsNot Nothing Then
            ' مسح الأدوات داخل جميع التبويبات
            For Each tab As TabPage In TabControl1.TabPages
                ClearControls(tab)
            Next
        End If

        ' مسح الأدوات داخل الفورم ككل (لو عندك أدوات خارج التاب كنترول)
        ClearControls(Me)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        Try
            ' 1) فتح الاتصال
            sqlcon.Open()

            ' 2) بدء معاملة لضمان إدخال البيانات أو التراجع في حال حدوث خطأ
            Dim transaction As SqlTransaction = sqlcon.BeginTransaction()


            ' 5) جملة SQL للإدخال: لاحظي إضافة حقلي Nominate_observer و Nominate_mentor ضمن الأعمدة
            Dim query1 As String = "INSERT INTO AXIS_fodalaa  (NAME, FATHER, GRANDFATHER, great_father, Title, Gender, Socialname, birthDate, familymembers, address, martialstatus, academicachievement, phone1, phone2, joiningdate, pointrefernce, Type, adjective, tributary, axlecardstatus, markaz_alekteaa, Pollingcenteraddress, axle_type, voted_2013, voted_2021, voted_2023, Unifiedcardnumber, axlecardnumber, Residencecardnumber, Rationcardnumber, Unifiedimagepath, Voterimagepath, Housingimagepath, Rationimagepath, Facebooklink, Instagramlink, Telegramlink,TikToklink, Anotherlink1, Anotherlink2) VALUES(@NAME, @FATHER, @GRANDFATHER, @great_father, @Title, @Gender, @Socialname, @birthDate, @familymembers, @address, @martialstatus, @academicachievement, @phone1, @phone2, @joiningdate, @pointrefernce, @Type, @adjective, @tributary, @axlecardstatus, @markaz_alekteaa, @Pollingcenteraddress, @axle_type, @voted_2013, @voted_2021, @voted_2023, @Unifiedcardnumber, @axlecardnumber, @Residencecardnumber, @Rationcardnumber, @Unifiedimagepath, @Voterimagepath, @Housingimagepath, @Rationimagepath, @Facebooklink, @Instagramlink, @Telegramlink,@TikToklink, @Anotherlink1, @Anotherlink2);"


            ' 6) إنشاء أمر الإدخال وربطه بالمعاملة
            Using cmd As New SqlCommand(query1, sqlcon, transaction)

                ' إضافة المعاملات (Parameters) بنفس ترتيب الأعمدة:
                'cmd.Parameters.AddWithValue("@ID", TextBox68.Text)
                cmd.Parameters.AddWithValue("@NAME", TextBox1.Text)
                cmd.Parameters.AddWithValue("@FATHER", TextBox2.Text)
                cmd.Parameters.AddWithValue("@GRANDFATHER", TextBox3.Text)
                cmd.Parameters.AddWithValue("@great_father", TextBox4.Text)
                cmd.Parameters.AddWithValue("@Title", TextBox5.Text)
                cmd.Parameters.AddWithValue("@Gender", ComboBox2.Text)

                cmd.Parameters.AddWithValue("@Socialname", If(String.IsNullOrWhiteSpace(TextBox6.Text), "لا يوجد", TextBox6.Text))
                cmd.Parameters.AddWithValue("@birthDate", If(String.IsNullOrWhiteSpace(TextBox7.Text), "لا يوجد", TextBox7.Text))
                cmd.Parameters.AddWithValue("@familymembers", If(String.IsNullOrWhiteSpace(TextBox8.Text), "لا يوجد", TextBox8.Text))
                cmd.Parameters.AddWithValue("@address", If(String.IsNullOrWhiteSpace(TextBox10.Text), "لا يوجد", TextBox10.Text))
                cmd.Parameters.AddWithValue("@martialstatus", If(String.IsNullOrWhiteSpace(ComboBox4.Text), "لا يوجد", ComboBox4.Text))
                cmd.Parameters.AddWithValue("@academicachievement", If(String.IsNullOrWhiteSpace(ComboBox6.Text), "لا يوجد", ComboBox6.Text))
                cmd.Parameters.AddWithValue("@phone1", If(String.IsNullOrWhiteSpace(TextBox11.Text), "لا يوجد", TextBox11.Text))
                cmd.Parameters.AddWithValue("@phone2", If(String.IsNullOrWhiteSpace(TextBox12.Text), "لا يوجد", TextBox12.Text))
                cmd.Parameters.AddWithValue("@joiningdate", If(String.IsNullOrWhiteSpace(TextBox13.Text), "لا يوجد", TextBox13.Text))
                cmd.Parameters.AddWithValue("@pointrefernce", If(String.IsNullOrWhiteSpace(TextBox16.Text), "لا يوجد", TextBox16.Text))
                cmd.Parameters.AddWithValue("@Type", If(String.IsNullOrWhiteSpace(ComboBox1.Text), "لا يوجد", ComboBox1.Text))
                cmd.Parameters.AddWithValue("@adjective", If(String.IsNullOrWhiteSpace(TextBox17.Text), "لا يوجد", TextBox17.Text))
                cmd.Parameters.AddWithValue("@tributary", If(String.IsNullOrWhiteSpace(TextBox18.Text), "لا يوجد", TextBox18.Text))
                cmd.Parameters.AddWithValue("@axlecardstatus", If(String.IsNullOrWhiteSpace(ComboBox3.Text), "لا يوجد", ComboBox3.Text))
                cmd.Parameters.AddWithValue("@markaz_alekteaa", If(String.IsNullOrWhiteSpace(ComboBox11.Text), "لا يوجد", ComboBox11.Text))
                cmd.Parameters.AddWithValue("@Pollingcenteraddress", If(String.IsNullOrWhiteSpace(TextBox20.Text), "لا يوجد", TextBox20.Text))
                cmd.Parameters.AddWithValue("@axle_type", If(String.IsNullOrWhiteSpace(ComboBox7.Text), "لا يوجد", ComboBox7.Text))

                cmd.Parameters.AddWithValue("@voted_2013", If(CheckBox1.Checked, 1, 0))
                cmd.Parameters.AddWithValue("@voted_2021", If(CheckBox2.Checked, 1, 0))
                cmd.Parameters.AddWithValue("@voted_2023", If(CheckBox3.Checked, 1, 0))

                cmd.Parameters.AddWithValue("@Unifiedcardnumber", If(String.IsNullOrWhiteSpace(TextBox26.Text), "لا يوجد", TextBox26.Text))
                cmd.Parameters.AddWithValue("@axlecardnumber", If(String.IsNullOrWhiteSpace(TextBox37.Text), "لا يوجد", TextBox37.Text))
                cmd.Parameters.AddWithValue("@Residencecardnumber", If(String.IsNullOrWhiteSpace(TextBox41.Text), "لا يوجد", TextBox41.Text))
                cmd.Parameters.AddWithValue("@Rationcardnumber", If(String.IsNullOrWhiteSpace(TextBox42.Text), "لا يوجد", TextBox42.Text))
                cmd.Parameters.AddWithValue("@Unifiedimagepath", If(String.IsNullOrWhiteSpace(TextBox43.Text), "لا يوجد", TextBox43.Text))
                cmd.Parameters.AddWithValue("@Voterimagepath", If(String.IsNullOrWhiteSpace(TextBox21.Text), "لا يوجد", TextBox21.Text))
                cmd.Parameters.AddWithValue("@Housingimagepath", If(String.IsNullOrWhiteSpace(TextBox39.Text), "لا يوجد", TextBox39.Text))
                cmd.Parameters.AddWithValue("@Rationimagepath", If(String.IsNullOrWhiteSpace(TextBox67.Text), "لا يوجد", TextBox67.Text))
                cmd.Parameters.AddWithValue("@Facebooklink", If(String.IsNullOrWhiteSpace(TextBox15.Text), "لا يوجد", TextBox15.Text))
                cmd.Parameters.AddWithValue("@Instagramlink", If(String.IsNullOrWhiteSpace(TextBox30.Text), "لا يوجد", TextBox30.Text))
                cmd.Parameters.AddWithValue("@Telegramlink", If(String.IsNullOrWhiteSpace(TextBox31.Text), "لا يوجد", TextBox31.Text))
                cmd.Parameters.AddWithValue("@TikToklink", If(String.IsNullOrWhiteSpace(TextBox32.Text), "لا يوجد", TextBox32.Text))
                cmd.Parameters.AddWithValue("@Anotherlink1", If(String.IsNullOrWhiteSpace(TextBox33.Text), "لا يوجد", TextBox33.Text))
                cmd.Parameters.AddWithValue("@Anotherlink2", If(String.IsNullOrWhiteSpace(TextBox34.Text), "لا يوجد", TextBox34.Text))


                ' 7) تنفيذ الإدخال
                cmd.ExecuteNonQuery()

                ' 8) تأكيد العملية
                transaction.Commit()
            End Using

            ' رسالة نجاح
            MessageBox.Show("تمت إضافة البيانات بنجاح!", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information)

            ' تحديث DataGridView
            LoadData()
            DataGridView1.Refresh()

        Catch ex As Exception
            ' في حالة الخطأ، التراجع عن العملية
            MessageBox.Show("خطأ أثناء الحفظ: " & ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            ' إغلاق الاتصال
            If sqlcon.State = ConnectionState.Open Then
                sqlcon.Close()
            End If
        End Try

    End Sub
    'البحث لجدول المحور

    Public Class Person
        Public Property FullName As String

        Public Sub New(name As String)
            FullName = name
        End Sub

        Public Overrides Function ToString() As String
            Return FullName
        End Function
    End Class
    Private Sub TextBox14_TextChanged(sender As Object, e As EventArgs) Handles TextBox14.TextChanged

        ' إذا كان النص فارغًا لا نعرض أي نتائج
        If String.IsNullOrWhiteSpace(TextBox14.Text) Then
            ListBox1.Items.Clear()
            Return
        End If

        Try
            sqlcon.Open()

            ' استعلام البحث باستخدام النص المدخل في TextBox14
            ' هنا نبحث عن النص المدخل داخل الاسم الكامل (يتضمن الأجزاء المختلفة للاسم)
            Dim cmd As New SqlCommand("SELECT NAME + ' ' + FATHER + ' ' + GRANDFATHER + ' ' + great_father + ' ' + Title AS FullName FROM AXIS_fodalaa WHERE NAME LIKE @Search OR FATHER LIKE @Search OR GRANDFATHER LIKE @Search OR great_father LIKE @Search OR Title LIKE @Search", sqlcon)

            ' إضافة قيمة الـ TextBox14 إلى الاستعلام باستخدام % لتمكين البحث عن جزء من النص
            cmd.Parameters.AddWithValue("@Search", "%" & TextBox14.Text & "%")

            ' ملء الـ ListBox1 بالنتائج
            Dim reader As SqlDataReader = cmd.ExecuteReader()
            ListBox1.Items.Clear()

            While reader.Read()
                ' إضافة الاسم الكامل إلى الـ ListBox
                Dim person As New Person(reader("FullName").ToString())
                ListBox1.Items.Add(person)
            End While
            reader.Close()

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        Finally
            sqlcon.Close()
        End Try
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged

        If ListBox1.SelectedItem IsNot Nothing Then
            ' نقل الاسم من الـ ListBox إلى TextBox14
            Dim selectedPerson As Person = CType(ListBox1.SelectedItem, Person)
            TextBox14.Text = selectedPerson.FullName

            ' مسح بيانات ListBox بعد اختيار العنصر
            ListBox1.Items.Clear()
        End If

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click

        Try
           

            ' فتح الاتصال بقاعدة البيانات
            sqlcon.Open()

            ' استعلام لجلب كافة تفاصيل الشخص بناءً على الاسم المدخل في TextBox14
            Dim cmd As New SqlCommand("SELECT * FROM AXIS_fodalaa WHERE NAME + ' ' + FATHER + ' ' + GRANDFATHER + ' ' + great_father + ' ' + Title = @FullName", sqlcon)
            cmd.Parameters.AddWithValue("@FullName", TextBox14.Text)

            ' تنفيذ الاستعلام
            Dim reader As SqlDataReader = cmd.ExecuteReader()

            ' التحقق إذا كان تم العثور على الشخص
            If reader.Read() Then
                ' تعبئة التكست بوكسات بالبيانات التي تم جلبها من قاعدة البيانات
                TextBox68.Text = reader("ID").ToString()
                TextBox1.Text = reader("NAME").ToString()
                TextBox2.Text = reader("FATHER").ToString()
                TextBox3.Text = reader("GRANDFATHER").ToString()
                TextBox4.Text = reader("great_father").ToString()
                TextBox5.Text = reader("Title").ToString()
                ComboBox2.Text = reader("Gender").ToString()
                TextBox6.Text = reader("Socialname").ToString()
                TextBox7.Text = reader("birthDate").ToString()
                TextBox8.Text = reader("familymembers").ToString()
                TextBox10.Text = reader("address").ToString()
                ComboBox4.Text = reader("martialstatus").ToString()
                ComboBox6.Text = reader("academicachievement").ToString()
                TextBox11.Text = reader("phone1").ToString()
                TextBox12.Text = reader("phone2").ToString()
                TextBox13.Text = reader("joiningdate").ToString()
                TextBox16.Text = reader("pointrefernce").ToString()
                ComboBox1.Text = reader("Type").ToString()
                TextBox17.Text = reader("adjective").ToString()
                TextBox18.Text = reader("tributary").ToString()
                ComboBox11.Text = reader("markaz_alekteaa").ToString()
                ComboBox7.Text = reader("axle_type").ToString()
                ComboBox3.Text = reader("axlecardstatus").ToString()
                TextBox20.Text = reader("Pollingcenteraddress").ToString()

                ' تعبئة CheckBox مع فحص القيم الفارغة
                CheckBox1.Checked = CBool(reader("voted_2013"))
                CheckBox2.Checked = CBool(reader("voted_2021"))
                CheckBox3.Checked = CBool(reader("voted_2023"))

                ' تعبئة بيانات البطاقات والروابط
                TextBox26.Text = reader("Unifiedcardnumber").ToString()
                TextBox37.Text = reader("axlecardnumber").ToString()
                TextBox41.Text = reader("Residencecardnumber").ToString()
                TextBox42.Text = reader("Rationcardnumber").ToString()
                TextBox43.Text = reader("Unifiedimagepath").ToString()
                TextBox21.Text = reader("Voterimagepath").ToString()
                TextBox39.Text = reader("Housingimagepath").ToString()
                TextBox67.Text = reader("Rationimagepath").ToString()

                ' جلب البيانات من قاعدة البيانات ووضعها في التكست بوكس
                TextBox15.Text = If(String.IsNullOrEmpty(reader("Facebooklink").ToString()), "لا يوجد", reader("Facebooklink").ToString())
                TextBox30.Text = If(String.IsNullOrEmpty(reader("Instagramlink").ToString()), "لا يوجد", reader("Instagramlink").ToString())
                TextBox31.Text = If(String.IsNullOrEmpty(reader("Telegramlink").ToString()), "لا يوجد", reader("Telegramlink").ToString())
                TextBox32.Text = If(String.IsNullOrEmpty(reader("TikToklink").ToString()), "لا يوجد", reader("TikToklink").ToString())
                TextBox33.Text = If(String.IsNullOrEmpty(reader("Anotherlink1").ToString()), "لا يوجد", reader("Anotherlink1").ToString())
                TextBox34.Text = If(String.IsNullOrEmpty(reader("Anotherlink2").ToString()), "لا يوجد", reader("Anotherlink2").ToString())

                ' إزالة الحدث أولًا لتجنب التكرار
                RemoveHandler TextBox15.Click, AddressOf OpenLink
                RemoveHandler TextBox30.Click, AddressOf OpenLink
                RemoveHandler TextBox31.Click, AddressOf OpenLink
                RemoveHandler TextBox32.Click, AddressOf OpenLink
                RemoveHandler TextBox33.Click, AddressOf OpenLink
                RemoveHandler TextBox34.Click, AddressOf OpenLink

                ' إضافة الحدث Click بعد تنفيذ البحث مباشرة
                AddHandler TextBox15.Click, AddressOf OpenLink
                AddHandler TextBox30.Click, AddressOf OpenLink
                AddHandler TextBox31.Click, AddressOf OpenLink
                AddHandler TextBox32.Click, AddressOf OpenLink
                AddHandler TextBox33.Click, AddressOf OpenLink
                AddHandler TextBox34.Click, AddressOf OpenLink


                'TextBox22.Text = reader("votercardnumber").ToString()
                TextBox22.Text = Convert.ToString(reader("NAME")) & " " & Convert.ToString(reader("FATHER")) & " " & Convert.ToString(reader("GRANDFATHER")) & " " & Convert.ToString(reader("great_father")) & " " & Convert.ToString(reader("Title"))
                TextBox27.Text = reader("Socialname").ToString()
                TextBox28.Text = reader("birthDate").ToString()
                TextBox29.Text = reader("address").ToString()
                TextBox38.Text = reader("axlecardnumber").ToString()
                TextBox40.Text = reader("markaz_alekteaa").ToString()


                ''    ' جلب الاسم الكامل من TextBox
                ''    Dim fullName As String = TextBox14.Text

                ''    ' جلب AxisID بناءً على الاسم الكامل
                ''    Dim axisID As Integer = GetAxisIDByFullName(fullName)

                ''    ' إذا تم العثور على المحور
                ''    If axisID <> -1 Then
                ''        ' جلب بيانات الناخبين التابعين لهذا المحور
                ''        Dim dt As DataTable = GetVotersByAxisID(axisID)
                ''        ' عرض البيانات في DataGridView
                ''        DataGridView1.DataSource = dt

                ''        ' تغيير لون النص فقط للناخبين التابعين لهذا المحور
                ''        For Each row As DataGridViewRow In DataGridView1.Rows
                ''            If Not row.IsNewRow Then
                ''                row.DefaultCellStyle.ForeColor = Color.HotPink ' تغيير لون النص إلى وردي
                ''            End If
                ''        Next

                ''        ' إذا لم يتم العثور على بيانات
                ''        If dt.Rows.Count = 0 Then
                ''            MessageBox.Show("لم يتم العثور على الناخبين التابعين لهذا المحور.")
                ''        End If
                ''    Else
                ''        MessageBox.Show("لم يتم العثور على المحور بهذا الاسم الرباعي واللقب.")
                ''    End If
                ''Else
                ''    MessageBox.Show("الشخص غير موجود.")
                ''End If
                ' جلب الاسم الكامل من TextBox
                Dim fullName As String = TextBox14.Text

                ' جلب AxisID بناءً على الاسم الكامل
                Dim axisID As Integer = GetAxisIDByFullName(fullName)

                ' إذا تم العثور على المحور
                If axisID <> -1 Then
                    ' جلب بيانات الناخبين التابعين لهذا المحور
                    Dim dt As DataTable = GetVotersByAxisID(axisID)
                    ' عرض البيانات في DataGridView
                    DataGridView1.DataSource = dt

                    ' تغيير لون النص فقط للناخبين التابعين لهذا المحور
                    For Each row As DataGridViewRow In DataGridView1.Rows
                        If Not row.IsNewRow Then
                            row.DefaultCellStyle.ForeColor = Color.HotPink ' تغيير لون النص إلى وردي
                        End If
                    Next

                    ' إذا لم يتم العثور على بيانات
                    If dt.Rows.Count = 0 Then
                        MessageBox.Show("لم يتم العثور على الناخبين التابعين لهذا المحور.")
                    End If
                Else
                    MessageBox.Show("لم يتم العثور على المحور بهذا الاسم الرباعي واللقب.")
                End If
            Else
                MessageBox.Show("الشخص غير موجود.")
            End If
            reader.Close()

            ' الحصول على اسم المحور من TextBox14
            Dim pivotName As String = TextBox14.Text.Trim()

            ' التأكد من أن اسم المحور ليس فارغًا قبل البحث
            If pivotName <> "" Then
                Try
                    ' استعلام SQL لجلب عدد السجلات الخاصة بالمحور المطلوب
                    Dim query As String = "SELECT COUNT(*) FROM voter_fodalaa WHERE voter_name = @voter_name AND voter_name IS NOT NULL"

                    Dim cmd1 As New SqlCommand(query, sqlcon)
                    cmd1.Parameters.AddWithValue("@voter_name", pivotName)

                    ' تنفيذ الاستعلام وجلب العدد
                    Dim count1 As Integer = Convert.ToInt32(cmd1.ExecuteScalar())

                    ' تحديث TextBox80 بالعدد
                    TextBox25.Text = count1.ToString()
                Catch ex As Exception
                    MessageBox.Show("حدث خطأ أثناء البحث: " & ex.Message)
                End Try
            Else
                ' إذا كان TextBox14 فارغًا، يتم مسح العدد في TextBox80
                TextBox25.Text = ""
            End If
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            If pivotName <> "" Then
                Try
                    ' استعلام SQL لجلب عدد السجلات الخاصة بالمحور المطلوب مع شرط أن البطاقة محدثة فقط
                    Dim query As String = "SELECT COUNT(*) FROM voter_fodalaa WHERE votercardstatus = @votercardstatus AND votercardstatus = 'محدثة'"

                    Dim cmd1 As New SqlCommand(query, sqlcon)
                    cmd1.Parameters.AddWithValue("@votercardstatus", pivotName)

                    ' تنفيذ الاستعلام وجلب العدد
                    Dim count1 As Integer = Convert.ToInt32(cmd1.ExecuteScalar())

                    ' تحديث TextBox25 بالعدد
                    TextBox23.Text = count1.ToString()
                Catch ex As Exception
                    MessageBox.Show("حدث خطأ أثناء البحث: " & ex.Message)
                End Try
            Else
                ' إذا كان TextBox14 فارغًا، يتم مسح العدد في TextBox25
                TextBox23.Text = ""
            End If
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            If pivotName <> "" Then
                Try
                    ' استعلام SQL لجلب عدد السجلات الخاصة بالمحور المطلوب مع شرط أن البطاقة محدثة فقط
                    Dim query As String = "SELECT COUNT(*) FROM voter_fodalaa WHERE votercardstatus = @votercardstatus AND votercardstatus = 'غير محدثة'"

                    Dim cmd1 As New SqlCommand(query, sqlcon)
                    cmd1.Parameters.AddWithValue("@votercardstatus", pivotName)

                    ' تنفيذ الاستعلام وجلب العدد
                    Dim count1 As Integer = Convert.ToInt32(cmd1.ExecuteScalar())

                    ' تحديث TextBox25 بالعدد
                    TextBox24.Text = count1.ToString()
                Catch ex As Exception
                    MessageBox.Show("حدث خطأ أثناء البحث: " & ex.Message)
                End Try
            Else
                ' إذا كان TextBox14 فارغًا، يتم مسح العدد في TextBox25
                TextBox24.Text = ""
            End If
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        Finally

            sqlcon.Close()
        End Try

    End Sub

    '' '' دالة لاسترجاع معرف المحور بناءً على الاسم الرباعي واللقب
    ' ''Public Function GetAxisIDByFullName(fullName As String) As Integer
    ' ''    ' استعلام SQL لجلب معرف المحور باستخدام الاسم الرباعي واللقب
    ' ''    Dim query As String = "SELECT ID FROM AXIS_FAEDZAHRAA WHERE (NAME + ' ' + FATHER + ' ' + GRANDFATHER + ' ' + great_father + ' ' + Title) = @FullName"
    ' ''    Dim aID As Integer = -1 ' قيمة افتراضية تدل على أنه لم يتم العثور على المحور

    ' ''    ' استخدام اتصال SQL لتنفيذ الاستعلام
    ' ''    Using sqlcon As New SqlConnection("Server=DESKTOP-08SGMQ2\SQLEXPRESS;Database=white_hand;Integrated Security=True")
    ' ''        Using cmd As New SqlCommand(query, sqlcon)
    ' ''            ' إضافة معلمة الاسم الكامل للاستعلام
    ' ''            cmd.Parameters.AddWithValue("@FullName", fullName)

    ' ''            ' فتح الاتصال وتنفيذ الاستعلام
    ' ''            sqlcon.Open()
    ' ''            Dim result = cmd.ExecuteScalar()
    ' ''            If result IsNot Nothing Then
    ' ''                ' تحويل النتيجة إلى معرف المحور
    ' ''                aID = Convert.ToInt32(result)
    ' ''            End If
    ' ''        End Using
    ' ''    End Using

    ' ''    ' إرجاع معرف المحور إذا تم العثور عليه، أو -1 إذا لم يتم العثور على المحور
    ' ''    Return aID
    ' ''End Function


    ' '' '' دالة لاسترجاع بيانات الناخبين بناءً على معرف المحور
    '' ''Public Function GetVotersByAxisID(aID As Integer) As DataTable
    '' ''    ' استعلام SQL لجلب بيانات الناخبين بناءً على معرف المحور
    '' ''    Dim query As String = "SELECT ID,voter_name,degree_kinship,voter_phone,voter_address,votercardnumber,votercardstatus,Filter_files,Nominate_observer,Nominate_mentor,Polling_center_number FROM voter_FAEDZAHRAA_1 WHERE ID = @ID"
    '' ''    Dim dt As New DataTable()

    '' ''    ' استخدام اتصال SQL لتنفيذ الاستعلام
    '' ''    Using sqlcon As New SqlConnection("Server=DESKTOP-08SGMQ2\SQLEXPRESS;Database=white_hand;Integrated Security=True")
    '' ''        Using cmd As New SqlCommand(query, sqlcon)
    '' ''            ' إضافة معلمة معرف المحور للاستعلام
    '' ''            cmd.Parameters.AddWithValue("@ID", aID)

    '' ''            ' استخدام SqlDataAdapter لملء DataTable بالنتائج
    '' ''            Using adapter As New SqlDataAdapter(cmd)
    '' ''                adapter.Fill(dt)
    '' ''            End Using
    '' ''        End Using
    '' ''    End Using

    ' '' '' إرجاع DataTable الذي يحتوي على بيانات الناخبين
    '' ''    Return dt
    '' ''End Function


    ' دالة لاسترجاع معرف المحور بناءً على الاسم الرباعي واللقب
    Public Function GetAxisIDByFullName(fullName As String) As Integer
        ' استعلام SQL لجلب معرف المحور باستخدام الاسم الرباعي واللقب
        Dim query As String = "SELECT ID FROM AXIS_fodalaa WHERE (NAME + ' ' + FATHER + ' ' + GRANDFATHER + ' ' + great_father + ' ' + Title) = @FullName"
        Dim aID As Integer = -1 ' قيمة افتراضية تدل على أنه لم يتم العثور على المحور

        ' استخدام اتصال SQL لتنفيذ الاستعلام
        Using sqlcon As New SqlConnection("Server=DESKTOP-08SGMQ2\SQLEXPRESS;Database=white_hand;Integrated Security=True")
            Using cmd As New SqlCommand(query, sqlcon)
                ' إضافة معلمة الاسم الكامل للاستعلام
                cmd.Parameters.AddWithValue("@FullName", fullName)

                ' فتح الاتصال وتنفيذ الاستعلام
                sqlcon.Open()
                Dim result = cmd.ExecuteScalar()
                If result IsNot Nothing Then
                    ' تحويل النتيجة إلى معرف المحور
                    aID = Convert.ToInt32(result)
                End If
            End Using
        End Using

        ' إرجاع معرف المحور أو -1 إذا لم يتم العثور عليه
        Return aID
    End Function


    ' دالة لاسترجاع بيانات الناخبين بناءً على معرف المحور
    Public Function GetVotersByAxisID(axisID As Integer) As DataTable
        ' استعلام SQL لجلب بيانات الناخبين بناءً على معرف المحور
        Dim query As String = "SELECT ID, voter_name, degree_kinship, voter_phone, voter_address, votercardnumber, votercardstatus, Filter_files, Nominate_observer, Nominate_mentor, Polling_center_number FROM voter_fodalaa WHERE voter_fodalaa.ID_AXIS_FAEDZAHRAA = @ID"
        Dim dt As New DataTable()

        ' استخدام اتصال SQL لتنفيذ الاستعلام
        Using sqlcon As New SqlConnection("Server=DESKTOP-08SGMQ2\SQLEXPRESS;Database=white_hand;Integrated Security=True")
            Using cmd As New SqlCommand(query, sqlcon)
                ' إضافة معلمة معرف المحور للاستعلام
                cmd.Parameters.AddWithValue("@ID", axisID)

                ' استخدام SqlDataAdapter لملء DataTable بالنتائج
                Using adapter As New SqlDataAdapter(cmd)
                    adapter.Fill(dt)
                End Using
            End Using
        End Using

        ' إرجاع DataTable الذي يحتوي على بيانات الناخبين
        Return dt
    End Function

    ' دالة لاسترجاع معرف المحور بناءً على الاسم الرباعي واللقب

    Private Sub OpenLink(sender As Object, e As EventArgs)
        Dim tb As TextBox = TryCast(sender, TextBox)

        ' إذا كان التكست فارغ أو ما بيه رابط، نخلي بيه "لا يوجد"
        If tb IsNot Nothing AndAlso String.IsNullOrWhiteSpace(tb.Text) Then
            tb.Text = "لا يوجد"
        ElseIf tb.Text.StartsWith("http") Then
            Process.Start(tb.Text) ' يفتح الرابط في المتصفح
        Else
            tb.Text = "لا يوجد"
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Try
            ' فتح الاتصال
            sqlcon.Open()

            ' بدء معاملة لضمان إدخال البيانات أو التراجع في حال حدوث خطأ
            Dim transaction As SqlTransaction = sqlcon.BeginTransaction()

            ' جملة SQL لتحديث البيانات
            Dim query As String = "UPDATE AXIS_fodalaa SET NAME = @NAME, FATHER = @FATHER, GRANDFATHER = @GRANDFATHER, great_father = @great_father, Title = @Title, Gender = @Gender, Socialname = @Socialname, birthDate = @birthDate, familymembers = @familymembers, address = @address, martialstatus = @martialstatus, academicachievement = @academicachievement, phone1 = @phone1, phone2 = @phone2, joiningdate = @joiningdate, pointrefernce = @pointrefernce, Type = @Type, adjective = @adjective, tributary = @tributary, axlecardstatus = @axlecardstatus, markaz_alekteaa = @markaz_alekteaa, Pollingcenteraddress = @Pollingcenteraddress, axle_type = @axle_type, voted_2013 = @voted_2013, voted_2021 = @voted_2021, voted_2023 = @voted_2023, Unifiedcardnumber = @Unifiedcardnumber, axlecardnumber = @axlecardnumber, Residencecardnumber = @Residencecardnumber, Rationcardnumber = @Rationcardnumber, Unifiedimagepath = @Unifiedimagepath, Voterimagepath = @Voterimagepath, Housingimagepath = @Housingimagepath, Rationimagepath = @Rationimagepath, Facebooklink = @Facebooklink, Instagramlink = @Instagramlink, Telegramlink = @Telegramlink, TikToklink = @TikToklink, Anotherlink1 = @Anotherlink1, Anotherlink2 = @Anotherlink2 WHERE ID = @ID "

            ' إنشاء أمر التحديث وربطه بالمعاملة
            Using cmd As New SqlCommand(query, sqlcon, transaction)

                ' إضافة المعاملات بنفس ترتيب الأعمدة
                cmd.Parameters.AddWithValue("@ID", TextBox68.Text)
                cmd.Parameters.AddWithValue("@NAME", TextBox1.Text)
                cmd.Parameters.AddWithValue("@FATHER", TextBox2.Text)
                cmd.Parameters.AddWithValue("@GRANDFATHER", TextBox3.Text)
                cmd.Parameters.AddWithValue("@great_father", TextBox4.Text)
                cmd.Parameters.AddWithValue("@Title", TextBox5.Text)
                cmd.Parameters.AddWithValue("@Gender", ComboBox2.Text)
                cmd.Parameters.AddWithValue("@Socialname", If(String.IsNullOrWhiteSpace(TextBox6.Text), "لا يوجد", TextBox6.Text))
                cmd.Parameters.AddWithValue("@birthDate", If(String.IsNullOrWhiteSpace(TextBox7.Text), "لا يوجد", TextBox7.Text))
                cmd.Parameters.AddWithValue("@familymembers", If(String.IsNullOrWhiteSpace(TextBox8.Text), "لا يوجد", TextBox8.Text))
                cmd.Parameters.AddWithValue("@address", If(String.IsNullOrWhiteSpace(TextBox10.Text), "لا يوجد", TextBox10.Text))
                cmd.Parameters.AddWithValue("@martialstatus", If(String.IsNullOrWhiteSpace(ComboBox4.Text), "لا يوجد", ComboBox4.Text))
                cmd.Parameters.AddWithValue("@academicachievement", If(String.IsNullOrWhiteSpace(ComboBox6.Text), "لا يوجد", ComboBox6.Text))
                cmd.Parameters.AddWithValue("@phone1", If(String.IsNullOrWhiteSpace(TextBox11.Text), "لا يوجد", TextBox11.Text))
                cmd.Parameters.AddWithValue("@phone2", If(String.IsNullOrWhiteSpace(TextBox12.Text), "لا يوجد", TextBox12.Text))
                cmd.Parameters.AddWithValue("@joiningdate", If(String.IsNullOrWhiteSpace(TextBox13.Text), "لا يوجد", TextBox13.Text))
                cmd.Parameters.AddWithValue("@pointrefernce", If(String.IsNullOrWhiteSpace(TextBox16.Text), "لا يوجد", TextBox16.Text))
                cmd.Parameters.AddWithValue("@Type", If(String.IsNullOrWhiteSpace(ComboBox1.Text), "لا يوجد", ComboBox1.Text))
                cmd.Parameters.AddWithValue("@adjective", If(String.IsNullOrWhiteSpace(TextBox17.Text), "لا يوجد", TextBox17.Text))
                cmd.Parameters.AddWithValue("@tributary", If(String.IsNullOrWhiteSpace(TextBox18.Text), "لا يوجد", TextBox18.Text))
                cmd.Parameters.AddWithValue("@axlecardstatus", If(String.IsNullOrWhiteSpace(ComboBox3.Text), "لا يوجد", ComboBox3.Text))
                cmd.Parameters.AddWithValue("@markaz_alekteaa", If(String.IsNullOrWhiteSpace(ComboBox11.Text), "لا يوجد", ComboBox11.Text))
                cmd.Parameters.AddWithValue("@Pollingcenteraddress", If(String.IsNullOrWhiteSpace(TextBox20.Text), "لا يوجد", TextBox20.Text))
                cmd.Parameters.AddWithValue("@axle_type", If(String.IsNullOrWhiteSpace(ComboBox7.Text), "لا يوجد", ComboBox7.Text))
                cmd.Parameters.AddWithValue("@voted_2013", If(CheckBox1.Checked, 1, 0))
                cmd.Parameters.AddWithValue("@voted_2021", If(CheckBox2.Checked, 1, 0))
                cmd.Parameters.AddWithValue("@voted_2023", If(CheckBox3.Checked, 1, 0))
                cmd.Parameters.AddWithValue("@Unifiedcardnumber", If(String.IsNullOrWhiteSpace(TextBox26.Text), "لا يوجد", TextBox26.Text))
                cmd.Parameters.AddWithValue("@axlecardnumber", If(String.IsNullOrWhiteSpace(TextBox37.Text), "لا يوجد", TextBox37.Text))
                cmd.Parameters.AddWithValue("@Residencecardnumber", If(String.IsNullOrWhiteSpace(TextBox41.Text), "لا يوجد", TextBox41.Text))
                cmd.Parameters.AddWithValue("@Rationcardnumber", If(String.IsNullOrWhiteSpace(TextBox42.Text), "لا يوجد", TextBox42.Text))
                cmd.Parameters.AddWithValue("@Unifiedimagepath", If(String.IsNullOrWhiteSpace(TextBox43.Text), "لا يوجد", TextBox43.Text))
                cmd.Parameters.AddWithValue("@Voterimagepath", If(String.IsNullOrWhiteSpace(TextBox21.Text), "لا يوجد", TextBox21.Text))
                cmd.Parameters.AddWithValue("@Housingimagepath", If(String.IsNullOrWhiteSpace(TextBox39.Text), "لا يوجد", TextBox39.Text))
                cmd.Parameters.AddWithValue("@Rationimagepath", If(String.IsNullOrWhiteSpace(TextBox67.Text), "لا يوجد", TextBox67.Text))
                cmd.Parameters.AddWithValue("@Facebooklink", If(String.IsNullOrWhiteSpace(TextBox15.Text), "لا يوجد", TextBox15.Text))
                cmd.Parameters.AddWithValue("@Instagramlink", If(String.IsNullOrWhiteSpace(TextBox30.Text), "لا يوجد", TextBox30.Text))
                cmd.Parameters.AddWithValue("@Telegramlink", If(String.IsNullOrWhiteSpace(TextBox31.Text), "لا يوجد", TextBox31.Text))
                cmd.Parameters.AddWithValue("@TikToklink", If(String.IsNullOrWhiteSpace(TextBox32.Text), "لا يوجد", TextBox32.Text))
                cmd.Parameters.AddWithValue("@Anotherlink1", If(String.IsNullOrWhiteSpace(TextBox33.Text), "لا يوجد", TextBox33.Text))
                cmd.Parameters.AddWithValue("@Anotherlink2", If(String.IsNullOrWhiteSpace(TextBox34.Text), "لا يوجد", TextBox34.Text))

                ' تنفيذ الاستعلام
                cmd.ExecuteNonQuery()

                ' تأكيد المعاملة
                transaction.Commit()

                ' إغلاق الاتصال
                sqlcon.Close()

                MessageBox.Show("تم تحديث البيانات بنجاح!", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End Using

        Catch ex As Exception
            ' في حالة حدوث خطأ، نقوم بالتراجع عن المعاملة
            If sqlcon.State = ConnectionState.Open Then
                sqlcon.Close()
            End If
            MessageBox.Show("حدث خطأ أثناء تحديث البيانات: " & ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub DeleteRecord1()
        Try
            Using sqlcon As New SqlConnection("Server=DESKTOP-08SGMQ2\SQLEXPRESS;Database=white_hand;Integrated Security=True")
                sqlcon.Open()
                Dim query As String = "DELETE FROM AXIS_fodalaa WHERE ID = @ID"
                Using cmd As New SqlCommand(query, sqlcon)
                    cmd.Parameters.AddWithValue("@ID", TextBox68.Text)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("حدث خطأ أثناء الحذف: " & ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click

        ' تأكيد الحذف
        Dim result As DialogResult = MessageBox.Show("هل أنت متأكد من الحذف؟", "تأكيد الحذف", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)

        If result = DialogResult.Yes Then
            ' فتح نافذة إدخال كلمة المرور
            Dim passwordForm As New تأكيد_حذف_بيانات_فيض_الزهراء()
            passwordForm.ShowDialog()

            ' إذا كانت كلمة المرور صحيحة، قم بتنفيذ الحذف
            If passwordForm.IsPasswordCorrect Then
                DeleteRecord1() ' استدعاء دالة الحذف
                MessageBox.Show("تم حذف البيانات بنجاح!", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("لم يتم الحذف! كلمة المرور غير صحيحة.", "إلغاء", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        End If
    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        Try
            ' فتح الاتصال
            sqlcon.Open()

            ' بدء معاملة لضمان إدخال البيانات أو التراجع في حال حدوث خطأ
            Dim transaction As SqlTransaction = sqlcon.BeginTransaction()

            ' جملة SQL لتحديث البيانات
            Dim query As String = "UPDATE voter_fodalaa SET ID_AXIS_FAEDZAHRAA = @ID_AXIS_FAEDZAHRAA, voter_name = @voter_name, degree_kinship = @degree_kinship, voter_phone = @voter_phone, voter_address = @voter_address, votercardnumber = @votercardnumber, votercardstatus = @votercardstatus, Filter_files = @Filter_files, Nominate_observer = @Nominate_observer, Nominate_mentor = @Nominate_mentor, Polling_center_number = @Polling_center_number, Voter_governorate_name = @Voter_governorate_name, New_judiciary = @New_judiciary, New_side = @New_side, Supply_number = @Supply_number, Supply_center_name = @Supply_center_name, Registration_Center_Code = @Registration_Center_Code, Registration_Center_Name = @Registration_Center_Name, Polling_Center_Name = @Polling_Center_Name, Polling_station_address = @Polling_station_address, Number_stations = @Number_stations WHERE voter_name = @voter_name "

            ' إنشاء أمر التحديث وربطه بالمعاملة
            Using cmd As New SqlCommand(query, sqlcon, transaction)

                ' إضافة المعاملات بنفس ترتيب الأعمدة
                cmd.Parameters.AddWithValue("@ID_AXIS_FAEDZAHRAA", TextBox54.Text)
                cmd.Parameters.AddWithValue("@voter_name", TextBox19.Text)
                cmd.Parameters.AddWithValue("@degree_kinship", ComboBox8.Text)
                cmd.Parameters.AddWithValue("@voter_phone", TextBox69.Text)
                cmd.Parameters.AddWithValue("@voter_address", ComboBox9.Text)
                cmd.Parameters.AddWithValue("@votercardnumber", TextBox71.Text)
                cmd.Parameters.AddWithValue("@votercardstatus", ComboBox10.Text)
                cmd.Parameters.AddWithValue("@Filter_files", ComboBox5.Text)
                cmd.Parameters.AddWithValue("@Nominate_observer", If(CheckBox4.Checked, 1, 0))
                cmd.Parameters.AddWithValue("@Nominate_mentor", If(CheckBox5.Checked, 1, 0))
                cmd.Parameters.AddWithValue("@Polling_center_number", TextBox74.Text)

                cmd.Parameters.AddWithValue("@Voter_governorate_name", TextBox55.Text)
                cmd.Parameters.AddWithValue("@New_judiciary", TextBox56.Text)
                cmd.Parameters.AddWithValue("@New_side", TextBox57.Text)
                cmd.Parameters.AddWithValue("@Supply_number", TextBox58.Text)
                cmd.Parameters.AddWithValue("@Supply_center_name", TextBox61.Text)
                cmd.Parameters.AddWithValue("@Registration_Center_Code", TextBox62.Text)
                cmd.Parameters.AddWithValue("@Registration_Center_Name", TextBox63.Text)
                cmd.Parameters.AddWithValue("@Polling_Center_Name", TextBox64.Text)
                cmd.Parameters.AddWithValue("@Polling_station_address", TextBox65.Text)
                cmd.Parameters.AddWithValue("@Number_stations", TextBox66.Text)


                ' بيانات الكسب "الجدول الثاني" 

                ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

                ' تنفيذ الاستعلام
                cmd.ExecuteNonQuery()

                ' تأكيد المعاملة
                transaction.Commit()

                ' إغلاق الاتصال
                sqlcon.Close()

                MessageBox.Show("تم تحديث البيانات بنجاح!", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End Using

        Catch ex As Exception
            ' في حالة حدوث خطأ، نقوم بالتراجع عن المعاملة
            If sqlcon.State = ConnectionState.Open Then
                sqlcon.Close()
            End If
            MessageBox.Show("حدث خطأ أثناء تحديث البيانات: " & ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub PictureBox5_Click(sender As Object, e As EventArgs) Handles PictureBox5.Click

        ' ✅ التأكد أن الصورة ليست "لا يوجد" قبل الفتح
        If PictureBox5.Image IsNot Nothing AndAlso TextBox43.Text <> "لا يوجد" Then
            عرض_صور_الفضلاء.PictureBoxshow.Image = PictureBox5.Image
            عرض_صور_الفضلاء.PictureBoxshow.BorderStyle = BorderStyle.FixedSingle ' ✅ إضافة إطار للصورة الكبيرة
            عرض_صور_الفضلاء.ShowDialog() ' ✅ جعلها تظهر كنافذة مستقلة وتغلق بعد المشاهدة
            عرض_صور_الفضلاء.PictureBoxshow.Image = Nothing ' ✅ مسح الصورة عند الإغلاق
        End If

    End Sub

    Private Sub TextBox43_TextChanged(sender As Object, e As EventArgs) Handles TextBox43.TextChanged

        If Not String.IsNullOrWhiteSpace(TextBox43.Text) AndAlso System.IO.File.Exists(TextBox43.Text) Then
            ' ✅ تحميل الصورة فقط عند وجود مسار صالح
            PictureBox5.Image = Image.FromFile(TextBox43.Text)
            PictureBox5.SizeMode = PictureBoxSizeMode.Zoom
        Else
            ' ✅ تعيين "لا يوجد" في TextBox43 وعرض صورة تحتوي على "لا يوجد"
            TextBox43.Text = "لا يوجد"

            Dim bmp As New Bitmap(350, 250) ' حجم الصورة
            Using g As Graphics = Graphics.FromImage(bmp)
                g.Clear(Color.White) ' خلفية بيضاء
                Dim font As New Font("Arial", 16, FontStyle.Bold)
                Dim brush As New SolidBrush(Color.Black)
                g.DrawString("لا يوجد", font, brush, New PointF(150, 100)) ' كتابة "لا يوجد"
            End Using
            PictureBox5.Image = bmp
            PictureBox5.SizeMode = PictureBoxSizeMode.CenterImage
        End If
    End Sub

    Private Sub Label28_Click(sender As Object, e As EventArgs) Handles Label28.Click

        Dim openFileDialog As New OpenFileDialog()
        openFileDialog.Filter = "ملفات الصور|*.jpg;*.jpeg;*.png;*.bmp;*.gif"
        openFileDialog.Title = "اختر صورة"

        If openFileDialog.ShowDialog() = DialogResult.OK Then
            TextBox43.Text = openFileDialog.FileName ' ✅ حفظ المسار فقط بدون عرض الصورة مباشرة
        End If
    End Sub

    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click
        Me.Hide()
        الملف_العقائدي.Show()
    End Sub

    Private Sub PictureBox6_Click(sender As Object, e As EventArgs) Handles PictureBox6.Click


        ' ✅ التأكد أن الصورة ليست "لا يوجد" قبل الفتح
        If PictureBox6.Image IsNot Nothing AndAlso TextBox21.Text <> "لا يوجد" Then
            عرض_صور_الفضلاء.PictureBoxshow.Image = PictureBox6.Image
            عرض_صور_الفضلاء.PictureBoxshow.BorderStyle = BorderStyle.FixedSingle ' ✅ إضافة إطار للصورة الكبيرة
            عرض_صور_الفضلاء.ShowDialog() ' ✅ جعلها تظهر كنافذة مستقلة وتغلق بعد المشاهدة
            عرض_صور_الفضلاء.PictureBoxshow.Image = Nothing ' ✅ مسح الصورة عند الإغلاق
        End If
    End Sub

    Private Sub Label29_Click(sender As Object, e As EventArgs) Handles Label29.Click

        Dim openFileDialog As New OpenFileDialog()
        openFileDialog.Filter = "ملفات الصور|*.jpg;*.jpeg;*.png;*.bmp;*.gif"
        openFileDialog.Title = "اختر صورة"

        If openFileDialog.ShowDialog() = DialogResult.OK Then
            TextBox21.Text = openFileDialog.FileName ' ✅ حفظ المسار فقط بدون عرض الصورة مباشرة
        End If
    End Sub

    Private Sub TextBox21_TextChanged(sender As Object, e As EventArgs) Handles TextBox21.TextChanged
        If Not String.IsNullOrWhiteSpace(TextBox21.Text) AndAlso System.IO.File.Exists(TextBox21.Text) Then
            ' ✅ تحميل الصورة فقط عند وجود مسار صالح
            PictureBox6.Image = Image.FromFile(TextBox21.Text)
            PictureBox6.SizeMode = PictureBoxSizeMode.Zoom
        Else
            ' ✅ تعيين "لا يوجد" في TextBox43 وعرض صورة تحتوي على "لا يوجد"
            TextBox21.Text = "لا يوجد"

            Dim bmp As New Bitmap(350, 250) ' حجم الصورة
            Using g As Graphics = Graphics.FromImage(bmp)
                g.Clear(Color.White) ' خلفية بيضاء
                Dim font As New Font("Arial", 16, FontStyle.Bold)
                Dim brush As New SolidBrush(Color.Black)
                g.DrawString("لا يوجد", font, brush, New PointF(150, 100)) ' كتابة "لا يوجد"
            End Using
            PictureBox6.Image = bmp
            PictureBox6.SizeMode = PictureBoxSizeMode.CenterImage
        End If
    End Sub

    Private Sub PictureBox7_Click(sender As Object, e As EventArgs) Handles PictureBox7.Click


        ' ✅ التأكد أن الصورة ليست "لا يوجد" قبل الفتح
        If PictureBox7.Image IsNot Nothing AndAlso TextBox39.Text <> "لا يوجد" Then
            عرض_صور_الفضلاء.PictureBoxshow.Image = PictureBox7.Image
            عرض_صور_الفضلاء.PictureBoxshow.BorderStyle = BorderStyle.FixedSingle ' ✅ إضافة إطار للصورة الكبيرة
            عرض_صور_الفضلاء.ShowDialog() ' ✅ جعلها تظهر كنافذة مستقلة وتغلق بعد المشاهدة
            عرض_صور_الفضلاء.PictureBoxshow.Image = Nothing ' ✅ مسح الصورة عند الإغلاق
        End If
    End Sub

    Private Sub TextBox39_TextChanged(sender As Object, e As EventArgs) Handles TextBox39.TextChanged
        If Not String.IsNullOrWhiteSpace(TextBox39.Text) AndAlso System.IO.File.Exists(TextBox39.Text) Then
            ' ✅ تحميل الصورة فقط عند وجود مسار صالح
            PictureBox7.Image = Image.FromFile(TextBox39.Text)
            PictureBox7.SizeMode = PictureBoxSizeMode.Zoom
        Else
            ' ✅ تعيين "لا يوجد" في TextBox43 وعرض صورة تحتوي على "لا يوجد"
            TextBox39.Text = "لا يوجد"

            Dim bmp As New Bitmap(350, 250) ' حجم الصورة
            Using g As Graphics = Graphics.FromImage(bmp)
                g.Clear(Color.White) ' خلفية بيضاء
                Dim font As New Font("Arial", 16, FontStyle.Bold)
                Dim brush As New SolidBrush(Color.Black)
                g.DrawString("لا يوجد", font, brush, New PointF(150, 100)) ' كتابة "لا يوجد"
            End Using
            PictureBox7.Image = bmp
            PictureBox7.SizeMode = PictureBoxSizeMode.CenterImage
        End If
    End Sub

    Private Sub Label80_Click(sender As Object, e As EventArgs) Handles Label80.Click

        Dim openFileDialog As New OpenFileDialog()
        openFileDialog.Filter = "ملفات الصور|*.jpg;*.jpeg;*.png;*.bmp;*.gif"
        openFileDialog.Title = "اختر صورة"

        If openFileDialog.ShowDialog() = DialogResult.OK Then
            TextBox39.Text = openFileDialog.FileName ' ✅ حفظ المسار فقط بدون عرض الصورة مباشرة
        End If
    End Sub

    Private Sub Label81_Click(sender As Object, e As EventArgs) Handles Label81.Click
        Dim openFileDialog As New OpenFileDialog()
        openFileDialog.Filter = "ملفات الصور|*.jpg;*.jpeg;*.png;*.bmp;*.gif"
        openFileDialog.Title = "اختر صورة"

        If openFileDialog.ShowDialog() = DialogResult.OK Then
            TextBox67.Text = openFileDialog.FileName ' ✅ حفظ المسار فقط بدون عرض الصورة مباشرة
        End If
    End Sub

    Private Sub PictureBox8_Click(sender As Object, e As EventArgs) Handles PictureBox8.Click


        ' ✅ التأكد أن الصورة ليست "لا يوجد" قبل الفتح
        If PictureBox8.Image IsNot Nothing AndAlso TextBox67.Text <> "لا يوجد" Then
            عرض_صور_الفضلاء.PictureBoxshow.Image = PictureBox8.Image
            عرض_صور_الفضلاء.PictureBoxshow.BorderStyle = BorderStyle.FixedSingle ' ✅ إضافة إطار للصورة الكبيرة
            عرض_صور_الفضلاء.ShowDialog() ' ✅ جعلها تظهر كنافذة مستقلة وتغلق بعد المشاهدة
            عرض_صور_الفضلاء.PictureBoxshow.Image = Nothing ' ✅ مسح الصورة عند الإغلاق
        End If
    End Sub

    Private Sub TextBox67_TextChanged(sender As Object, e As EventArgs) Handles TextBox67.TextChanged
        If Not String.IsNullOrWhiteSpace(TextBox67.Text) AndAlso System.IO.File.Exists(TextBox67.Text) Then
            ' ✅ تحميل الصورة فقط عند وجود مسار صالح
            PictureBox8.Image = Image.FromFile(TextBox67.Text)
            PictureBox8.SizeMode = PictureBoxSizeMode.Zoom
        Else
            ' ✅ تعيين "لا يوجد" في TextBox43 وعرض صورة تحتوي على "لا يوجد"
            TextBox67.Text = "لا يوجد"

            Dim bmp As New Bitmap(350, 250) ' حجم الصورة
            Using g As Graphics = Graphics.FromImage(bmp)
                g.Clear(Color.White) ' خلفية بيضاء
                Dim font As New Font("Arial", 16, FontStyle.Bold)
                Dim brush As New SolidBrush(Color.Black)
                g.DrawString("لا يوجد", font, brush, New PointF(150, 100)) ' كتابة "لا يوجد"
            End Using
            PictureBox8.Image = bmp
            PictureBox8.SizeMode = PictureBoxSizeMode.CenterImage
        End If
    End Sub


    Private Sub تحديث_جميع_الإحصائيات()
        Try
            sqlcon.Open()

            ' استعلام لحساب عدد السجلات في حقل المحور
            Dim cmd As New SqlCommand("SELECT COUNT(*) FROM AXIS_fodalaa WHERE NAME IS NOT NULL", sqlcon)
            ' الحصول على العدد
            Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())
            ' تحديث الـ TextBox بالعدد
            TextBox45.Text = count.ToString()
            '''''''''''''''''''''''''''''''''''''''
            ' استعلام لحساب عدد السجلات في حقل الكسب
            Dim cmd1 As New SqlCommand("SELECT COUNT(*) FROM voter_fodalaa WHERE voter_name IS NOT NULL", sqlcon)
            ' الحصول على العدد
            Dim count1 As Integer = Convert.ToInt32(cmd1.ExecuteScalar())
            ' تحديث الـ TextBox بالعدد
            TextBox46.Text = count1.ToString()
            '''''''''''''''''''''''''''''''''''''''

            ' استعلام لحساب عدد السجلات في الحقل الأول (مثلاً: الانتساب)
            Dim cmd2 As New SqlCommand("SELECT COUNT(*) FROM AXIS_fodalaa WHERE NAME IS NOT NULL", sqlcon)
            Dim count2 As Integer = Convert.ToInt32(cmd2.ExecuteScalar())

            ' استعلام لحساب عدد السجلات في الحقل الثاني (مثلاً: الحقل الثاني)
            Dim cmd3 As New SqlCommand("SELECT COUNT(*) FROM voter_fodalaa WHERE voter_name IS NOT NULL", sqlcon)
            Dim count3 As Integer = Convert.ToInt32(cmd3.ExecuteScalar())

            ' دمج الإحصائيات معًا (يمكنك تعديل الطريقة التي تريد بها دمج الأعداد)
            Dim total As Integer = count2 + count3

            ' تحديث الـ TextBox بالعدد الإجمالي
            TextBox44.Text = total.ToString()

            '''''''''''''''''''''''''''''''''''''''
            ' استعلام لحساب عدد السجلات في حقل الكسب
            Dim cmd4 As New SqlCommand("SELECT COUNT(*) FROM voter_fodalaa WHERE voter_name IS NOT NULL", sqlcon)
            ' الحصول على العدد
            Dim count4 As Integer = Convert.ToInt32(cmd4.ExecuteScalar())
            ' تحديث الـ TextBox بالعدد
            TextBox9.Text = count4.ToString()

            ' '''''''''''''''''''''''''''''''''''''''
            '' استعلام لحساب عدد السجلات في حقل الكسب
            'Dim cmd5 As New SqlCommand("SELECT COUNT(*) FROM FAEDZAHRAA WHERE voter_name IS NOT NULL", sqlcon)
            '' الحصول على العدد
            'Dim count5 As Integer = Convert.ToInt32(cmd5.ExecuteScalar())
            '' تحديث الـ TextBox بالعدد
            'TextBox25.Text = count5.ToString()
            ' ''''''''''''''''''''''''''''''''''''''

        Catch ex As Exception
            MessageBox.Show("حدث خطأ: " & ex.Message)
        Finally
            sqlcon.Close()
        End Try
    End Sub
    Private Sub تحديث_إحصائيات_الكلمات()
        Try
            sqlcon.Open()  ' فتح الاتصال بقاعدة البيانات

            ' استعلام SQL لحساب عدد تكرار كلمة "الشباب" في الحقل المعين
            Dim cmd As New SqlCommand("SELECT COUNT(*) FROM voter_fodalaa WHERE votercardstatus LIKE @Keyword", sqlcon)
            ' إضافة الكلمة "الشباب" للبحث في الاستعلام
            cmd.Parameters.AddWithValue("@Keyword", "%" & "محدثة" & "%")
            ' تنفيذ الاستعلام وحساب العدد
            Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())
            ' تحديث الـ TextBox بالعدد الذي تم حسابه
            TextBox35.Text = count.ToString()  ' تحديث  أو أي TextBox آخر
            ''''''''''''''''''''''''''''''''''''''

            ' استعلام SQL لحساب عدد تكرار كلمة "الشباب" في الحقل المعين
            Dim cmd1 As New SqlCommand("SELECT COUNT(*) FROM voter_fodalaa WHERE votercardstatus LIKE @Keyword1", sqlcon)
            ' إضافة الكلمة "الشباب" للبحث في الاستعلام
            cmd1.Parameters.AddWithValue("@Keyword1", "%" & "غير محدثة" & "%")
            ' تنفيذ الاستعلام وحساب العدد
            Dim count1 As Integer = Convert.ToInt32(cmd1.ExecuteScalar())
            ' تحديث الـ TextBox بالعدد الذي تم حسابه
            TextBox36.Text = count1.ToString()
            ''''''''''''''''''''''''''''''''''''''
            'مشتركــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــات

            ' استعلام SQL لحساب عدد تكرار كلمة "الشباب" في الحقل المعين
            Dim cmd2 As New SqlCommand("SELECT COUNT(*) FROM AXIS_fodalaa WHERE axlecardstatus LIKE @Keyword2", sqlcon)
            ' إضافة الكلمة "الشباب" للبحث في الاستعلام
            cmd2.Parameters.AddWithValue("@Keyword2", "%" & "محدثة" & "%")
            ' تنفيذ الاستعلام وحساب العدد
            Dim count2 As Integer = Convert.ToInt32(cmd2.ExecuteScalar())

            Dim cmd12 As New SqlCommand("SELECT COUNT(*) FROM voter_fodalaa WHERE votercardstatus LIKE @Keyword12", sqlcon)
            ' إضافة الكلمة "الشباب" للبحث في الاستعلام
            cmd12.Parameters.AddWithValue("@Keyword12", "%" & "محدثة" & "%")
            ' تنفيذ الاستعلام وحساب العدد
            Dim count12 As Integer = Convert.ToInt32(cmd12.ExecuteScalar())

            Dim total As Integer = count2 + count12


            ' تحديث الـ TextBox بالعدد الإجمالي
            TextBox47.Text = total.ToString()
            ''''''''''''''''''''''''''''''''''''''
            'votercardstatus

            ' استعلام SQL لحساب عدد تكرار كلمة "الشباب" في الحقل المعين
            Dim cmd3 As New SqlCommand("SELECT COUNT(*) FROM AXIS_fodalaa WHERE axlecardstatus LIKE @Keyword3", sqlcon)
            ' إضافة الكلمة "الشباب" للبحث في الاستعلام
            cmd3.Parameters.AddWithValue("@Keyword3", "%" & "غير محدثة" & "%")
            ' تنفيذ الاستعلام وحساب العدد
            Dim count3 As Integer = Convert.ToInt32(cmd3.ExecuteScalar())


            ' استعلام SQL لحساب عدد تكرار كلمة "الشباب" في الحقل المعين
            Dim cmd13 As New SqlCommand("SELECT COUNT(*) FROM voter_fodalaa WHERE votercardstatus LIKE @Keyword13", sqlcon)
            ' إضافة الكلمة "الشباب" للبحث في الاستعلام
            cmd13.Parameters.AddWithValue("@Keyword13", "%" & "غير محدثة" & "%")
            ' تنفيذ الاستعلام وحساب العدد
            Dim count13 As Integer = Convert.ToInt32(cmd13.ExecuteScalar())


            Dim total1 As Integer = count3 + count13


            ' تحديث الـ TextBox بالعدد الإجمالي
            TextBox48.Text = total1.ToString()
            ''''''''''''''''''''''''''''''''''''''
            'الشبـــــــــــــــــــــــــــــــــــــــــاب
            ' استعلام SQL لحساب عدد تكرار كلمة "الشباب" في الحقل المعين
            Dim cmd4 As New SqlCommand("SELECT COUNT(*) FROM AXIS_fodalaa WHERE axle_type LIKE @Keyword4", sqlcon)
            ' إضافة الكلمة "الشباب" للبحث في الاستعلام
            cmd4.Parameters.AddWithValue("@Keyword4", "%" & "ملف الشباب" & "%")
            ' تنفيذ الاستعلام وحساب العدد
            Dim count4 As Integer = Convert.ToInt32(cmd4.ExecuteScalar())



            ' استعلام SQL لحساب عدد تكرار كلمة "الشباب" في الحقل المعين
            Dim cmd14 As New SqlCommand("SELECT COUNT(*) FROM voter_fodalaa WHERE Filter_files LIKE @Keyword14", sqlcon)
            ' إضافة الكلمة "الشباب" للبحث في الاستعلام
            cmd14.Parameters.AddWithValue("@Keyword14", "%" & "ملف الشباب" & "%")
            ' تنفيذ الاستعلام وحساب العدد
            Dim count14 As Integer = Convert.ToInt32(cmd14.ExecuteScalar())

            Dim total2 As Integer = count4 + count14

            ' تحديث الـ TextBox بالعدد الإجمالي
            TextBox49.Text = total2.ToString()


            ''''''''''''''''''''''''''''''''''''''
            'الرياضــــــــــــــة

            ' استعلام SQL لحساب عدد تكرار كلمة "الشباب" في الحقل المعين
            Dim cmd5 As New SqlCommand("SELECT COUNT(*) FROM AXIS_fodalaa WHERE axle_type LIKE @Keyword5", sqlcon)
            ' إضافة الكلمة "الشباب" للبحث في الاستعلام
            cmd5.Parameters.AddWithValue("@Keyword5", "%" & "ملف الرياضة" & "%")
            ' تنفيذ الاستعلام وحساب العدد
            Dim count5 As Integer = Convert.ToInt32(cmd5.ExecuteScalar())


            ' استعلام SQL لحساب عدد تكرار كلمة "الشباب" في الحقل المعين
            Dim cmd15 As New SqlCommand("SELECT COUNT(*) FROM voter_fodalaa WHERE Filter_files LIKE @Keyword15", sqlcon)
            ' إضافة الكلمة "الشباب" للبحث في الاستعلام
            cmd15.Parameters.AddWithValue("@Keyword15", "%" & "ملف الرياضة" & "%")
            ' تنفيذ الاستعلام وحساب العدد
            Dim count15 As Integer = Convert.ToInt32(cmd15.ExecuteScalar())


            Dim total4 As Integer = count5 + count15

            ' تحديث الـ TextBox بالعدد الإجمالي
            TextBox50.Text = total4.ToString()

            ''''''''''''''''''''''''''''''''''''''



            'النســـــــــــــــــــوي

            ' استعلام SQL لحساب عدد تكرار كلمة "الشباب" في الحقل المعين
            Dim cmd6 As New SqlCommand("SELECT COUNT(*) FROM AXIS_fodalaa WHERE axle_type LIKE @Keyword6", sqlcon)
            ' إضافة الكلمة "الشباب" للبحث في الاستعلام
            cmd6.Parameters.AddWithValue("@Keyword6", "%" & "الملف النسوي" & "%")
            ' تنفيذ الاستعلام وحساب العدد
            Dim count6 As Integer = Convert.ToInt32(cmd6.ExecuteScalar())

            ' استعلام SQL لحساب عدد تكرار كلمة "الشباب" في الحقل المعين
            Dim cmd16 As New SqlCommand("SELECT COUNT(*) FROM voter_fodalaa WHERE Filter_files LIKE @Keyword16", sqlcon)
            ' إضافة الكلمة "الشباب" للبحث في الاستعلام
            cmd16.Parameters.AddWithValue("@Keyword16", "%" & "الملف النسوي" & "%")
            ' تنفيذ الاستعلام وحساب العدد
            Dim count16 As Integer = Convert.ToInt32(cmd16.ExecuteScalar())


            Dim total3 As Integer = count6 + count16


            ' تحديث الـ TextBox بالعدد الإجمالي
            TextBox51.Text = total3.ToString()



            ''''''''''''''''''''''''''''''''''''''
            'العشائـــــــــــــــــــر
            ' استعلام SQL لحساب عدد تكرار كلمة "الشباب" في الحقل المعين
            Dim cmd7 As New SqlCommand("SELECT COUNT(*) FROM AXIS_fodalaa WHERE axle_type LIKE @Keyword7", sqlcon)
            ' إضافة الكلمة "الشباب" للبحث في الاستعلام
            cmd7.Parameters.AddWithValue("@Keyword7", "%" & "ملف العشائر" & "%")
            ' تنفيذ الاستعلام وحساب العدد
            Dim count7 As Integer = Convert.ToInt32(cmd7.ExecuteScalar())

            Dim cmd17 As New SqlCommand("SELECT COUNT(*) FROM voter_fodalaa WHERE Filter_files LIKE @Keyword17", sqlcon)
            ' إضافة الكلمة "الشباب" للبحث في الاستعلام
            cmd17.Parameters.AddWithValue("@Keyword17", "%" & "ملف العشائر" & "%")
            ' تنفيذ الاستعلام وحساب العدد
            Dim count17 As Integer = Convert.ToInt32(cmd17.ExecuteScalar())

            Dim total5 As Integer = count7 + count17


            ' تحديث الـ TextBox بالعدد الإجمالي
            TextBox52.Text = total5.ToString()
            ''''''''''''''''''''''''''''''''''''''

            'الملف المهنــــــــــــــــــــــــــــــــــي
            ' استعلام SQL لحساب عدد تكرار كلمة "الشباب" في الحقل المعين
            Dim cmd8 As New SqlCommand("SELECT COUNT(*) FROM AXIS_fodalaa WHERE axle_type LIKE @Keyword8", sqlcon)
            ' إضافة الكلمة "الشباب" للبحث في الاستعلام
            cmd8.Parameters.AddWithValue("@Keyword8", "%" & "الملف المهني" & "%")
            ' تنفيذ الاستعلام وحساب العدد
            Dim count8 As Integer = Convert.ToInt32(cmd8.ExecuteScalar())

            Dim cmd18 As New SqlCommand("SELECT COUNT(*) FROM voter_fodalaa WHERE Filter_files LIKE @Keyword18", sqlcon)
            ' إضافة الكلمة "الشباب" للبحث في الاستعلام
            cmd18.Parameters.AddWithValue("@Keyword18", "%" & "الملف المهني" & "%")
            ' تنفيذ الاستعلام وحساب العدد
            Dim count18 As Integer = Convert.ToInt32(cmd18.ExecuteScalar())

            Dim total6 As Integer = count8 + count18

            ' تحديث الـ TextBox بالعدد الإجمالي
            TextBox53.Text = total6.ToString()
            ''''''''''''''''''''''''''''''''''''''
            'ملف المراقبيــــــــــــــــــــــــــــــــن

            ' استعلام SQL لحساب عدد تكرار كلمة "الشباب" في الحقل المعين
            Dim cmd9 As New SqlCommand("SELECT COUNT(*) FROM AXIS_fodalaa WHERE axle_type LIKE @Keyword9", sqlcon)
            ' إضافة الكلمة "الشباب" للبحث في الاستعلام
            cmd9.Parameters.AddWithValue("@Keyword9", "%" & "ملف المراقبين" & "%")
            ' تنفيذ الاستعلام وحساب العدد
            Dim count9 As Integer = Convert.ToInt32(cmd9.ExecuteScalar())

            Dim cmd19 As New SqlCommand("SELECT COUNT(*) FROM voter_fodalaa WHERE Filter_files LIKE @Keyword19", sqlcon)
            ' إضافة الكلمة "الشباب" للبحث في الاستعلام
            cmd19.Parameters.AddWithValue("@Keyword19", "%" & "ملف المراقبين" & "%")
            ' تنفيذ الاستعلام وحساب العدد
            Dim count19 As Integer = Convert.ToInt32(cmd19.ExecuteScalar())

            Dim total7 As Integer = count9 + count19

            ' تحديث الـ TextBox بالعدد الإجمالي
            TextBox59.Text = total7.ToString()

            ''''''''''''''''''''''''''''''''''''''''''''''
            'ملف المرشديـــــــــــــــــــــــــــــــــــــــن
            ' استعلام SQL لحساب عدد تكرار كلمة "الشباب" في الحقل المعين
            Dim cmd10 As New SqlCommand("SELECT COUNT(*) FROM AXIS_fodalaa WHERE axle_type LIKE @Keyword10", sqlcon)
            ' إضافة الكلمة "الشباب" للبحث في الاستعلام
            cmd10.Parameters.AddWithValue("@Keyword10", "%" & "ملف المرشدين" & "%")
            ' تنفيذ الاستعلام وحساب العدد
            Dim count10 As Integer = Convert.ToInt32(cmd10.ExecuteScalar())

            Dim cmd20 As New SqlCommand("SELECT COUNT(*) FROM voter_fodalaa WHERE Filter_files LIKE @Keyword20", sqlcon)
            ' إضافة الكلمة "الشباب" للبحث في الاستعلام
            cmd20.Parameters.AddWithValue("@Keyword20", "%" & "ملف المرشدين" & "%")
            ' تنفيذ الاستعلام وحساب العدد
            Dim count20 As Integer = Convert.ToInt32(cmd20.ExecuteScalar())

            Dim total8 As Integer = count10 + count20

            ' تحديث الـ TextBox بالعدد الإجمالي
            TextBox60.Text = total8.ToString()
            ''''''''''''''''''''''''''''''''''''''''''''''


        Catch ex As Exception
            ' في حالة حدوث خطأ، سيتم عرض رسالة
            MessageBox.Show("حدث خطأ: " & ex.Message)
        Finally
            ' إغلاق الاتصال بقاعدة البيانات
            sqlcon.Close()
        End Try
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Timer1.Enabled = True
        Timer1.Interval = 5000 '(كل 5 ثواني).
        تحديث_جميع_الإحصائيات()
        تحديث_إحصائيات_الكلمات()
    End Sub

    Private Sub TextBox25_TextChanged(sender As Object, e As EventArgs) Handles TextBox25.TextChanged

    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Me.Hide()
        تقرير_ملف_الفضلاء.Show()
    End Sub

   
End Class



