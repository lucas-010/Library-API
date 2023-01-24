using System.ComponentModel.DataAnnotations;

namespace Library.Models;

public class Author
{
    [Key]
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Campo Requerido")]
    [MaxLength(60, ErrorMessage = "Este campo deve conter entre 3 e 80 caracteres")]
    [MinLength(3, ErrorMessage = "Este campo deve conter entre 3 e 80 caracteres")]
    public string? Name { get; set; }
}