import {useState } from 'react';
import api from '../../services/api'

export default function Register(){
	///app.MapGet("/routes/Advanced/", ([FromQuery] Guid? guid,[FromQuery] string? origin, [FromQuery] string? destiny, [FromQuery] decimal? value) =>{

	const [origin,setStateOrigin] = useState('');
	const [destiny,setStateDestiny] = useState('');
	async function searchRoutes(){
		alert("comecou");
		const respose = await api.get("/routes/Advanced/",{headers: {"Access-Control-Allow-Origin": "*"}});
		console.log(respose);
		alert('terminou');
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
			</div>
		</div>
	)
}