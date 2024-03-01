Imports System.Data.SqlClient
Public Class ItemDashboard

    Private Sub ItemDashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim con = New SqlConnection("Data Source=(LocalDB)\v11.0;AttachDbFilename=E:\College Project\College Project\college_project.mdf;Integrated Security=True;Connect Timeout=30")
        con.Open()

        'Total Items
        Dim item_cmd As New SqlCommand
        Dim item_stmt As String
        Dim itemtotalcount As String
        item_stmt = "SELECT count(*) FROM ItemTbl"
        item_cmd = New SqlCommand(item_stmt, con)
        itemtotalcount = item_cmd.ExecuteScalar()
        Button1.Text = "Total Items :" + itemtotalcount
        con.Close()

    End Sub

    'logout
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim obj = New Items
        obj.Show()
        Me.Hide()
    End Sub
    'exit
    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Dim iExit As DialogResult
        iExit = MessageBox.Show("Confirm if you want to exit", "Items", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
        If iExit = Windows.Forms.DialogResult.Yes Then
            Application.Exit()
        End If
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
End Class