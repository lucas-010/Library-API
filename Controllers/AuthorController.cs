using Microsoft.AspNetCore.Mvc;
using Library.Data;
using Library.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Controllers;

[ApiController]
[Route("authors")]
public class AuthorController : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<Author>>> Get(DataContext context)
    {
        List<Author> authors = await context.Authors.AsNoTracking().ToListAsync();
        return Ok(authors);
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<ActionResult<Author>> GetById(DataContext context, int id)
    {
        var author = await context.Authors.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        return Ok(author);
    }

    [HttpPost]
    public async Task<ActionResult<Author>> Post(Author model, DataContext context)
    {
        try
        {
            context.Add(model);
            await context.SaveChangesAsync();
            return Ok(model);
        }
        catch
        {
            return BadRequest(new { message = "Não foi possível criar o autor"});
        }
    }

    [HttpPut]
    [Route("{id:int}")]
    public async Task<ActionResult<Author>> Put(Author model, DataContext context, int id)
    {
        if(id != model.Id) return NotFound(new { message ="Autor não encontrado" });
        try
        {
            context.Entry<Author>(model).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return Ok(model);
        }
        catch
        {
            return BadRequest(new { message = "Não foi possível atualizar o autor"});
        }
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<ActionResult<Author>> Delete(DataContext context, int id)
    {
        var author = await context.Authors.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        if(author == null) return NotFound(new { message = "Autor não encontrado"});
        try
        {
            context.Authors.Remove(author);
            await context.SaveChangesAsync();
            return Ok(new { message = "Autor removido"});
        }
        catch
        {
            return BadRequest(new { message = "Não foi possível remover o autor"});
        }
    }

}