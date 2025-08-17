
import './App.css';
import Navbar from './components/Navbar/Navbar';
import { Outlet } from 'react-router';
import "react-toastify/dist/ReactToastify.css";
import { ToastContainer } from "react-toastify";
import { UserProvider } from './Context/useAuth';

const App = () => {
  return (
     <>
      <UserProvider>
        <Navbar />
        <Outlet />
        <ToastContainer />
      </UserProvider>
    </>
  )
}

export default App