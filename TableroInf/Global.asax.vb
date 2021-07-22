Imports System.Web.Optimization

Public Class Global_asax
    Inherits HttpApplication

    Sub Application_Start(sender As Object, e As EventArgs)
        ' Se desencadena al iniciar la aplicación
        RouteConfig.RegisterRoutes(RouteTable.Routes)
        BundleConfig.RegisterBundles(BundleTable.Bundles)
    End Sub

    Sub Session_Start(sender As Object, e As EventArgs)
        Session("nPages") = 0
        Session("nContador") = 0
        Session("nFinal") = 0
    End Sub


End Class