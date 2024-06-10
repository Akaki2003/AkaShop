import React, {
  createContext,
  useContext,
  useState,
  useEffect,
  useMemo,
} from "react";
import axios from "../api_client/axios";
import { APIURL } from "../Constants";
import { useNavigate } from "react-router-dom";
import { useLocalStorage } from "../hooks/useLocalStorage";
import { jwtDecode } from "jwt-decode";

const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
  const [user, setUser] = useLocalStorage("user", null);
  const navigate = useNavigate();

  const login = async (user) => {
    try {
      const url = APIURL + "User/Login";
      const response = await axios.post(url, user);
      const token = response.data.token;
      const decoded = jwtDecode(token);
      localStorage.setItem("jwt_authorization", token);
      setUser(decoded);
      navigate("/");
    } catch (error) {
      console.error(error);
    }
  };

  const register = async (user) => {
    try {
      const url = APIURL + "User/Register";
      const response = await axios.post(url, user);
      const token = response.data.token;
      const decoded = jwtDecode(token);
      localStorage.setItem("jwt_authorization", token);
      setUser(decoded);
      navigate("/");
    } catch (error) {
      console.error(error);
    }
  };

  const logout = async () => {
    try {
      localStorage.removeItem("jwt_authorization");
      setUser(null);
      navigate("/", { replace: true });
    } catch (error) {
      console.error(error);
    }
  };

  const value = useMemo(
    () => ({
      user,
      login,
      register,
      logout,
    }),
    [user]
  );

  return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>;
};

export const useAuth = () => {
  const context = useContext(AuthContext);
  return context;
};
