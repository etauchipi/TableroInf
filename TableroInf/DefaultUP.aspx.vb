
Imports System.Data
'Imports System.Data.Odbc
'Imports System.Reflection
Imports wsServinteProxy.IwsServinteClient
'Imports System.Reflection.Assembly
'Imports SQLIfxOdbcConnect.SQLOdbcOdbcConnect

Public Class _DefaultUP
    Inherits Page

    Private inicio As Integer
    Private sCnn As String
    Private compresion As Compresion.Compresion
    Private appProxy As wsServinteProxy.IwsServinteClient
    'Private Connection As OdbcConnection
    'Private Command As OdbcCommand
    'Private SqlDat As SQLIfxOdbcConnect.SQLOdbcOdbcConnect

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            TimerUP.Interval = 10000
        End If
        inicio = 0
        carga()
    End Sub

    Private Sub carga()
        lblFecha.Text = "Actualización: " & Now().ToLongDateString & " " & Now().ToLongTimeString
        'updp_main.Update()
    End Sub

    Protected Sub Timer1_Tick(sender As Object, e As EventArgs) Handles TimerUP.Tick
        carga()
        Renderiza1()
        updp_main.Update()
    End Sub

    Private Sub Renderiza1()

        Dim cConn As String
        Dim dsUP As System.Data.DataSet
        Dim ds_comp As System.Data.DataSet
        Dim rRow As TableRow
        Dim cCol As TableCell
        Dim sCadena As String
        Dim cConn1 As String
        Dim sId As String
        Dim sCad As String
        Dim sincolor As Boolean
        Dim bVacio As Boolean
        Dim bCambio As Boolean
        Dim nPages As Integer
        Dim nTotal As Integer

        compresion = New Compresion.Compresion
        appProxy = New wsServinteProxy.IwsServinteClient
        sCadena = String.Empty
        sCad = String.Empty
        cConn1 = String.Empty
        sId = String.Empty
        sincolor = False
        bVacio = False
        nPages = 1
        nTotal = 1

        dsUP = New System.Data.DataSet
        ds_comp = New System.Data.DataSet
        'cConn = "SELECT * FROM ti_tae11_board WHERE Trim(Idtable) = 'UP' ORDER BY name"
        cConn = "SELECT DISTINCT abpac.pacide, abpac.pacap1, abpac.pachis, tx_tae10_cronos.tae01_code, ti_tae11_board.*"
        cConn += " FROM ((hiepiact INNER JOIN abpac ON hiepiact.epiacthis = abpac.pachis)"
        cConn += " INNER JOIN tx_tae10_cronos ON hiepiact.epiacthis = tx_tae10_cronos.tae10_history_id)"
        cConn += " LEFT JOIN ti_tae11_board ON abpac.pacide = ti_tae11_board.patientid"
        cConn += " WHERE (((tx_tae10_cronos.tae01_code)='UP'))"
        cConn += " And (abpac.pacide Not Like ('%PRUEBA%'))"
        cConn += " AND (abpac.pacap1 NOT LIKE ('%PRUEBA%'))"
        cConn += " AND (abpac.pacap2 NOT LIKE ('%PRUEBA%'))"
        cConn += " ORDER BY name"
        cConn1 = "select distinct tae09_document_id from tx_tae10_cronos inner join ormovult"
        cConn1 += " on movulthis = tae10_history_id and movultnum = tae10_identry_id"

        Try
            sCadena = appProxy.get_data_Gral(cConn)
            sCad = appProxy.get_data_Gral(cConn1)
            dsUP = compresion.DescomprimirDataset(sCadena)
            ds_comp = compresion.DescomprimirDataset(sCad)
            appProxy.Close()

            If dsUP.Tables(0).Rows.Count > 0 Then

                If Session("nPages") = 0 Then
                    Session("nPages") = CInt((dsUP.Tables(0).Rows.Count / 20) + 0.5)
                    Session("nContador") = 1
                    inicio = 0
                Else
                    If Session("nContador") <= Session("nPages") Then
                        inicio = (Session("nContador") * 20)
                        Session("nContador") += 1
                    Else
                        Session("nContador") = 1
                        inicio = 0
                    End If

                End If

            Else
                Session("nFinal") = 0
                Session("nPages") = 0
                inicio = 0
            End If

            'If dsUP.Tables(0).Rows.Count > 0 Then
            bVacio = False
            'Session("nFinal") = (inicio + 20)
            Session("nFinal") = (inicio + 19)

            Select Case inicio
                Case Is < 20
                    nPages = 1
                Case Is < 40
                    nPages = 2
                Case Is < 60
                    nPages = 3
                Case Is < 80
                    nPages = 4
                Case Is < 100
                    nPages = 5
                Case Is < 120
                    nPages = 6
                Case Is < 140
                    nPages = 7
                Case Is < 160
                    nPages = 8
                Case Is < 180
                    nPages = 9
                Case Is < 200
                    nPages = 10
            End Select


            If (dsUP.Tables(0).Rows.Count = 0) Then
                Session("nFinal") = 0
                Session("nPages") = 0
                inicio = 0
            End If

            rRow = New TableRow

            cCol = New TableCell
            cCol.Text = "PACIENTE"
            cCol.BorderStyle = BorderStyle.Solid
            cCol.BorderColor = Drawing.Color.DarkGreen
            cCol.BackColor = Drawing.Color.Orange
            cCol.HorizontalAlign = HorizontalAlign.Center
            cCol.VerticalAlign = VerticalAlign.Middle
            cCol.ColumnSpan = 3
            rRow.Cells.Add(cCol)

            cCol = New TableCell
            cCol.Text = "INTERCONSULTAS"
            cCol.BorderStyle = BorderStyle.Solid
            cCol.BorderColor = Drawing.Color.DarkGreen
            cCol.BackColor = Drawing.Color.Orange
            cCol.HorizontalAlign = HorizontalAlign.Center
            cCol.VerticalAlign = VerticalAlign.Middle
            cCol.ColumnSpan = 3
            rRow.Cells.Add(cCol)

            cCol = New TableCell
            cCol.Text = "LABORATORIO CLÍNICO"
            cCol.BorderStyle = BorderStyle.Solid
            cCol.BorderColor = Drawing.Color.DarkGreen
            cCol.BackColor = Drawing.Color.Orange
            cCol.HorizontalAlign = HorizontalAlign.Center
            cCol.VerticalAlign = VerticalAlign.Middle
            cCol.ColumnSpan = 4
            rRow.Cells.Add(cCol)

            cCol = New TableCell
            cCol.Text = "IMAGENOLOGÍA"
            cCol.BorderStyle = BorderStyle.Solid
            cCol.BorderColor = Drawing.Color.DarkGreen
            cCol.BackColor = Drawing.Color.Orange
            cCol.HorizontalAlign = HorizontalAlign.Center
            cCol.VerticalAlign = VerticalAlign.Middle
            cCol.ColumnSpan = 3
            rRow.Cells.Add(cCol)

            cCol = New TableCell
            cCol.Text = "DESTINO"
            cCol.BorderStyle = BorderStyle.Solid
            cCol.BorderColor = Drawing.Color.DarkGreen
            cCol.BackColor = Drawing.Color.Orange
            cCol.HorizontalAlign = HorizontalAlign.Center
            cCol.VerticalAlign = VerticalAlign.Middle
            cCol.RowSpan = 2
            rRow.Cells.Add(cCol)

            tbl_data.Rows.Add(rRow) ' Header 1

            rRow = New TableRow

            cCol = New TableCell
            cCol.Text = "IDENTIFICACIÓN"
            cCol.BorderStyle = BorderStyle.Solid
            cCol.BorderColor = Drawing.Color.DarkGreen
            cCol.BackColor = Drawing.Color.Orange
            cCol.HorizontalAlign = HorizontalAlign.Center
            cCol.VerticalAlign = VerticalAlign.Middle
            rRow.Cells.Add(cCol)

            cCol = New TableCell
            cCol.Text = "APELLIDO"
            cCol.BorderStyle = BorderStyle.Solid
            cCol.BorderColor = Drawing.Color.DarkGreen
            cCol.BackColor = Drawing.Color.Orange
            cCol.HorizontalAlign = HorizontalAlign.Center
            cCol.VerticalAlign = VerticalAlign.Middle
            rRow.Cells.Add(cCol)

            cCol = New TableCell
            cCol.Text = "UBICACIÓN"
            cCol.BorderStyle = BorderStyle.Solid
            cCol.BorderColor = Drawing.Color.DarkGreen
            cCol.BackColor = Drawing.Color.Orange
            cCol.HorizontalAlign = HorizontalAlign.Center
            cCol.VerticalAlign = VerticalAlign.Middle
            rRow.Cells.Add(cCol)

            cCol = New TableCell
            cCol.Text = "ORD"
            cCol.BorderStyle = BorderStyle.Solid
            cCol.BorderColor = Drawing.Color.DarkGreen
            cCol.BackColor = Drawing.Color.LightBlue
            cCol.HorizontalAlign = HorizontalAlign.Center
            cCol.VerticalAlign = VerticalAlign.Middle
            rRow.Cells.Add(cCol)

            cCol = New TableCell
            cCol.Text = "NOT"
            cCol.BorderStyle = BorderStyle.Solid
            cCol.BorderColor = Drawing.Color.DarkGreen
            cCol.BackColor = Drawing.Color.LightBlue
            cCol.HorizontalAlign = HorizontalAlign.Center
            cCol.VerticalAlign = VerticalAlign.Middle
            rRow.Cells.Add(cCol)

            cCol = New TableCell
            cCol.Text = "CAN"
            cCol.BorderStyle = BorderStyle.Solid
            cCol.BorderColor = Drawing.Color.DarkGreen
            cCol.BackColor = Drawing.Color.LightBlue
            cCol.HorizontalAlign = HorizontalAlign.Center
            cCol.VerticalAlign = VerticalAlign.Middle
            rRow.Cells.Add(cCol)

            cCol = New TableCell
            cCol.Text = "ORD"
            cCol.BorderStyle = BorderStyle.Solid
            cCol.BorderColor = Drawing.Color.DarkGreen
            cCol.BackColor = Drawing.Color.LightCoral
            cCol.HorizontalAlign = HorizontalAlign.Center
            cCol.VerticalAlign = VerticalAlign.Middle
            rRow.Cells.Add(cCol)

            cCol = New TableCell
            cCol.Text = "TOM"
            cCol.BorderStyle = BorderStyle.Solid
            cCol.BorderColor = Drawing.Color.DarkGreen
            cCol.BackColor = Drawing.Color.LightCoral
            cCol.HorizontalAlign = HorizontalAlign.Center
            cCol.VerticalAlign = VerticalAlign.Middle
            rRow.Cells.Add(cCol)

            cCol = New TableCell
            cCol.Text = "PRO"
            cCol.BorderStyle = BorderStyle.Solid
            cCol.BorderColor = Drawing.Color.DarkGreen
            cCol.BackColor = Drawing.Color.LightCoral
            cCol.HorizontalAlign = HorizontalAlign.Center
            cCol.VerticalAlign = VerticalAlign.Middle
            rRow.Cells.Add(cCol)

            cCol = New TableCell
            cCol.Text = "CAN"
            cCol.BorderStyle = BorderStyle.Solid
            cCol.BorderColor = Drawing.Color.DarkGreen
            cCol.BackColor = Drawing.Color.LightCoral
            cCol.HorizontalAlign = HorizontalAlign.Center
            cCol.VerticalAlign = VerticalAlign.Middle
            rRow.Cells.Add(cCol)

            cCol = New TableCell
            cCol.Text = "ORD"
            cCol.BorderStyle = BorderStyle.Solid
            cCol.BorderColor = Drawing.Color.DarkGreen
            cCol.BackColor = Drawing.Color.LightSeaGreen
            cCol.HorizontalAlign = HorizontalAlign.Center
            cCol.VerticalAlign = VerticalAlign.Middle
            rRow.Cells.Add(cCol)

            cCol = New TableCell
            cCol.Text = "REA"
            cCol.BorderStyle = BorderStyle.Solid
            cCol.BorderColor = Drawing.Color.DarkGreen
            cCol.BackColor = Drawing.Color.LightSeaGreen
            cCol.HorizontalAlign = HorizontalAlign.Center
            cCol.VerticalAlign = VerticalAlign.Middle
            rRow.Cells.Add(cCol)

            cCol = New TableCell
            cCol.Text = "CAN"
            cCol.BorderStyle = BorderStyle.Solid
            cCol.BorderColor = Drawing.Color.DarkGreen
            cCol.BackColor = Drawing.Color.LightSeaGreen
            cCol.HorizontalAlign = HorizontalAlign.Center
            cCol.VerticalAlign = VerticalAlign.Middle
            rRow.Cells.Add(cCol)

            tbl_data.Rows.Add(rRow) ' Header

            For k As Integer = inicio To Session("nFinal")

                If ((k > dsUP.Tables(0).Rows.Count - 1) Or (dsUP.Tables(0).Rows.Count = 0)) Then
                    Session("nContador") = 1000
                    Session("nPages") = 0
                    inicio = 0
                    Exit For
                End If

                bCambio = False
                sId = Trim((dsUP.Tables(0).Rows(k).Item("patientid").ToString))

                If ds_comp.Tables(0).Rows.Count > 0 Then
                    For n As Integer = 0 To ((ds_comp.Tables(0).Rows.Count) - 1)
                        If (Trim(ds_comp.Tables(0).Rows(n).Item(0).ToString) = sId) Then
                            bCambio = True
                            Exit For
                        End If
                    Next
                End If

                rRow = New TableRow

                'Identificación

                cCol = New TableCell
                cCol.Text = Trim((dsUP.Tables(0).Rows(k).Item("pacide").ToString))
                cCol.BorderStyle = BorderStyle.Solid
                cCol.BorderColor = Drawing.Color.DarkGreen
                cCol.HorizontalAlign = HorizontalAlign.Center
                cCol.VerticalAlign = VerticalAlign.Middle
                If bCambio = True Then
                    cCol.BackColor = Drawing.Color.HotPink
                End If
                rRow.Cells.Add(cCol)

                cCol = New TableCell
                cCol.Text = Trim((dsUP.Tables(0).Rows(k).Item("pacap1").ToString))
                cCol.BorderStyle = BorderStyle.Solid
                cCol.BorderColor = Drawing.Color.DarkGreen
                cCol.HorizontalAlign = HorizontalAlign.Left
                cCol.VerticalAlign = VerticalAlign.Middle
                If bCambio = True Then
                    cCol.BackColor = Drawing.Color.HotPink
                End If
                rRow.Cells.Add(cCol)

                cCol = New TableCell
                cCol.Text = "URGENCIAS PEDIATRICAS"  'Trim((dsUP.Tables(0).Rows(k).Item("location").ToString))
                cCol.BorderStyle = BorderStyle.Solid
                cCol.BorderColor = Drawing.Color.DarkGreen
                cCol.HorizontalAlign = HorizontalAlign.Left
                cCol.VerticalAlign = VerticalAlign.Middle
                rRow.Cells.Add(cCol)


                cCol = New TableCell
                cCol.Text = Trim((dsUP.Tables(0).Rows(k).Item("i_or").ToString))
                cCol.BorderStyle = BorderStyle.Solid
                cCol.BorderColor = Drawing.Color.DarkGreen
                cCol.HorizontalAlign = HorizontalAlign.Center
                cCol.VerticalAlign = VerticalAlign.Middle
                Select Case (dsUP.Tables(0).Rows(k).Item("c_ior").ToString)
                    Case "R"
                        cCol.BackColor = Drawing.Color.Red
                    Case "V"
                        cCol.BackColor = Drawing.Color.LawnGreen
                    Case "A"
                        cCol.BackColor = Drawing.Color.Yellow
                    Case "P"
                        cCol.BackColor = Drawing.Color.Peru
                    Case Else
                        If Trim((dsUP.Tables(0).Rows(k).Item("i_or").ToString)) <> "" Then
                            cCol.BackColor = Drawing.Color.LightBlue
                            sincolor = True
                        Else
                            cCol.BackColor = Drawing.Color.LightBlue
                        End If
                End Select
                rRow.Cells.Add(cCol)

                cCol = New TableCell
                cCol.Text = Trim((dsUP.Tables(0).Rows(k).Item("i_no").ToString))
                cCol.BorderStyle = BorderStyle.Solid
                cCol.BorderColor = Drawing.Color.DarkGreen
                cCol.HorizontalAlign = HorizontalAlign.Center
                cCol.VerticalAlign = VerticalAlign.Middle
                Select Case (dsUP.Tables(0).Rows(k).Item("c_ino").ToString)
                    Case "R"
                        cCol.BackColor = Drawing.Color.Red
                    Case "V"
                        cCol.BackColor = Drawing.Color.LawnGreen
                    Case "A"
                        cCol.BackColor = Drawing.Color.Yellow
                    Case "P"
                        cCol.BackColor = Drawing.Color.Peru
                    Case Else
                        If Trim((dsUP.Tables(0).Rows(k).Item("i_no").ToString)) <> "" Then
                            cCol.BackColor = Drawing.Color.LightBlue
                            sincolor = True
                        Else
                            cCol.BackColor = Drawing.Color.LightBlue
                        End If
                End Select
                rRow.Cells.Add(cCol)

                cCol = New TableCell
                cCol.Text = Trim((dsUP.Tables(0).Rows(k).Item("i_re").ToString))
                cCol.BorderStyle = BorderStyle.Solid
                cCol.BorderColor = Drawing.Color.DarkGreen
                cCol.HorizontalAlign = HorizontalAlign.Center
                cCol.VerticalAlign = VerticalAlign.Middle
                Select Case (dsUP.Tables(0).Rows(k).Item("c_ire").ToString)
                    Case "R"
                        cCol.BackColor = Drawing.Color.Red
                    Case "V"
                        cCol.BackColor = Drawing.Color.LawnGreen
                    Case "A"
                        cCol.BackColor = Drawing.Color.Yellow
                    Case "P"
                        cCol.BackColor = Drawing.Color.Peru
                    Case Else
                        If Trim((dsUP.Tables(0).Rows(k).Item("i_re").ToString)) <> "" Then
                            cCol.BackColor = Drawing.Color.LightBlue
                            sincolor = True
                        Else
                            cCol.BackColor = Drawing.Color.LightBlue
                        End If
                End Select
                rRow.Cells.Add(cCol)

                'Laboratorio

                cCol = New TableCell
                cCol.Text = Trim((dsUP.Tables(0).Rows(k).Item("l_or").ToString))
                cCol.BorderStyle = BorderStyle.Solid
                cCol.BorderColor = Drawing.Color.DarkGreen
                cCol.HorizontalAlign = HorizontalAlign.Center
                cCol.VerticalAlign = VerticalAlign.Middle
                Select Case (dsUP.Tables(0).Rows(k).Item("c_lor").ToString)
                    Case "R"
                        cCol.BackColor = Drawing.Color.Red
                    Case "V"
                        cCol.BackColor = Drawing.Color.LawnGreen
                    Case "A"
                        cCol.BackColor = Drawing.Color.Yellow
                    Case "P"
                        cCol.BackColor = Drawing.Color.Peru
                    Case Else
                        If Trim((dsUP.Tables(0).Rows(k).Item("l_or").ToString)) <> "" Then
                            cCol.BackColor = Drawing.Color.LightCoral
                            sincolor = True
                        Else
                            cCol.BackColor = Drawing.Color.LightCoral
                        End If
                End Select
                rRow.Cells.Add(cCol)

                cCol = New TableCell
                cCol.Text = Trim((dsUP.Tables(0).Rows(k).Item("l_to").ToString))
                cCol.BorderStyle = BorderStyle.Solid
                cCol.BorderColor = Drawing.Color.DarkGreen
                cCol.HorizontalAlign = HorizontalAlign.Center
                cCol.VerticalAlign = VerticalAlign.Middle
                Select Case (dsUP.Tables(0).Rows(k).Item("c_lto").ToString)
                    Case "R"
                        cCol.BackColor = Drawing.Color.Red
                    Case "V"
                        cCol.BackColor = Drawing.Color.LawnGreen
                    Case "A"
                        cCol.BackColor = Drawing.Color.Yellow
                    Case "P"
                        cCol.BackColor = Drawing.Color.Peru
                    Case Else
                        If Trim((dsUP.Tables(0).Rows(k).Item("l_to").ToString)) <> "" Then
                            cCol.BackColor = Drawing.Color.LightCoral
                            sincolor = True
                        Else
                            cCol.BackColor = Drawing.Color.LightCoral
                        End If
                End Select
                rRow.Cells.Add(cCol)

                cCol = New TableCell
                cCol.Text = Trim((dsUP.Tables(0).Rows(k).Item("l_pr").ToString))
                cCol.BorderStyle = BorderStyle.Solid
                cCol.BorderColor = Drawing.Color.DarkGreen
                cCol.HorizontalAlign = HorizontalAlign.Center
                cCol.VerticalAlign = VerticalAlign.Middle
                Select Case (dsUP.Tables(0).Rows(k).Item("c_lpr").ToString)
                    Case "R"
                        cCol.BackColor = Drawing.Color.Red
                    Case "V"
                        cCol.BackColor = Drawing.Color.LawnGreen
                    Case "A"
                        cCol.BackColor = Drawing.Color.Yellow
                    Case "P"
                        cCol.BackColor = Drawing.Color.Peru
                    Case Else
                        If Trim((dsUP.Tables(0).Rows(k).Item("l_pr").ToString)) <> "" Then
                            cCol.BackColor = Drawing.Color.LightCoral
                            sincolor = True
                        Else
                            cCol.BackColor = Drawing.Color.LightCoral
                        End If
                End Select
                rRow.Cells.Add(cCol)

                cCol = New TableCell
                cCol.Text = Trim((dsUP.Tables(0).Rows(k).Item("l_in").ToString))
                cCol.BorderStyle = BorderStyle.Solid
                cCol.BorderColor = Drawing.Color.DarkGreen
                cCol.HorizontalAlign = HorizontalAlign.Center
                cCol.VerticalAlign = VerticalAlign.Middle
                Select Case (dsUP.Tables(0).Rows(k).Item("c_lin").ToString)
                    Case "R"
                        cCol.BackColor = Drawing.Color.Red
                    Case "V"
                        cCol.BackColor = Drawing.Color.LawnGreen
                    Case "A"
                        cCol.BackColor = Drawing.Color.Yellow
                    Case "P"
                        cCol.BackColor = Drawing.Color.Peru
                    Case Else
                        If Trim((dsUP.Tables(0).Rows(k).Item("l_in").ToString)) <> "" Then
                            cCol.BackColor = Drawing.Color.LightCoral
                            sincolor = True
                        Else
                            cCol.BackColor = Drawing.Color.LightCoral
                        End If
                End Select
                rRow.Cells.Add(cCol)

                'Imagenología

                cCol = New TableCell
                cCol.Text = Trim((dsUP.Tables(0).Rows(k).Item("p_or").ToString))
                cCol.BorderStyle = BorderStyle.Solid
                cCol.BorderColor = Drawing.Color.DarkGreen
                cCol.HorizontalAlign = HorizontalAlign.Center
                cCol.VerticalAlign = VerticalAlign.Middle
                Select Case (dsUP.Tables(0).Rows(k).Item("c_por").ToString)
                    Case "R"
                        cCol.BackColor = Drawing.Color.Red
                    Case "V"
                        cCol.BackColor = Drawing.Color.LawnGreen
                    Case "A"
                        cCol.BackColor = Drawing.Color.Yellow
                    Case "P"
                        cCol.BackColor = Drawing.Color.Peru
                    Case Else
                        If Trim((dsUP.Tables(0).Rows(k).Item("p_or").ToString)) <> "" Then
                            cCol.BackColor = Drawing.Color.LightSeaGreen
                            sincolor = True
                        Else
                            cCol.BackColor = Drawing.Color.LightSeaGreen
                        End If
                End Select
                rRow.Cells.Add(cCol)

                cCol = New TableCell
                cCol.Text = Trim((dsUP.Tables(0).Rows(k).Item("p_re").ToString))
                cCol.BorderStyle = BorderStyle.Solid
                cCol.BorderColor = Drawing.Color.DarkGreen
                cCol.HorizontalAlign = HorizontalAlign.Center
                cCol.VerticalAlign = VerticalAlign.Middle
                Select Case (dsUP.Tables(0).Rows(k).Item("c_pre").ToString)
                    Case "R"
                        cCol.BackColor = Drawing.Color.Red
                    Case "V"
                        cCol.BackColor = Drawing.Color.LawnGreen
                    Case "A"
                        cCol.BackColor = Drawing.Color.Yellow
                    Case "P"
                        cCol.BackColor = Drawing.Color.Peru
                    Case Else
                        If Trim((dsUP.Tables(0).Rows(k).Item("p_re").ToString)) <> "" Then
                            cCol.BackColor = Drawing.Color.LightSeaGreen
                            sincolor = True
                        Else
                            cCol.BackColor = Drawing.Color.LightSeaGreen
                        End If
                End Select
                rRow.Cells.Add(cCol)

                cCol = New TableCell
                cCol.Text = Trim((dsUP.Tables(0).Rows(k).Item("p_in").ToString))
                cCol.BorderStyle = BorderStyle.Solid
                cCol.BorderColor = Drawing.Color.DarkGreen
                cCol.HorizontalAlign = HorizontalAlign.Center
                cCol.VerticalAlign = VerticalAlign.Middle
                Select Case (dsUP.Tables(0).Rows(k).Item("c_pin").ToString)
                    Case "R"
                        cCol.BackColor = Drawing.Color.Red
                    Case "V"
                        cCol.BackColor = Drawing.Color.LawnGreen
                    Case "A"
                        cCol.BackColor = Drawing.Color.Yellow
                    Case "P"
                        cCol.BackColor = Drawing.Color.Peru
                    Case Else
                        If Trim((dsUP.Tables(0).Rows(k).Item("p_in").ToString)) <> "" Then
                            cCol.BackColor = Drawing.Color.LightSeaGreen
                            sincolor = True
                        Else
                            cCol.BackColor = Drawing.Color.LightSeaGreen
                        End If
                End Select
                rRow.Cells.Add(cCol)


                'Destino

                cCol = New TableCell
                cCol.Text = Trim((dsUP.Tables(0).Rows(k).Item("dest").ToString))
                cCol.BorderStyle = BorderStyle.Solid
                cCol.BorderColor = Drawing.Color.DarkGreen
                cCol.HorizontalAlign = HorizontalAlign.Left
                cCol.VerticalAlign = VerticalAlign.Middle
                Select Case (dsUP.Tables(0).Rows(k).Item("c_dest").ToString)
                    Case "R"
                        cCol.BackColor = Drawing.Color.Red
                    Case "V"
                        cCol.BackColor = Drawing.Color.LawnGreen
                    Case "A"
                        cCol.BackColor = Drawing.Color.Yellow
                    Case "P"
                        cCol.BackColor = Drawing.Color.Peru
                    Case Else
                        If Trim((dsUP.Tables(0).Rows(k).Item("dest").ToString)) <> "No Definido" Then
                            cCol.BackColor = Drawing.Color.LightGray
                        Else
                            cCol.BackColor = Drawing.Color.LightGray
                        End If
                End Select


                rRow.Cells.Add(cCol)

                tbl_data.Rows.Add(rRow)
                bVacio = True
            Next

            'End If
            If nPages <= nTotal Then
                lblFecha.Text = "Página " & nPages.ToString & " de " & nTotal.ToString & ".       Actualización: " & Now().ToLongDateString & " " & Now().ToLongTimeString
            Else
                lblFecha.Text = "Actualización: " & Now().ToLongDateString & " " & Now().ToLongTimeString
            End If

        Catch ex As Exception
            appProxy = Nothing
            compresion = Nothing
            Response.Redirect("~/Pediatria")

        Finally
            appProxy = Nothing
            compresion = Nothing
        End Try

        If (TimerUP.Interval = 10000) Then
            TimerUP.Interval = 30000
        End If

        Try
            If ((sincolor = True) Or (dsUP.Tables(0).Rows.Count <= 0) Or (bVacio = False)) Then
                'Response.Redirect(Request.Url.AbsoluteUri)
                TimerUP.Interval = 10000
            End If
        Catch ex As Exception
            TimerUP.Interval = 10000
        Finally
        End Try

    End Sub

End Class