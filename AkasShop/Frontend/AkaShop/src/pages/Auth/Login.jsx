import React, { useContext, useState } from "react";
import { useAuth } from "../../context/AuthContext";

const Login = () => {
  const { login } = useAuth();
  const [user, setUser] = useState({
    email: "Admin@gmail.com",
    password: "Admin123!",
  });

  const handleChange = (e) => {
    setUser({ ...user, [e.target.name]: e.target.value });
  };
  const handleLogin = async () => {
    if (user) {
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
