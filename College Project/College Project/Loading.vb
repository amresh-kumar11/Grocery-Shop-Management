Public Class Loading


    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Myprogress.Increment(5)
        Dim Percentage As String ' for % increment varable
        Percentage = Convert.ToString(Myprogress.Value)
        percentagelbl.Text = Percentage + "%"
        If Myprogress.Value = 100 Then
            Me.Hide()
            Dim log = New Loading
            Login.Show()
            Timer1.Enabled = False
        End If
    End Sub

    Private Sub Loading_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer1.Start()
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click

    End Sub

    Private Sub Myprogress_ValueChanged(sender As Object, e As EventArgs) Handles Myprogress.ValueChanged

    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        Dim iExit As DialogResult
        iExit = MessageBox.Show("Confirm if you want to exit", "Loding", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
        If iExit = Windows.Forms.DialogResult.Yes Then
            Application.Exit()
        End If


    End Sub
End Class