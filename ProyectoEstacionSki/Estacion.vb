Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Runtime.CompilerServices
Imports System.IO

Public Class Estacion
    Implements INotifyPropertyChanged
    Private _Nombre As String
    Public Property Nombre() As String
        Get
            Return _Nombre
        End Get
        Set(ByVal value As String)
            _Nombre = value
            NotifyPropertyChanged()
        End Set
    End Property
    Private _Kilometros As Integer
    Public Property Kilometros() As Integer
        Get
            Return _Kilometros
        End Get
        Set(ByVal value As Integer)
            _Kilometros = value
            NotifyPropertyChanged()
        End Set
    End Property
    Private _Pais As String
    Public Property Pais() As String
        Get
            Return _Pais
        End Get
        Set(ByVal value As String)
            _Pais = value
            NotifyPropertyChanged()
        End Set
    End Property
    Private _Abierta As Boolean
    Public Property Abierta() As Boolean
        Get
            Return _Abierta
        End Get
        Set(ByVal value As Boolean)
            _Abierta = value
            NotifyPropertyChanged()
        End Set
    End Property
    Private _Logo As String
    Public Property Logo() As String
        Get
            Return _Logo
        End Get
        Set(ByVal value As String)
            _Logo = value
            NotifyPropertyChanged()
        End Set
    End Property

    Public Sub New(nombre As String, kilometros As Integer, pais As String, abierta As Boolean, logo As String)
        _Nombre = nombre
        _Kilometros = kilometros
        _Pais = pais
        _Abierta = abierta
        _Logo = logo
    End Sub

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

    Private Sub NotifyPropertyChanged(<CallerMemberName()> Optional ByVal propertyName As String = Nothing)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
    End Sub

    ''' <summary>
    ''' Lee el archivo especificado en la ruta y "mapea" su contenido a una ObservableCollection
    ''' donde cada linea se corresponde a un objeto Estacion. Si la ruta no existe devuelve Nothing.
    ''' </summary>
    ''' <param name="ruta">Ruta del archivo a leer</param>
    ''' <returns>ObservableCollection de objetos Estacion,
    ''' o Nothing si la ruta no existe.</returns>
    Public Shared Function GetEstaciones(ruta As String) As ObservableCollection(Of Estacion)
        Dim lista As ObservableCollection(Of Estacion) = Nothing

        If File.Exists(ruta) Then
            lista = New ObservableCollection(Of Estacion)

            Dim reader As New StreamReader(ruta, Text.Encoding.Default)

            While Not reader.EndOfStream
                Dim datosEstacion As String()
                Try
                    datosEstacion = reader.ReadLine().Split(","c)
                Catch ex As Exception
                    Throw New Exception("Error de lectura de linea al cargar fichero: " & ex.Message)
                End Try
                Dim km As Integer
                Dim abier_cerrada As Boolean
                If datosEstacion.Length = 5 Then
                    Try
                        km = Integer.Parse(datosEstacion(1))
                    Catch ex As FormatException
                        Throw New FormatException("Error al parsear los kilometros del archivo .txt: " & ex.Message)
                    End Try
                    Try
                        abier_cerrada = Boolean.Parse(datosEstacion(3))
                    Catch ex As FormatException
                        Throw New FormatException("Error al parsear el estado abierta/cerrada(True/False) del archivo .txt: " & ex.Message)
                    End Try
                    lista.Add(New Estacion(datosEstacion(0), km, datosEstacion(2), abier_cerrada, datosEstacion(4)))
                End If
            End While

            reader.Close()
        End If

        Return lista
    End Function

    ''' <summary>
    ''' Sobreescritura del método ToString() usada en la
    ''' escritura del archivo al guardar.
    ''' </summary>
    ''' <returns>Propiedades del objeto Estacion formateadas para su guardado en fichero.</returns>
    Public Overrides Function ToString() As String
        Return Nombre & "," & Kilometros & "," & Pais & "," & Abierta & "," & Logo
    End Function

End Class
