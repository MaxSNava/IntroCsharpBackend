/// <summary>
/// Punto de entrada principal del programa.
/// </summary>
var sale = new SaleWithTax(15, 5);
var message = sale.GetInfo();
var taxMessage = sale.GetInfoWithTax();

Console.WriteLine(message);
Console.WriteLine(taxMessage);

/// <summary>
/// Representa una venta genérica.
/// </summary>
class Sale
{
    /// <summary>
    /// Total de la venta.
    /// </summary>
    public decimal Total { get; set; }

    /// <summary>
    /// Inicializa una nueva instancia de la clase Sale.
    /// </summary>
    /// <param name="total">Total de la venta</param>
    /// <exception cref="ArgumentException">Lanzada si el total es negativo</exception>
    public Sale(decimal total)
    {
        if (total < 0)
            throw new ArgumentException("El total no puede ser negativo.", nameof(total));

        Total = total;
    }

    /// <summary>
    /// Obtiene información básica de la venta.
    /// </summary>
    /// <returns>Texto con el total de la venta</returns>
    public virtual string GetInfo()
    {
        return $"Total sale: {Total:C}";
    }

    /// <summary>
    /// Sobrecarga que incluye un descuento.
    /// </summary>
    /// <param name="discount">Valor del descuento</param>
    /// <returns>Texto con el total y el descuento aplicado</returns>
    public string GetInfo(decimal discount)
    {
        return $"Total sale: {Total:C} with discount: {discount:C}";
    }
}

/// <summary>
/// Representa una venta con impuesto, que hereda de Sale.
/// </summary>
class SaleWithTax : Sale
{
    /// <summary>
    /// Impuesto aplicado a la venta.
    /// </summary>
    public decimal Tax { get; set; }

    /// <summary>
    /// Inicializa una nueva instancia de la clase SaleWithTax.
    /// </summary>
    /// <param name="total">Total de la venta</param>
    /// <param name="tax">Impuesto aplicado</param>
    /// <exception cref="ArgumentException">Lanzada si el impuesto es negativo</exception>
    public SaleWithTax(decimal total, decimal tax) : base(total)
    {
        if (tax < 0)
            throw new ArgumentException("El impuesto no puede ser negativo.", nameof(tax));

        Tax = tax;
    }

    /// <summary>
    /// Método adicional que muestra el total con impuesto.
    /// </summary>
    /// <returns>Texto con el total e impuesto</returns>
    public string GetInfoWithTax()
    {
        return $"El total de la venta es: {Total:C} y el impuesto es: {Tax:C}";
    }

    /// <summary>
    /// Sobrescribe el método base para incluir el impuesto.
    /// </summary>
    /// <returns>Texto con total e impuesto</returns>
    public override string GetInfo()
    {
        return $"Total sale: {Total:C} with tax: {Tax:C}";
    }
}