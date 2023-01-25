using Library.Data;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library.Controllers;

[ApiController]
[Route("books")]
public class BookController : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<Book>>> Get(DataContext context)
    {
        List<Book> books = await context.Books.
        Include(x => x.Category).
        Include(x => x.Author).
        AsTracking().
        ToListAsync();
        return Ok(books);
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<ActionResult<Book>> GetById(DataContext context, int id)
    {
        var book = await context.Books.
        Include(x => x.Category).
        Include(x => x.Author).
        AsNoTracking().
        FirstOrDefaultAsync(x => x.Id == id);
        return Ok(book);
    }

    [HttpGet]
    [Route("category/{id:int}")]
    public async Task<ActionResult<List<Book>>> GetByCategory(DataContext context, int id)
    {
        List<Book> books = await context.Books.
        Include(x => x.Category).
        Include(x => x.Author).
        AsNoTracking().
        Where(x => x.CategoryId == id).
        ToListAsync();
        return Ok(books);
    }

    [HttpGet]
    [Route("author/{id:int}")]
    public async Task<ActionResult<List<Book>>> GetByAuthor(DataContext context, int id)
    {
        List<Book> books = await context.Books.
        Include(x => x.Category).
        Include(x => x.Author).
        AsNoTracking().
        Where(x => x.AuthorId == id).
        ToListAsync();
        return Ok(books);
    }

    [HttpPost]
    public async Task<ActionResult> Post(DataContext context, Book model)
    {
        try
        {
            context.Add(model);
            await context.SaveChangesAsync();
            return Ok(new { message = "Livro criado com sucesso"});
        }
        catch
        {
            return BadRequest(new { message = "Não foi possível criar o livro"});
        }
    }

    [HttpPut]
    [Route("{id:int}")]
    public async Task<ActionResult> Put(DataContext context, Book model, int id)
    {
        if(model.Id != id) return BadRequest(new { message = "Livro não encontrado"});
        try
        {
            context.Entry<Book>(model).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return Ok(new { message = "Livro atualizado com sucesso" });
        }
        catch
        {
            return BadRequest(new { message = "Não foi possível atualizar o livro"});
        }
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<ActionResult> Delete(DataContext context, int id)
    {
        var book = await context.Books.FirstOrDefaultAsync(x => x.Id == id);
        if(book == null) return NotFound("Livro não encontrado");
        try
        {
            context.Remove(book);
            await context.SaveChangesAsync();
            return Ok(new { message = "Livro removido com sucesso"});
        }
        catch
        {
            return BadRequest(new { message = "Não foi possível remover o livro"});
        }
    }
}