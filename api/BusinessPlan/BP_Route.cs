using Entities;
using DataAccess;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace BusinessPlan{
	public static class BP_Route{
		public static List<EN_Route>? Select(IConfiguration configuration,Guid? guid=null,string? origin=null,string? destiny=null){
			List<EN_Route>? route_lst =  (List<EN_Route>?)DA_Route.Select(configuration,guid, origin, destiny);
			return route_lst;
		}
	}
}