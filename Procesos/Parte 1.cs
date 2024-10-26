using System.Collections.Immutable;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.RegularExpressions;

public class Excerpt
    {
        public string rendered { get; set; }
    }

    public class Notice
    {
        public Title title { get; set; }
        public Excerpt excerpt { get; set; }
    }

    public class Title
    {
        public string rendered { get; set; }
    }


class parte_1{

    public  static async Task<ServerResult> Ejecutar(){
        try{
        var url = "https://remolacha.net/wp-json/wp/v2/posts?search=migraci%C3%B3n&_fields=title,excerpt";
        var client = new HttpClient();
        var response = await client.GetAsync(url);
        var json = await response.Content.ReadAsStringAsync();
        var noticia = JsonSerializer.Deserialize<List<Notice>>(json);

        var final = new List<Dictionary<string, string>>();

        foreach(var noticias in noticia){

            var titulo = noticias.title.rendered;
            var resumen = noticias.excerpt.rendered;

            resumen =Regex.Replace(resumen, "<.*?>", string.Empty);
            
            var dic = new Dictionary<string, string>();
            dic.Add("Titulo", titulo);
            dic.Add("Resumen", resumen);
            final.Add(dic);
        }

        return new ServerResult(true, "Noticias cargadas", final);
        }
        catch(Exception ex){
            return new ServerResult(false, ex.Message);
        }
        
    }
}