Module Faithful_adaptation

    Public Sub Faith_Permute(ByVal length As Integer, ByRef nodes As List(Of String), ByVal End_dest As Boolean)
        'Generate, count and print (if printem is not false) all permutations of first n integers
        Dim P(length - 1) As Integer
        Dim swapper As Integer
        Dim initial_comp As Integer
        Dim rearrange As Integer
        Dim asc_swapper As Integer
        Dim count As Long
        Dim Last As Boolean

        For i = 0 To length - 1
            P(i) = i
        Next

        If End_dest = True Then
            length -= 1
        End If

        count = 0
        Last = False
        Dim watch As Stopwatch = Stopwatch.StartNew()

        Do While Not Last
            Print_array(P, length, nodes, End_dest)

            count = count + 1

            Last = True
            initial_comp = length - 2

            Do While initial_comp > 0
                If P(initial_comp) < P(initial_comp + 1) Then
                    Last = False
                    Exit Do
                End If
                initial_comp = initial_comp - 1
            Loop

            rearrange = initial_comp + 1
            asc_swapper = length - 1

            While rearrange < asc_swapper
                ' Swap p(j) and p(k)
                swapper = P(rearrange)
                P(rearrange) = P(asc_swapper)
                P(asc_swapper) = swapper
                rearrange += 1
                asc_swapper -= 1
            End While

            rearrange = length - 1

            While P(rearrange) > P(initial_comp)
                rearrange = rearrange - 1
            End While

            rearrange = rearrange + 1

            'makes the swap to place the large number in front
            swapper = P(initial_comp)
            P(initial_comp) = P(rearrange)
            P(rearrange) = swapper
        Loop 'While not last
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
        Waypointing(array, length, nodes)
    End Sub
End Module
