using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        int opcion = 0;

        do
        {
            try
            {
                Console.Clear();
                Console.WriteLine("===== DESAFÍO 2: PROGRAMACIÓN DE ALGORITMOS =====");
                Console.WriteLine("1. Juego del Ahorcado");
                Console.WriteLine("2. Sistema de Notas");
                Console.WriteLine("3. Salir");
                Console.Write("Seleccione una opción: ");

                if (!int.TryParse(Console.ReadLine(), out opcion))
                {
                    Console.WriteLine("Por favor, ingrese un número válido.");
                    Console.ReadKey();
                    continue;
                }

                switch (opcion)
                {
                    case 1:
                        JuegoAhorcado();
                        break;
                    case 2:
                        SistemaNotas();
                        break;
                    case 3:
                        Console.WriteLine("Saliendo del programa...");
                        break;
                    default:
                        Console.WriteLine("Opción inválida. Intente de nuevo.");
                        break;
                }

                if (opcion != 3)
                {
                    Console.WriteLine("\nPresione cualquier tecla para volver al menú...");
                    Console.ReadKey();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error: " + ex.Message);
                Console.ReadKey();
            }

        } while (opcion != 3);
    }

    // ==========================================
    // EJERCICIO 1: JUEGO DEL AHORCADO
    // ==========================================
    static void JuegoAhorcado()
    {
        string[] palabras = { "CASA", "PERRO", "GATO", "ESCUELA", "PROGRAMA", "TECLADO", "MONITOR", "RATON", "CODIGO", "JUEGO" };
        Random rand = new Random();
        string palabraSecreta = palabras[rand.Next(palabras.Length)];

        char[] estado = new string('_', palabraSecreta.Length).ToCharArray();
        List<char> usadas = new List<char>();

        int intentos = 6;
        bool ganado = false;

        while (intentos > 0 && !ganado)
        {
            Console.Clear();
            Console.WriteLine("=== JUEGO DEL AHORCADO ===");
            Console.WriteLine("\nPalabra a adivinar: " + string.Join(" ", estado));
            Console.WriteLine("Intentos restantes: " + intentos);
            Console.WriteLine("Letras ya usadas: " + string.Join(", ", usadas));
            
            // Dibujito simple para el video
            DibujarMonigote(intentos);

            Console.Write("\nIngrese una letra: ");
            string entrada = Console.ReadLine().ToUpper();
            
            if (string.IsNullOrEmpty(entrada)) continue;
            char letra = entrada[0];

            if (usadas.Contains(letra))
            {
                Console.WriteLine("¡Ya usaste esa letra! Intenta con otra.");
                System.Threading.Thread.Sleep(1000);
                continue;
            }

            usadas.Add(letra);

            if (palabraSecreta.Contains(letra))
            {
                for (int i = 0; i < palabraSecreta.Length; i++)
                {
                    if (palabraSecreta[i] == letra)
                        estado[i] = letra;
                }
            }
            else
            {
                intentos--;
                Console.WriteLine("¡Letra incorrecta!");
                System.Threading.Thread.Sleep(800);
            }

            if (!new string(estado).Contains('_'))
                ganado = true;
        }

        Console.Clear();
        if (ganado)
        {
            Console.WriteLine("¡FELICIDADES! GANASTE.");
            Console.WriteLine("La palabra era: " + palabraSecreta);
        }
        else
        {
            DibujarMonigote(0);
            Console.WriteLine("GAME OVER. PERDISTE.");
            Console.WriteLine("La palabra era: " + palabraSecreta);
        }
    }

    static void DibujarMonigote(int intentos)
    {
        string[] fases = {
            "  +---+\n  |   |\n  O   |\n /|\\  |\n / \\  |\n      |\n=========", // 0 intentos
            "  +---+\n  |   |\n  O   |\n /|\\  |\n /    |\n      |\n=========", // 1
            "  +---+\n  |   |\n  O   |\n /|\\  |\n      |\n      |\n=========", // 2
            "  +---+\n  |   |\n  O   |\n /|   |\n      |\n      |\n=========", // 3
            "  +---+\n  |   |\n  O   |\n  |   |\n      |\n      |\n=========", // 4
            "  +---+\n  |   |\n  O   |\n      |\n      |\n      |\n=========", // 5
            "  +---+\n  |   |\n      |\n      |\n      |\n      |\n========="  // 6
        };
        Console.WriteLine(fases[intentos]);
    }

    // ==========================================
    // EJERCICIO 2: SISTEMA DE NOTAS
    // ==========================================
    static void SistemaNotas()
    {
        Console.Clear();
        Console.WriteLine("=== SISTEMA DE REGISTRO DE NOTAS ===");
        Console.Write("Ingrese la cantidad de estudiantes a registrar: ");
        
        if (!int.TryParse(Console.ReadLine(), out int n) || n <= 0)
        {
            Console.WriteLine("Cantidad no válida.");
            return;
        }

        string[] nombres = new string[n];
        double[] notas = new double[n];

        for (int i = 0; i < n; i++)
        {
            Console.WriteLine($"\nRegistro del estudiante #{i + 1}:");
            Console.Write("Nombre: ");
            nombres[i] = Console.ReadLine();

            double notaValida;
            do
            {
                Console.Write("Nota (0.0 - 10.0): ");
                if (!double.TryParse(Console.ReadLine(), out notaValida) || notaValida < 0 || notaValida > 10)
                {
                    Console.WriteLine("Error: La nota debe estar entre 0 y 10.");
                    notaValida = -1;
                }
            } while (notaValida < 0);

            notas[i] = notaValida;
        }

        // Cálculos
        double suma = 0;
        double notaMax = notas[0];
        double notaMin = notas[0];
        int aprobados = 0;

        Console.WriteLine("\n" + new string('=', 50));
        Console.WriteLine("{0,-20} {1,-10} {2,-10} {3,-10}", "NOMBRE", "NOTA", "LETRA", "ESTADO");
        Console.WriteLine(new string('-', 50));

        for (int i = 0; i < n; i++)
        {
            suma += notas[i];
            if (notas[i] > notaMax) notaMax = notas[i];
            if (notas[i] < notaMin) notaMin = notas[i];

            string estado = (notas[i] >= 6.0) ? "APROBADO" : "REPROBADO";
            if (notas[i] >= 6.0) aprobados++;

            Console.WriteLine("{0,-20} {1,-10} {2,-10} {3,-10}", nombres[i], notas[i], ObtenerCalificacionLetra(notas[i]), estado);
        }

        double promedio = suma / n;

        Console.WriteLine(new string('=', 50));
        Console.WriteLine($"Promedio Grupal: {promedio:F2}");
        Console.WriteLine($"Nota más alta:   {notaMax}");
        Console.WriteLine($"Nota más baja:   {notaMin}");
        Console.WriteLine($"Aprobados:       {aprobados}");
        Console.WriteLine($"Reprobados:      {n - aprobados}");
    }

    static string ObtenerCalificacionLetra(double nota)
    {
        if (nota >= 9.0) return "A";
        if (nota >= 8.0) return "B";
        if (nota >= 7.0) return "C";
        if (nota >= 6.0) return "D";
        return "F";
    }
}
