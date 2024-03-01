Imports System.Data.SqlClient
Imports System.Drawing.Printing
Public Class Billing
    Dim conn As SqlConnection = New SqlConnection(" Data Source=(LocalDB)\v11.0;AttachDbFilename=E:\College Project\College Project\college_project.mdf;Integrated Security=True;Connect Timeout=30")
    Dim cmd As SqlCommand

    Private Sub AddBill()
        Try
            con.Open()

            ' Parameterized query to avoid SQL injection
            Dim query As String = "INSERT INTO BillTbl (ClientName, Amount, MobileNo, BDate) VALUES (@ClientName, @Amount,  @MobileNo, @BDate)"

            Using cmd As SqlCommand = New SqlCommand(query, con)
                ' Adding parameters to the query
                cmd.Parameters.AddWithValue("@ClientName", ClNameTb.Text)
                cmd.Parameters.AddWithValue("@Amount", GrdTotal)
                cmd.Parameters.AddWithValue("@MobileNo", MobileNoTb.Text)
                cmd.Parameters.AddWithValue("@BDate", DateTime.Now)

                cmd.ExecuteNonQuery()
            End Using
            MsgBox("Bill saved successfully on " & DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
            ' TotaLbill.Text = "Total"
            con.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
    Private Sub UpdateItem()
        Dim newQty = Stock - Convert.ToInt32(QtyTb.Text)
        Try
            con.open()
            Dim query = "Update ItemTbl set ItQty=" & newQty & " where ItId=" & key & ""
            Dim cmd As SqlCommand
            cmd = New SqlCommand(query, con)
            cmd.ExecuteNonQuery()
            MsgBox("Item Update Successfully")
            con.close()
            DisplayItem()
        Catch ex As Exception

        End Try

    End Sub
    Dim i = 0, GrdTotal = 0

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            If PriceTb.Text = "" Or QtyTb.Text = "" Then
                MsgBox("Enter the Quantity")
            ElseIf ItNameTb.Text = "" Then
                MsgBox("Select the item")
            ElseIf Convert.ToInt32(QtyTb.Text) > Stock Then
                MsgBox("Not Enough Stock")
            Else
               
                Dim rnum As Integer = Guna2DataGridView1.Rows.Add()
                i = i + 1
                Dim total = Convert.ToString(QtyTb.Text) * Convert.ToString(PriceTb.Text)
                Guna2DataGridView1.Rows.Item(rnum).Cells("column1").Value = i
                Guna2DataGridView1.Rows.Item(rnum).Cells("column2").Value = ItNameTb.Text
                Guna2DataGridView1.Rows.Item(rnum).Cells("column3").Value = PriceTb.Text
                Guna2DataGridView1.Rows.Item(rnum).Cells("column4").Value = QtyTb.Text
                Guna2DataGridView1.Rows.Item(rnum).Cells("column5").Value = total
                GrdTotal = GrdTotal + total
                Dim tot As String
                tot = "Rs :" + Convert.ToString(GrdTotal)
                TotaLbill.Text = tot
                UpdateItem()
                DisplayItem()
                Reset()
                AddBill()
            End If
        Catch ex As Exception

            Dim il As Integer
            If Not Integer.TryParse(QtyTb.Text, il) Then
                MessageBox.Show("please enter a vaild Quantity number")
                Return
            End If
        End Try
       
    End Sub
    Dim con = New SqlConnection("Data Source=(LocalDB)\v11.0;AttachDbFilename=E:\College Project\College Project\college_project.mdf;Integrated Security=True;Connect Timeout=30")
    Private Sub DisplayItem()
        con.open()
        Dim query = "select * from ItemTbl"
        Dim cmd = New SqlCommand(query, con)
        Dim adapter As SqlDataAdapter
        adapter = New SqlDataAdapter(cmd)
        Dim builder As New SqlCommandBuilder(adapter)
        Dim ds As DataSet
        ds = New DataSet
        adapter.Fill(ds)
        ItemDGV.DataSource = ds.Tables(0)
        con.close()
    End Sub
    Private Sub Billing_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If conn.State = ConnectionState.Open Then
            conn.Close()
        End If
        conn.Open()
        disp_data()
        DisplayItem()
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs)
        Application.Exit()
    End Sub

    Dim key = 0, Stock = 0
    Private Sub ItemDGV_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles ItemDGV.CellMouseClick
        Dim row As DataGridViewRow = ItemDGV.Rows(e.RowIndex)
        ItNameTb.Text = row.Cells(1).Value.ToString
        PriceTb.Text = row.Cells(3).Value.ToString

        If ItNameTb.Text = "" Then
            key = 0
        Else
            key = Convert.ToInt32(row.Cells(0).Value.ToString)
            Stock = Convert.ToInt32(row.Cells(2).Value.ToString)
        End If
    End Sub
    Private Sub Reset()
        ItNameTb.Text = ""
        PriceTb.Text = ""
        QtyTb.Text = ""
        TextBox1.Text = ""
        disp_data()
        key = 0
        Stock = 0

    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Reset()
    End Sub

    'Below code for normal print page
    'Private Sub PrintDocument1_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
    'Dim logoimg As Image = My.Resources.ResourceManager.GetObject("logo")
    ' e.Graphics.DrawImage(logoimg, CInt((e.PageBounds.Width - 450) / 2), 675, 500, 300)

    ' e.Graphics.DrawString("GROCERY SHOP", New Font("Times New Roman", 50, FontStyle.Bold), Brushes.Black, 140, 35)
    'e.Graphics.DrawString("-----------------------------------------------------------------------------------------------", New Font("Arial", 20), Brushes.Black, 0, 100)

    '     e.Graphics.DrawImage(bitmap, 170, 160)
    '    Dim Printview As RectangleF = e.PageSettings.PrintableArea
    '   If Me.Guna2DataGridView1.Height - Printview.Height > 0 Then
    ' e.HasMorePages = True

    'End If
    'e.Graphics.DrawString("-----------------------------------------------------------------------------------------------", New Font("Arial", 20), Brushes.Black, 0, 550)
    'Display the total amount on the bill
    '  e.Graphics.DrawString("Total Amount Rs : " + GrdTotal.ToString, New Font("Times New Roman", 20), Brushes.Black, 305, 535)

    'Display a thank you message
    ' e.Graphics.DrawString("Thank You ! " + ClNameTb.Text.ToString, New Font("Times New Roman", 25, FontStyle.Bold), Brushes.Black, 220, 575)

    '  e.Graphics.DrawString("Thanks For Buying In Our Shop ", New Font("Times New Roman", 20), Brushes.Black, 220, 575)
    ' e.Graphics.DrawString("Please Visit Again Our Shop ", New Font("Times New Roman", 25, FontStyle.Bold), Brushes.Black, 220, 620)

    '  Dim bm As New Bitmap(Me.Panel4.Width, Me.Panel4.Height)
    '  Panel4.DrawToBitmap(bm, New Rectangle(0, 0, Me.Panel4.Width, Me.Panel4.Height))
    '  e.Graphics.DrawImage(bm, 300, 200)
    '  Dim aps As New PageSetupDialog
    '  aps.Document = PrintDocument1
    'End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim obj = New Login
        obj.Show()
        Me.Hide()

    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        TotaLbill.Text = "Total"
        Guna2DataGridView1.Rows.Clear()

    End Sub

    Private Sub PictureBox4_Click(sender As Object, e As EventArgs) Handles PictureBox4.Click
        DisplayItem()
    End Sub

    Private Sub FilterByCat()
        con.open()
        Dim query = "select * from ItemTbl  where Itcat= '" & SearchCb.SelectedItem.ToString() & "'"
        Dim cmd = New SqlCommand(query, con)
        Dim adapter As SqlDataAdapter
        adapter = New SqlDataAdapter(cmd)
        Dim builder As New SqlCommandBuilder(adapter)
        Dim ds As DataSet
        ds = New DataSet
        adapter.Fill(ds)
        ItemDGV.DataSource = ds.Tables(0)
        con.close()
    End Sub
    Private Sub SearchCb_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles SearchCb.SelectionChangeCommitted
        FilterByCat()
    End Sub

    Private bitmap As Bitmap

    Private Sub PrintToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles PrintToolStripMenuItem1.Click
        changelongpaper()
        PrintPreviewDialog1.Document = PD
        PrintPreviewDialog1.ShowDialog()

    End Sub

    Private Sub PictureBox1_Click_1(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Dim iExit As DialogResult
        iExit = MessageBox.Show("Confirm if you want to exit", "Billing", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
        If iExit = Windows.Forms.DialogResult.Yes Then
            Application.Exit()
        End If

    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        BillingReport.Show()

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        BillingDashboard.Show()

    End Sub



    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Try
            ' Validate if Client Name and Mobile No are not empty
            If String.IsNullOrEmpty(ClNameTb.Text) Then
                MsgBox("Please enter Client Name.")
                Return
            End If

            If String.IsNullOrEmpty(MobileNoTb.Text) Then
                MsgBox("Please enter Mobile Number.")
                Return
            End If

            ' Validate if Mobile No is numeric and has 10 digits
            Dim mobileNo As String = MobileNoTb.Text.Trim()
            If Not IsNumeric(mobileNo) OrElse mobileNo.Length <> 10 Then
                MsgBox("Mobile number must be a 10-digit numeric value.")
                Return
            End If

            ' Insert into Search_Tbl
            cmd = conn.CreateCommand()
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "INSERT INTO Search_Tbl VALUES(@ClientName, @MobileNo)"
            cmd.Parameters.AddWithValue("@ClientName", ClNameTb.Text)
            cmd.Parameters.AddWithValue("@MobileNo", MobileNoTb.Text)
            cmd.ExecuteNonQuery()

            ' Refresh data
            disp_data()

            ' Clear input fields
            ClNameTb.Text = ""
            MobileNoTb.Text = ""

            MsgBox("Record Inserted Successfully")
        Catch ex As Exception
            MsgBox("Exception: " & ex.Message)
        End Try


    End Sub
    Public Sub disp_data()
        cmd = conn.CreateCommand()
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "select * from Search_Tbl"
        cmd.ExecuteNonQuery()
        Dim dt As New DataTable()
        Dim da As New SqlDataAdapter(cmd)
        da.Fill(dt)

        Guna2DataGridView2.DataSource = dt
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click

        cmd = conn.CreateCommand()
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "select * from Search_Tbl where MobileNo='" + TextBox1.Text + "'"
        '  cmd.ExecuteNonQuery()
        Dim dt As New DataTable()
        Dim da As New SqlDataAdapter(cmd)
        da.Fill(dt)
        If dt.Rows.Count > 0 Then
            Guna2DataGridView2.DataSource = dt
            MsgBox("Data Found")
        Else
            ' Data not found
            MsgBox("Data Not Found")
            ' Optionally, you may want to clear the DataGridView
            Guna2DataGridView2.DataSource = Nothing

        End If


    End Sub


    Private Sub Guna2DataGridView2_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles Guna2DataGridView2.CellDoubleClick
        ' Check if the double-clicked event occurred on a row (not header or empty space)
        If e.RowIndex >= 0 Then
            ' Get the data from the selected row
            Dim selectedRow As DataGridViewRow = Guna2DataGridView2.Rows(e.RowIndex)

            ' Assuming the data you want to display is in the first column of the selected row
            Dim cellValue As String = selectedRow.Cells(0).Value.ToString()
            Dim cellValue1 As String = selectedRow.Cells(1).Value.ToString()


            ' Fill the TextBox with the retrieved data
            ClNameTb.Text = cellValue
            MobileNoTb.Text = cellValue1


        End If
    End Sub

    Dim WithEvents PD As New PrintDocument
    Dim PPD As New PrintPreviewDialog
    Dim longpaper As Integer
    Sub changelongpaper()
        Dim rowcount As Integer
        longpaper = 0
        rowcount = Guna2DataGridView1.Rows.Count
        longpaper = rowcount * 15
        longpaper = longpaper + 210
    End Sub

    Private Sub PD_BeginPrint(sender As Object, e As PrintEventArgs) Handles PD.BeginPrint

        Dim pagesetup As New PageSettings
        ' pagesetup.PaperSize = New PaperSize("Custom", 250, 500) 'fix size paper
        pagesetup.PaperSize = New PaperSize("Custom", 250, longpaper)

        PD.DefaultPageSettings = pagesetup
    End Sub

    Private Sub PD_PrintPage(sender As Object, e As PrintPageEventArgs) Handles PD.PrintPage
        Dim f8 As New Font("Calibri", 8, FontStyle.Regular)
        Dim f10 As New Font("Calibri", 10, FontStyle.Regular)
        Dim f10b As New Font("Calibri", 10, FontStyle.Bold)
        Dim f17 As New Font("Calibri", 20, FontStyle.Bold)

        Dim centermargin As Integer = e.PageBounds.Width / 2

        ' Font alignment
        Dim center As New StringFormat
        center.Alignment = StringAlignment.Center
        'font alignment
        Dim right As New StringFormat

        right.Alignment = StringAlignment.Far
        center.Alignment = StringAlignment.Center


        Dim leftmargin As Integer = PD.DefaultPageSettings.Margins.Left
        ' Dim centermargin As Integer = PD.DefaultPageSettings.PaperSize.Width / 2
        Dim rightmargin As Integer = PD.DefaultPageSettings.PaperSize.Width

        Dim line As String
        line = "----------------------------------------------------------------------------------"

        e.Graphics.DrawString("Grocery Shop ", f17, Brushes.Black, centermargin, 10, center)
        e.Graphics.DrawString("Raipur,Chattisgarh,India", f10, Brushes.Black, centermargin, 40, center)
        e.Graphics.DrawString("Tel +917856324856", f10, Brushes.Black, centermargin, 55, center)




        'To Take Random Invoice Id
        Dim random As New Random()
        Dim invoiceId As Integer = random.Next(1000000, 10000000) ' Generate a random number between 10000 and 99999
        Dim invoiceIdString As String = "Invoice ID: " & invoiceId.ToString()
        e.Graphics.DrawString(invoiceIdString, f8, Brushes.Black, 0, 75)

        'show client name when i add bill without bill added not showing name
        e.Graphics.DrawString("Client Name: " + ClNameTb.Text.ToString, f8, Brushes.Black, 0, 85)

        Dim logoimg As Image = My.Resources.ResourceManager.GetObject("logo")
        'e.Graphics.DrawImage(logoimg, CInt((e.PageBounds.Width - 450) / 2), 675, 500, 300)

        ' Draw the logo at the bottom of the page
        Dim logoWidth As Integer = 40 ' Adjust the width of the logo as needed
        Dim logoHeight As Integer = 30 ' Adjust the height of the logo as needed
        e.Graphics.DrawImage(logoimg, CInt((e.PageBounds.Width - logoWidth) / 1), 75, logoWidth, logoHeight)
        ' Dim logoHeight As Integer = 50
        'Dim logoWidth As Integer = 50 ' Adjust the width of the logo as needed
        ' e.Graphics.DrawImage(logoimg, leftmargin, e.PageBounds.Height - logoHeight, logoWidth, logoHeight)
        'To show time and date
        Dim currentDateAndTime As String = DateTime.Now.ToString("MM/dd/yyyy | hh:mm tt")
        e.Graphics.DrawString(currentDateAndTime, f8, Brushes.Black, 0, 95)

        ' e.Graphics.DrawString("08/17/2021 | 15.34", f8, Brushes.Black, 0, 95)
        e.Graphics.DrawString(line, f8, Brushes.Black, 0, 100)          'draw a break line

        Dim height As Integer 'DGV Position
        Dim i As Long
        Guna2DataGridView1.AllowUserToAddRows = False
        For row As Integer = 0 To Guna2DataGridView1.RowCount - 1
            height += 15
            'for SHOW HEADING coloum id
            ' e.Graphics.DrawString("ID", f8, Brushes.Black, 0, 100 + height)
            ' e.Graphics.DrawString("Name", f8, Brushes.Black, 25, 100 + height)
            '  e.Graphics.DrawString("Quantity", f8, Brushes.Black, 100, 100 + height)
            ' e.Graphics.DrawString("Price", f8, Brushes.Black, 200, 100 + height)
            
            e.Graphics.DrawString(Guna2DataGridView1.Rows(row).Cells(0).Value.ToString, f8, Brushes.Black, 0, 115 + height)
            'for Item name
            e.Graphics.DrawString(Guna2DataGridView1.Rows(row).Cells(1).Value.ToString, f8, Brushes.Black, 25, 115 + height)
            'for price
            e.Graphics.DrawString(Guna2DataGridView1.Rows(row).Cells(2).Value.ToString, f8, Brushes.Black, 200, 115 + height)

            'for Quantity
            i = Guna2DataGridView1.Rows(row).Cells(3).Value
            Guna2DataGridView1.Rows(row).Cells(3).Value = Format(i, "##,##0")
            e.Graphics.DrawString(Guna2DataGridView1.Rows(row).Cells(3).Value.ToString, f8, Brushes.Black, 100, 117 + height)
        Next
        'for break line
        Dim height2 As Integer
        sumprice()  'calling sum price
        height2 = 145 + height
        e.Graphics.DrawString(line, f8, Brushes.Black, 0, height2)
        e.Graphics.DrawString("Total:" & Format(t_price, "##,##0"), f10b, Brushes.Black, rightmargin, 20 + height2, right)
        ' e.Graphics.DrawString("t_qty", f10b, Brushes.Black, 0, 10 + height2)

        'Thanks message
        e.Graphics.DrawString("~ Thanks for shopping! ~", f10, Brushes.Black, centermargin, 40 + height2, center)
        e.Graphics.DrawString("~ Please Visit Again ~", f10, Brushes.Black, centermargin, 50 + height2, center)


    End Sub
    Dim t_price As Long
    Dim t_qty As Long
    Sub sumprice()
        Dim countprice As Long = 0
        For rowitem As Long = 0 To Guna2DataGridView1.RowCount - 1

            countprice = countprice + Val(Guna2DataGridView1.Rows(rowitem).Cells(2).Value * Val(Guna2DataGridView1.Rows(rowitem).Cells(3).Value))

        Next
        t_price = countprice
    End Sub

End Class