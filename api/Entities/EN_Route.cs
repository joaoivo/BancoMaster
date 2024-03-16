namespace Entities{
    public class EN_Route{
        public Guid? rtsGuid  {get; set;}= null;
        public string? rtsOrigin  {get; set;}= null;
        public string? rtsDestintion  {get; set;}= null;
        public decimal? rtsPrice  {get; set;}= null;
        public List<String> invalidList{get {
            List<String> invalidRules_lst = new List<string>();
            
            if(rtsOrigin==null){invalidRules_lst.Add("Sem Sigla de Origem");}
            else if(rtsOrigin.Length!=3){invalidRules_lst.Add("Sigla de Origem tem que ter 3 letras '"+rtsOrigin+"'");}
            
            if(rtsDestintion==null){invalidRules_lst.Add("Sem Sigla de Destino");}
            else if(rtsDestintion.Length!=3){invalidRules_lst.Add("Sigla de Destino tem que ter 3 letras '"+rtsDestintion+"'");}

            if(rtsPrice==null){invalidRules_lst.Add("Sem Preço da Rota");}
            else if(rtsPrice<=0){invalidRules_lst.Add("Preço de Rota Tem que ser positivo");}

            return invalidRules_lst;
        }}
        public bool isValid { get{return invalidList.Count <=0;}}
    }
}
