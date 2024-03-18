using Microsoft.AspNetCore.Mvc;
using Repositories;
using Entities;
using System.Text.Json;
using System.Text.Json.Nodes;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSqlServer<ApplicationDbContext>(builder.Configuration["Database:Default"]);
var app = builder.Build();


IResult returnResults(EN_Return result_en, string operation=""){
	
	if(result_en==null){
		return Results.BadRequest("Operação não efetuada: "+operation);
	}else if(result_en.code!=0){
		return Results.Problem("Problema não execução: "+result_en.description,null,result_en.code,result_en.tittle);
	}else if(result_en.code==0){
		JsonSerializerOptions jsonOptions =new JsonSerializerOptions { WriteIndented = true, IgnoreReadOnlyFields =true, AllowTrailingCommas =false };
		string data = JsonSerializer.Serialize<EN_Return>(result_en,jsonOptions);
		return Results.Ok(JsonObject.Parse(data));
	}else{
		return Results.NotFound("fugiu");
	}
}

app.MapGet("/", () => "Bem vindos ao Teste Banco Master utilize a interface para acessar os conteúdos");
app.MapGet("/routes", () => {
	try{
		return returnResults( RE_Route.Search(builder.Configuration));
	}catch(Exception ex){
		return returnResults( new EN_Return{code=99, tittle="Erro de Runtime", description="Comando não executado: "+ex.Message + " em \n " +ex.StackTrace});
	}
});
// rota mais barata - melhor rota
app.MapGet("/routes/bestprice/{origin}/{destiny}", ([FromRoute] string origin, [FromRoute] string destiny) => {
	try{
		return returnResults( RE_Route.Bestprice(builder.Configuration, origin,destiny));
	}catch(Exception ex){
		return returnResults( new EN_Return{code=99, tittle="Erro de Runtime", description="Comando não executado: "+ex.Message + " em \n " +ex.StackTrace});
	}
});

//routes
app.MapGet("/routes/Advanced/", ([FromQuery] Guid? guid,[FromQuery] string? origin, [FromQuery] string? destiny, [FromQuery] decimal? value) =>{
	try{
		return returnResults( RE_Route.Search(builder.Configuration, guid, origin,destiny,value));
	}catch(Exception ex){
		return returnResults( new EN_Return{code=99, tittle="Erro de Runtime", description="Comando não executado: "+ex.Message + " em \n " +ex.StackTrace});
	}
});
app.MapGet("/routes/id/{guid}", ([FromRoute] Guid guid) => {
	try{
		return returnResults( RE_Route.Search(builder.Configuration, guid));
	}catch(Exception ex){
		return returnResults( new EN_Return{code=99, tittle="Erro de Runtime", description="Comando não executado: "+ex.Message + " em \n " +ex.StackTrace});
	}
} );

//add
app.MapPost("/routes/{origin}/{destiny}/{price}", ([FromRoute] string origin, [FromRoute] string destiny, [FromRoute] decimal price) => {
	try {
		return returnResults( RE_Route.Add(builder.Configuration,new EN_Route { rtsOrigin = origin, rtsDestintion = destiny, rtsPrice = price }));
	} catch (Exception ex) {
		return returnResults( new EN_Return { code = 99, tittle = "Erro de Runtime", description = "Comando não executado: " + ex.Message + " em \n " +ex.StackTrace});
	}
});
//alter
app.MapPut("/routes/{guid}/{origin}/{destiny}/{value}", ([FromRoute] Guid guid,[FromRoute] string origin, [FromRoute] string destiny, [FromRoute] decimal value) => {
	//"Adicionar rotas de "+origin +" à "+destiny
	try{
		return returnResults( RE_Route.Alter(builder.Configuration,new EN_Route{rtsGuid =guid ,rtsOrigin=origin, rtsDestintion=destiny, rtsPrice=value}));
	}catch(Exception ex){
		return returnResults( new EN_Return{code=99, tittle="Erro de Runtime", description="Comando não executado: "+ex.Message});
	}
});
//delete
app.MapDelete("/routes/{guid}", ([FromRoute] Guid guid) => {
	//"Excluir rotas de "+guid
	try{
		return returnResults( RE_Route.Delete(builder.Configuration,guid));
	}catch(Exception ex){
		return returnResults( new EN_Return{code=99, tittle="Erro de Runtime", description="Comando não executado: "+ex.Message});
	}
} );

app.Run();

