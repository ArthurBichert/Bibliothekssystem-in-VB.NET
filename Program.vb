Imports System
Imports System.IO
'Bibliothekssystem in VB.NET von Arthur Bichert
Module LibraryApp


    ' Datenstrukturen


    ' Definieren der Variablen
    Structure Book
        Dim isbn As String
        Dim title As String
        Dim author As String
        Dim status As String
        Dim borrowedBy As String
    End Structure

    ' Struktur für Benutzer
    Structure User
        Dim userID As String
        Dim name As String
    End Structure


    ' Arrays zur Speicherung der Daten
    Dim books(999) As Book
    Dim users(999) As User

    Dim bookCount As Integer = 0
    Dim userCount As Integer = 0


    Sub Main()

        ' CSV Dateien laden
        LoadBooks("library_books.csv")
        LoadUsers("library_users.csv")

        Dim running As Boolean = True

        ' Hauptmenü Schleife
        While running

            Console.WriteLine()
            Console.WriteLine("===== Bibliothekssystem =====")
            Console.WriteLine("1 - Alle Bücher anzeigen")
            Console.WriteLine("2 - Alle Benutzer anzeigen")
            Console.WriteLine("3 - Neuen Benutzer anlegen")
            Console.WriteLine("4 - Buch ausleihen")
            Console.WriteLine("5 - Buch zurückgeben")
            Console.WriteLine("6 - Ausgeliehene Bücher eines Benutzers")
            Console.WriteLine("0 - Programm beenden")
            Console.Write("Auswahl: ")

            Dim input As String = Console.ReadLine()

            Select Case input

                Case "1"
                    ShowAllBooks()

                Case "2"
                    ShowAllUsers()

                Case "3"
                    CreateUser()

                Case "4"
                    BorrowBook()

                Case "5"
                    ReturnBook()

                Case "6"
                    ShowBorrowedBooks()

                Case "0"
                    running = False

                Case Else
                    Console.WriteLine("Ungültige Eingabe.")

            End Select

        End While

    End Sub



    ' ----------------------------------------------------------
    ' Liest Bücher aus einer CSV Datei ein und speichert diese im Array.
    '<param name="filePath">Pfad zur CSV Datei</param>
    'Format der CSV: ISBN;Titel;Autor;Status

    Sub LoadBooks(filePath As String)

        If File.Exists(filePath) Then

            Dim lines() As String = File.ReadAllLines(filePath)

            For Each line In lines

                Dim parts() As String = line.Split(","c)

                books(bookCount).isbn = parts(0)
                books(bookCount).title = parts(1)
                books(bookCount).author = parts(2)
                books(bookCount).status = parts(3)
                books(bookCount).borrowedBy = ""

                bookCount += 1

            Next

        Else
            Console.WriteLine("Buchdatei nicht gefunden.")
        End If

    End Sub



    ' ----------------------------------------------------------
    'Liest Benutzer aus einer CSV Datei ein.
    ' <param name="filePath">Pfad zur CSV Datei</param>
    Sub LoadUsers(filePath As String)

        If File.Exists(filePath) Then

            Dim lines() As String = File.ReadAllLines(filePath)

            For Each line In lines

                Dim parts() As String = line.Split(","c)

                users(userCount).userID = parts(0)
                users(userCount).name = parts(1)

                userCount += 1

            Next

        Else
            Console.WriteLine("Benutzerdatei nicht gefunden.")
        End If

    End Sub



    ' ----------------------------------------------------------
    ' Gibt alle Bücher aus der Bibliothek aus.

    Sub ShowAllBooks()

        Console.WriteLine()
        Console.WriteLine("===== Bücher =====")

        For i = 0 To bookCount - 1

            Console.WriteLine(books(i).isbn & " | " &
                              books(i).title & " | " &
                              books(i).author & " | " &
                              books(i).status)

        Next

    End Sub



    ' ----------------------------------------------------------
    ' Gibt alle registrierten Benutzer aus.

    Sub ShowAllUsers()

        Console.WriteLine()
        Console.WriteLine("===== Benutzer =====")

        For i = 0 To userCount - 1

            Console.WriteLine(users(i).userID & " | " &
                              users(i).name)

        Next

    End Sub



    ' ----------------------------------------------------------
    'Erstellt einen neuen Benutzer anhand einer Konsoleneingabe.

    Sub CreateUser()

        If userCount >= 999 Then
            Console.WriteLine("Maximale Benutzeranzahl erreicht.")
            Return
        End If

        Console.Write("Name des neuen Benutzers: ")
        Dim name As String = Console.ReadLine()

        Dim newID As String = "U" & (userCount + 1).ToString("000")

        users(userCount).userID = newID
        users(userCount).name = name

        userCount += 1

        Console.WriteLine("Benutzer erstellt mit ID: " & newID)

    End Sub



    ' ----------------------------------------------------------
    ' Leiht ein Buch aus, wenn es verfügbar ist.

    Sub BorrowBook()

        Console.Write("Benutzer-ID: ")
        Dim userID As String = Console.ReadLine()

        Console.Write("ISBN des Buches: ")
        Dim isbn As String = Console.ReadLine()

        For i = 0 To bookCount - 1

            If books(i).isbn = isbn Then

                If books(i).status = "verfügbar" Then

                    books(i).status = "ausgeliehen"
                    books(i).borrowedBy = userID

                    Console.WriteLine("Buch erfolgreich ausgeliehen.")
                    Return

                Else
                    Console.WriteLine("Buch ist bereits ausgeliehen.")
                    Return
                End If

            End If

        Next

        Console.WriteLine("Buch nicht gefunden.")

    End Sub



    ' ----------------------------------------------------------
    ' Gibt ein ausgeliehenes Buch zurück.

    Sub ReturnBook()

        Console.Write("ISBN des Buches: ")
        Dim isbn As String = Console.ReadLine()

        For i = 0 To bookCount - 1

            If books(i).isbn = isbn Then

                If books(i).status = "ausgeliehen" Then

                    books(i).status = "verfügbar"
                    books(i).borrowedBy = ""

                    Console.WriteLine("Buch erfolgreich zurückgegeben.")
                    Return

                Else
                    Console.WriteLine("Buch ist nicht ausgeliehen.")
                    Return
                End If

            End If

        Next

        Console.WriteLine("Buch nicht gefunden.")

    End Sub



    ''' ----------------------------------------------------------
    ' Zeigt alle ausgeliehenen Bücher eines bestimmten Benutzers.

    Sub ShowBorrowedBooks()

        Console.Write("Benutzer-ID: ")
        Dim userID As String = Console.ReadLine()

        Console.WriteLine("Ausgeliehene Bücher:")

        For i = 0 To bookCount - 1

            If books(i).borrowedBy = userID Then

                Console.WriteLine(books(i).title & " (" & books(i).isbn & ")")

            End If

        Next

    End Sub

End Module