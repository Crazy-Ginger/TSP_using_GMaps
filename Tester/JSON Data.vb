Namespace JSON_data

    Public Class GeocodedWaypoint
        Public Property geocoder_status As String
        Public Property place_id As String
        Public Property types As String()
    End Class

    Public Class Northeast
        Public Property lat As Double
        Public Property lng As Double
    End Class

    Public Class Southwest
        Public Property lat As Double
        Public Property lng As Double
    End Class

    Public Class Bounds
        Public Property northeast As Northeast
        Public Property southwest As Southwest
    End Class

    Public Class Distance
        Public Property text As String
        Public Property value As Integer
        Public Property dist_result() As Distance
    End Class

    Public Class Duration
        Public Property text As String
        Public Property value As Integer
        Public Property time_result() As Duration
    End Class

    Public Class EndLocation
        Public Property lat As Double
        Public Property lng As Double
    End Class

    Public Class StartLocation
        Public Property lat As Double
        Public Property lng As Double
    End Class

    Public Class Polyline
        Public Property points As String
    End Class

    Public Class Steps
        Public Property distance As Distance
        Public Property duration As Duration
        Public Property end_location As EndLocation
        Public Property html_instructions As String
        Public Property polyline As Polyline
        Public Property start_location As StartLocation
        Public Property travel_mode As String
        Public Property maneuver As String
    End Class

    Public Class Location
        Public Property lat As Double
        Public Property lng As Double
    End Class

    Public Class ViaWaypoint
        Public Property location As Location
        Public Property step_index As Integer
        Public Property step_interpolation As Double
    End Class

    Public Class Leg
        Public Property distance As Distance
        Public Property duration As Duration
        Public Property end_address As String
        Public Property end_location As EndLocation
        Public Property start_address As String
        Public Property start_location As StartLocation
        Public Property steps As Steps()
        Public Property traffic_speed_entry As Object()
        Public Property via_waypoint As ViaWaypoint()
    End Class

    Public Class OverviewPolyline
        Public Property points As String
    End Class

    Public Class Route
        Public Property bounds As Bounds
        Public Property copyrights As String
        Public Property legs As Leg()
        Public Property overview_polyline As OverviewPolyline
        Public Property summary As String
        Public Property warnings As Object()
        Public Property waypoint_order As Object()
    End Class
End Namespace