using System;
using System.IO;

class Fiados
{
    public static int MAX = 100;

    public static string[] clientes = new string[MAX];
    public static double[] deudas = new double[MAX];
    public static int contadorClientes = 0;

    // Fiados

    public static void RegistrarFiado()
    {
        Console.Clear();
        Console.WriteLine("=== REGISTRAR FIADO ===");

        Inventario.MostrarInventario();

        if (Inventario.contador == 0)
        {
            Console.WriteLine("No hay productos.");
            return;
        }

        // Clientes
        // =========================
        Console.Write("Nombre del cliente: ");
        string nombre = Console.ReadLine();

        int indiceCliente = -1;

        for (int i = 0; i < contadorClientes; i++)
        {
            if (clientes[i].ToLower() == nombre.ToLower())
            {
                indiceCliente = i;
                break;
            }
        }

        if (indiceCliente == -1)
        {
            indiceCliente = contadorClientes;
            clientes[indiceCliente] = nombre;
            contadorClientes++;
        }

        // Producto
        Console.Write("Seleccione producto: ");
        int producto;

        if (!int.TryParse(Console.ReadLine(), out producto))
        {
            Console.WriteLine("Entrada inválida.");
            return;
        }
        producto--;

        if (producto < 0 || producto >= Inventario.contador)
        {
            Console.WriteLine("Producto inválido.");
            return;
        }

        Console.Write("Cantidad: ");
        int cantidad;

        if (!int.TryParse(Console.ReadLine(), out cantidad))
        {
            Console.WriteLine("Cantidad inválida.");
            return;
        }

        if (cantidad <= 0)
        {
            Console.WriteLine("Debe ser mayor a 0.");
            return;
        }

        if (cantidad > Inventario.cantidades[producto])
        {
            Console.WriteLine("No hay suficiente inventario.");
            return;
        }

        // Calcular deuda

        double deuda = cantidad * Inventario.precios[producto];
        deudas[indiceCliente] += deuda;

        Inventario.cantidades[producto] -= cantidad;
        Inventario.GuardarProductos();
        GuardarFiados();
        Console.WriteLine("\nFiado registrado correctamente.");
        Console.WriteLine("Deuda agregada: $" + deuda);
    }

    // COnsultar Deuda
    public static void ConsultarDeuda()
    {
        Console.Clear();
        Console.WriteLine("=== CLIENTES CON DEUDA ===");

        if (contadorClientes == 0)
        {
            Console.WriteLine("No hay clientes.");
            return;
        }

        for (int i = 0; i < contadorClientes; i++)
        {
            Console.WriteLine((i + 1) + ". " + clientes[i] + " - $" + deudas[i]);
        }
    }

    // Abonos
    public static void RegistrarAbono()
    {
        Console.Clear();
        Console.WriteLine("=== ABONAR DEUDA ===");
        ConsultarDeuda();

        Console.Write("\nSeleccione cliente: ");
        int indice;

        if (!int.TryParse(Console.ReadLine(), out indice))
        {
            Console.WriteLine("Inválido.");
            return;
        }
        indice--;

        if (indice < 0 || indice >= contadorClientes)
        {
            Console.WriteLine("Cliente inválido.");
            return;
        }

        Console.Write("Valor abono: ");

        double abono;
        if (!double.TryParse(Console.ReadLine(), out abono))
        {
            Console.WriteLine("Inválido.");
            return;
        }

        deudas[indice] -= abono;

        if (deudas[indice] < 0)
            deudas[indice] = 0;

        GuardarFiados();

        Console.WriteLine("Abono registrado.");
    }

    // Guardar Fiados

    public static void GuardarFiados()
    {
        StreamWriter archivo = new StreamWriter("fiados.txt");

        for (int i = 0; i < contadorClientes; i++)
        {
            archivo.WriteLine(clientes[i] + "," + deudas[i]);
        }

        archivo.Close();
    }

    // Cargar Fiados
    public static void CargarFiados()
    {
        if (File.Exists("fiados.txt"))
        {
            string[] lineas = File.ReadAllLines("fiados.txt");

            foreach (string linea in lineas)
            {
                string[] datos = linea.Split(',');

                clientes[contadorClientes] = datos[0];
                deudas[contadorClientes] = double.Parse(datos[1]);

                contadorClientes++;
            }
        }
    }
}