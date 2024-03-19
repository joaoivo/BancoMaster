using Entities;
using DataAccess;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace BusinessPlan{
	public static class BP_Route{
		private const string CNT_SEP = "-";
		public static Dictionary<string,decimal> Bestprice(IConfiguration configuration,string? origin=null,string? destiny=null){
			Dictionary<string,decimal> dicReturn = new Dictionary<string, decimal>();
			if(string.IsNullOrWhiteSpace(origin) || string.IsNullOrWhiteSpace(destiny) || origin==destiny ){return dicReturn;}
			List<EN_Route>? routes_lst = (List<EN_Route>?)DA_Route.Select(configuration,null, null, null);
			if(routes_lst==null || routes_lst.Count<=0){return dicReturn;}

			foreach(EN_Route route in routes_lst.Where(p=>p.rtsOrigin==origin)){
				if(String.IsNullOrWhiteSpace(route.rtsOrigin) ||String.IsNullOrWhiteSpace(route.rtsDestintion) ){continue;}
				dicReturn.Add(route.rtsOrigin+CNT_SEP+route.rtsDestintion,route.rtsPrice==null?0:(Decimal)route.rtsPrice);
			}
			if(dicReturn.Count<=0){return dicReturn;}
			dicReturn = searchBestRoutes(dicReturn,routes_lst,destiny).Where(rt=>rt.Key.Split(CNT_SEP).First()==origin && rt.Key.Split(CNT_SEP).Last()==destiny).ToDictionary(p => p.Key,p=> p.Value);
			return dicReturn ;
		}
		public static Dictionary<string,decimal> searchBestRoutes(Dictionary<string,decimal> dicRoutes,List<EN_Route> routes_lst,string destiny,int loopingRecursiveCount=0){
			const int CNT_ROUTES_POINTCOUNTMAX = 6;
			const int CNT_RECURSIVE_LOOPINGCOUNTMAX = 10; 

			loopingRecursiveCount++;
			if(loopingRecursiveCount >= CNT_RECURSIVE_LOOPINGCOUNTMAX){ return dicRoutes;}

			Dictionary<string,decimal> dicRoutesReturn = new Dictionary<string, decimal>();
			foreach(KeyValuePair<string,decimal> dicRoute in dicRoutes){
				List<string> pointsLst= dicRoute.Key.Split("-").ToList();
				if(pointsLst.Last()==destiny){
					dicRoutesReturn.Add(dicRoute.Key,dicRoute.Value);
					continue;
				} // se já chegou no destino não precisa processar

				List<EN_Route> routesNextPoint = routes_lst.Where(p=>p.rtsOrigin == pointsLst.Last()).ToList();
				for(int ix =0; ix< routesNextPoint.Count; ix++ ){
					if(routesNextPoint[ix].rtsDestintion==null || routesNextPoint[ix].rtsPrice==null){continue;}

					pointsLst.Add(routesNextPoint[ix].rtsDestintion);
					string newKey=String.Join(CNT_SEP,pointsLst);
					decimal newValue = dicRoute.Value+(Decimal)routesNextPoint[ix].rtsPrice;

					if(dicRoutesReturn.ContainsKey(newKey)){
						if(dicRoutesReturn[newKey]>newValue){dicRoutesReturn[newKey]= newValue;}
					}else{
						dicRoutesReturn.Add(newKey,newValue);
					}
				}
			}
			dicRoutesReturn = searchBestRoutes(dicRoutesReturn,routes_lst,destiny,loopingRecursiveCount);

			return dicRoutesReturn.Select(p=> new {p.Key , p.Value}).Where(p=>
					p.Key.IndexOf(CNT_SEP)>=0 // com origem e destino
				&& p.Value>0 // com valor maior que zero
				&& p.Key.Split(CNT_SEP).Count()<=CNT_ROUTES_POINTCOUNTMAX // quantidades de paradas menor que o máximo permitido				
			).OrderBy(p=>p.Value).ThenBy(p=>p.Key)
			.ToDictionary(p => p.Key,p=> p.Value);
		}
		public static List<EN_Route>? Select(IConfiguration configuration,Guid? guid=null,string? origin=null,string? destiny=null){
			List<EN_Route>? route_lst =  (List<EN_Route>?)DA_Route.Select(configuration,guid, origin, destiny);
			return route_lst;
		}
		public static EN_Return Insert(IConfiguration configuration,EN_Route route){
			EN_Return route_return =  DA_Route.Insert(configuration,route);
			return route_return;
		}
		public static EN_Return Update(IConfiguration configuration,EN_Route route){
			EN_Return route_return =  DA_Route.Update(configuration,route);
			return route_return;
		}
		public static EN_Return Delete(IConfiguration configuration,Guid guid){
			EN_Return route_return =  DA_Route.Delete(configuration,guid);
			return route_return;
		}
	}
}