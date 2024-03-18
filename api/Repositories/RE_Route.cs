using BusinessPlan;
using Entities;
using System.Linq;
namespace Repositories{
    public static class RE_Route{
        public static EN_Return Search(IConfiguration config,Guid? guid=null,string? origin=null,string? destiny=null,decimal? value=null){
            List<EN_Route>? route_lst = BP_Route.Select(config,guid,origin,destiny);
            return new EN_Return{code=0, tittle="sucesso", description="Rota pesquisada com sucesso", dataList=route_lst};
        }
        public static EN_Return Bestprice(IConfiguration config,string? origin=null,string? destiny=null){
            Dictionary<string,decimal> routes_dic = BP_Route.Bestprice(config,origin,destiny);
            EN_Return return_en = new EN_Return{code=0, tittle="sucesso na busca de rotas mais baratas", description="Rota mais barata pesquisada com sucesso"}; 
            return_en.dataList = routes_dic.Select(rt => new { route = rt.Key, price = rt.Value });
            return return_en;
        }
        public static EN_Return Add(IConfiguration config,EN_Route route){
            if(route == null){
                return new EN_Return{code=1, tittle="Vazio", description="Rota não Adicionada por dados nulos"};
            }else if (!route.isValid){
                return new EN_Return{code=2, tittle="Inválido", description="Dados inválidos da Rota: "+string.Join(";\n",route.invalidList)};
            }
            return BP_Route.Insert(config,route);
        }
        public static EN_Return Alter(IConfiguration config,EN_Route route){
            if(route == null){
                return new EN_Return{code=1, tittle="Vazio", description="Rota não Alterada por dados nulos"};
            }else if (!route.isValid){
                return new EN_Return{code=2, tittle="Inválido", description="Dados inválidos da Rota: "+string.Join(";\n",route.invalidList)};
            }
            return BP_Route.Update(config,route);
        }
        public static EN_Return Delete(IConfiguration config,Guid guid){
            return BP_Route.Delete(config,guid);
        }
    }
}