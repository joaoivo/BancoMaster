import {useState } from 'react';
import { FaLongArrowAltRight,FaDotCircle,FaDollarSign, FaAmazonPay } from "react-icons/fa";
import { MdEditSquare,MdDeleteForever  } from "react-icons/md";
import api from '../../services/api'
import {  toast } from 'react-toastify';

export default function Register(){

	const [origin	,setStateOrigin	] = useState('');
	const [destiny	,setStateDestiny	] = useState('');
	const [dataList,setStateDataList	] = useState([]);

	async function searchRoutes(){
		console.clear();
		var apiUrlParameters=[];
		if(origin){apiUrlParameters.push("origin="+origin);}
		if(destiny){apiUrlParameters.push("destiny="+destiny);}
		var apiUrl="/routes/Advanced/";
		if(apiUrlParameters){apiUrl+="?"+(apiUrlParameters.join("&"));}

		const response = await api.get(apiUrl);
		if(!response){return;}
		setStateDataList(response.data.dataList);
	}

	async function deleteRoutes(guid){
		if(!!!guid){
			toast.error("Sem ID para Exclusão!");
			return;
		}
		if(!window.confirm("Confirma exclusão da Rota?")){
			return;
		}
		console.clear();
		var apiUrl=`/routes/${guid}`;
		var response = await api.delete(apiUrl);
		searchRoutes();
		toast.success("Dados Excluídos com sucesso!");
	}
	return(
		<div>
			<h1>Cadastro e Consulta de Rotas</h1>
			<div>
				<div className='formLineDiv'>
					<label>Origem: <input type="text" placeholder="Origem" value={origin} onChange={(e)=>setStateOrigin(e.target.value)} maxLength={3} size={5}/></label>
					<label>Destino: <input type="text" placeholder="Destino" value={destiny} onChange={(e)=>setStateDestiny(e.target.value)} maxLength={3} size={5}/></label>
					<button onClick={(e)=>searchRoutes()}>Pesquisar</button>
					<button onClick={() => window.location.href =('/route/')}>Novo</button>
				</div>
				<div >
					{
						dataList &&
						dataList.map((rt, idx)=>{
							return(
								<div className='formLineDiv'  key={`div_${rt.rtsGuid}`}>
									<FaDotCircle className='icons'/>
									<label><span>{rt.rtsOrigin}</span></label>
									<FaLongArrowAltRight className='icons'/>
									<label><span>{rt.rtsDestintion}</span></label>
									<FaDollarSign className='icons'/>
									<label> <span>{rt.rtsPrice}</span></label>
									<MdEditSquare className='iconButton' onClick={() => window.location.href =('/route/'+rt.rtsGuid)}/>
									<MdDeleteForever className='iconButton' onClick={()=>deleteRoutes(rt.rtsGuid)}/>
								</div>
							)
						})
					}
				</div>
			</div>
		</div>
	)
}