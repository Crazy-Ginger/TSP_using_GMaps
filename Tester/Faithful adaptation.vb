Module Faithful_adaptation

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
            Print_array(P, length, nodes, End_dest)
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

    Public Sub Print_array(ByRef array() As Integer, ByVal length As Integer, ByRef nodes As List(Of String), ByVal end_dest As Boolean)
        If end_dest = True Then
            length += 1
        End If
        For t As Integer = 0 To length - 1
            Console.Write(array(t) & ": " & nodes.Item(array(t)) & ", ")
        Next
        Console.WriteLine()
        Waypointing(array, length, nodes)   'sends this to the function that should return the length/duration of the route
    End Sub
End Module
