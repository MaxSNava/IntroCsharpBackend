
var sale = new Sale(5);
var message = sale.GetInfo();

Console.WriteLine(message);

public class Sale
{
    // Las propiedades se escriben en PascalCase 
    public decimal Total { get; set; }

    public Sale(decimal total)
    {
        if (total < 0)
            throw new ArgumentException("El total no puede ser negativo.", nameof(total));

        Total = total;
    }

    public string GetInfo()
    {
        // Las variables locales se escriben en camelCase
        var info = $"Total de la venta: {Total}";
        return info;
    }
}
