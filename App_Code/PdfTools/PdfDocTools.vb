Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.IO
Imports System.Linq
Imports System.Text
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports Font = iTextSharp.text.Font


Public Class PdfTools

    Public Sub CreateDocument2(inPdf As String, outStream As Stream, text As String)

        Dim reader = New PdfReader(inPdf)
        Dim pageSize = reader.GetPageSize(1)

        Dim stamper = New PdfStamper(reader, outStream)
        Dim overContent = stamper.GetOverContent(1)
        overContent.SetColorFill(New BaseColor(Color.Navy.R, Color.Navy.G, Color.Navy.B))

        Dim baseFont__1 = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.WINANSI, BaseFont.EMBEDDED)

        WriteToPdf(overContent, baseFont__1, 12, 215, 188, text)

        'Do other stuff here

        stamper.Close()

    End Sub

    Public Class PageInfo
        Public Property Page() As Integer
            Get
                Return m_Page
            End Get
            Set(value As Integer)
                m_Page = value
            End Set
        End Property
        Private m_Page As Integer
        Public Property SerialNumber() As String
            Get
                Return m_SerialNumber
            End Get
            Set(value As String)
                m_SerialNumber = value
            End Set
        End Property
        Private m_SerialNumber As String
    End Class

    Public Sub CreateDocument(inPdf As String, outStream As Stream, docInfo As DocInfo, firstPageMaxNumbers As Integer, nextPagesMaxNumber As Integer, Marche As List(Of String), Peso As List(Of String), Modello As List(Of String), SerialNumbers As List(Of String))

        Dim firstPage = docInfo.ModuleSerialNumbers.Take(firstPageMaxNumbers).[Select](Function(i, idx) New PageInfo() With { _
            .Page = 1, _
            .SerialNumber = i _
        }).GroupBy(Function(i) i.Page)

        Dim restOfThePages = docInfo.ModuleSerialNumbers.Skip(firstPageMaxNumbers).[Select](Function(i, idx) New PageInfo() With { _
            .Page = Math.Floor(idx / nextPagesMaxNumber) + 2, _
            .SerialNumber = i _
        }).GroupBy(Function(i) i.Page)

        Dim inputPdf = New PdfReader(inPdf)
        Dim pageCount = inputPdf.NumberOfPages

        Dim inputDoc = New Document(inputPdf.GetPageSizeWithRotation(1))

        Dim preparedPdf As Byte()
        Using preparedPdfStream = New MemoryStream()
            Dim outputWriter = PdfWriter.GetInstance(inputDoc, preparedPdfStream)

            inputDoc.Open()
            Dim cb1 = outputWriter.DirectContent

            For i As Int32 = 1 To pageCount
                inputDoc.SetPageSize(inputPdf.GetPageSizeWithRotation(i))
                inputDoc.NewPage()

                Dim page = outputWriter.GetImportedPage(inputPdf, i)
                Dim rotation = inputPdf.GetPageRotation(i)

                Select Case i
                    Case 1, 3
                        If rotation = 90 OrElse rotation = 270 Then
                            cb1.AddTemplate(page, 0, -1.0F, 1.0F, 0, 0, _
                                inputPdf.GetPageSizeWithRotation(i).Height)
                        Else
                            cb1.AddTemplate(page, 1.0F, 0, 0, 1.0F, 0, _
                                0)
                        End If
                        Exit Select
                    Case 2
                        For j As Int32 = 0 To restOfThePages.Count() - 1
                            If j > 0 Then
                                inputDoc.SetPageSize(inputPdf.GetPageSizeWithRotation(i))
                                inputDoc.NewPage()

                                page = outputWriter.GetImportedPage(inputPdf, i)
                                rotation = inputPdf.GetPageRotation(i)

                            End If

                            If rotation = 90 OrElse rotation = 270 Then
                                cb1.AddTemplate(page, 0, -1.0F, 1.0F, 0, 0, _
                                    inputPdf.GetPageSizeWithRotation(i).Height)
                            Else
                                cb1.AddTemplate(page, 1.0F, 0, 0, 1.0F, 0, _
                                    0)
                            End If
                        Next
                        Exit Select
                End Select
            Next

            inputDoc.Close()

            preparedPdf = preparedPdfStream.ToArray()
        End Using

        Dim reader = New PdfReader(preparedPdf)

        Dim pageSize = reader.GetPageSize(1)        

        Dim stamper = New PdfStamper(reader, outStream)
        Dim overContent = stamper.GetOverContent(1)
        overContent.SetColorFill(New BaseColor(Color.Navy.R, Color.Navy.G, Color.Navy.B))

        Dim baseFont As BaseFont = baseFont.CreateFont(baseFont.TIMES_ROMAN, baseFont.WINANSI, baseFont.EMBEDDED)

        WriteToPdf(overContent, baseFont, 10, 190, 177, docInfo.No)
        WriteToPdf(overContent, baseFont, 10, 55, 190, docInfo.Producer)
        WriteToPdf(overContent, baseFont, 10, 175, 236, docInfo.FirstName)
        WriteToPdf(overContent, baseFont, 10, 285, 234, docInfo.LastName)
        WriteToPdf(overContent, baseFont, 10, 160, 250, docInfo.GseNumber)
        WriteToPdf(overContent, baseFont, 10, 355, 250, docInfo.ServiceStartDate)
        If docInfo.ContoEnergia <> "N" Then
            If docInfo.ContoEnergia = "I" Then
                WriteToPdf(overContent, baseFont, 12, 445, 250, "I conto energia")
            ElseIf docInfo.ContoEnergia = "II" Then
                WriteToPdf(overContent, baseFont, 12, 445, 250, "II conto energia")
            ElseIf docInfo.ContoEnergia = "III" Then
                WriteToPdf(overContent, baseFont, 12, 445, 250, "III conto energia")
            ElseIf docInfo.ContoEnergia = "IV" Then
                WriteToPdf(overContent, baseFont, 12, 445, 250, "IV conto energia")
            Else
                WriteToPdf(overContent, baseFont, 12, 445, 250, "V conto energia")
            End If
        End If

        WriteToPdf(overContent, baseFont, 10, 85, 305, docInfo.City)
        WriteToPdf(overContent, baseFont, 10, 340, 305, docInfo.Region)

        WriteToPdf(overContent, baseFont, 10, 85, 320, docInfo.Cap)
        WriteToPdf(overContent, baseFont, 10, 295, 320, docInfo.Address)

        WriteToPdf(overContent, baseFont, 10, 303, 346, docInfo.Latitude)
        WriteToPdf(overContent, baseFont, 10, 417, 346, docInfo.Longitude)

        WriteToPdf(overContent, baseFont, 10, 303, 375, docInfo.Quantity)
        'WriteToPdf(overContent, baseFont, 12, 303, 403, docInfo.ModuleProducerOrBrand)
        'WriteToPdf(overContent, baseFont, 12, 303, 417, docInfo.ModuleModel)
        'WriteToPdf(overContent, baseFont, 12, 303, 431, docInfo.ModuleWeight)

        Dim conta As Integer

        For Each page As IGrouping(Of Int32, PageInfo) In firstPage.Union(restOfThePages)
            Dim tablePdf As Byte()

            Using tempTablePdfStream = New MemoryStream()
                Dim tempDoc = New Document(pageSize)
                tempDoc.SetPageSize(pageSize)
                tempDoc.SetMargins(51.4F, 55.0F, 0F, 0F)

                PdfWriter.GetInstance(tempDoc, tempTablePdfStream)

                tempDoc.Open()
                Dim table = New PdfPTable(4) With { _
                    .WidthPercentage = 100.0F _
                }
                
                Dim tableFont = New iTextSharp.text.Font(baseFont, 7)

                For Each serialNumber As PageInfo In page

                    Dim cell = New PdfPCell(New Phrase(Marche(conta), tableFont)) With { _
                        .HorizontalAlignment = 0, _
                        .BorderWidth = 0.45F _
                    }

                    Dim cell2 = New PdfPCell(New Phrase(Modello(conta), tableFont)) With { _
                     .HorizontalAlignment = 0, _
                     .BorderWidth = 0.45F _
                 }

                    Dim cell3 = New PdfPCell(New Phrase(SerialNumbers(conta), tableFont)) With { _
                    .HorizontalAlignment = 0, _
                    .BorderWidth = 0.45F _
                }

                    Dim cell4 = New PdfPCell(New Phrase(Peso(conta), tableFont)) With { _
                   .HorizontalAlignment = 0, _
                   .BorderWidth = 0.45F _
               }

                    table.AddCell(cell)
                    table.AddCell(cell2)
                    table.AddCell(cell3)
                    table.AddCell(cell4)
                    conta += 1
                Next

                tempDoc.Add(table)
                tempDoc.Close()

                tablePdf = tempTablePdfStream.ToArray()
            End Using

            Dim tablePdfReader = New PdfReader(tablePdf)

            Dim currentPage = 1

            If page.Any() AndAlso page.First().Page > 1 Then
                currentPage = page.First().Page
                overContent = stamper.GetOverContent(currentPage)
                overContent.SetColorFill(New BaseColor(Color.Navy.R, Color.Navy.G, Color.Navy.B))
            End If

            Dim tablePdfPage = stamper.GetImportedPage(tablePdfReader, 1)
            overContent.AddTemplate(tablePdfPage, 1, 0, 0, 1, 0, _
                If(currentPage > 1, -210, -407.2F))
        Next

        stamper.Close()
    End Sub

    Private Sub WriteToPdf(cb As PdfContentByte, bf As BaseFont, fSize As Integer, x As Single, y As Single, text As String)
        cb.BeginText()
        cb.SetFontAndSize(bf, fSize)
        cb.SetTextMatrix(x, (PageSize.A4.Height - (y)))
        cb.ShowText(text)
        cb.EndText()
    End Sub

End Class