Imports System.Data.SqlClient

Public Class BillingDashboard


    Private Sub BillingDashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'sql connectionstring
        Dim con = New SqlConnection("Data Source=(LocalDB)\v11.0;AttachDbFilename=E:\College Project\College Project\college_project.mdf;Integrated Security=True;Connect Timeout=30")
        con.Open()

        'Total customers
        ' Dim Bill_cmd As New SqlCommand
        ' Dim Bill_stmt As String
        '    Dim Billtotalcount As String
        ''   Bill_stmt = "SELECT count(*) FROM BillTbl"
        ' Bill_cmd = New SqlCommand(Bill_stmt, con)
        ' Billtotalcount = Bill_cmd.ExecuteScalar()
        '  TotCust.Text = "Total Customers :" + Billtotalcount
        'for total bill
        Dim Amt_cmd As New SqlCommand
        Dim Amt_stmt As String
        Dim Amttotalcount As String
        Amt_stmt = "select Sum(Amount) from BillTbl"
        Amt_cmd = New SqlCommand(Amt_stmt, con)
        Amttotalcount = Amt_cmd.ExecuteScalar()
        TotBill.Text = "Total Bill: " + Amttotalcount
        con.Close()
    End Sub


    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        'for exit
        Dim iExit As DialogResult
        iExit = MessageBox.Show("Confirm if you want to exit", "Login", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
        If iExit = Windows.Forms.DialogResult.Yes Then
            Application.Exit()
        End If
    End Sub
    'logout
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim obj = New Billing
        obj.Show()
        Me.Hide()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim con = New SqlConnection("Data Source=(LocalDB)\v11.0;AttachDbFilename=E:\College Project\College Project\college_project.mdf;Integrated Security=True;Connect Timeout=30")
        con.Open()
        '   Dim cmd As New SqlCommand
        Dim cmd As New SqlCommand("SELECT * FROM BillTbl where Bdate between @date1 and @date2", con)
        cmd.Parameters.Add("date1", SqlDbType.DateTime).Value = DateTimePicker1.Value
        cmd.Parameters.Add("date2", SqlDbType.DateTime).Value = DateTimePicker2.Value
        Dim da As New SqlDataAdapter
        da.SelectCommand = cmd
        Dim dt As New DataTable
        dt.Clear()
        da.Fill(dt)
        ItemDGV.DataSource = dt
        con.Close()
    End Sub

    Private Sub TotCust_Click(sender As Object, e As EventArgs)

    End Sub
End Class