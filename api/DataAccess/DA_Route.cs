using System.Data;
using Dapper;
using Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace DataAccess{
	public static class DA_Route{

		public static IEnumerable<EN_Route> Select(IConfiguration config,Guid? guid=null,string? origin=null,string? destiny=null){
			
			IEnumerable<EN_Route> route_lst = new List<EN_Route>();
			DynamicParameters parameters = new DynamicParameters();
			parameters.Add("@guid", guid, DbType.Guid, ParameterDirection.Input);
			parameters.Add("@origin", origin, DbType.String, ParameterDirection.Input);
			parameters.Add("@Destintion", destiny, DbType.String, ParameterDirection.Input);

			using (SqlConnection db = new SqlConnection(config["Database:Default"])){
				route_lst = db.Query<EN_Route>("[dbo].[pr_routes_sel]",parameters);
			}
			return route_lst;
		}
		public static EN_Return Insert(IConfiguration config,EN_Route route){
			
			IEnumerable<EN_Route> route_lst = new List<EN_Route>();
			DynamicParameters parameters = new DynamicParameters();
			parameters.Add("@origin", route.rtsOrigin, DbType.String, ParameterDirection.Input,3);
			parameters.Add("@Destintion", route.rtsDestintion, DbType.String, ParameterDirection.Input,3);
			parameters.Add("@Price", route.rtsPrice, DbType.String, ParameterDirection.Input);
			parameters.Add("@id", null, DbType.Guid, ParameterDirection.Output);
			parameters.Add("@isOK", null, DbType.Int32, ParameterDirection.Output);
			parameters.Add("@message", null, DbType.String, ParameterDirection.Output,1000);

			EN_Return route_return = new EN_Return();
			using (SqlConnection db = new SqlConnection(config["Database:Default"])){
				db.Execute("[dbo].[pr_routes_ins]",parameters);
				route_return.id = parameters.Get<Guid?>("id");
				route_return.description = parameters.Get<string>("message");
				
				int isOk = parameters.Get<int>("isOK");
				route_return.tittle = (isOk==1?"Inserção efetuada com sucesso":"Erro na tentativa de inserção");
				route_return.code=(isOk==1?0:98);
			}
			return route_return;
		}
	}
}