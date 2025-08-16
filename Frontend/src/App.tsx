
import './App.css';
import Navbar from './components/Navbar/Navbar';
import { Outlet } from 'react-router';


const App = () => {
  return (
     <>
      <Navbar />
      <Outlet />
   
    </>
  )
}

export default App