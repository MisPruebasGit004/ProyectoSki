Imports Microsoft.Win32
Imports System.IO
Class MainWindow
    Dim estacionesFileDialog As OpenFileDialog
    Dim estacionesSaveFileDialog As SaveFileDialog

    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
        estacionesFileDialog = New OpenFileDialog()
        estacionesFileDialog.Filter = "Archivo de texto | *.txt"
        estacionesSaveFileDialog = New SaveFileDialog()
        estacionesSaveFileDialog.Filter = "Archivo de texto | *.txt"
    End Sub

    Private Sub CargarButton_Click(sender As Object, e As RoutedEventArgs)
        estacionesFileDialog.ShowDialog()
        If estacionesFileDialog.FileName IsNot "" Then
            EstacionesListBox.DataContext = Estacion.GetEstaciones(estacionesFileDialog.FileName)
        End If
    End Sub

    Private Sub EstacionesListBox_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
        DatosModificablesEstacionGrid.DataContext = EstacionesListBox.SelectedItem
        EstacionImage.DataContext = EstacionesListBox.SelectedItem
    End Sub

    Private Sub GuardarButton_Click(sender As Object, e As RoutedEventArgs)
        estacionesSaveFileDialog.ShowDialog()

        If estacionesSaveFileDialog.FileName IsNot "" Then
            Dim writer As StreamWriter = Nothing
            Try
                writer = New StreamWriter(estacionesSaveFileDialog.FileName, False, Text.Encoding.Default)
            Catch ex As Exception
                Throw New Exception("Error al crear el flujo de escritura: " & ex.Message)
            End Try
            For Each estacion As Estacion In EstacionesListBox.Items
                Try
                    writer.WriteLine(estacion.ToString())
                Catch ex As Exception
                    Throw New Exception("Error al escribir una linea: " & ex.Message)
                End Try
            Next
            writer.Close()
        End If
    End Sub
End Class
