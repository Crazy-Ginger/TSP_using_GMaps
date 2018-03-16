Imports System
Imports System.IO
Imports System.Net
Imports System.Text
Imports System.Data
Imports System.Threading
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports System.Text.RegularExpressions


Module Module1
    Sub Main()
        Dim latLngKey As String = "AIzaSyA3A7tDgpFISYEY3B5qYdXm9StRa0pJkcA"
        Dim node_list As New List(Of String)
        Dim cont As Boolean = True

        Console.Write("Your first destination: ")
        node_list.Add(Console.ReadLine())

        List_print(node_list, True)

        While cont = True Or node_list.Count < 2
            Console.Write("Next destination: ")
            node_list.Add(Console.ReadLine())

            List_print(node_list, True)

            Console.Write("Continue? ")
            cont = Console.ReadLine()
        End While

        Console.Write("Do you have a final destination? ")
        cont = Console.ReadLine()
        If cont = True Then
            Console.Write("Final destination: ")
            node_list.Add(Console.ReadLine())
        End If
        Console.Clear()
        List_print(node_list, False)

        Faith_Permute(node_list.Count, node_list, cont)
        Console.ReadLine()
    End Sub

    Function List_print(ByRef list As List(Of String), clear As Boolean)
        If clear = True Then
            Console.Clear()
        End If
        For i As Integer = 0 To list.Count - 1
            Console.WriteLine("Address: " & list.Item(i))
        Next
        Console.WriteLine()
        Return Nothing
    End Function


    Public Function Approximation(ByVal length As Integer, ByVal nodes As List(Of String))
        Dim order(length - 1) As String
        Dim nodesLeft As Integer = length - 1
        Dim currentNode As String = nodes.Item(0)
        For i As Integer = 0 To nodesLeft
            Dim shortestRoute As Integer = 2147483647

        Next
        Return nodes
    End Function


    Function Waypointing(ByRef array() As Integer, ByVal length As String, ByRef nodes As List(Of String))
        'Builds request string
        Dim waypointkey As String = "AIzaSyBqN-1pDwR8taEDQESDP5mnJjiJkIXmv-w"
        Dim request_url As String = "https://maps.googleapis.com/maps/api/directions/json?origin="
        request_url += nodes.Item(0)
        request_url += "&destination=" & nodes.Item(array(length - 1)) & "&waypoints="
        For t As Integer = 1 To length - 2
            request_url += "via:" & nodes.Item(array(t)) & "|"
        Next
        request_url += "&key=" & waypointkey
        Console.WriteLine(request_url)

        ''https://msdn.microsoft.com/en-us/library/system.xml.xmlreader(v=vs.110).aspx
        ''https://developers.google.com/maps/documentation/directions/intro

        'determines whether the request string is valid and connects to the webserver
        Dim connected As Boolean = False        'variable to test connection
        Try                                                   'test if the created url responds and won't throw an error if it doesn't
            Using client = New WebClient()
                Using Stream = client.OpenRead(request_url)
                    connected = True                    'if there is a connection the variable is changed so that the program will try and pull the data
                End Using
            End Using
        Catch
            connected = False
        End Try
        Console.WriteLine(connected)


        'pulls the GEOJSON data
        If connected = True Then
            Dim request As HttpWebRequest = WebRequest.Create(request_url)
            request.Method = "POST"
            Dim data As Byte() = Nothing
            Dim datastream As Stream = request.GetRequestStream()

            Dim response As WebResponse = request.GetResponse()
            datastream = response.GetResponseStream()
            Dim reader As New StreamReader(datastream)
            Dim responsefromServer As String = reader.ReadToEnd()

            Dim Server_JSON As String = responsefromServer
            'Dim ser As JObject = JObject.Parse(Server_JSON)
            'Dim GEOJSON_data As List(Of JToken) = ser.Children().ToList
            Dim distance As JSON_data.Distance = JsonConvert.DeserializeObject(Of JSON_data.Distance)(Server_JSON)

            Console.WriteLine(distance.text & vbTab & distance.value)
            Dim duration As JSON_data.Duration = JsonConvert.DeserializeObject(Of JSON_data.Duration)(Server_JSON)
            Console.WriteLine(duration.text & vbTab & distance.value)
        Else
            Console.WriteLine("Did not retrieve any usable information or your addresses are invalide. Better luck next time.")
        End If
        Console.ReadLine()
        Return Nothing
    End Function


    Function FindLat(ByVal destination As String, ByVal key As String)
        'https://www.aspsnippets.com/Articles/Find-Co-ordinates-Latitude-And-Longitude-of-an-Address-Location-using-Google-Geocoding-API-in-ASPNet-using-C-And-VBNet.aspx
        Dim url2 As String = "https://maps.googleapis.com/maps/api/geocode/json?address=" & destination & "&key=" & key

        'links to google's API stuff (really really useful)  https://developers.google.com/maps/documentation/geocoding/start?hl=en_US
        'https://developers.google.com/maps/documentation/directions/intro#Waypoints
        'https://developers.google.com/maps/documentation/directions/?hl=en_US

        Dim request As WebRequest = WebRequest.Create(url2)
        Using response As WebResponse = DirectCast(request.GetResponse(), HttpWebResponse)
            Using reader As New StreamReader(response.GetResponseStream(), Encoding.UTF8)
                Dim dsResult As New DataSet()
                dsResult.ReadXml(reader)
                Dim dtCoordinates As New DataTable()
                dtCoordinates.Columns.AddRange(New DataColumn(3) {New DataColumn("Id", GetType(Integer)), New DataColumn("Address", GetType(String)), New DataColumn("Latitude", GetType(String)), New DataColumn("Longitude", GetType(String))})
                Dim geometry_id As String = dsResult.Tables("geometry").[Select]("result_id = " + ("result_id").ToString())(0)("geometry_id").ToString()
                Dim location As DataRow = dsResult.Tables("location").[Select](Convert.ToString("geometry_id = ") & geometry_id)(0)
                'dtCoordinates.Rows.Add(row("result_id"), row("formatted_address"), location("lat"), location("lng"))
                Return location("Lat")
            End Using
        End Using
    End Function
End Module
