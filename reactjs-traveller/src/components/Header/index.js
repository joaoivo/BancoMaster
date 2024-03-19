import React from "react";
import { Link } from 'react-router-dom';
import './header.css';
import mainLogo from '../../imgs/travel_explore.svg' ;
export default function Header(){
	return(
		<header className="main">
			<img src={mainLogo} className="mainLogo" alt="Traveller"/>
			<Link to="/">Traveller Header</Link>
			<Link to="/Register/">Cadastro e Consulta</Link>
			<Link to="/BestPrices/">Buscar Rotas</Link>
			<Link to="https://www.linkedin.com/in/jivo-ti-automation/" target="_Blank">Sobre</Link>
		</header>
	)
}