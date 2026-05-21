using System;

class Menu
{
    public static void MostrarMenu()
    {
        int opcion;

        do
        {
            Console.Clear();

            Console.WriteLine("================================");
            Console.WriteLine(" PAPELERÍA");
            Console.WriteLine("================================");
            Console.WriteLine("1. Registrar Producto");
            Console.WriteLine("2. Mostrar Inventario");
            Console.WriteLine("3. Registrar Venta");
            Console.WriteLine("4. Registrar Fiado");
            Console.WriteLine("5. Registrar Abono");
            Console.WriteLine("6. Consultar Deuda");
            Console.WriteLine("7. Ver Total Vendido");
            Console.WriteLine("0. Salir");
            Console.WriteLine("================================");

            while (!int.TryParse(Console.ReadLine(), out opcion))
            {
                Console.WriteLine(
                    "Ingrese un número válido:");
            }

            switch (opcion)
            {
                case 1:
                    Inventario.RegistrarProducto();
                    break;

                case 2:
                    Inventario.MostrarInventario();
                    break;

                case 3:
                    Inventario.RegistrarVenta();
                    break;

                case 4:
                    Fiados.RegistrarFiado();
                    break;

                case 5:
                    Fiados.RegistrarAbono();
                    break;

                case 6:
                    Fiados.ConsultarDeuda();
                    break;

                case 7:
                    Inventario.MostrarTotalVentas();
                    break;

                case 0:
                    Console.WriteLine("Saliendo...");
                    break;

                default:
                    Console.WriteLine(
                        "Opción inválida.");
                    break;
            }

            Console.WriteLine(
                "\nPresione una tecla...");
            Console.ReadKey();

        } while (opcion != 0);
    }
}