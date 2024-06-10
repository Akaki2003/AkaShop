import React, { useContext, useState } from "react";
import { useAuth } from "../../context/AuthContext";

const Register = () => {
  const { register } = useAuth();

  const [user, setUser] = useState({
    email: "",
    password: "",
  });

  const handleChange = (e) => {
    setUser({ ...user, [e.target.name]: e.target.value });
  };

  const handleRegister = async () => {
    if (user) {
      await register(user);
    }
  };

  return (
    <div className="registerPage">
      <div className="form-container">
        <div className="registerForm">
          <h1 className="registerText">Sign up</h1>
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
          <div className="registerBtn">
            <button type="button" onClick={handleRegister}>
              Register
            </button>
          </div>
        </div>
      </div>
    </div>
  );
};

export default Register;
