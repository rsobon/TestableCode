using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Example6.Enums;

namespace Example6.Model;

[Table("Pokemon")]
public class Pokemon
{
    [Key] 
    [Column("ObjectId")] 
    public Guid ObjectId { get; set; }

    [Column("Id")]
    public int Id { get; set; }

    [Column("Name")]
    public string Name { get; set; }

    [Column("Type")]
    public PokemonType Type { get; set; }
    
    [Column("Timestamp")]
    public DateTime? Timestamp { get; set; }
}