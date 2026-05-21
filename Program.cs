using System;

class Program
{
    static void Main(string[] args)
    {
        Inventario.CargarProductos();
        Inventario.CargarVentas();
        Fiados.CargarFiados();
        Menu.MostrarMenu();
    }
}