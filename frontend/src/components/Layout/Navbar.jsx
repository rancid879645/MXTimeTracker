import { useAuth } from '../../context/useAuth'

export default function Navbar() {
  const { user, signOut } = useAuth()

  return (
    <nav className="navbar">
      <span className="navbar-brand">🏍 MX Lap Tracker</span>
      {user && (
        <div className="navbar-right">
          <span className="navbar-user">Hello, {user.username}</span>
          <button className="btn-logout" onClick={signOut}>Logout</button>
        </div>
      )}
    </nav>
  )
}
