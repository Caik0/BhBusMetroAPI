using System.ComponentModel.DataAnnotations;


namespace BhBusMetropApi.Models;

public class Onibus
{
    [Key]
    public int Id { get; set; }
    public string Nome { get; set; }
    public int Numero { get; set; }
    public string Cor { get; set; }
    public string Tipo { get; set; }
    public double Peso { get; set; }

}
