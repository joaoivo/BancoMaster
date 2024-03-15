using BusinessPlan;
using Entities;
using System.Linq;
namespace Repositories{
    public static class RE_Route{
        public static EN_Return Search(IConfiguration config,Guid? guid=null,string? origin=null,string? destiny=null,decimal? value=null){
            List<EN_Route>? route_lst = BP_Route.Select(config,guid,origin,destiny);
            return new EN_Return{code=0, tittle="sucesso", description="Rota pesquisada com sucesso", dataList=route_lst};
        }
        public static EN_Return Add(IConfiguration config,EN_Route route){
            if(route == null){
                return new EN_Return{code=1, tittle="Vazio", description="Rota não Adicionada por dados nulos"};
            }else if (!route.isValid){
                return new EN_Return{code=2, tittle="Inválido", description="Dados inválidos da Rota: "+string.Join(";\n",route.invalidList)};
            }
            EN_Return route_return = BP_Route.Insert(config,route);
            return new EN_Return{code=0, tittle="sucesso", description="Rota Adicionada com sucesso"};
        }
        public static EN_Return Alter(EN_Route route){
            if(route == null){
                return new EN_Return{code=1, tittle="Inválida", description="Rota não Alterada por dados inválidos"};
            }
            
            return new EN_Return{code=0, tittle="sucesso", description="Rota Alterada com sucesso"};
        }
        public static EN_Return Delete(Guid? guid=null){
            if(guid == null){
                return new EN_Return{code=1, tittle="Inválida", description="Rota não Alterada por dados inválidos"};
            }
            
            return new EN_Return{code=0, tittle="sucesso", description="Rota Excluída com sucesso"};
        }
    }
}