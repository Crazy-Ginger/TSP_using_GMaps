﻿Namespace JSON_data
    Public Class Rootobject
        Public Property geocoded_waypoints() As Geocoded_Waypoints
        Public Property routes() As Route
        Public Property status As String
    End Class

    Public Class Geocoded_Waypoints
        Public Property geocoder_status As String
        Public Property place_id As String
        Public Property types() As String
    End Class

    Public Class Route
        Public Property bounds As Bounds
        Public Property copyrights As String
        Public Property legs() As Leg
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
'    Public Property geocoded_waypoints() As Rootobject
'    Public Property routes() As Rootobject
'    Public Property status As String
'    'End Class

'    'Public Class Geocoded_Waypoints
'    Public Property geoway_geocoder_status As String
'    Public Property geoway_place_id As String
'    Public Property geoway_types() As String
'    'End Class

'    'Public Class Route
'    Public Property route_bounds As Rootobject
'    Public Property route_copyrights As String
'    Public Property route_legs() As Rootobject
'    Public Property route_overview_polyline As Rootobject
'    Public Property route_summary As String
'    Public Property route_warnings() As Object
'    Public Property route_waypoint_order() As Object
'    'End Class

'    'Public Class Bounds
'    Public Property bound_northeast As Rootobject
'    Public Property bound_southwest As Rootobject
'    'End Class

'    'Public Class Northeast
'    Public Property north_lat As Single
'    Public Property north_lng As Single
'    'End Class

'    'Public Class Southwest
'    Public Property south_lat As Single
'    Public Property south_lng As Single
'    'End Class

'    'Public Class Overview_Polyline
'    Public Property overpoly_points As String
'    'End Class

'    'Public Class Leg
'    Public Property leg_distance As Rootobject
'    Public Property leg_duration As Rootobject
'    Public Property leg_end_address As String
'    Public Property leg_end_location As Rootobject
'    Public Property leg_start_address As String
'    Public Property leg_start_location As Rootobject
'    Public Property leg_steps() As Rootobject
'    Public Property leg_traffic_speed_entry() As Object
'    Public Property leg_via_waypoint() As Rootobject
'    'End Class

'    'Public Class Distance
'    Public Property maindist_text As String
'    Public Property maindist_value As Integer
'    'End Class

'    'Public Class Duration
'    Public Property maindura_text As String
'    Public Property maindura_value As Integer
'    'End Class

'    'Public Class End_Location
'    Public Property end_lat As Single
'    Public Property end_lng As Single
'    'End Class

'    'Public Class Start_Location
'    Public Property start_lat As Single
'    Public Property start_lng As Single
'    'End Class

'    'Public Class _Step
'    Public Property step_distance As Rootobject
'    Public Property step_duration As Rootobject
'    Public Property step_end_location As Rootobject
'    Public Property step_html_instructions As String
'    Public Property step_polyline As Rootobject
'    Public Property step_start_location As Rootobject
'    Public Property step_travel_mode As String
'    Public Property step_maneuver As String
'    'End Class

'    'Public Class Distance1
'    Public Property dist1_text As String
'    Public Property dist1_value As Integer
'    'End Class

'    'Public Class Duration1
'    Public Property dura1_text As String
'    Public Property dura1_value As Integer
'    'End Class

'    'Public Class End_Location1
'    Public Property end1_lat As Single
'    Public Property end1_lng As Single
'    'End Class

'    'Public Class Polyline
'    Public Property poly_points As String
'    'End Class

'    'Public Class Start_Location1
'    Public Property start1_lat As Single
'    Public Property start1_lng As Single
'    'End Class

'    'Public Class Via_Waypoint
'    Public Property viaway_location As Rootobject
'    Public Property viaway_step_index As Integer
'    Public Property viaway_step_interpolation As Single
'    'End Class

'    'Public Class Location
'    Public Property loca_lat As Single
'    Public Property loca_lng As Single
'End Class