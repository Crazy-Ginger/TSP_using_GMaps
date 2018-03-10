Module Faithful_adaptation

    Public Sub Faith_Permute(ByVal n As Integer, ByRef nodes As List(Of String), ByVal End_dest As Boolean)
        'Generate, count and print (if printem is not false) all permutations of first n integers
        Dim P(n - 1) As Integer
        Dim t As Integer, i As Integer, j As Integer, k As Integer
        Dim count As Long
        Dim Last As Boolean

        For i = 0 To n - 1
            P(i) = i
        Next

        If End_dest = True Then
            n -= 1
        End If

        count = 0
        Last = False
        Dim watch As Stopwatch = Stopwatch.StartNew()

        Do While Not Last
            Print_array(P, n, nodes, End_dest)

            count = count + 1

            Last = True
            i = n - 2

            Do While i > 0

                If P(i) < P(i + 1) Then

                    Last = False
                    Exit Do

                End If

                i = i - 1
            Loop

            j = i + 1
            k = n - 1

            While j < k
                ' Swap p(j) and p(k)
                t = P(j)
                P(j) = P(k)
                P(k) = t
                j = j + 1
                k = k - 1
            End While

            j = n - 1

            While P(j) > P(i)
                j = j - 1
            End While

            j = j + 1
            'Swap p(i) and p(j)
            t = P(i)
            P(i) = P(j)
            P(j) = t
        Loop 'While not last
        watch.Stop()
        Console.WriteLine("Number of permutations: " & count & vbTab & "That was: " & watch.Elapsed.TotalMilliseconds & "ms, " & watch.ElapsedTicks & " ticks")

    End Sub

    Public Sub Print_array(ByRef array() As Integer, ByVal length As Integer, ByRef nodes As List(Of String), ByVal end_dest As Boolean)
        If end_dest = True Then
            length += 1
        End If
        For t = 0 To length - 1
            Console.Write(array(t) & ": " & nodes.Item(array(t)) & ", ")
        Next
        Console.WriteLine()
    End Sub
End Module
