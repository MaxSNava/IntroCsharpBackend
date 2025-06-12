using System;

var venta = new VentaOnline(100, "https://mitienda.com/orden/123");
Console.WriteLine(venta.ObtenerDetalle());

/// <summary>
/// Clase base que representa una venta general.
/// </summary>
public class Venta
{
    /// <summary>
    /// Total de la venta. Puede ser accedido desde cualquier parte del programa.
    /// </summary>
    public decimal Total { get; set; }

    /// <summary>
    /// Código interno de control. Solo puede usarse dentro de la misma clase.
    /// </summary>
    private string _codigoInterno = Guid.NewGuid().ToString();

    /// <summary>
    /// Fecha de creación de la venta. Accesible por clases derivadas.
    /// </summary>
    protected DateTime FechaCreacion { get; set; }

    /// <summary>
    /// Constructor que inicializa una venta con un total específico.
    /// </summary>
    /// <param name="total">Total de la venta</param>
    public Venta(decimal total)
    {
        if (total < 0)
            throw new ArgumentException("El total no puede ser negativo.", nameof(total));

        Total = total;
        FechaCreacion = DateTime.Now;
    }

    /// <summary>
    /// Devuelve una descripción general de la venta.
    /// </summary>
    public virtual string ObtenerDetalle()
    {
        return $"Total: {Total:C}, Fecha: {FechaCreacion}";
    }
}

/// <summary>
/// Representa una venta hecha en línea, hereda de Venta. Herencia
/// </summary>
public class VentaOnline : Venta
{
    /// <summary>
    /// URL de seguimiento del pedido. Accesible desde cualquier parte.
    /// </summary>
    public string UrlSeguimiento { get; set; }

    /// <summary>
    /// Constructor que inicializa una venta en línea.
    /// </summary>
    /// <param name="total">Total de la venta</param>
    /// <param name="urlSeguimiento">URL para seguimiento</param>
    public VentaOnline(decimal total, string urlSeguimiento)
        : base(total)
    {
        UrlSeguimiento = urlSeguimiento;
    }

    /// <summary>
    /// Sobrescribe el método base para incluir la URL.
    /// </summary>
    public override string ObtenerDetalle()
    {
        return base.ObtenerDetalle() + $", Seguimiento: {UrlSeguimiento}";
    }
}