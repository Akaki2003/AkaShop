import { useContext } from "react";
import { Link, useMatch, useResolvedPath, useNavigate } from "react-router-dom";
import { AppContext } from "../../App";
import { useAuth } from "../../context/AuthContext";
import { jwtDecode } from "jwt-decode";

const Navbar = () => {
  const appContext = useContext(AppContext);
  const { logout } = useAuth();
  const cookies = appContext.cookies;
  const navigate = useNavigate();

  const handleLogout = () => {
    logout();
  };

  return (
    <nav className="nav">
      <Link to="/" className="site-title">
        <img className="logo" src="/akashop-high-resolution-logo.png"></img>
      </Link>
      <ul>
        {cookies.get("jwt_authorization") && (
          <Link to="/profile">
            {jwtDecode(cookies.get("jwt_authorization")).email}
          </Link>
        )}
        {cookies.get("jwt_authorization") ? (
          <Link to="/" onClick={handleLogout}>
            Logout
          </Link>
        ) : (
          <>
            <CustomLink to="/login">Login</CustomLink>
            <CustomLink to="/register">Register</CustomLink>
          </>
        )}
        {/* <h2>Welcome{user && user.email}!</h2> */}
        <CustomLink to="/pricing">Pricing</CustomLink>
        <CustomLink to="/about">About</CustomLink>
      </ul>
    </nav>
  );
};

export default Navbar;

function CustomLink({ to, children, ...props }) {
  const resolvedPath = useResolvedPath(to);
  const isActive = useMatch({ path: resolvedPath.pathname, end: true });
  return (
    <li className={isActive ? "active" : ""}>
      <Link to={to} {...props}>
        {children}
      </Link>
    </li>
  );
}
