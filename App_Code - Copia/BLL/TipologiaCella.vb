﻿Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports ASPNET.StarterKit.DataAccessLayer
Imports System.Data

Namespace ASPNET.StarterKit.BusinessLogicLayer
    Public Class TipologiaCella

        Private _Id As Integer
        Private _Descrizione As String
        Private _DataUltModifica As DateTime

        Public Sub New()
        End Sub 'New

        Public Sub New(ByVal Id As Integer, ByVal Descrizione As String, ByVal DataUltModifica As DateTime)

            Me._Id = Id
            Me._Descrizione = Descrizione
            Me._DataUltModifica = DataUltModifica

        End Sub

        Public Shared Function Carica(ByVal Id As Integer) As TipologiaCella

            If Id <= DefaultValues.GetCategoryIdMinValue() Then
                Return (Nothing)
            End If

            Return DataAccessHelper.GetDataAccess().CaricaTipologiaCella(Id)

        End Function

        Public Function Save() As Boolean

            If Id <= DefaultValues.GetCategoryIdMinValue() Then
                Dim TempId As Integer = DataAccessHelper.GetDataAccess().CreaNuovaTipologiaCella(Me)
                If TempId > DefaultValues.GetCategoryIdMinValue() Then
                    Me._Id = TempId
                    Return True
                Else
                    Return False
                End If
            Else
                Return DataAccessHelper.GetDataAccess().AggiornaTipologiaCella(Me)
            End If

        End Function
        Public Shared Function Verifica(ByVal IdTipologia As Integer) As Boolean

            ' Verifica se presente su pannelli

            If IdTipologia <= DefaultValues.GetCategoryIdMinValue() Then
                Return (Nothing)
            End If

            Return DataAccessHelper.GetDataAccess().VerificaTipologiaCella(IdTipologia)

        End Function

        Public Function Verifica() As Boolean

            ' Verifica se presente in abbinamenti            

            Return DataAccessHelper.GetDataAccess().VerificaTipologiaCella(Me)

        End Function

        Public Function Elimina() As Boolean

            Return DataAccessHelper.GetDataAccess().EliminaTipologiaCella(Me)

        End Function

        Public ReadOnly Property Id() As Integer

            Get
                Return Me._Id
            End Get

        End Property

        Public Property Descrizione() As String

            Get
                If String.IsNullOrEmpty(Me._Descrizione) Then
                    Return String.Empty
                Else
                    Return Me._Descrizione
                End If
            End Get

            Set(ByVal value As String)
                Me._Descrizione = value
            End Set

        End Property
        Public Property DataUltModifica() As DateTime
            Get
                Return _DataUltModifica
            End Get
            Set(ByVal value As DateTime)
                _DataUltModifica = value
            End Set

        End Property
    End Class
End Namespace