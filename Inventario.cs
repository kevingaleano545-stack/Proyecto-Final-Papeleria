using System;
using System.IO;

class Inventario
{
    public static int MAX = 100;

    public static string[] nombres = new string[MAX];
    public static double[] precios = new double[MAX];
    public static int[] cantidades = new int[MAX];
    public static int contador = 0;
    public static double totalVendido = 0;

    // Registro de productos 
    public static void RegistrarProducto()
    {
        Console.Clear();
        Console.WriteLine("=== REGISTRAR PRODUCTO ===");
        Console.Write("Nombre: ");
        nombres[contador] = Console.ReadLine();
        Console.Write("Precio: ");
        while (!double.TryParse(Console.ReadLine(), out precios[contador]))
        {
            Console.WriteLine("Ingrese un precio válido:");
        }
        Console.Write("Cantidad: ");
        while (!int.TryParse(Console.ReadLine(), out cantidades[contador]))
        {
            Console.WriteLine("Ingrese una cantidad válida:");
        }
        contador++;
        GuardarProductos();
        Console.WriteLine("Producto registrado.");
    }

    // Mostarr inventario  
    public static void MostrarInventario()
    {
        Console.Clear();
        Console.WriteLine("=== INVENTARIO ===");

        if (contador == 0)
        {
            Console.WriteLine("No hay productos.");
            return;
        }

        for (int i = 0; i < contador; i++)
        {
            Console.WriteLine((i + 1) + ". " + nombres[i] + " | $" + precios[i] + " | Cant: " + cantidades[i]);
        }
    }

    // Registrar ventas
    public static void RegistrarVenta()
    {
        Console.Clear();
        MostrarInventario();

        if (contador == 0)
        {
            Console.WriteLine("No hay productos.");
            return;
        }

        Console.Write("\nSeleccione producto: ");

        int producto;
        if (!int.TryParse(Console.ReadLine(), out producto))
        {
            Console.WriteLine("Entrada inválida.");
            return;
        }
        producto--;

        if (producto < 0 || producto >= contador)
        {
            Console.WriteLine("Producto inválido.");
            return;
        }

        Console.Write("Cantidad a vender: ");
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

        if (cantidad > cantidades[producto])
        {
            Console.WriteLine("Inventario insuficiente.");
            return;
        }

        cantidades[producto] -= cantidad;

        double total = cantidad * precios[producto];

        // ✔ sumar ventas
        totalVendido += total;

        GuardarProductos();
        GuardarVentas();

        Console.WriteLine("\nVenta realizada correctamente.");
        Console.WriteLine("Total: $" + total);
    }

    // Guardar productos
    public static void GuardarProductos()
    {
        StreamWriter archivo = new StreamWriter("productos.txt");

        for (int i = 0; i < contador; i++)
        {
            archivo.WriteLine(nombres[i] + "," + precios[i] + "," + cantidades[i]);
        }

        archivo.Close();
    }

    // Cargar productos
    public static void CargarProductos()
    {
        if (File.Exists("productos.txt"))
        {
            string[] lineas = File.ReadAllLines("productos.txt");

            foreach (string linea in lineas)
            {
                string[] datos = linea.Split(',');

                nombres[contador] = datos[0];
                precios[contador] = double.Parse(datos[1]);
                cantidades[contador] = int.Parse(datos[2]);

                contador++;
            }
        }
    }

    // Guardar Ventas
    public static void GuardarVentas()
    {
        StreamWriter archivo = new StreamWriter("ventas.txt");
        archivo.WriteLine(totalVendido);
        archivo.Close();
    }

    // Cargar Ventas

    public static void CargarVentas()
    {
        if (File.Exists("ventas.txt"))
        {
            totalVendido = double.Parse(File.ReadAllText("ventas.txt"));
        }
    }

    // Mostrar total ventas
    public static void MostrarTotalVentas()
    {
        Console.Clear();
        Console.WriteLine("=== TOTAL VENDIDO ===");
        Console.WriteLine("Total acumulado: $" + totalVendido);
    }
}