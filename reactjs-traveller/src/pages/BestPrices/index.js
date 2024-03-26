import {useState } from 'react';
import { FaLongArrowAltRight,FaDotCircle,FaDollarSign, FaAmazonPay } from "react-icons/fa";
import { MdEditSquare,MdDeleteForever  } from "react-icons/md";
import api from '../../services/api'
import {  toast } from 'react-toastify';

export default function BestPrices(){
	const [origin	,setStateOrigin	] = useState('');
	const [destiny	,setStateDestiny	] = useState('');
	const [dataList,setStateDataList	] = useState([]);

	async function searchRoutes(){
		console.clear();
		var apiUrl=`/routes/bestprice/${origin}/${destiny}`;
		const response = await api.get(apiUrl);
		console.log('====================================');
		console.log(response);
		console.log('====================================');
		if(!response){return;}
		setStateDataList(response.data.dataList);
	}

	return(
		<div>
			<h1>Buscar melhor preço</h1>
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
								<div className='formLineDiv'  key={`div_${idx}`}>
									<FaDotCircle className='icons'/>
									<label><span>{rt.route}</span></label>
									<FaDollarSign className='icons'/>
									<label> <span>{rt.price}</span></label>
								</div>
							)
						})
					}
				</div>
		</div>
	)
}