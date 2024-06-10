import React, { useContext, useState } from "react";
import { APIURL } from "../../Constants";
import axios from "axios";
import { jwtDecode } from "jwt-decode";
import { AppContext } from "../../App";
import { useAuth } from "../../context/AuthContext";
import { useNavigate } from "react-router-dom";

const Login = () => {
  const navigate = useNavigate();
  const { login } = useAuth();
  // const login = useAuth();
  const appContext = useContext(AppContext);
  const cookies = appContext.cookies;
  const [user, setUser] = useState({
    email: "Admin@gmail.com",
    password: "Admin123", //needs to be removed
  });

  const handleChange = (e) => {
    setUser({ ...user, [e.target.name]: e.target.value });
  };
  const handleLogin = async () => {
    if (user) {
      const email = user.email;
      console.log("user in Login.jsx:" + JSON.stringify(user));
      await login(user);
    }
  };
  return (
    <div className="loginPage">
      <div className="form-container">
        <div className="loginForm">
          <h1 className="loginText">Sign in</h1>
          <form>
            <div>
              <div className="form-group">
                <input
                  type="email"
                  name="email"
                  placeholder="Email"
                  required
                  value={user.email}
                  onChange={handleChange}
                />
              </div>
              <div className="form-group">
                <input
                  type="password"
                  name="password"
                  required
                  placeholder="Password"
                  value={user.password}
                  onChange={handleChange}
                />
              </div>
            </div>
          </form>
          <div className="loginBtn">
            <button type="button" onClick={handleLogin}>
              Login
            </button>
          </div>
        </div>
      </div>
    </div>
  );
};

export default Login;
