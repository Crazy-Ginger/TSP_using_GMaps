Namespace JSON_data
    Public Class Rootobject
        Public Property geocoded_waypoints As Geocoded_Waypoints()
        Public Property routes() As Route()
        Public Property status As String
    End Class

    Public Class Geocoded_Waypoints
        Public Property geocoder_status As String
        Public Property place_id As String
        Public Property types As String()
    End Class

    Public Class Route
        Public Property bounds As Bounds
        Public Property copyrights As String
        Public Property legs() As Leg()
        Public Property overview_polyline As Overview_Polyline
        Public Property summary As String
        Public Property warnings() As Object
        Public Property waypoint_order() As Object
    End Class

    Public Class Bounds
        Public Property northeast As Northeast
        Public Property southwest As Southwest
    End Class

    Public Class Northeast
        Public Property lat As Single
        Public Property lng As Single
    End Class

    Public Class Southwest
        Public Property lat As Single
        Public Property lng As Single
    End Class

    Public Class Overview_Polyline
        Public Property points As String
    End Class

    Public Class Leg
        Public Property distance As Distance
        Public Property duration As Duration
        Public Property end_address As String
        Public Property end_location As End_Location
        Public Property start_address As String
        Public Property start_location As Start_Location
        Public Property steps() As _Step
        Public Property traffic_speed_entry() As Object
        Public Property via_waypoint() As Via_Waypoint
    End Class

    Public Class Distance
        Public Property text As String
        Public Property value As Integer
    End Class

    Public Class Duration
        Public Property text As String
        Public Property value As Integer
    End Class

    Public Class End_Location
        Public Property lat As Single
        Public Property lng As Single
    End Class

    Public Class Start_Location
        Public Property lat As Single
        Public Property lng As Single
    End Class

    Public Class _Step
        Public Property distance As Distance1
        Public Property duration As Duration1
        Public Property end_location As End_Location1
        Public Property html_instructions As String
        Public Property polyline As Polyline
        Public Property start_location As Start_Location1
        Public Property travel_mode As String
        Public Property maneuver As String
    End Class

    Public Class Distance1
        Public Property text As String
        Public Property value As Integer
    End Class

    Public Class Duration1
        Public Property text As String
        Public Property value As Integer
    End Class

    Public Class End_Location1
        Public Property lat As Single
        Public Property lng As Single
    End Class

    Public Class Polyline
        Public Property points As String
    End Class

    Public Class Start_Location1
        Public Property lat As Single
        Public Property lng As Single
    End Class

    Public Class Via_Waypoint
        Public Property location As Location
        Public Property step_index As Integer
        Public Property step_interpolation As Single
    End Class

    Public Class Location
        Public Property lat As Single
        Public Property lng As Single
    End Class
End Namespace
'Public Class Rootobject
'    Public Property geocoded_waypoints As Geocoded_Waypoints()
'    Public Property routes As Route()
'    Public Property status As String
'End Class

'Public Class Geocoded_Waypoints
'    Public Property geocoder_status As String
'    Public Property place_id As String
'    Public Property types As String()
'End Class

'Public Class Route
'    Public Property bounds As Bounds
'    Public Property copyrights As String
'    Public Property legs As Leg()
'    Public Property overview_polyline As Overview_Polyline
'    Public Property summary As String
'    Public Property warnings As Object()
'    Public Property waypoint_order As Object()
'End Class

'Public Class Bounds
'    Public Property northeast As Northeast
'    Public Property southwest As Southwest
'End Class

'Public Class Northeast
'    Public Property lat As Single
'    Public Property lng As Single
'End Class

'Public Class Southwest
'    Public Property lat As Single
'    Public Property lng As Single
'End Class

'Public Class Overview_Polyline
'    Public Property points As String
'End Class

'Public Class Leg
'    Public Property distance As Distance
'    Public Property duration As Duration
'    Public Property end_address As String
'    Public Property end_location As End_Location
'    Public Property start_address As String
'    Public Property start_location As Start_Location
'    Public Property steps As _Step()
'    Public Property traffic_speed_entry As Object()
'    Public Property via_waypoint As Via_Waypoint()
'End Class

'Public Class Distance
'    Public Property text As String
'    Public Property value As Integer
'End Class

'Public Class Duration
'    Public Property text As String
'    Public Property value As Integer
'End Class

'Public Class End_Location
'    Public Property lat As Single
'    Public Property lng As Single
'End Class

'Public Class Start_Location
'    Public Property lat As Single
'    Public Property lng As Single
'End Class

'Public Class _Step
'    Public Property distance As Distance1
'    Public Property duration As Duration1
'    Public Property end_location As End_Location1
'    Public Property html_instructions As String
'    Public Property polyline As Polyline
'    Public Property start_location As Start_Location1
'    Public Property travel_mode As String
'    Public Property maneuver As String
'End Class

'Public Class Distance1
'    Public Property text As String
'    Public Property value As Integer
'End Class

'Public Class Duration1
'    Public Property text As String
'    Public Property value As Integer
'End Class

'Public Class End_Location1
'    Public Property lat As Single
'    Public Property lng As Single
'End Class

'Public Class Polyline
'    Public Property points As String
'End Class

'Public Class Start_Location1
'    Public Property lat As Single
'    Public Property lng As Single
'End Class

'Public Class Via_Waypoint
'    Public Property location As Location
'    Public Property step_index As Integer
'    Public Property step_interpolation As Single
'End Class

'Public Class Location
'    Public Property lat As Single
'    Public Property lng As Single
'End Class
