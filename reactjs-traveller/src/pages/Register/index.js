import {useState } from 'react';
import api from '../../services/api'

export default function Register(){
	///app.MapGet("/routes/Advanced/", ([FromQuery] Guid? guid,[FromQuery] string? origin, [FromQuery] string? destiny, [FromQuery] decimal? value) =>{

	const [origin,setStateOrigin] = useState('');
	const [destiny,setStateDestiny] = useState('');
	const [dataList,setStateDataList] = useState([]);
	async function searchRoutes(){
		var apiUrlParameters=[];
		console.log(origin);
		if(origin){apiUrlParameters["origin"]=origin;}
		if(destiny){apiUrlParameters["destiny"]=destiny;}
		console.log(apiUrlParameters);
		var apiUrl="/routes/Advanced/";
		console.log(apiUrlParameters.map((value,key)=>{console.log(key);return `${key}:${value}`}));
		if(apiUrlParameters){apiUrl+="?"+(apiUrlParameters.map((value,key)=>{return `${key}:${value}`}).join("&"));}

		console.log(apiUrl);
		const response = await api.get(apiUrl);
		if(!response){return;}
		console.log(response.data.dataList);
		setStateDataList(response.data.dataList);
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
								<div className='formLineDiv' id={rt.rtsGuid}>
									<label>Origem: <span>{rt.rtsOrigin}</span></label>
									<label>Destino: <span>{rt.rtsDestintion}</span></label>
									<label>Preço: <span>{rt.rtsPrice}</span></label>
								</div>
							)
						})
					}
				</div>
			</div>
		</div>
	)
}