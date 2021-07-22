Imports System.Web.Routing
Imports Microsoft.AspNet.FriendlyUrls

Public Module RouteConfig
    Sub RegisterRoutes(ByVal routes As RouteCollection)
        Dim settings As FriendlyUrlSettings = New FriendlyUrlSettings()
        settings.AutoRedirectMode = RedirectMode.Permanent
        routes.EnableFriendlyUrls(settings)

        routes.MapPageRoute("Home", "Home", "~/Default.aspx")
        routes.MapPageRoute("Pediatria", "Pediatria", "~/DefaultUP.aspx")
        routes.MapPageRoute("GinecoObstetricia", "Gineco", "~/DefaultUG.aspx")
        routes.MapPageRoute("Adultos", "Adultos", "~/DefaultUR.aspx")

    End Sub
End Module

