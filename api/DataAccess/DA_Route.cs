using System.Data;
using Dapper;
using Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace DataAccess{
	public static class DA_Route{

		public static IEnumerable<EN_Route> Select(IConfiguration config,string? guid,string origin,string destiny){
			
			IEnumerable<EN_Route> route_lst = new List<EN_Route>();
			DynamicParameters parameters = new DynamicParameters();
			parameters.Add("@guid", guid, DbType.Guid, ParameterDirection.Input);
			parameters.Add("@origin", origin, DbType.Guid, ParameterDirection.Input);
			parameters.Add("@Destintion", destiny, DbType.Guid, ParameterDirection.Input);

			using (SqlConnection db = new SqlConnection(this.config["Database:Default"])){
				route_lst = db.Query<EN_Route>("[dbo].[pr_routes_sel]",parameters);
			}
			return route_lst;
		}
	}
}