Imports Microsoft.Win32
Class MainWindow
    Dim estacionesFileDialog As OpenFileDialog

    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
        estacionesFileDialog = New OpenFileDialog()
        estacionesFileDialog.Filter = "Archivo de texto | *.txt"
    End Sub

    Private Sub CargarButton_Click(sender As Object, e As RoutedEventArgs) Handles CargarButton.Click
        estacionesFileDialog.ShowDialog()
        If estacionesFileDialog.FileName IsNot "" Then
            EstacionesListBox.DataContext = Estacion.GetEstaciones(estacionesFileDialog.FileName)
        End If
    End Sub
    'en listbox: ItemSource = "{Binding}" para que coja toooda la lista
End Class
