import { useState } from 'react'
import LoginForm from '../components/Auth/LoginForm'
import RegisterForm from '../components/Auth/RegisterForm'

export default function LoginPage() {
  const [mode, setMode] = useState('login')

  return (
    <div className="auth-page">
      <div className="auth-card">
        <div className="auth-logo">🏍</div>
        <h1>MX Lap Tracker</h1>
        {mode === 'login'
          ? <LoginForm onSwitch={() => setMode('register')} />
          : <RegisterForm onSwitch={() => setMode('login')} />}
      </div>
    </div>
  )
}
