import './App.css'
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Cliente from './Pages/Cliente';
import Produto from './Pages/Produto';

function App() {

  return (
    <Router>
      <Routes>
        <Route exact path='/' element={<Cliente />} />
        <Route exact path='/produto' element={<Produto />} />
      </Routes>
    </Router>
  )
}

export default App
