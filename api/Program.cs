var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Bem vindos ao Teste Banco Master utilize a interface para acessar os conteúdos");
app.MapGet("/availables", () => "mapas das rotas disponiveis");
app.MapGet("/routes/{origin}/{destiny}", (string origin, string destiny) => "mapas das rotas de "+origin +" à "+destiny);
app.Run();
