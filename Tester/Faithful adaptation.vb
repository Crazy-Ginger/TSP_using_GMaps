Module Faithful_adaptation

    Public Sub Faith_Permute(n As Integer, ByRef nodes As List(Of String))
        'Generate, count and print (if printem is not false) all permutations of first n integers

        Dim P() As Integer
        Dim t As Integer, i As Integer, j As Integer, k As Integer
        Dim count As Long
        Dim Last As Boolean



        'Initialize
        ReDim P(n)

        For i = 1 To n
            P(i) = i
        Next

        count = 0
        Last = False

        Do While Not Last
            Print_array(P, n, nodes)

            count = count + 1

            Last = True
            i = n - 1

            Do While i > 0

                If P(i) < P(i + 1) Then

                    Last = False
                    Exit Do

                End If

                i = i - 1
            Loop

            j = i + 1
            k = n

            While j < k
                ' Swap p(j) and p(k)
                t = P(j)
                P(j) = P(k)
                P(k) = t
                j = j + 1
                k = k - 1
            End While

            j = n

            While P(j) > P(i)
                j = j - 1
            End While

            j = j + 1
            'Swap p(i) and p(j)
            t = P(i)
            P(i) = P(j)
            P(j) = t
        Loop 'While not last
        Console.WriteLine("Hi there")
        Console.WriteLine("Number of permutations: " & count)

    End Sub

    Public Sub Print_array(ByRef array() As Integer, ByVal length As Integer, ByRef nodes As List(Of String))
        For t = 1 To length
            Console.Write(array(t) & ": " & nodes.Item(array(t)) & ", ")
        Next
        Console.WriteLine()
    End Sub
End Module
