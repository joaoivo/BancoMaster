import {useEffect, useState } from 'react';
import { FaLongArrowAltRight,FaDotCircle,FaDollarSign, FaAmazonPay } from "react-icons/fa";
import { PiIdentificationCardFill } from "react-icons/pi";

import api from '../../services/api'
import { useParams } from 'react-router-dom';

import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

export default function RouteData(){
	const [guid	,setStateGuid	] = useState('');
	const [origin	,setStateOrigin	] = useState('');
	const [destiny	,setStateDestiny	] = useState('');
	const [price	,setStatePrice	] = useState(0);

	const {id} =useParams();

	useEffect(()=>{
		async function loadRoute(){
			var apiUrl="/routes/id/"+id;
			const response = await api.get(apiUrl);
			var data = (response.data.dataList[0]);
			
			setStateGuid(data.rtsGuid);
			setStateOrigin(data.rtsOrigin);
			setStateDestiny(data.rtsDestintion);
			setStatePrice(data.rtsPrice);
		}
		loadRoute();
	},[])

	function itIsNumeric(str) {
		if (typeof str != "string") return false // we only process strings!  
		return !isNaN(str) && // use type coercion to parse the _entirety_ of the string (`parseFloat` alone does not do this)...
				 !isNaN(parseFloat(str)) // ...and ensure strings of whitespace fail
	 }

	async function saveRoutes(){
		var arrInvalidList=[];
		if(origin==null || origin===undefined){arrInvalidList.push("Sem origem");}
		else if(origin.length<3){arrInvalidList.push("A Sigla da Origem tem que ter exatamente 3 caractéres");}

		if(destiny==null || destiny===undefined){arrInvalidList.push("Sem Destino");}
		else if(destiny.length<3){arrInvalidList.push("A Sigla da Destino tem que ter exatamente 3 caractéres");}
		
		if(price==null || price===undefined){arrInvalidList.push("Sem Preço");}
		else if(!itIsNumeric(price)){arrInvalidList.push("O valor de preço não é numérico");}
		else if(price<=0){arrInvalidList.push("O valor de preço precisa ser positivo");}

		console.clear();
		console.log("arrInvalidList",arrInvalidList);
		toast("Wow so easy!");
		if(arrInvalidList){
			
			return;
		}

	}
	return(
		<div>
			<h1>{guid==null?"Nova Rota":"Atualização de Rota"}</h1>
			<div className='formLineDiv'>
				<PiIdentificationCardFill className='icons'/>id: 
				{guid}
			</div>
			<div className='formLineDiv'>
				<FaDotCircle className='icons'/>
				<label>Origem:<input type="text" 	placeholder="Origem" 	value={origin} 	onChange={(e)=>setStateOrigin(e.target.value)} 	maxLength={3} size={5}/></label>
				<FaLongArrowAltRight className='icons'/>
				<label>Destino:<input type="text" 	placeholder="Destino" 	value={destiny} 	onChange={(e)=>setStateDestiny(e.target.value)} maxLength={3} size={5}/></label>
				<FaDollarSign className='icons'/>
				<label>Preço:<input type="number" 	placeholder="Preço" 		value={price} 		onChange={(e)=>setStatePrice(e.target.value)} 	maxLength={6} size={5} min="0.00" max="10000.00" step="0.01"/></label>
			</div>
			<div className='formLineDiv'>
				<button onClick={() => {window.location.href =('/Register/')}}>Voltar</button>
				<button onClick={(e)=>saveRoutes()}>Salvar</button>
			</div>
		</div>
	)
}