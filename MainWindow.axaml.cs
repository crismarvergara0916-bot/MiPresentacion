using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;

namespace MiPresentacion;

public partial class MainWindow : Window
{
    private const string MensajeInicial =
        "Completa el formulario y presiona «Presentarme».";

    // Reto adicional: alterna entre dos mensajes de presentación.
    private bool _saludoAlterno = false;

    public MainWindow()
    {
        InitializeComponent();
        TxtResultado.Text = MensajeInicial;
    }

    private void BtnPresentarme_Click(object? sender, RoutedEventArgs e)
    {
        Presentar();
    }

    private void BtnLimpiar_Click(object? sender, RoutedEventArgs e)
    {
        TxtNombre.Text = string.Empty;
        TxtApellido.Text = string.Empty;
        TxtEdad.Text = string.Empty;

        TxtResultado.ClearValue(TextBlock.ForegroundProperty);
        TxtResultado.Text = MensajeInicial;

        TxtNombre.Focus();
    }

    private void BtnCambiarSaludo_Click(object? sender, RoutedEventArgs e)
    {
        _saludoAlterno = !_saludoAlterno;

        // Si el formulario ya es válido, se muestra la presentación con el nuevo saludo.
        if (!Presentar())
        {
            // Si no, se avisa qué saludo quedó activo (se mantiene la advertencia visible).
            string modo = _saludoAlterno ? "formal" : "informal";
            TxtResultado.Text += $"\n\n(Saludo cambiado a modo {modo}.)";
        }
    }

    /// <summary>
    /// Valida los campos y muestra la presentación o una advertencia.
    /// Devuelve true si los datos eran válidos.
    /// </summary>
    private bool Presentar()
    {
        string nombre = (TxtNombre.Text ?? string.Empty).Trim();
        string apellido = (TxtApellido.Text ?? string.Empty).Trim();
        string textoEdad = (TxtEdad.Text ?? string.Empty).Trim();

        if (string.IsNullOrWhiteSpace(nombre))
        {
            MostrarAdvertencia("⚠ El nombre no puede estar vacío.");
            return false;
        }

        if (string.IsNullOrWhiteSpace(apellido))
        {
            MostrarAdvertencia("⚠ El apellido no puede estar vacío.");
            return false;
        }

        if (!int.TryParse(textoEdad, out int edad))
        {
            MostrarAdvertencia("⚠ La edad debe ser un número entero.");
            return false;
        }

        if (edad <= 0)
        {
            MostrarAdvertencia("⚠ La edad debe ser un número entero positivo (mayor que 0).");
            return false;
        }

        string mensaje = _saludoAlterno
            ? $"Buen día. Me llamo {nombre} {apellido}, tengo {edad} años\n" +
              "y me interesa crear aplicaciones de escritorio con Avalonia UI."
            : $"¡Hola! Mi nombre es {nombre} {apellido},\n" +
              $"tengo {edad} años y estoy aprendiendo\n" +
              "a desarrollar interfaces gráficas con C#.";

        TxtResultado.Foreground = Brushes.SeaGreen;
        TxtResultado.Text = mensaje;
        return true;
    }

    private void MostrarAdvertencia(string texto)
    {
        TxtResultado.Foreground = Brushes.Firebrick;
        TxtResultado.Text = texto;
    }
}
