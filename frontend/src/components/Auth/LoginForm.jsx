import { useState } from 'react'
import { login } from '../../api/auth'
import { useAuth } from '../../context/useAuth'

export default function LoginForm({ onSwitch }) {
  const { signIn } = useAuth()
  const [form, setForm] = useState({ username: '', password: '' })
  const [error, setError] = useState('')
  const [loading, setLoading] = useState(false)

  const handleChange = (e) => setForm({ ...form, [e.target.name]: e.target.value })

  const handleSubmit = async (e) => {
    e.preventDefault()
    setError('')
    setLoading(true)
    try {
      const data = await login(form)
      signIn(data)
    } catch (err) {
      setError(err.message)
    } finally {
      setLoading(false)
    }
  }

  return (
    <form className="auth-form" onSubmit={handleSubmit}>
      <h2>Login</h2>
      {error && <p className="error">{error}</p>}
      <input name="username" placeholder="Username" value={form.username} onChange={handleChange} required />
      <input name="password" type="password" placeholder="Password" value={form.password} onChange={handleChange} required />
      <button type="submit" disabled={loading}>{loading ? 'Logging in...' : 'Login'}</button>
      <p className="switch-link">
        No account? <span onClick={onSwitch}>Register</span>
      </p>
    </form>
  )
}
