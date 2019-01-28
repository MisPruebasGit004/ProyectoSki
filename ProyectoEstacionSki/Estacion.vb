Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Runtime.CompilerServices

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

    Public Shared Function GetEstaciones(ruta As String) As ObservableCollection(Of Estacion)
        Dim lista As New ObservableCollection(Of Estacion)

        Dim reader As New IO.StreamReader(ruta, Text.Encoding.Default)

        While Not reader.EndOfStream
            Dim datosEstacion As String() = reader.ReadLine().Split(","c)
            If datosEstacion.Length = 5 Then
                'falta manejar excepcion
                lista.Add(New Estacion(datosEstacion(0), Integer.Parse(datosEstacion(1)), datosEstacion(2), Boolean.Parse(datosEstacion(3)), datosEstacion(4)))
            End If
        End While

        Return lista
    End Function

End Class
