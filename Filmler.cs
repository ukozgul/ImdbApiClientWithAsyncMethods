using System.Text.Json.Serialization;

// public class Film
// {
//     public int id { get; set; }
//     public string? title { get; set; }
//     public string? rating { get; set; }
//     public string? genre { get; set; }
//     public int duration { get; set; }
// }



public record class Film( )
{
  
    public int? id { get; set; }
    public string? director_name { get; set; }
    public int? duration { get; set; } 
    public string? actor_2_name { get; set; }
    public string? genres { get; set; }
    public string? actor_1_name { get; set; }
    public string? movie_title { get; set; }
    public int? num_voted_users { get; set; }
    public string? actor_3_name { get; set; }
    public string? movie_imdb_link { get; set; }
    public string? num_user_for_reviews { get; set; }
    public string? language { get; set; }
    public string? country { get; set; }
    public int? title_year { get; set; }
    public int? imdb_score { get; set; }
}
