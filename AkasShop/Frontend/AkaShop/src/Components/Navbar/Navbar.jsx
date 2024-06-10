import { Link, useMatch, useResolvedPath, useNavigate } from "react-router-dom";
import { useAuth } from "../../context/AuthContext";
import { jwtDecode } from "jwt-decode";

const Navbar = () => {
  const { logout } = useAuth();

  const handleLogout = () => {
    logout();
  };

  const token = localStorage.getItem("jwt_authorization");
  const decodedToken = token ? jwtDecode(token) : null;

  return (
    <nav className="nav">
      <Link to="/" className="site-title">
        <img
          className="logo"
          src="/akashop-high-resolution-logo.png"
          alt="logo"
        />
      </Link>
      <ul>
        {decodedToken && <Link to="/profile">{decodedToken.email}</Link>}
        {decodedToken ? (
          <Link to="/" onClick={handleLogout}>
            Logout
          </Link>
        ) : (
          <>
            <CustomLink to="/login">Login</CustomLink>
            <CustomLink to="/register">Register</CustomLink>
          </>
        )}
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
