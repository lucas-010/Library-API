using System.ComponentModel.DataAnnotations;

namespace Library.Models;

public class Book
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Campo Requerido")]
    [MaxLength(90, ErrorMessage = "Este campo deve conter entre 2 e 90 caracteres")]
    [MinLength(2, ErrorMessage = "Este campo deve conter entre 2 e 90 caracteres")]
    public string? Title { get; set; }
    
    [Required(ErrorMessage = "Campo Obrigatório")]
    [Range(1, int.MaxValue, ErrorMessage = "Autor inválido")]
    public int AuthorId { get; set; }
    public Author? Author { get; set; }

    [Required(ErrorMessage = "Campo Obrigatório")]
    [Range(1, int.MaxValue, ErrorMessage = "Categoria inválida")]
    public int CategoryId { get; set; }
    public Category? Category { get; set; }
}