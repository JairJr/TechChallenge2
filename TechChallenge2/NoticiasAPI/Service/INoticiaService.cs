using NoticiasAPI.Model;

namespace NoticiasAPI.Service;

public interface INoticiaService
{
    int? CreateNoticia(Noticia noticia);
    Noticia? GetNoticiaById(int id);
    IEnumerable<Noticia> GetAllNoticia();
    bool UpdateNoticia(Noticia noticia);
    bool DeleteNoticia(int id);
}
