using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NoticiasAPI.Model;
using NoticiasAPI.Service;

namespace NoticiasAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class NoticiasController : ControllerBase
{
    private readonly INoticiaService _noticiaService;

    public NoticiasController(INoticiaService noticiaService)
    {
        _noticiaService = noticiaService;
    }

    [HttpPost]
    public IActionResult Create([FromBody] Noticia noticia)
    {
        if (noticia == null)
        {
            return BadRequest("Dados inválidos");
        }

        var createdNoticia = _noticiaService.CreateNoticia(noticia);
        if (createdNoticia == null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Falha ao criar notícia");
        }

        return CreatedAtAction(nameof(Get), new { id = createdNoticia }, createdNoticia);
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var noticia = _noticiaService.GetNoticiaById(id);
        if (noticia == null)
        {
            return NotFound("Notícia não encontrada");
        }
        return Ok(noticia);
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var noticias = _noticiaService.GetAllNoticia();
        if (noticias.IsNullOrEmpty())
        {
            return NotFound("Nenhuma notícia encontrada");
        }
        return Ok(noticias);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var success = _noticiaService.DeleteNoticia(id);
        if (!success)
        {
            return NotFound("Notícia não encontrada");
        }
        return NoContent();
    }

    [HttpPut]
    public IActionResult Update([FromBody] Noticia noticia)
    {
        if (noticia == null)
        {
            return BadRequest("Dados inválidos");
        }

        var success = _noticiaService.UpdateNoticia(noticia);
        if (!success)
        {
            return NotFound("Notícia não encontrada");
        }
        return Ok();
    }
}
