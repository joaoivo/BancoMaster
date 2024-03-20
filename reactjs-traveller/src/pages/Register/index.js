import {useState } from 'react';
import { FaLongArrowAltRight,FaDotCircle,FaDollarSign } from "react-icons/fa";
import { MdEditSquare } from "react-icons/md";
import api from '../../services/api'

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

	const editRoutes = (guid)=>{
		alert("bom");
	}
	return(
		<div>
			<h1>Cadastro e Consulta de Rotas</h1>
			<div>
				<div className='formLineDiv'>
					<label>Origem: <input type="text" placeholder="Origem" value={origin} onChange={(e)=>setStateOrigin(e.target.value)} maxLength={3} size={5}/></label>
					<label>Destino: <input type="text" placeholder="Destino" value={destiny} onChange={(e)=>setStateDestiny(e.target.value)} maxLength={3} size={5}/></label>
					<button onClick={(e)=>searchRoutes()}>Pesquisar</button>
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
									<MdEditSquare className='iconButton' onClick={editRoutes}/>
								</div>
							)
						})
					}
				</div>
			</div>
		</div>
	)
}