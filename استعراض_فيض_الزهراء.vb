Imports System.Data.SqlClient
Imports Excel = Microsoft.Office.Interop.Excel
Imports Word = Microsoft.Office.Interop.Word
Imports Access = Microsoft.Office.Interop.Access
Imports System.IO
Public Class استعراض_فيض_الزهراء
    ' تعريف الاتصال بحيث يكون متاحًا في جميع الدوال داخل الفورم
    Dim sqlcon As New SqlConnection("Server=DESKTOP-08SGMQ2\SQLEXPRESS;Database=white_hand;Integrated Security=True;")
    Dim query As String
    Dim الاسم_المحدد As String ' متغير لتخزين الاسم الممرر

    ' منشئ الفئة مع باراميتر (يستقبل الاستعلام من الفورم الأول)
    Public Sub New(ByVal sqlQuery As String)
        MyBase.New() ' استدعاء المنشئ الأساسي
        InitializeComponent()
        query = sqlQuery
    End Sub
    ' تعريف الاتصال بحيث يكون متاحًا في جميع الدوال داخل الفورم

    ' منشئ الفئة مع باراميتر (يستقبل الاستعلام من الفورم الأول)
    Public Sub New(ByVal sqlQuery As String, ByVal selectedName As String)
        MyBase.New()
        InitializeComponent()
        query = sqlQuery
        الاسم_المحدد = selectedName ' تخزين الاسم الممرر
    End Sub



    Private Sub Button4_Click(sender As Object, e As EventArgs)
        Me.Hide()
        تقرير_فيض_الزهراء.Show()
    End Sub

    Private Sub استعراض_فيض_الزهراء_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ' ضبط حدود DataGridView
        DataGridView1.BorderStyle = BorderStyle.Fixed3D
        DataGridView1.GridColor = Color.Black

        ' ضبط ألوان عناوين الأعمدة
        DataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.Pink
        DataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
        DataGridView1.ColumnHeadersDefaultCellStyle.Font = New Font("Arial", 12, FontStyle.Bold)

        ' تعطيل النمط الافتراضي للعناوين
        DataGridView1.EnableHeadersVisualStyles = False

        ' ضبط ألوان نصوص البيانات
        DataGridView1.DefaultCellStyle.ForeColor = Color.Navy
        DataGridView1.DefaultCellStyle.Font = New Font("Arial", 13)

        ' ضبط لون تحديد الصفوف
        DataGridView1.RowsDefaultCellStyle.SelectionBackColor = Color.LightGray

        ' ضبط عرض الأعمدة ليكون 200 بكسل لكل عمود
        For Each col As DataGridViewColumn In DataGridView1.Columns
            col.Width = 200
        Next
        ''''''''''''''''''''''''''''''''''''''''''''

        Try
            ' التأكد من أن الاستعلام ليس فارغًا
            If String.IsNullOrWhiteSpace(query) Then
                Return ' يمكنك تجاهل الاستعلام الفارغ إذا كان مقبول
            End If

            ' فتح الاتصال إذا لم يكن مفتوحًا
            If sqlcon.State <> ConnectionState.Open Then
                sqlcon.Open()
            End If

            ' إنشاء أمر SQL مع إضافة المتغير @name
            Dim cmd As New SqlCommand(query, sqlcon)

            ' التأكد من أن الاستعلام يحتوي على المتغير @name وتم تمريره
            If query.Contains("@name") Then
                If Not String.IsNullOrEmpty(الاسم_المحدد) Then
                    cmd.Parameters.AddWithValue("@name", الاسم_المحدد)
                Else
                    ' عرض تنبيه بدلاً من الخطأ
                    MessageBox.Show("تنبيه: لم يتم تمرير بعض البيانات , قد تكون غير مكتملة.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Return
                End If
            End If

            ' تنفيذ الاستعلام وجلب البيانات
            Dim adapter As New SqlDataAdapter(cmd)
            Dim dt As New DataTable()
            adapter.Fill(dt)

            ' التحقق مما إذا كانت هناك بيانات
            If dt.Rows.Count = 0 Then
                MessageBox.Show("لا توجد بيانات متاحة لعرضها.", "ملاحظة", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            ' عرض البيانات في DataGridView
            DataGridView1.DataSource = dt

            ' ضبط حجم الأعمدة
            For Each column As DataGridViewColumn In DataGridView1.Columns
                column.Width = 200
            Next

            ' تغيير الخط لعناوين الأعمدة
            If DataGridView1.Columns.Count > 0 Then
                For Each column As DataGridViewColumn In DataGridView1.Columns
                    column.HeaderCell.Style.Font = New Font("Arial", 15, FontStyle.Bold)
                Next
            End If

            ' إضافة عمود التسلسل
            dt.Columns.Add("التسلسل", GetType(Integer))
            For i As Integer = 0 To dt.Rows.Count - 1
                dt.Rows(i)("التسلسل") = i + 1
            Next

            ' إعادة ترتيب العمود الأول
            DataGridView1.DataSource = dt
            DataGridView1.Columns("التسلسل").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            DataGridView1.Columns("التسلسل").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            DataGridView1.Columns("التسلسل").DisplayIndex = 0

        Catch ex As Exception
            MessageBox.Show("خطأ في جلب البيانات: " & ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            sqlcon.Close()
        End Try
    End Sub

    Private Sub PictureBox3_Click_1(sender As Object, e As EventArgs) Handles PictureBox3.Click
        Me.Hide()
        تقرير_فيض_الزهراء.Show()
    End Sub

    Private Sub Button4_Click_1(sender As Object, e As EventArgs) Handles Button4.Click
        Try
            ' تحديد مسار ملف Access الذي سيتم إنشاؤه
            Dim savePath As String = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) & "\ExportedData.accdb"

            ' إذا كان الملف موجودًا، احذفه لإنشاء ملف جديد
            If File.Exists(savePath) Then
                File.Delete(savePath)
            End If

            ' إنشاء ملف Access جديد
            Dim accessApp As New Access.Application
            accessApp.NewCurrentDatabase(savePath, Access.AcNewDatabaseFormat.acNewDatabaseFormatAccess2007)

            ' إضافة جدول جديد
            Dim tableName As String = "ExportedTable"
            accessApp.DoCmd.RunSQL("CREATE TABLE " & tableName & " ([ID] AUTOINCREMENT PRIMARY KEY)")

            ' إضافة الحقول بناءً على أسماء الأعمدة في DataGridView
            For Each col As DataGridViewColumn In DataGridView1.Columns
                accessApp.DoCmd.RunSQL("ALTER TABLE " & tableName & " ADD COLUMN [" & col.HeaderText & "] TEXT(255)")
            Next

            ' إدخال البيانات من DataGridView إلى Access
            For Each row As DataGridViewRow In DataGridView1.Rows
                If Not row.IsNewRow Then
                    Dim values As New List(Of String)
                    For Each cell As DataGridViewCell In row.Cells
                        values.Add("'" & cell.Value.ToString().Replace("'", "''") & "'") ' تفادي أخطاء الاقتباس
                    Next
                    Dim insertQuery As String = "INSERT INTO " & tableName & " (" & String.Join(",", DataGridView1.Columns.Cast(Of DataGridViewColumn).Select(Function(c) "[" & c.HeaderText & "]")) & ") VALUES (" & String.Join(",", values) & ")"
                    accessApp.DoCmd.RunSQL(insertQuery)
                End If
            Next

            ' حفظ وإغلاق Access
            accessApp.CloseCurrentDatabase()
            accessApp.Quit()

            MessageBox.Show("تم تصدير البيانات إلى Access بنجاح!", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information)

            ' فتح ملف Access بعد الحفظ
            Process.Start(savePath)

        Catch ex As Exception
            MessageBox.Show("حدث خطأ أثناء التصدير: " & ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        MessageBox.Show("⚠️ تنبيه هام ⚠️" & Environment.NewLine & Environment.NewLine &
                    "........يُستخدم هذا السياق لعرض البيانات الإحصائية أو الشخصية فقط" & Environment.NewLine &
                    "لا يمكن استخدامه لعرض بيانات مجموعة من الأشخاص." & Environment.NewLine &
                    "يرجى التأكد من اختيار البيانات المناسبة قبل المتابعة.",
                    "🔹 وضع العرض العامودي", MessageBoxButtons.OK, MessageBoxIcon.Information)

        'MessageBox.Show("يستخدم هذا السياق لعرض البيانات الاحصائية او استعراض بيانات شخص واحد فقط و لا يمكن استخدامه لعرض بيانات مجموعة من الاشخاص", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)

        ' إنشاء DataTable جديد
        Dim dt As New DataTable
        dt.Columns.Add("التسلسل") ' العمود الأول للتسلسل
        dt.Columns.Add("اسم الحقل") ' العمود الثاني لأسماء الحقول
        dt.Columns.Add("البيانات")    ' العمود الثالث للقيم الفعلية

        ' التأكد من أن DataGridView يحتوي على بيانات
        If DataGridView1.Rows.Count > 0 Then
            Dim firstRow As DataGridViewRow = DataGridView1.Rows(0) ' نأخذ أول صف فقط

            ' تحويل كل عمود إلى صف جديد
            For i As Integer = 0 To DataGridView1.Columns.Count - 1
                dt.Rows.Add(i + 1, DataGridView1.Columns(i).HeaderText, firstRow.Cells(i).Value)
            Next

            ' حذف الصف الذي يحتوي على "التسلسل" في عمود "اسم الحقل"
            For i As Integer = dt.Rows.Count - 1 To 0 Step -1
                If dt.Rows(i)("اسم الحقل").ToString() = "التسلسل" Then
                    dt.Rows.RemoveAt(i)
                    Exit For ' حذف أول صف مطابق فقط
                End If
            Next

            ' عرض البيانات الجديدة بعد الحذف
            DataGridView1.DataSource = dt
        Else
            MessageBox.Show("لا توجد بيانات لعرضها", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Try
            ' إنشاء كائن Excel جديد
            Dim xlApp As New Excel.Application
            Dim xlWorkbook As Excel.Workbook = xlApp.Workbooks.Add()
            Dim xlWorksheet As Excel.Worksheet = xlWorkbook.Sheets(1)

            ' تصدير عناوين الأعمدة
            For col As Integer = 0 To DataGridView1.Columns.Count - 1
                xlWorksheet.Cells(1, col + 1) = DataGridView1.Columns(col).HeaderText
            Next

            ' تصدير البيانات من DataGridView إلى Excel
            For row As Integer = 0 To DataGridView1.Rows.Count - 1
                For col As Integer = 0 To DataGridView1.Columns.Count - 1
                    xlWorksheet.Cells(row + 2, col + 1) = DataGridView1.Rows(row).Cells(col).Value
                Next
            Next

            ' تحديد المسار لحفظ الملف
            Dim savePath As String = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) & "\FilteredData.xlsx"
            xlWorkbook.SaveAs(savePath)
            xlWorkbook.Close()
            xlApp.Quit()

            ' تحرير موارد Excel
            ReleaseObject(xlWorksheet)
            ReleaseObject(xlWorkbook)
            ReleaseObject(xlApp)

            MessageBox.Show("تم تصدير البيانات إلى Excel بنجاح!", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information)

            ' فتح الملف بعد الحفظ
            Process.Start(savePath)

        Catch ex As Exception
            MessageBox.Show("حدث خطأ أثناء التصدير: " & ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' دالة لتحرير موارد Excel من الذاكرة
    Private Sub ReleaseObject(ByVal obj As Object)
        Try
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj)
            obj = Nothing
        Catch ex As Exception
            obj = Nothing
        Finally
            GC.Collect()
        End Try
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Try
            ' التحقق من تحديد سطر
            If DataGridView1.SelectedRows.Count = 0 Then
                MessageBox.Show("الرجاء تحديد سطر للحذف.", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            ' التأكيد قبل الحذف
            Dim confirm As DialogResult = MessageBox.Show("هل أنت متأكد من حذف هذا السطر؟", "تأكيد الحذف", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If confirm = DialogResult.No Then Exit Sub

            ' حذف السطر من DataGridView
            DataGridView1.Rows.Remove(DataGridView1.SelectedRows(0))

            MessageBox.Show("تم حذف السطر بنجاح!", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            MessageBox.Show("حدث خطأ أثناء الحذف: " & ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            ' إنشاء كائن Word جديد
            Dim wordApp As New Word.Application
            Dim wordDoc As Word.Document = wordApp.Documents.Add()

            ' إنشاء جدول في Word
            Dim table As Word.Table = wordDoc.Tables.Add(wordDoc.Range(), DataGridView1.Rows.Count + 1, DataGridView1.Columns.Count)

            ' تنسيق الجدول
            table.Borders.Enable = 1
            table.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter

            ' إضافة عناوين الأعمدة
            For col As Integer = 0 To DataGridView1.Columns.Count - 1
                table.Cell(1, col + 1).Range.Text = DataGridView1.Columns(col).HeaderText
            Next

            ' تصدير البيانات من DataGridView إلى Word
            For row As Integer = 0 To DataGridView1.Rows.Count - 1
                For col As Integer = 0 To DataGridView1.Columns.Count - 1
                    table.Cell(row + 2, col + 1).Range.Text = DataGridView1.Rows(row).Cells(col).Value
                Next
            Next

            ' تحديد المسار لحفظ الملف
            Dim savePath As String = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) & "\FilteredData.docx"
            wordDoc.SaveAs2(savePath)
            wordDoc.Close()
            wordApp.Quit()

            ' تحرير موارد Word
            ReleaseObject1(table)
            ReleaseObject1(wordDoc)
            ReleaseObject1(wordApp)

            MessageBox.Show("تم تصدير البيانات إلى Word بنجاح!", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information)

            ' فتح الملف بعد الحفظ
            Process.Start(savePath)

        Catch ex As Exception
            MessageBox.Show("حدث خطأ أثناء التصدير: " & ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' دالة لتحرير موارد Word من الذاكرة
    Private Sub ReleaseObject1(ByVal obj As Object)
        Try
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj)
            obj = Nothing
        Catch ex As Exception
            obj = Nothing
        Finally
            GC.Collect()
        End Try

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            ' فتح نافذة اختيار الطابعة
            If PrintDialog1.ShowDialog() = DialogResult.OK Then
                PrintDocument1.Print()
            End If
        Catch ex As Exception
            MessageBox.Show("حدث خطأ أثناء الطباعة: " & ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Dim form1 As New تقرير_فيض_الزهراء()
        form1.Show()
    End Sub

    Private Sub PrintDocument1_PrintPage_1(sender As Object, e As Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        'Dim font As New Font("Arial", 12, FontStyle.Bold)
        'Dim brush As New SolidBrush(Color.Black)
        'Dim startX As Integer = 50
        'Dim startY As Integer = 50
        'Dim offsetY As Integer = 25

        '' طباعة عنوان الجدول
        'e.Graphics.DrawString("تقرير البيانات", New Font("Arial", 14, FontStyle.Bold), brush, startX, startY)
        'startY += 40

        '' طباعة رؤوس الأعمدة
        'For Each column As DataGridViewColumn In DataGridView1.Columns
        '    e.Graphics.DrawString(column.HeaderText, font, brush, startX, startY)
        '    startX += 100
        'Next

        'startX = 50
        'startY += offsetY

        '' طباعة كل الصفوف
        'For Each row As DataGridViewRow In DataGridView1.Rows
        '    If row.IsNewRow Then Continue For ' تجاهل الصف الفارغ الأخير

        '    For Each cell As DataGridViewCell In row.Cells
        '        e.Graphics.DrawString(cell.Value.ToString(), font, brush, startX, startY)
        '        startX += 100
        '    Next

        '    startX = 50
        '    startY += offsetY
        'Next
        ''Dim fontHeader As New Font("Arial", 13, FontStyle.Bold)
        ''Dim fontBody As New Font("Arial", 12, FontStyle.Regular)
        ''Dim brushBlack As New SolidBrush(Color.Black)
        ''Dim brushMaroon As New SolidBrush(Color.Pink)
        ''Dim penBorder As New Pen(Color.Black, 2)

        ''Dim startX As Integer = 50
        ''Dim startY As Integer = 50
        ''Dim cellWidth As Integer = 200 ' تحديد عرض الأعمدة
        ''Dim rowHeight As Integer = 30
        ''Dim tableWidth As Integer = DataGridView1.Columns.Count * cellWidth

        ' '' إطار حول العنوان
        ''Dim titleRect As New Rectangle(startX, startY, tableWidth, rowHeight)
        ''e.Graphics.FillRectangle(New SolidBrush(Color.LightBlue), titleRect)
        ''e.Graphics.DrawRectangle(penBorder, titleRect)

        ' '' طباعة عنوان التقرير في جهة اليمين
        ''Dim titleFormat As New StringFormat()
        ''titleFormat.Alignment = StringAlignment.Far
        ''titleFormat.LineAlignment = StringAlignment.Center
        ''e.Graphics.DrawString("تقرير البيانات", fontHeader, brushBlack, New RectangleF(startX, startY, tableWidth, rowHeight), titleFormat)

        ''startY += rowHeight + 10

        ' '' طباعة رؤوس الأعمدة
        ''Dim colX As Integer = startX
        ''For Each column As DataGridViewColumn In DataGridView1.Columns
        ''    Dim headerRect As New Rectangle(colX, startY, cellWidth, rowHeight)
        ''    e.Graphics.FillRectangle(New SolidBrush(Color.LightGray), headerRect)
        ''    e.Graphics.DrawRectangle(penBorder, headerRect)
        ''    e.Graphics.DrawString(column.HeaderText, fontHeader, brushMaroon, headerRect, titleFormat)
        ''    colX += cellWidth
        ''Next

        ''startY += rowHeight

        ' '' طباعة البيانات داخل الجدول
        ''For Each row As DataGridViewRow In DataGridView1.Rows
        ''    If row.IsNewRow Then Continue For ' تجاهل الصف الفارغ الأخير

        ''    colX = startX
        ''    For Each cell As DataGridViewCell In row.Cells
        ''        Dim cellRect As New Rectangle(colX, startY, cellWidth, rowHeight)
        ''        e.Graphics.DrawRectangle(penBorder, cellRect)

        ''        ' تنسيق النص داخل الخلايا
        ''        Dim cellFormat As New StringFormat()
        ''        cellFormat.Alignment = StringAlignment.Center
        ''        cellFormat.LineAlignment = StringAlignment.Center

        ''        e.Graphics.DrawString(cell.Value.ToString(), fontBody, brushBlack, cellRect, cellFormat)
        ''        colX += cellWidth
        ''    Next

        ''    startY += rowHeight
        ''Next
        Dim fontHeader As New Font("Arial", 13, FontStyle.Bold)
        Dim fontBody As New Font("Arial", 12, FontStyle.Regular)
        Dim brushBlack As New SolidBrush(Color.Black)
        Dim brushWhite As New SolidBrush(Color.White)
        Dim brushPink As New SolidBrush(Color.Pink)
        Dim penBorder As New Pen(Color.Black, 2)

        Dim startX As Integer = 50
        Dim startY As Integer = 50
        Dim cellWidth As Integer = 150 ' تقليل عرض الأعمدة
        Dim rowHeight As Integer = 30
        Dim tableWidth As Integer = DataGridView1.Columns.Count * cellWidth

        ' تعديل حجم الإطار العلوي ليكون على قد الكلام فقط
        Dim titleText As String = "تقرير البيانات"
        Dim titleSize As SizeF = e.Graphics.MeasureString(titleText, fontHeader)
        Dim titleRect As New Rectangle(startX, startY, CInt(titleSize.Width) + 20, rowHeight) ' جعل الإطار مناسبًا للنص
        e.Graphics.FillRectangle(New SolidBrush(Color.LightBlue), titleRect)
        e.Graphics.DrawRectangle(penBorder, titleRect)

        ' طباعة عنوان التقرير
        Dim titleFormat As New StringFormat()
        titleFormat.Alignment = StringAlignment.Center
        titleFormat.LineAlignment = StringAlignment.Center
        e.Graphics.DrawString(titleText, fontHeader, brushBlack, titleRect, titleFormat)

        startY += rowHeight + 10

        ' طباعة رؤوس الأعمدة (عناوين الحقول فقط)
        Dim colX As Integer = startX
        For Each column As DataGridViewColumn In DataGridView1.Columns
            Dim headerRect As New Rectangle(colX, startY, cellWidth, rowHeight)
            e.Graphics.FillRectangle(brushPink, headerRect) ' لون خلفية العناوين وردي
            e.Graphics.DrawRectangle(penBorder, headerRect)
            e.Graphics.DrawString(column.HeaderText, fontHeader, brushWhite, headerRect, titleFormat) ' النص أبيض
            colX += cellWidth
        Next

        startY += rowHeight

        ' طباعة البيانات داخل الجدول بدون تغيير في الألوان
        For Each row As DataGridViewRow In DataGridView1.Rows
            If row.IsNewRow Then Continue For ' تجاهل الصف الفارغ الأخير

            colX = startX
            For Each cell As DataGridViewCell In row.Cells
                Dim cellRect As New Rectangle(colX, startY, cellWidth, rowHeight)
                e.Graphics.DrawRectangle(penBorder, cellRect)

                ' تنسيق النص داخل الخلايا
                Dim cellFormat As New StringFormat()
                cellFormat.Alignment = StringAlignment.Center
                cellFormat.LineAlignment = StringAlignment.Center

                e.Graphics.DrawString(cell.Value.ToString(), fontBody, brushBlack, cellRect, cellFormat) ' البيانات تبقى كما هي
                colX += cellWidth
            Next

            startY += rowHeight
        Next
    End Sub
End Class