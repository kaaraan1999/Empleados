
Imports System
Imports System.IO
Imports System.Text


Module Program
    'Objeto de departamento
    Public Structure departamento
        Public IDDEP As Integer
        Public NDEP As String
    End Structure

    'Objeto de Trabajador
    Public Structure trabajador
        Public ID As Integer
        Public Nombre As String
        Public Pass As String
        Public DPTO As Integer
        Public Edad As Integer
        Public ATrabajados As Integer
    End Structure

    'Creacion de trabajadores y departamentos
    Public d1 As New departamento With {
    .IDDEP = 1,
    .NDEP = "Departamento 1"
    }
    Public d2 As New departamento With {
    .IDDEP = 2,
    .NDEP = "Departamento 2"
    }
    Public d3 As New departamento With {
    .IDDEP = 3,
    .NDEP = "Departamento 3"
    }

    Public tr1 As New trabajador With {
        .ID = 1,
        .Nombre = "Rebeca Garcia",
        .Pass = "RGarcia",
        .DPTO = 1,
        .Edad = 43,
        .ATrabajados = 6
    }
    Public tr2 As New trabajador With {
        .ID = 2,
        .Nombre = "Aitor Santos",
        .Pass = "ASantos",
        .DPTO = 2,
        .Edad = 56,
        .ATrabajados = 3
    }
    Public tr3 As New trabajador With {
        .ID = 3,
        .Nombre = "Manu Vazquez",
        .Pass = "MVazquez",
        .DPTO = 3,
        .Edad = 67,
        .ATrabajados = 10
    }

    'Creacion de las listas
    Public LDepartamento As New List(Of departamento) From {
    d1,
    d2,
    d3}

    Public LTrabajador As New List(Of trabajador) From {
    tr1,
    tr2,
    tr3}

    'Funcion que calcula el salario segun la edad del trabajador
    Public Function CalculoSalario(Edad As Integer) As Double
        Dim base As Integer = 1500
        Dim salario As Double = 0
        If Edad >= 18 AndAlso Edad <= 50 Then
            salario = base + (base * 0.05)
        End If
        If Edad >= 51 AndAlso Edad <= 60 Then
            salario = base + (base * 0.1)
        End If
        If Edad > 60 Then
            salario = base + (base * 0.15)
        End If
        Return salario
    End Function

    'Calcula los dias de vacaciones en base al tiempo que lleva trabajando y al departamento que pertenezca
    Public Function CalculoVacaciones(anyos As Integer, dep As Integer) As Integer
        Dim dias As Integer = 0
        If (dep = 1) Then
            If anyos >= 2 AndAlso anyos <= 6 Then
                dias = 15
            End If
            If anyos >= 7 Then
                dias = 20
            End If
        End If
        If (dep = 2) Then
            If anyos >= 2 AndAlso anyos <= 6 Then
                dias = 15
            End If
            If anyos >= 7 Then
                dias = 25
            End If
        End If
        If (dep = 3) Then
            If anyos >= 2 AndAlso anyos <= 6 Then
                dias = 15
            End If
            If anyos >= 7 Then
                dias = 30
            End If
        End If
        Return dias
    End Function

    'Programa principal
    Sub Main(args As String())
        Dim nombre As String = ""
        Dim pass As String = ""
        Dim ruta As String
        Dim vacaciones As Integer = 0
        Dim departamento As Integer = 0
        Dim anyosTrabajados As Integer = 0
        Dim anyos As Integer = 0
        Dim ID As Integer = 4
        Dim salario As Double = 0
        Dim encontrado As Boolean = False
        Console.WriteLine("Introduzca la ruta donde se generará la lista de empleados")
        ruta = Console.ReadLine()
        Dim fs As FileStream = File.Create(ruta)
        fs.Close()
        FileOpen(1, ruta, OpenMode.Output)

        'Mientras el imput sea diferente a 0 el programa sigue en ejecucion
        While nombre <> "0"
            Console.WriteLine("Introduzca el nombre del trabajador")
            nombre = ""
            nombre = Console.ReadLine()
            If nombre <> "0" Then
                'Busca el trabajador mediante el nombre que hemos introducido previamente
                For Each item As trabajador In LTrabajador
                    If nombre.Equals(item.Nombre) Then
                        encontrado = True
                        Console.WriteLine("Introduzca la contraseña del trabajador")
                        pass = ""
                        pass = Console.ReadLine()
                        'Comprueba si la pass que hemos introducido es la misma que la que tiene el usuario, si coincide y es mayor de edad hace los calculos y los pone en el archivo txt
                        If pass.Equals(item.Pass) Then
                            'Si tiene menos de 18 años no calcula nada
                            If item.Edad < 18 Then
                                Console.WriteLine("Esta persona es menor de edad, no puede trabajar")
                            Else
                                Console.WriteLine("El calculo se ha procesado correctamente")
                                salario = CalculoSalario(item.Edad)
                                vacaciones = CalculoVacaciones(item.ATrabajados, item.DPTO)
                                PrintLine(1, "ID del trabajador: " + item.ID.ToString)
                                PrintLine(1, "Nombre del trabajador: " + item.Nombre)
                                PrintLine(1, "Contraseña del trabajador: " + item.Pass.ToString)
                                PrintLine(1, "Departamento del trabajador: " + item.DPTO.ToString)
                                PrintLine(1, "Edad del trabajador: " + item.Edad.ToString)
                                PrintLine(1, "Años trabajados del trabajador: " + item.ATrabajados.ToString)
                                PrintLine(1, "Salario del trabajador: " + salario.ToString)
                                PrintLine(1, "Años trabajados del trabajador: " + vacaciones.ToString)
                                PrintLine(1, "")
                            End If
                            'Si la contraseña es incorrecta lo notifica por pantalla
                        Else
                            Console.WriteLine("La contraseña es incorrecta.")
                        End If
                    End If
                Next
                'Tras introducir el nombre si no ha encontrado a la persona porque no existe, la crea y pide los datos
                If encontrado = False Then
                    Console.WriteLine("Error, no hay ningún usuario con ese nombre.")
                    Console.WriteLine("Introduzca el nombre del nuevo trabajador")
                    nombre = ""
                    nombre = Console.ReadLine()
                    Console.WriteLine("Introduzca la contraseña para " + nombre)
                    pass = ""
                    pass = Console.ReadLine()
                    Console.WriteLine("Introduzca el departamento donde trabaja " + nombre)
                    departamento = Console.ReadLine()
                    Console.WriteLine("Introduzca la edad de " + nombre)
                    anyos = Console.ReadLine()
                    Console.WriteLine("Introduzca los años que " + nombre + " lleva trabajando")
                    anyosTrabajados = Console.ReadLine()
                    Dim nuevoTrabajador As New trabajador With {
                    .ID = ID,
                    .Nombre = nombre,
                    .Pass = pass,
                    .DPTO = departamento,
                    .Edad = anyos,
                    .ATrabajados = anyosTrabajados
                    }
                    LTrabajador.Add(nuevoTrabajador)
                    ID += 1
                End If
                encontrado = False
            End If
        End While
        Console.WriteLine("Apagando sistema...")
    End Sub
End Module
