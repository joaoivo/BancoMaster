using Microsoft.AspNetCore.Mvc;
using Repositories;
using Entities;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSqlServer<ApplicationDbContext>(builder.Configuration["Database:Default"]);
var app = builder.Build();

app.MapGet("/", () => "Bem vindos ao Teste Banco Master utilize a interface para acessar os conteúdos");
app.MapGet("/routes", () => "mapas das rotas disponiveis");
app.MapGet("/routes/order/{origin}/{destiny}", ([FromRoute] string origin, [FromRoute] string destiny) => "mapas das rotas de "+origin +" à "+destiny);
//add
app.MapPut("/routes/{origin}/{destiny}/{price}", ([FromRoute] string origin, [FromRoute] string destiny, [FromRoute] decimal price) => {
	try{
		return RE_Route.Add(new EN_Route{rtsOrigin=origin, rtsDestintion=destiny, rtsPrice=value});
	}catch(Exception ex){
		return Results.Problem( new EN_Return{code=99, tittle="Erro de Runtime", description="Comando não executado: "+ex.Message});
	}
});
//alter
app.MapPost("/routes/alter/{guid}/{origin}/{destiny}/{value}", ([FromRoute] string guid,[FromRoute] string origin, [FromRoute] string destiny, [FromRoute] decimal value) => {
	//"Adicionar rotas de "+origin +" à "+destiny
	try{
		return RE_Route.Alter(new EN_Route{rtsOrigin=origin, rtsDestintion=destiny, rtsPrice=value});
	}catch(Exception ex){
		return Results.Problem( new EN_Return{code=99, tittle="Erro de Runtime", description="Comando não executado: "+ex.Message});
	}
});
//delete
app.MapDelete("/routes/delete/{guid}", ([FromRoute] string id) => {
	//"Excluir rotas de "+guid
	try{
		return RE_Route.Delete(id);
	}catch(Exception ex){
		return Results.Problem( new EN_Return{code=99, tittle="Erro de Runtime", description="Comando não executado: "+ex.Message});
	}
} );
app.MapGet("/routes/search", ([FromQuery] string? guid,[FromQuery] string origin, [FromQuery] string destiny, [FromQuery] decimal? value) => "Consultar rotas Pelos parametros "+origin +" à "+destiny);
app.MapGet("/routes/{guid}", ([FromRoute] string guid) => "Adicionar rotas de "+guid );

app.Run();
