import {useEffect, useState } from 'react';
import { FaLongArrowAltRight,FaDotCircle,FaDollarSign } from "react-icons/fa";
import { PiIdentificationCardFill } from "react-icons/pi";

import api from '../../services/api'
import { useParams } from 'react-router-dom';

import {  toast } from 'react-toastify';

export default function RouteData(){
	const [message	,setStateMessage	] = useState([]);
	const [guid	,setStateGuid	] = useState('');
	const [origin	,setStateOrigin	] = useState('');
	const [destiny	,setStateDestiny	] = useState('');
	const [price	,setStatePrice	] = useState(0);

	const {id} =useParams();

	useEffect(()=>{
		async function loadRoute(){
			if(!id){
				return;
			}
			var apiUrl="/routes/id/"+id;
			const response = await api.get(apiUrl);
			var data = (response.data.dataList[0]);
			
			setStateGuid(data.rtsGuid);
			setStateOrigin(data.rtsOrigin);
			setStateDestiny(data.rtsDestintion);
			setStatePrice(data.rtsPrice);
		}
		loadRoute();
	},[id])

	function onlyUnique(value, index, array) {
		return array.indexOf(value) === index;
	 }

	function itIsNumeric(str) {
		//if (typeof str != "string") return isNaN(str) // we only process strings!  
		return !isNaN(str) && // use type coercion to parse the _entirety_ of the string (`parseFloat` alone does not do this)...
				 !isNaN(parseFloat(str)) // ...and ensure strings of whitespace fail
	 }

	async function saveRoutes(){
		setStateMessage("");
		console.clear();
		var arrInvalidList=[];
		if(origin==null || origin===undefined){arrInvalidList.push("Sem origem");}
		else if(origin.length<3){arrInvalidList.push("A Sigla da Origem tem que ter exatamente 3 caractéres");}

		if(destiny==null || destiny===undefined){arrInvalidList.push("Sem Destino");}
		else if(destiny.length<3){arrInvalidList.push("A Sigla da Destino tem que ter exatamente 3 caractéres");}
		
		if(price==null || price===undefined){arrInvalidList.push("Sem Preço");}
		else if(!itIsNumeric(price)){arrInvalidList.push(`O valor de preço não é numérico '${price}'`);}
		else if(price<=0){arrInvalidList.push("O valor de preço precisa ser positivo");}
		
		if(arrInvalidList.length>0){
			var mess=[]
			arrInvalidList = arrInvalidList.filter(onlyUnique);
			arrInvalidList.forEach((val)=>{mess.push(val);});
			mess = mess.filter(onlyUnique);
			setStateMessage(mess);
			toast.error(mess.join("<br/>"));
			return;
		}
		var apiUrl="/routes";
		var apiRouteData =`/${origin}/${destiny}/${price}`;
		var response;
		if(guid!=null && guid!=""){
			//atualização 
			apiRouteData=`/${guid}`+apiRouteData;
			response = await api.put(apiUrl+apiRouteData);
		}else{
			//adição
			response = await api.post(apiUrl+apiRouteData);
		}
		
		if(!response){
			const message = "Sem resposta do servidor";
			toast.error(message);
			setStateMessage(message);
			return;
		}
		toast.success("Dados Salvos com sucesso!");
	}
	return(
		<div>
			<h1>{!!!guid?"Nova Rota":"Atualização de Rota"}</h1>
			{!!id &&
				<div className='formLineDiv'>
					<PiIdentificationCardFill className='icons'/>id:{guid}
				</div>
			}
			<div className='formLineDiv'>
				<FaDotCircle className='icons'/>
				<label>Origem:<input type="text" 	placeholder="Origem" 	value={origin} 	onChange={(e)=>setStateOrigin(e.target.value)} 	maxLength={3} size={5}/></label>
				<FaLongArrowAltRight className='icons'/>
				<label>Destino:<input type="text" 	placeholder="Destino" 	value={destiny} 	onChange={(e)=>setStateDestiny(e.target.value)} maxLength={3} size={5}/></label>
				<FaDollarSign className='icons'/>
				<label>Preço:<input type="number" 	placeholder="Preço" 		value={price} 		onChange={(e)=>setStatePrice(e.target.value)} 	maxLength={6} size={5} min="0.00" max="10000.00" step="0.01"/></label>
			</div>
			{!!message.length&&
				<div className='formLineDiMessage'>
					{message.map((val, idx)=>{return(<div key={idx}>{val}</div>)})}
				</div>
			}
			<div className='formLineDiv'>
				<button onClick={() => {window.location.href =('/Register/')}}>Voltar</button>
				<button onClick={(e)=>saveRoutes()}>Salvar</button>
			</div>
		</div>
	)
}