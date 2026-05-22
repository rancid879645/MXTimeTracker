import { AuthProvider } from './context/AuthContext'
import { useAuth } from './context/useAuth'
import Navbar from './components/Layout/Navbar'
import LoginPage from './pages/LoginPage'
import DashboardPage from './pages/DashboardPage'
import './styles.css'

function AppContent() {
  const { user } = useAuth()
  return (
    <>
      <Navbar />
      <main className="main-content">
        {user ? <DashboardPage /> : <LoginPage />}
      </main>
    </>
  )
}

export default function App() {
  return (
    <AuthProvider>
      <AppContent />
    </AuthProvider>
  )
}
