import './App.css'
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Cliente from './Pages/Cliente';

function App() {

  return (
    <Router>
      <Routes>
        <Route exact path='/' element={<Cliente />} />
      </Routes>
    </Router>
  )
}

export default App
