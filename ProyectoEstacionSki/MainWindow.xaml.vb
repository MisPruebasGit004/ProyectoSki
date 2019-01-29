Imports Microsoft.Win32
Imports System.IO
Class MainWindow
    Dim estacionesFileDialog As OpenFileDialog
    Dim estacionesSaveFileDialog As SaveFileDialog

    ''' <summary>
    ''' Inicialización de los diálogos.
    ''' </summary>
    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
        estacionesFileDialog = New OpenFileDialog()
        estacionesFileDialog.Filter = "Archivo de texto | *.txt"
        estacionesSaveFileDialog = New SaveFileDialog()
        estacionesSaveFileDialog.Filter = "Archivo de texto | *.txt"
    End Sub

    ''' <summary>
    ''' Abre un diálogo para seleccionar un archivo desde el que leer datos y
    ''' establece el DataContext del ListBox como una ObservableCollection de
    ''' objetos Estacion.
    ''' </summary>
    Private Sub CargarButton_Click(sender As Object, e As RoutedEventArgs)
        estacionesFileDialog.ShowDialog()
        If estacionesFileDialog.FileName IsNot "" Then
            EstacionesListBox.DataContext = Estacion.GetEstaciones(estacionesFileDialog.FileName)
        End If
    End Sub

    ''' <summary>
    ''' Cada vez que cambia la selección del ListBox, se establece el DataContext de
    ''' los elementos que permiten su modificación.
    ''' </summary>
    Private Sub EstacionesListBox_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
        DatosModificablesEstacionGrid.DataContext = EstacionesListBox.SelectedItem
        EstacionImage.DataContext = EstacionesListBox.SelectedItem
    End Sub

    ''' <summary>
    ''' Muestra un diálogo para guardar un archivo. En él se guardan los datos de las
    ''' propiedades de cada objeto Estacion enlazado a los items del ListBox.
    ''' Las propiedades del objeto se formatean mediante el método ToString() de la clase
    ''' Estacion
    ''' </summary>
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
