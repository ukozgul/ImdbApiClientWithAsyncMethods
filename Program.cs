using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

using HttpClient client = new();
client.BaseAddress = new Uri("https://localhost:7197/");
client.DefaultRequestHeaders.Accept.Clear();
client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

//For POST and UPDATE Methods
Film testFilm = new Film
{
    id = 0,
    director_name = "DirectorNameTest1",
    duration = 58,
    actor_2_name = "Actor2NameTest1",
    genres = "GenresTest1",
    actor_1_name = "Actor1NameTest1",
    movie_title = "MovieTitleTest1",
    num_voted_users = 5,
    actor_3_name = "Actor3NameTest1",
    movie_imdb_link = "LinkTest1",
    num_user_for_reviews = "Test1",
    language = "Turkish",
    country = "Turkiye",
    title_year = 2005,
    imdb_score = 10
};


#region GET-Api/Movie/{id}

int whichId = 5046;
await GetFilmAsync(client, whichId);

static async Task<Film> GetFilmAsync(HttpClient client, int whichId)
{
    Film? film = null;
    var url = $"api/Movies/{whichId}";
    HttpResponseMessage response = await client.GetAsync(url);
    if (response.IsSuccessStatusCode)
    {
        film = await response.Content.ReadAsAsync<Film>();
        System.Console.WriteLine("Id: {0}\nMovie Title: {1}\nDirector Name: {2}",film.id,film.movie_title,film.director_name);
    }
    return film ?? new();
}

#endregion

#region GET-Api/Movies

var films = await GetFilmsAsync(client);
ShowFilms(films);

void ShowFilms(List<Film> film)
{
    foreach (var f in film)
    {
        Console.WriteLine(f.id);
        Console.WriteLine(f.movie_title);
        Console.WriteLine(f.director_name);
        Console.WriteLine(f.duration);
        Console.WriteLine();

    }
}

static async Task<List<Film>> GetFilmsAsync(HttpClient client)
{
    var url = "api/Movies";
    await using Stream stream = await client.GetStreamAsync(url);

    var films = await JsonSerializer.DeserializeAsync<List<Film>>(stream);
    return films ?? new();
}


#endregion

#region POST-Api/Movies

var url = await CreateFilmAsync(testFilm, client);
Console.WriteLine($"Created at {url}");

static async Task<Uri> CreateFilmAsync(Film testFilm, HttpClient client)
{
    HttpResponseMessage response = await client.PostAsJsonAsync(
        "api/movies", testFilm);
    response.EnsureSuccessStatusCode();

    // return URI of the created resource.
    return response.Headers.Location;
}


#endregion

#region DELETE-Api/Movies/{id}

int filmid = 5046;
var statusCode = await DeleteFilmAsync(filmid, client);
Console.WriteLine($"Deleted (HTTP Status = {(int)statusCode})");

static async Task<HttpStatusCode> DeleteFilmAsync(int id, HttpClient client)
{
    HttpResponseMessage response = await client.DeleteAsync(
        $"api/Movies/{id}");
    return response.StatusCode;
}

#endregion

#region PUT-Api/Movies/{id}

Console.WriteLine("Updating film...");
testFilm.director_name = "Osman Sinav Put deneme-2";
testFilm.id = 5045;
await UpdateFilmAsync(testFilm, client);

static async Task<Film> UpdateFilmAsync(Film film, HttpClient client)
{

    HttpResponseMessage response = await client.PutAsJsonAsync($"api/movies/{film.id}", film);
    response.EnsureSuccessStatusCode();
    System.Console.WriteLine($"{((int)response.StatusCode)},{response.StatusCode}");

    film = await response.Content.ReadAsAsync<Film>();
    return film;
}

#endregion






