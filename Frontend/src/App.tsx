
import './App.css';
import Navbar from './components/Navbar/Navbar';
import { Outlet } from 'react-router';
import "react-toastify/dist/ReactToastify.css";
import { ToastContainer } from "react-toastify";

const App = () => {
  return (
     <>
      <Navbar />
      <Outlet />
      <ToastContainer/>
    </>
  )
}

export default App