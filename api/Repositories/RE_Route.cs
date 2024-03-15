using Entities;
namespace Repositories{
    public static class RE_Route{
        public static List<EN_Route> route_lst = new List<EN_Route>();
        public static EN_Return Add(EN_Route route){
            if(route == null){
                return new EN_Return{code=1, tittle="Inválida", description="Rota não Adicionada por dados inválidos"};
            }
            route_lst.Add(route);
            return new EN_Return{code=0, tittle="sucesso", description="Rota Adicionada com sucesso"};
        }
        public static EN_Return Alter(EN_Route route){
            if(route == null){
                return new EN_Return{code=1, tittle="Inválida", description="Rota não Alterada por dados inválidos"};
            }
            route_lst.Add(route);// comando de update
            return new EN_Return{code=0, tittle="sucesso", description="Rota Alterada com sucesso"};
        }
        public static EN_Return Delete(string? guid=null){
            if(guid == null){
                return new EN_Return{code=1, tittle="Inválida", description="Rota não Alterada por dados inválidos"};
            }
            //route_lst.Add(route);// comando de update
            return new EN_Return{code=0, tittle="sucesso", description="Rota Excluída com sucesso"};
        }
    }
}