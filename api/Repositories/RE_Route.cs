using BusinessPlan;
using Entities;
using System.Linq;
namespace Repositories{
    public static class RE_Route{
        public static List<EN_Route>? route_lst = new List<EN_Route>();
        public static EN_Return Add(EN_Route route){
            if(route == null){
                return new EN_Return{code=1, tittle="Inválida", description="Rota não Adicionada por dados inválidos"};
            }
          
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
        public static EN_Return Search(IConfiguration config,Guid? guid=null,string? origin=null,string? destiny=null,decimal? value=null){
            route_lst = BP_Route.Select(config,guid,origin,destiny);// comando de update
            return new EN_Return{code=0, tittle="sucesso", description="Rota pesquisada com sucesso", dataList=route_lst};
        }
    }
}