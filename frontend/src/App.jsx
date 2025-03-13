import './App.css'
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Cliente from './Pages/Cliente';
import Produto from './Pages/Produto';
import Venda from './Pages/Venda';

function App() {

  return (
    <Router>
      <Routes>
        <Route exact path='/' element={<Cliente />} />
        <Route exact path='/produto' element={<Produto />} />
        <Route exact path='/venda' element={<Venda />} />
      </Routes>
    </Router>
  )
}

export default App
