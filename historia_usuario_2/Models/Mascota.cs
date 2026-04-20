namespace historia_usuario_2.Models;

public class Mascota
{
    public int Id { get; set; }
    public int IdDueno { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Especie { get; set; } = string.Empty;
    public string Raza { get; set; } = string.Empty;
    public int Edad { get; set; }
}
