import {BrowserRouter, Routes, Route} from 'react-router-dom';

import Header from "./components/Header";
import Footer from "./components/Footer";

import Home from './pages/Home';
import BestPrices from './pages/BestPrices';
import Register from './pages/Register'
import RouteData from './pages/RouteData';

import NotFound from './pages/NotFound';
import Error from './pages/Error';

export default function RoutesApp(){
	return(
		<BrowserRouter>
			<Header/>
			<Routes>
				
				<Route path='/' element={<Home/>} />
				<Route path='/BestPrices' element={<BestPrices/>} />
				<Route path='/Register' element={<Register/>} />
				<Route path='/Route/:id' element={<RouteData/>} />

				<Route path='/Error' element={<Error/>} />
				<Route path='*' element={<NotFound/>} />
			</Routes>
			<Footer/>
		</BrowserRouter>
	)
}