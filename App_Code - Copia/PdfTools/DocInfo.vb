Imports Microsoft.VisualBasic

Public Class DocInfo
    Public Property No() As String
        Get
            Return m_No
        End Get
        Set(value As String)
            m_No = Value
        End Set
    End Property
    Private m_No As String
    Public Property Producer() As String
        Get
            Return m_Producer
        End Get
        Set(value As String)
            m_Producer = Value
        End Set
    End Property
    Private m_Producer As String

    Public Property FirstName() As String
        Get
            Return m_FirstName
        End Get
        Set(value As String)
            m_FirstName = Value
        End Set
    End Property
    Private m_FirstName As String
    Public Property LastName() As String
        Get
            Return m_LastName
        End Get
        Set(value As String)
            m_LastName = Value
        End Set
    End Property
    Private m_LastName As String
    Public Property Address() As String
        Get
            Return m_Address
        End Get
        Set(value As String)
            m_Address = value
        End Set
    End Property
    Private m_Address As String
    Public Property GseNumber() As String
        Get
            Return m_GseNumber
        End Get
        Set(value As String)
            m_GseNumber = Value
        End Set
    End Property
    Private m_GseNumber As String
    Public Property ServiceStartDate() As String
        Get
            Return m_ServiceStartDate
        End Get
        Set(value As String)
            m_ServiceStartDate = Value
        End Set
    End Property
    Private m_ServiceStartDate As String

    Public Property City() As String
        Get
            Return m_City
        End Get
        Set(value As String)
            m_City = Value
        End Set
    End Property
    Private m_City As String
    Public Property Region() As String
        Get
            Return m_Region
        End Get
        Set(value As String)
            m_Region = Value
        End Set
    End Property
    Private m_Region As String
    Public Property Cap() As String
        Get
            Return m_Cap
        End Get
        Set(value As String)
            m_Cap = Value
        End Set
    End Property
    Private m_Cap As String

    Public Property Latitude() As String
        Get
            Return m_Latitude
        End Get
        Set(value As String)
            m_Latitude = Value
        End Set
    End Property
    Private m_Latitude As String
    Public Property Longitude() As String
        Get
            Return m_Longitude
        End Get
        Set(value As String)
            m_Longitude = Value
        End Set
    End Property
    Private m_Longitude As String

    Public Property ModuleProducerOrBrand() As List(Of String)
        Get
            Return m_ModuleProducerOrBrand
        End Get
        Set(value As List(Of String))
            m_ModuleProducerOrBrand = value
        End Set
    End Property
    Private m_ModuleProducerOrBrand As List(Of String)
    Public Property ModuleModel() As List(Of String)
        Get
            Return m_ModuleModel
        End Get
        Set(value As List(Of String))
            m_ModuleModel = value
        End Set
    End Property
    Private m_ModuleModel As List(Of String)
    Public Property ModuleWeight() As List(Of String)
        Get
            Return m_ModuleWeight
        End Get
        Set(value As List(Of String))
            m_ModuleWeight = value
        End Set
    End Property
    Private m_ModuleWeight As List(Of String)
    Public Property Quantity() As String
        Get
            Return m_Quantity
        End Get
        Set(value As String)
            m_Quantity = Value
        End Set
    End Property
    Private m_Quantity As String

    Public Property ContoEnergia() As String
        Get
            Return m_ContoEnergia
        End Get
        Set(value As String)
            m_ContoEnergia = value
        End Set
    End Property
    Private m_ContoEnergia As String

    Public Property ModuleSerialNumbers() As List(Of String)
        Get
            Return m_ModuleSerialNumbers
        End Get
        Set(value As List(Of String))
            m_ModuleSerialNumbers = value
        End Set
    End Property
    Private m_ModuleSerialNumbers As List(Of String)

    
End Class