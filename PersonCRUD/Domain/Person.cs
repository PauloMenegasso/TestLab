using Dapper;

namespace PersonCRUD.Domain;

[Table("Person")]
public class Person
{
    [Key]
    [ReadOnly(true)]
    public int PersonId { get; set; }
    [System.ComponentModel.DataAnnotations.Required]
    public string? Name { get; set; }
    [System.ComponentModel.DataAnnotations.Required]
    public int Age { get; set; }
    [System.ComponentModel.DataAnnotations.Required]
    public string? Email { get; set; }
}
