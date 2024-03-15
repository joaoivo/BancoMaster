using Microsoft.AspNetCore.Mvc;
using Repositories;
using Entities;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSqlServer<ApplicationDbContext>(builder.Configuration["Database:Default"]);
var app = builder.Build();


IResult returnResults(EN_Return result_en, string operation=""){
	
	if(result_en==null){
		return Results.BadRequest("Operação não efetuada: "+operation);
	}else if(result_en.code!=0){
		return Results.Problem("Problema não execução: "+result_en.description,null,result_en.code,result_en.tittle);
	}else if(result_en.code==0){
		return Results.Ok( JsonSerializer.Serialize(result_en));
	}else{
		return Results.NotFound("fugiu");
	}
}

app.MapGet("/", () => "Bem vindos ao Teste Banco Master utilize a interface para acessar os conteúdos");
app.MapGet("/routes", () => "mapas das rotas disponiveis");
app.MapGet("/routes/order/{origin}/{destiny}", ([FromRoute] string origin, [FromRoute] string destiny) => "mapas das rotas de "+origin +" à "+destiny);

//routes
app.MapGet("/routes/Advanced/", ([FromQuery] Guid? guid,[FromQuery] string? origin, [FromQuery] string? destiny, [FromQuery] decimal? value) =>{
// "Consultar rotas Pelos parametros "+origin +" à "+destiny
	try{
		return returnResults( RE_Route.Search(builder.Configuration, guid, origin,destiny,value));
	}catch(Exception ex){
		return returnResults( new EN_Return{code=99, tittle="Erro de Runtime", description="Comando não executado: "+ex.Message + " em \n " +ex.StackTrace});
	}
});
app.MapGet("/routes/id/{guid}", ([FromRoute] string guid) => "Adicionar rotas de "+guid );

//add
app.MapPost("/routes/{origin}/{destiny}/{price}", ([FromRoute] string origin, [FromRoute] string destiny, [FromRoute] decimal price) => {
	try {
		return returnResults( RE_Route.Add(builder.Configuration,new EN_Route { rtsOrigin = origin, rtsDestintion = destiny, rtsPrice = price }));
	} catch (Exception ex) {
		return returnResults( new EN_Return { code = 99, tittle = "Erro de Runtime", description = "Comando não executado: " + ex.Message + " em \n " +ex.StackTrace});
	}
});
//alter
app.MapPut("/routes/alter/{guid}/{origin}/{destiny}/{value}", ([FromRoute] Guid guid,[FromRoute] string origin, [FromRoute] string destiny, [FromRoute] decimal value) => {
	//"Adicionar rotas de "+origin +" à "+destiny
	try{
		return returnResults( RE_Route.Alter(new EN_Route{rtsGuid =guid ,rtsOrigin=origin, rtsDestintion=destiny, rtsPrice=value}));
	}catch(Exception ex){
		return returnResults( new EN_Return{code=99, tittle="Erro de Runtime", description="Comando não executado: "+ex.Message});
	}
});
//delete
app.MapDelete("/routes/delete/{guid}", ([FromRoute] Guid guid) => {
	//"Excluir rotas de "+guid
	try{
		return returnResults( RE_Route.Delete(guid));
	}catch(Exception ex){
		return returnResults( new EN_Return{code=99, tittle="Erro de Runtime", description="Comando não executado: "+ex.Message});
	}
} );

app.Run();

