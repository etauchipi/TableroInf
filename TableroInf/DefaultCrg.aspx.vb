
Imports System.Data
Imports wsServinteProxy.IwsServinteClient

Public Class _DefaultCrg
    Inherits Page

    Private inicio As Integer
    Private sCnn As String
    Private compresion As Compresion.Compresion
    Private appProxy As wsServinteProxy.IwsServinteClient

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        Timer1.Interval = 181000
        inicio = 0
        carga()
    End Sub

    Private Sub carga()
        lblFecha.Text = "Actualización: " & Now().ToLongDateString & " " & Now().ToLongTimeString
        updp_main.Update()
    End Sub

    Protected Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Renderiza1()
        Renderiza2()
        Renderiza3()
        carga()
        updp_main.Update()
    End Sub

    Private Sub Renderiza1()

        Dim cConn As String

        Try

            appProxy = New wsServinteProxy.IwsServinteClient

            cConn = "execute procedure sp_importacion_datos('UG')"
            appProxy.exec_data_Gral(cConn)
            cConn = "execute procedure sp_actulizacion_color('UG')"
            appProxy.exec_data_Gral(cConn)
            cConn = "execute procedure export_table('UG')"
            appProxy.exec_data_Gral(cConn)

         Catch ex As Exception
            '
        Finally
            appProxy.Close()
        End Try

    End Sub

    Private Sub Renderiza2()

        Dim cConn As String

        Try

            appProxy = New wsServinteProxy.IwsServinteClient

            cConn = "execute procedure sp_importacion_datos('UP')"
            appProxy.exec_data_Gral(cConn)
            cConn = "execute procedure sp_actulizacion_color('UP')"
            appProxy.exec_data_Gral(cConn)
            cConn = "execute procedure export_table('UP')"
            appProxy.exec_data_Gral(cConn)

        Catch ex As Exception
            '
        Finally
            appProxy.Close()
        End Try

    End Sub

    Private Sub Renderiza3()

        Dim cConn As String

        Try

            appProxy = New wsServinteProxy.IwsServinteClient

            cConn = "execute procedure sp_importacion_datos('UR')"
            appProxy.exec_data_Gral(cConn)
            cConn = "execute procedure sp_actulizacion_color('UR')"
            appProxy.exec_data_Gral(cConn)
            cConn = "execute procedure export_table('UR')"
            appProxy.exec_data_Gral(cConn)

        Catch ex As Exception
            '
        Finally
            appProxy.Close()
        End Try

    End Sub


End Class