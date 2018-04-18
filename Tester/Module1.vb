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
    Public nodes As New List(Of String)
    Public URL As String
End Class


Module Module1
    Public shortest As New Shorter
    Public Current_URL As New StringBuilder
    'Public watch As Stopwatch
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
        Console.WriteLine("Shortest distance was: " & shortest.distance / 1000 & " km")
        For i As Integer = 0 To node_list.Count - 1
            Console.Write(shortest.nodes.Item(i) & ", ")
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
        'Dim waypointkey As String = "AIzaSyBqN-1pDwR8taEDQESDP5mnJjiJkIXmv-w"
        'Dim request_url As String = "https://maps.googleapis.com/maps/api/directions/json?origin="
        'request_url += nodes.Item(0)
        'request_url += "&destination=" & nodes.Item(array(length - 1)) & "&waypoints="
        'For t As Integer = 1 To length - 2
        '    request_url += "via:" & nodes.Item(array(t)) & "|"
        'Nexts
        'request_url += "&key=" & waypointkey


        Dim waypointkey As String = "AIzaSyBqN-1pDwR8taEDQESDP5mnJjiJkIXmv-w"
        Current_URL.Clear()
        Current_URL.Append("https://maps.googleapis.com/maps/api/directions/json?origin=")
        Current_URL.Append(nodes.Item(0))
        Current_URL.Append("&destination=" & nodes.Item(array(length - 1)) & "&waypoints=")
        For t As Integer = 1 To length - 2
            Current_URL.Append("via:" & nodes.Item(array(t)) & "|")
        Next
        Current_URL.Append("&key=" & waypointkey)


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
        Dim passed(1) As Integer
        For count As Integer = 1 To 3


            Dim client As New WebClient()
            'client.DownloadFile(request_url, "JSON_data.json")
            'Dim client_Stream As Stream = client.OpenRead(request_url)
            Dim client_Stream As Stream = client.OpenRead(Current_URL.ToString)
            Dim streamreading As New StreamReader(client_Stream)
            Dim JSON_str As String = streamreading.ReadToEnd()



            '++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            'using the class system (that doesn't work yet)
            'Dim JSON_token As JToken = JObject.Parse(JSON_str)
            'Dim distance As Integer = JSON_token.SelectToken("value")
            'Dim temp_json As DynamicObject = JsonConvert.DeserializeObject(Of ExpandoObject)(JSON_str)
            'tried to make this convert the string from the website to a class to make getting data really easily
            'above are the ones that I don't think will ever work


            'promising ========================================================
            'Dim temp_json As JSON_data.Rootobject = JsonConvert.DeserializeObject(Of JSON_data.Rootobject)(JSON_str)
            'Dim temp_jsonlist As List(Of JSON_data.Rootobject) = JsonConvert.DeserializeObject(Of List(Of JSON_data.Rootobject))(JSON_str)
            streamreading.Close()
            client_Stream.Close()

            'Dim distance As Integer
            'Dim duration As Integer
            'Console.WriteLine(temp_json.status)
            ''Console.WriteLine(temp_jsonlist.Item(0).status)
            'If temp_json.status = "OK" Then
            '    'If temp_jsonlist.Item(0).status = "OK" Then

            '    distance = temp_json.routes.legs.distance.value
            '    duration = temp_json.routes.legs.duration.value

            '    'distance = temp_jsonlist.Item(0).routes.legs.distance.value
            '    'duration = temp_jsonlist.Item(0).routes.legs.duration.value
            'Else
            '    distance = 2147483646
            '    duration = 2147483646
            'End If
            'Console.WriteLine("The distance is: " & distance)
            'Console.WriteLine("The duration is: " & duration)
            'Dim passed(1) As Integer
            'passed(0) = distance    'distance
            'passed(1) = duration   'duration
            'Return passed
            '----------------------------------------------------------------------------------------------------------------------------------------------------




            'searches for a key phrase then retrieves the value associated with the key phrase
            'finds the length and estimated duration of the journey
            '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++


            Dim status_search As String = Chr(34) & "status" & Chr(34)
            Dim status_index As Integer = JSON_str.IndexOf(status_search)
            Console.WriteLine(status_search)
            status_index += 12
            Dim status As New StringBuilder
            For i As Integer = status_index To JSON_str.Length
                If JSON_str.Substring(i, 1) = Chr(34) Then
                    Exit For
                    count += 1
                Else
                    status.Append(JSON_str.Substring(i, 1))
                End If
            Next
            Console.WriteLine(status.ToString)
            Console.ReadLine()
            'test if the status of the route is valid or not
            If status.ToString = "OK" Then
                'find the distance of the route
                Dim dist_char As Integer = JSON_str.IndexOf("value")
                dist_char += 9
                Dim dist_converter As String = ""
                For i As Integer = dist_char To JSON_str.Length
                    If JSON_str.Substring(i, 1) = " " Then
                        Exit For
                    Else
                        dist_converter += JSON_str.Substring(i, 1)
                    End If
                Next
                Dim distance As Integer = CInt(dist_converter)
                'Console.WriteLine("Dist converter: " & dist_converter)


                'finds the time length of the journey
                Dim damaged_JSON As String = Right(JSON_str, JSON_str.Length - dist_char)

                Dim dura_char As Integer

                dura_char = damaged_JSON.IndexOf("value")
                dura_char += 9
                Dim dura_converter As String = ""
                For i As Integer = dura_char To damaged_JSON.Length
                    If damaged_JSON.Substring(i, 1) = " " Then
                        Exit For
                    Else
                        dura_converter += damaged_JSON.Substring(i, 1)
                    End If
                Next
                Dim duration As Integer = CInt(dura_converter)

                'Console.WriteLine("Dist converter: " & dura_converter)
                '------------------------------------------------------------------------------------------------------------------------------------------------

                'Console.WriteLine("Distance: " & distance & " (m)")
                'Console.WriteLine("Duration: " & duration & " (s)" & vbTab & Math.Floor(duration / 3600) & " hours  " & Math.Round((duration Mod 3600) / 60) & " minutes")


                'passed(0) = temp_json.routes.legs.distance.value    'distance using the class
                'passed(1) = temp_json.routes.legs.duration.value    'duration using the class
                passed(0) = distance                        'distance using string parser
                passed(1) = duration                        'distance using string parser
                'Console.WriteLine("route found")
                Return passed
                Exit Function

                'Else
                'Return Nothing
                'End If
            ElseIf status.ToString = "ZERO_RESULTS" Then
                passed(0) = 2147483645
                passed(1) = 2147483645
                Console.WriteLine("Zero_results")
                Exit For

            ElseIf status.ToString = "NOT_FOUND" Then
                passed(0) = 2147483645
                passed(1) = -1
                Console.WriteLine("not_found")
                Exit For

            ElseIf status.ToString = "OVER_QUERY_LIMIT" Then
                passed(0) = 2147483645
                passed(1) = -2
                Console.WriteLine("Query limit achieved")
                Exit For

            ElseIf status.ToString = "MAX_WAYPOINTS_EXCEEDED" Then
                passed(0) = 2147483645
                passed(1) = -3

                Exit For

            ElseIf status.ToString = "MAX_ROUTE_LENGTH_EXCEEDED" Then
                passed(0) = 2147483645
                passed(1) = 2147483645
                Exit For

            Else
                passed(0) = 2147483645
                passed(1) = 2147483645
            End If

        Next

        Return passed
    End Function


    Public Sub Faith_Permute(ByVal length As Integer, ByRef nodes As List(Of String), ByVal End_dest As Boolean)
        'Creates the variables to be used in the algorithm
        Dim P(length - 1) As Integer
        Dim swapper As Integer
        Dim initial_comp As Integer
        Dim rearrange As Integer
        Dim asc_swapper As Integer
        Dim count As Long = 0
        Dim Last As Boolean = False

        'Fills the array with pointers that can be sorted
        For i = 0 To length - 1
            P(i) = i
            shortest.nodes.Add(i)
        Next

        'Checks if there is a set end point that should not be shuffled
        If End_dest = True Then
            length -= 1
        End If


        'records the time it takes for all the permutations to be calculated (temp)
        Dim watch As New Stopwatch
        watch.Start

        Do While Not Last
            'outputs the pointers and destinations in order
            Sort_array(P, length, nodes, End_dest)

            count += 1
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
                rearrange -= 1
            End While

            rearrange += 1

            'makes the swap to place the large number in front
            watch.Stop()
            swapper = P(initial_comp)
            P(initial_comp) = P(rearrange)
            P(rearrange) = swapper
        Loop

        Console.WriteLine("Number of permutations: " & count & vbTab & "That was: " & watch.Elapsed.TotalMilliseconds & "ms, " & watch.ElapsedTicks & " ticks")

    End Sub


    Public Sub Sort_array(ByRef array() As Integer, ByVal length As Integer, ByRef nodes As List(Of String), ByVal end_dest As Boolean)
        'if there was an end destination being used then the length must be increased so that it is printed
        If end_dest = True Then
            length += 1
        End If

        'For t As Integer = 0 To length - 1
        '    Console.Write(array(t) & ": " & nodes.Item(array(t)) & ", ")
        'Next
        'Console.WriteLine()

        'sends this to the function that should return the length/duration of the route
        Dim current_route() As Integer = Datapull(array, length, nodes)

        'Dim retest As Integer = shortest.distance

        'compares the existing shortest route with the current one
        If current_route(0) < shortest.distance Then
            shortest.distance = current_route(0)
            shortest.duration = current_route(1)
            shortest.URL = Current_URL.ToString
            For i As Integer = 0 To array.Length - 1
                Console.WriteLine("nodes.Item(array(i))")
                shortest.nodes.Item(i) = nodes.Item(array(i))
            Next
        End If
        'watch.Stop()
        'Console.WriteLine(watch)
        Console.ReadLine()
        'Console.WriteLine("Shortest: " & shortest.distance)
        'Console.WriteLine()
    End Sub


    Public Function Approximation(ByVal nodes As List(Of String), ByVal last As Boolean)
        'creates length that is the number of addresses being used
        Dim length As Integer = nodes.Count - 1
        'shortens length to ensure that if a last destination is chosen it remains the last address
        If last = True Then
            length -= 1
        End If
        'instantiating key variables
        Dim using_URL As New StringBuilder
        Dim chosen As String = ""
        Dim head As String = ""
        Dim next_nodes As List(Of String) = nodes
        Dim final_route As New List(Of String)
        Dim distance As Integer = 0
        Dim duration As Integer = 0
        Dim comp_dist As Integer = Integer.MaxValue
        Dim current_index As New Integer

        'add the starting address and remove it from next_nodes
        final_route.Add(next_nodes.Item(0))
        next_nodes.Remove(0)
        length -= 1

        'run whilst there are still nodes within the copied list this will loop
        While next_nodes.Count > 0
            For i As Integer = 0 To next_nodes.Count - 1
                current_index = next_nodes.Item(i)
                using_URL.Append("https://maps.googleapis.com/maps/api/directions/json?origin=")
                using_URL.Append(final_route.Item(final_route.Count - 1))
                using_URL.Append("&destination=" & next_nodes.Item(current_index) & "&key=AIzaSyBqN-1pDwR8taEDQESDP5mnJjiJkIXmv-w")

                For count As Integer = 1 To 3
                    'pulls the route data and creates a string with it filled in
                    Dim client As New WebClient()
                    Dim client_Stream As Stream = client.OpenRead(using_URL.ToString)
                    Dim streamreading As New StreamReader(client_Stream)
                    Dim JSON_str As String = streamreading.ReadToEnd()
                    streamreading.Close()
                    client_Stream.Close()

                    'checks if the route is possible or not
                    Dim status_search As String = Chr(34) & "status" & Chr(34)
                    Dim status_index As Integer = JSON_str.IndexOf(status_search)
                    Console.WriteLine(status_search)
                    status_index += 12
                    Dim status As New StringBuilder
                    For i As Integer = status_index To JSON_str.Length
                        If JSON_str.Substring(i, 1) = Chr(34) Then
                            Exit For
                            count += 1
                        Else
                            status.Append(JSON_str.Substring(i, 1))
                        End If
                    Next
                    If status.ToString = "OK" Then
                        'find the distance of the route
                        Dim dist_char As Integer = JSON_str.IndexOf("value")
                        dist_char += 9
                        Dim dist_converter As String = ""
                        For i As Integer = dist_char To JSON_str.Length
                            If JSON_str.Substring(i, 1) = " " Then
                                Exit For
                            Else
                                dist_converter += JSON_str.Substring(i, 1)
                            End If
                        Next
                        comp_dist = CInt(dist_converter)

                    ElseIf status.ToString = "ZERO_RESULTS" Then
                        comp_dist = -1
                        Console.WriteLine("Zero_results")
                        Exit For
                    ElseIf status.ToString = "NOT_FOUND" Then
                        comp_dist = -2
                        Console.WriteLine("not_found")
                        Exit For
                    ElseIf status.ToString = "OVER_QUERY_LIMIT" Then
                        comp_dist = -3
                        Console.WriteLine("Query limit achieved")
                        Exit For
                    ElseIf status.ToString = "MAX_WAYPOINTS_EXCEEDED" Then
                        comp_dist = -4
                        Exit For
                    ElseIf status.ToString = "MAX_ROUTE_LENGTH_EXCEEDED" Then
                        comp_dist = -1
                        Exit For
                    Else
                        comp_dist = -1
                    End If
                Next
                If comp_dist = -1 Then
                    Console.WriteLine("Well that didn't work")
                ElseIf comp_dist = -2 Then
                    Return -2
                    Exit While
                ElseIf comp_dist = -3 Then
                    Return -2
                    Exit While
                ElseIf comp_dist = -4 Then
                    Return -2
                    Exit While
                ElseIf comp_dist < distance Then
                    distance = comp_dist
                    current_index = i
                End If
            Next
            final_route.Add(next_nodes(current_index))
            next_nodes.Remove(current_index)
            length -= 1
        End While
        Current_URL.Append("https://maps.googleapis.com/maps/api/directions/json?origin=")
        Current_URL.Append(final_route.Item(0) & "&destination=")
        If last = True Then
            Current_URL.Append(nodes.Item(nodes.Count - 1))
            For t As Integer = 1 To length - 1
                Current_URL.Append("via:" & final_route.Item((t)) & "|")
            Next
        Else
            Current_URL.Append(final_route.Item(final_route.Count - 1))
            For t As Integer = 1 To length - 2
                Current_URL.Append("via:" & final_route.Item(t) & "|")
            Next
        End If
        Current_URL.Append("&key=AIzaSyBqN-1pDwR8taEDQESDP5mnJjiJkIXmv-w")
        Dim passed(1) As Integer
        For count As Integer = 1 To 3
            Dim client As New WebClient()
            Dim client_Stream As Stream = client.OpenRead(Current_URL.ToString)
            Dim streamreading As New StreamReader(client_Stream)
            Dim JSON_str As String = streamreading.ReadToEnd()
            streamreading.Close()
            client_Stream.Close()

            Dim status_search As String = Chr(34) & "status" & Chr(34)
            Dim status_index As Integer = JSON_str.IndexOf(status_search)
            Console.WriteLine(status_search)
            status_index += 12
            Dim status As New StringBuilder
            For i As Integer = status_index To JSON_str.Length
                If JSON_str.Substring(i, 1) = Chr(34) Then
                    Exit For
                    count += 1
                Else
                    status.Append(JSON_str.Substring(i, 1))
                End If
            Next
            Console.WriteLine(status.ToString)
            Console.ReadLine()
            'test if the status of the route is valid or not
            If status.ToString = "OK" Then
                'find the distance of the route
                Dim dist_char As Integer = JSON_str.IndexOf("value")
                dist_char += 9
                Dim dist_converter As String = ""
                For i As Integer = dist_char To JSON_str.Length
                    If JSON_str.Substring(i, 1) = " " Then
                        Exit For
                    Else
                        dist_converter += JSON_str.Substring(i, 1)
                    End If
                Next
                passed(0) = CInt(dist_converter)


                'finds the time length of the journey
                Dim damaged_JSON As String = Right(JSON_str, JSON_str.Length - dist_char)

                Dim dura_char As Integer

                dura_char = damaged_JSON.IndexOf("value")
                dura_char += 9
                Dim dura_converter As String = ""
                For i As Integer = dura_char To damaged_JSON.Length
                    If damaged_JSON.Substring(i, 1) = " " Then
                        Exit For
                    Else
                        dura_converter += damaged_JSON.Substring(i, 1)
                    End If
                Next
                passed(1) = CInt(dura_converter)

                Return passed
                Exit Function

            ElseIf status.ToString = "ZERO_RESULTS" Then
                passed(0) = 2147483645
                passed(1) = 2147483645
                Console.WriteLine("Zero_results")
                Exit For
            ElseIf status.ToString = "NOT_FOUND" Then
                passed(0) = 2147483645
                passed(1) = -1
                Console.WriteLine("not_found")
                Exit For
            ElseIf status.ToString = "OVER_QUERY_LIMIT" Then
                passed(0) = 2147483645
                passed(1) = -2
                Console.WriteLine("Query limit achieved")
                Exit For
            ElseIf status.ToString = "MAX_WAYPOINTS_EXCEEDED" Then
                passed(0) = 2147483645
                passed(1) = -3
                Exit For
            ElseIf status.ToString = "MAX_ROUTE_LENGTH_EXCEEDED" Then
                passed(0) = 2147483645
                passed(1) = 2147483645
                Exit For
            Else
                passed(0) = 2147483645
                passed(1) = 2147483645
            End If
        Next
        Return passed
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
