import { createContext, useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { toast } from "react-toastify";
import React from "react";
import axios from "axios";
import type { UserProfile } from "../Models/User";
import { loginAPI, registerAPI } from "../Services/AuthService";


type UserContextType = {
  user: UserProfile | null;
  token: string | null;
  registerUser: (email: string, userName: string, password: string) => void;
  loginUser: (userName: string, password: string) => void;
  logout: () => void;
  isLoggedIn: () => boolean;
};

type Props = { children: React.ReactNode };

const UserContext = createContext<UserContextType>({} as UserContextType);

export const UserProvider = ({ children }: Props) => {
  const navigate = useNavigate();
  const [token, setToken] = useState<string | null>(null);
  const [user, setUser] = useState<UserProfile | null>(null);
  const [isReady, setIsReady] = useState(false);

  // ✅ Keep axios auth header in sync with token
  useEffect(() => {
    if (token) {
      axios.defaults.headers.common["Authorization"] = "Bearer " + token;
    } else {
      delete axios.defaults.headers.common["Authorization"];
    }
  }, [token]);

  // ✅ Load user/token from localStorage on first load
  useEffect(() => {
    const storedUser = localStorage.getItem("user");
    const storedToken = localStorage.getItem("token");
    if (storedUser && storedToken) {
      setUser(JSON.parse(storedUser));
      setToken(storedToken);
    }
    setIsReady(true);
  }, []);

  const registerUser = async (
    email: string,
    userName: string,
    password: string
  ) => {
    try {
      const res = await registerAPI(email, userName, password);
      if (res) {
        localStorage.setItem("token", res.data.token);
        const userObj = {
          userName: res.data.userName,
          email: res.data.email,
        };
        localStorage.setItem("user", JSON.stringify(userObj));
        setToken(res.data.token);
        setUser(userObj);
        toast.success("Registration & login successful!");
        navigate("/search");
      }
    } catch (e) {
      toast.warning("Server error occurred");
    }
  };

  const loginUser = async (userName: string, password: string) => {
    try {
      const res = await loginAPI(userName, password);
      if (res) {
        localStorage.setItem("token", res.data.token);
        const userObj = {
          userName: res.data.userName,
          email: res.data.email,
        };
        localStorage.setItem("user", JSON.stringify(userObj));
        setToken(res.data.token);
        setUser(userObj);
        toast.success("Login successful!");
        navigate("/search");
      }
    } catch (e) {
      toast.warning("Server error occurred");
    }
  };

  const isLoggedIn = () => !!user;

  const logout = () => {
    localStorage.removeItem("token");
    localStorage.removeItem("user");
    setUser(null);
    setToken(null);
    navigate("/");
  };

  return (
    <UserContext.Provider
      value={{ loginUser, user, token, logout, isLoggedIn, registerUser }}
    >
      {isReady ? children : null}
    </UserContext.Provider>
  );
};

export const useAuth = () => React.useContext(UserContext);