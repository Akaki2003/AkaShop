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
import { AppContext } from "../App";

const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
  const [user, setUser] = useLocalStorage("user", null);
  const appContext = useContext(AppContext);
  const cookies = appContext.cookies;
  const [loading, setLoading] = useState(true);
  const navigate = useNavigate();

  useEffect(() => {
    const fetchUser = async () => {
      try {
        const response = await axios.get(APIURL + "User", {
          withCredentials: true,
        });
        setUser(response.data);
      } catch (error) {
        console.error(error);
      } finally {
        setLoading(false);
      }
    };

    fetchUser();
  }, []);

  const login = async (user) => {
    console.log("user in AuthContext.jsx:" + JSON.stringify(user));
    try {
      const url = APIURL + "User/Login";
      await axios.post(url, user).then((response) => {
        const token = response.data.token;
        const decoded = jwtDecode(token);
        cookies.set("jwt_authorization", token, {
          expires: new Date(decoded.exp * 1000),
        });
        setUser(response.data);
        navigate("/");
      });
    } catch (error) {
      console.error(error);
    }
  };

  const register = async (user) => {
    console.log("user in AuthContext.jsx:" + JSON.stringify(user));
    try {
      const url = APIURL + "User/Register";
      await axios.post(url, user).then((response) => {
        const token = response.data.token;
        const decoded = jwtDecode(token);
        cookies.set("jwt_authorization", token, {
          expires: new Date(decoded.exp * 1000),
        });
        setUser(response.data);
        navigate("/");
      });
    } catch (error) {
      console.error(error);
    }
  };

  const logout = async () => {
    try {
      setUser(null);
      cookies.remove("jwt_authorization");
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
