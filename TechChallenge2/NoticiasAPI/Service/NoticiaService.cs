using ElmahCore;
using ElmahCore.Mvc.Logger;
using Microsoft.IdentityModel.Tokens;
using NoticiasAPI.Model;
using TechChallenge.NoticiasAPI.Data;

namespace NoticiasAPI.Service;

public class NoticiaService : INoticiaService
{
    public NoticiasContext _context;

    public NoticiaService(NoticiasContext context)
    {
        _context = context;
    }
    public int? CreateNoticia(Noticia noticia)
    {
        try
        {
            _context.Noticias.Add(noticia);
            _context.SaveChanges();            
            return noticia.Id;
        }
        catch (Exception ex)
        {
            //TODO: logar a mensagem do exception
            ElmahExtensions.RaiseError(new Exception(ex.Message));
            return null;
        }
    }

    public IEnumerable<Noticia> GetAllNoticia() => _context.Noticias.ToList();

    public Noticia? GetNoticiaById(int id) => _context.Noticias.FirstOrDefault(x => x.Id == id);

    public bool UpdateNoticia(Noticia noticia)
    {
        try
        {
            _context.Noticias.Update(noticia);
            _context.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            //TODO: logar a mensagem do exception
            ElmahExtensions.RaiseError(new Exception(ex.Message));
            return false;
        }
    }

    public bool DeleteNoticia(int id)
    {
        try
        {
            var noticia = _context.Noticias.FirstOrDefault(x => x.Id == id);
            if (noticia != null)
            {
                _context.Noticias.Remove(noticia);
                _context.SaveChanges();
            return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            //TODO: logar a mensagem do exception
            ElmahExtensions.RaiseError(new Exception(ex.Message));
            return false;
        }
    }
}