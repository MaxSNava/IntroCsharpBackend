using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

class Program
{
    static List<IVenta> ventas = new List<IVenta>();

    static void Main()
    {
        bool salir = false;

        while (!salir)
        {
            Console.Clear();
            Console.WriteLine("=== Sistema de Ventas ===");
            Console.WriteLine("1. Registrar venta simple");
            Console.WriteLine("2. Registrar venta con impuesto");
            Console.WriteLine("3. Mostrar ventas");
            Console.WriteLine("4. Guardar ventas en archivo (JSON)");
            Console.WriteLine("5. Consultar ventas con LINQ");
            Console.WriteLine("6. Probar Generics personalizados");
            Console.WriteLine("7. Probar funciones lambda y funcionales");
            Console.WriteLine("8. Salir");
            Console.Write("Seleccione una opción: ");
            var opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1": RegistrarVentaSimple(); break;
                case "2": RegistrarVentaConImpuesto(); break;
                case "3": MostrarVentas(); break;
                case "4": GuardarVentasEnArchivo(); break;
                case "5": ConsultarVentasConLINQ(); break;
                case "6": ProbarGenerics(); break;
                case "7": ProbarFunciones(); break;
                case "8": salir = true; break;
                default:
                    Console.WriteLine("Opción inválida. Presione una tecla para continuar...");
                    Console.ReadKey();
                    break;
            }
        }
    }

    static void RegistrarVentaSimple()
    {
        try
        {
            Console.Write("Ingrese el total de la venta: ");
            decimal total = decimal.Parse(Console.ReadLine() ?? "0");
            var venta = new Sale(total);
            ventas.Add(venta);
            Console.WriteLine("Venta registrada con éxito.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        Console.ReadKey();
    }

    static void RegistrarVentaConImpuesto()
    {
        try
        {
            Console.Write("Ingrese el total de la venta: ");
            decimal total = decimal.Parse(Console.ReadLine() ?? "0");
            Console.Write("Ingrese el impuesto: ");
            decimal tax = decimal.Parse(Console.ReadLine() ?? "0");
            var venta = new SaleWithTax(total, tax);
            ventas.Add(venta);
            Console.WriteLine("Venta con impuesto registrada con éxito.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        Console.ReadKey();
    }

    static void MostrarVentas()
    {
        if (ventas.Count == 0)
            Console.WriteLine("No hay ventas registradas.");
        else
            ventas.ForEach(v => Console.WriteLine(v.GetInfo()));

        Console.ReadKey();
    }

    static void GuardarVentasEnArchivo()
    {
        var opciones = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(ventas, opciones);
        File.WriteAllText("ventas.json", json);
        Console.WriteLine("Ventas guardadas en ventas.json.");
        Console.ReadKey();
    }

    static void ConsultarVentasConLINQ()
    {
        var mayoresImpuestos = ventas.OfType<SaleWithTax>()
            .Where(v => v.Tax > 0)
            .OrderByDescending(v => v.Tax);

        foreach (var venta in mayoresImpuestos)
        {
            Console.WriteLine(venta.GetInfo());
        }
        Console.ReadKey();
    }

    static void ProbarGenerics()
    {
        var lista = new MyList<string>(3);
        lista.Add("Uno");
        lista.Add("Dos");
        lista.Add("Tres");
        lista.Add("Cuatro");
        Console.WriteLine(lista.GetContent());
        Console.ReadKey();
    }

    static void ProbarFunciones()
    {
        Func<decimal, decimal, decimal> calcularTotalConIVA = (total, iva) => total + (total * iva);
        decimal resultado = calcularTotalConIVA(100, 0.16m);
        Console.WriteLine($"Total con IVA: {resultado:C}");

        Action<string> log = msg => Console.WriteLine($"[LOG] {msg}");
        log("Mensaje funcional ejecutado correctamente");

        Predicate<IVenta> filtro = venta => venta.Total > 100;
        var filtradas = ventas.FindAll(filtro);
        Console.WriteLine("Ventas con total > 100:");
        filtradas.ForEach(v => Console.WriteLine(v.GetInfo()));

        Console.ReadKey();
    }
}

interface IVenta
{
    decimal Total { get; set; }
    string GetInfo();
}

class Sale : IVenta
{
    public decimal Total { get; set; }
    public Sale(decimal total)
    {
        if (total < 0) throw new ArgumentException("El total no puede ser negativo.");
        Total = total;
    }
    public virtual string GetInfo() => $"Venta simple: Total = {Total:C}";
}

class SaleWithTax : Sale
{
    public decimal Tax { get; set; }
    public SaleWithTax(decimal total, decimal tax) : base(total)
    {
        if (tax < 0) throw new ArgumentException("El impuesto no puede ser negativo.");
        Tax = tax;
    }
    public override string GetInfo() => $"Venta con impuesto: Total = {Total:C}, Impuesto = {Tax:C}";
    public string GetInfoWithTax() => $"Total + Impuesto: {Total + Tax:C}";
}

public class MyList<T>
{
    private List<T> _list;
    private int _limit;

    public MyList(int limit)
    {
        _limit = limit;
        _list = new List<T>();
    }

    public void Add(T element)
    {
        if (_list.Count < _limit)
        {
            _list.Add(element);
        }
    }

    public string GetContent()
    {
        string content = string.Empty;
        foreach (var element in _list)
        {
            content += element.ToString() + "\n";
        }
        return content;
    }
}
