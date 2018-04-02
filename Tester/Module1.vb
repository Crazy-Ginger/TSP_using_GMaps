Imports System
Imports System.IO
Imports System.Dynamic
Imports System.Net
Imports System.Text
Imports System.Data
Imports System.Threading
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports System.Text.RegularExpressions

Public Class Shorter
    Public distance As Integer
    Public duration As Integer
    Public nodes As New List(Of Integer)
End Class

Module Module1
    Public shortest As New Shorter

    Sub Main()
        shortest.distance = 2147483646
        shortest.duration = 0
        'Dim latLngKey As String = "AIzaSyA3A7tDgpFISYEY3B5qYdXm9StRa0pJkcA"
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
        Console.WriteLine("Shortest distance was: " & shortest.distance / 1000)
        For i As Integer = 0 To node_list.Count - 1
            Console.Write(node_list.Item(shortest.nodes(i)) & ", ")
        Next
        Console.WriteLine()
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


    Function Datapull(ByRef array() As Integer, ByVal length As String, ByRef nodes As List(Of String)) As Integer()
        'Builds request string
        Dim waypointkey As String = "AIzaSyBqN-1pDwR8taEDQESDP5mnJjiJkIXmv-w"
        Dim request_url As String = "https://maps.googleapis.com/maps/api/directions/json?origin="
        request_url += nodes.Item(0)
        request_url += "&destination=" & nodes.Item(array(length - 1)) & "&waypoints="
        For t As Integer = 1 To length - 2
            request_url += "via:" & nodes.Item(array(t)) & "|"
        Next
        request_url += "&key=" & waypointkey

        'pulls the GEOJSON data and puts it into a string
        'Console.WriteLine(request_url)

        'Try
        '    Dim test_stream As Stream = client.OpenRead(request_url)
        '    Console.WriteLine("It worked inside the try")
        '    sucess = True
        'Catch ex As Exception
        '    sucess = False
        '    Console.WriteLine("It did't work")
        'End Try
        Dim client As New WebClient()
        'client.DownloadFile(request_url, "JSON_data.json")
        Dim client_Stream As Stream = client.OpenRead(request_url)
        Dim streamreading As New StreamReader(client_Stream)
        Dim JSON_str As String = streamreading.ReadToEnd()


        'Dim JSON_token As JToken = JObject.Parse(JSON_str)
        'Dim distance As Integer = JSON_token.SelectToken("value")
        'Dim temp_json As DynamicObject = JsonConvert.DeserializeObject(Of ExpandoObject)(JSON_str)
        Dim temp_json As JSON_data.Rootobject = JsonConvert.DeserializeObject(Of JSON_data.Rootobject)(JSON_str)
        'Dim temp_jsonlist As List(Of JSON_data.Rootobject) = JsonConvert.DeserializeObject(Of List(Of JSON_data.Rootobject))(JSON_str)
        streamreading.Close()

        Dim distance As Integer
        Dim duration As Integer
        Console.WriteLine(temp_json.status)
        'Console.WriteLine(temp_jsonlist.Item(0).status)
        If temp_json.status = "OK" Then
            'If temp_jsonlist.Item(0).status = "OK" Then

            distance = temp_json.routes.legs.distance.value
            duration = temp_json.routes.legs.duration.value

            'distance = temp_jsonlist.Item(0).routes.legs.distance.value
            'duration = temp_jsonlist.Item(0).routes.legs.duration.value
        Else
            distance = 2147483646
            duration = 2147483646
        End If
        Console.WriteLine("The distance is: " & distance)
        Console.WriteLine("The duration is: " & duration)
        Dim passed(1) As Integer
        passed(0) = distance    'distance
        passed(1) = duration   'duration
        Return passed

        'tries To make the JSON data Using JObject (did't know how to search the JObject for useful data
        'Dim jdata As JObject = JObject.Parse(Server_JSON_str)


        'searches for a key phrase then retrieves the value associated with the key phrase
        'finds the physical length of the journey
        '_________________________________________________________________________________________________-
        'Dim search_dist As String = "distance"
        'Dim dist_char As Integer = JSON_str.IndexOf(search_dist)

        'dist_char = JSON_str.IndexOf("value")
        'dist_char += 9
        'Dim dist_converter As String = ""
        'For i As Integer = dist_char To JSON_str.Length
        '    If JSON_str.Substring(i, 1) = " " Then
        '        Exit For
        '    Else
        '        dist_converter += JSON_str.Substring(i, 1)
        '    End If
        'Next
        ''Console.WriteLine()
        ''Console.WriteLine("Dist converter: " & dist_converter)
        'Dim distance As Integer = CInt(dist_converter)

        ''finds the time length of the journey
        'Dim damaged_JSON As String = Right(JSON_str, JSON_str.Length - dist_char)
        'Dim search_dura As String = "duration"
        'Dim dura_char As Integer = damaged_JSON.IndexOf(search_dura)
        ''Console.WriteLine(dura_char)
        'dura_char = damaged_JSON.IndexOf("value")
        'dura_char += 9
        'Dim dura_converter As String = ""
        'For i As Integer = dura_char To damaged_JSON.Length
        '    If damaged_JSON.Substring(i, 1) = " " Then
        '        Exit For
        '    Else
        '        dura_converter += damaged_JSON.Substring(i, 1)
        '    End If
        'Next
        'Dim duration As Integer = Integer.Parse(dura_converter)

        '_____________________________________________________________________________________________


        'tried to make this convert the string from the website to a class to make getting data really easily
        'Dim JSON_object As List(Of JSON_data.Rootobject) = JsonConvert.DeserializeObject(Of List(Of JSON_data.Rootobject))(Server_JSON_str)

        'Console.WriteLine("Distance: " & distance & " (m)")
        'Console.WriteLine("Duration: " & duration & " (s)" & vbTab & Math.Floor(duration / 3600) & " hours  " & Math.Round((duration Mod 3600) / 60) & " minutes")
        'Dim passed(1) As Integer
        'passed(0) = temp_json.routes.legs.distance.value    'distance
        'passed(1) = temp_json.routes.legs.duration.value    'duration
        'Return passed
        'Else
        '    Return Nothing
        'End If
    End Function


    Public Sub Faith_Permute(ByVal length As Integer, ByRef nodes As List(Of String), ByVal End_dest As Boolean)
        'Creates the variables to be used in the algorithm
        Dim P(length - 1) As Integer
        Dim swapper As Integer
        Dim initial_comp As Integer
        Dim rearrange As Integer
        Dim asc_swapper As Integer
        Dim count As Long
        Dim Last As Boolean

        'Fills the array with pointers that can be sorted
        For i = 0 To length - 1
            P(i) = i
            shortest.nodes.Add(i)
        Next
        'Checks if there is a set end point that should not be shuffled
        If End_dest = True Then
            length -= 1
        End If

        count = 0
        Last = False
        Dim watch As Stopwatch = Stopwatch.StartNew()   'records the time it takes for all the permutations to be calculated (temp)

        Do While Not Last
            'outputs the pointers and destinations in order
            Sort_array(P, length, nodes, End_dest)
            count = count + 1
            Last = True
            initial_comp = length - 2
            'finds the largest pointer out of place (from the back)
            Do While initial_comp > 0
                If P(initial_comp) < P(initial_comp + 1) Then
                    Last = False
                    Exit Do
                End If
                initial_comp = initial_comp - 1
            Loop

            rearrange = initial_comp + 1
            asc_swapper = length - 1
            'rearranges all the pointers behind the out of place pointer into ascending numerical order
            While rearrange < asc_swapper
                ' Swap p(j) and p(k)
                swapper = P(rearrange)
                P(rearrange) = P(asc_swapper)
                P(asc_swapper) = swapper
                rearrange += 1
                asc_swapper -= 1
            End While

            rearrange = length - 1
            'finds the pointer to be swapped with the out of place pointer
            While P(rearrange) > P(initial_comp)
                rearrange = rearrange - 1
            End While

            rearrange = rearrange + 1

            'makes the swap to place the large number in front
            swapper = P(initial_comp)
            P(initial_comp) = P(rearrange)
            P(rearrange) = swapper
        Loop
        watch.Stop()
        Console.WriteLine("Number of permutations: " & count & vbTab & "That was: " & watch.Elapsed.TotalMilliseconds & "ms, " & watch.ElapsedTicks & " ticks")

    End Sub


    Public Sub Sort_array(ByRef array() As Integer, ByVal length As Integer, ByRef nodes As List(Of String), ByVal end_dest As Boolean)
        If end_dest = True Then 'if there was an end destination being used then the length must be increased so that it is printed
            length += 1
        End If

        For t As Integer = 0 To length - 1
            Console.Write(array(t) & ": " & nodes.Item(array(t)) & ", ")
        Next

        Dim dist_dura() As Integer = Datapull(array, length, nodes)   'sends this to the function that should return the length/duration of the route
        Dim retest As Integer = shortest.distance

        If dist_dura(0) < retest Then   'compares the existing shortest route with the current one
            shortest.distance = dist_dura(0)
            shortest.duration = dist_dura(1)
            For i As Integer = 0 To array.Length - 1
                shortest.nodes.Item(i) = array(i)
            Next
        Else

        End If
        Console.WriteLine("Shortest: " & shortest.distance)
        Console.WriteLine()
    End Sub


    Public Function Approximation(ByVal nodes As List(Of String), ByVal last As Boolean)
        Dim length As Integer = nodes.Count - 1
        If last = True Then
            length -= 1
        End If
        Dim final_order As New Shorter
        Dim pointers As New List(Of Integer)
        For i As Integer = 0 To length
            pointers.Add(i)
        Next
        final_order.nodes.Add(nodes.Item(0))
        pointers.Remove(0)

        For i As Integer = 0 To length

            Dim request_url As String = "https://maps.googleapis.com/maps/api/directions/json?origin="
            request_url += nodes.Item(0)
            request_url += "&destination=" & nodes.Item(i)
        Next
        Return Nothing
    End Function


    'Function FindLat(ByVal destination As String, ByVal key As String)
    '    'https://www.aspsnippets.com/Articles/Find-Co-ordinates-Latitude-And-Longitude-of-an-Address-Location-using-Google-Geocoding-API-in-ASPNet-using-C-And-VBNet.aspx
    '    Dim url2 As String = "https://maps.googleapis.com/maps/api/geocode/json?address=" & destination & "&key=" & key

    '    'links to google's API stuff (really really useful)  https://developers.google.com/maps/documentation/geocoding/start?hl=en_US
    '    'https://developers.google.com/maps/documentation/directions/intro#Waypoints
    '    'https://developers.google.com/maps/documentation/directions/?hl=en_US

    '    Dim request As WebRequest = WebRequest.Create(url2)
    '    Using response As WebResponse = DirectCast(request.GetResponse(), HttpWebResponse)
    '        Using reader As New StreamReader(response.GetResponseStream(), Encoding.UTF8)
    '            Dim dsResult As New DataSet()
    '            dsResult.ReadXml(reader)
    '            Dim dtCoordinates As New DataTable()
    '            dtCoordinates.Columns.AddRange(New DataColumn(3) {New DataColumn("Id", GetType(Integer)), New DataColumn("Address", GetType(String)), New DataColumn("Latitude", GetType(String)), New DataColumn("Longitude", GetType(String))})
    '            Dim geometry_id As String = dsResult.Tables("geometry").[Select]("result_id = " + ("result_id").ToString())(0)("geometry_id").ToString()
    '            Dim location As DataRow = dsResult.Tables("location").[Select](Convert.ToString("geometry_id = ") & geometry_id)(0)
    '            'dtCoordinates.Rows.Add(row("result_id"), row("formatted_address"), location("lat"), location("lng"))
    '            Return location("Lat")
    '        End Using
    '    End Using
    'End Function
End Module
