import { useState } from '@lynx-js/react'
import type { ChangeEvent } from 'react'
import './App.css'
import { useAuth } from './contexts/AuthContext.jsx'
import { Home } from './Home.jsx'

export function App() {
  const [email, setEmail] = useState('')
  const [password, setPassword] = useState('')
  const { token, login } = useAuth()

  const handleLogin = async () => {
    try {
      await login(email, password)
      // Login successful, no need to do anything else as the token will trigger a re-render
    } catch (error) {
      // Handle login error
      console.error('Login failed:', error)
    }
  }

  const handleEmailChange = (e: any) => {
    setEmail(e.detail?.value)
  }

  const handlePasswordChange = (e: any) => {
    setPassword(e.detail?.value)
  }

  // If we have a token, show the Home page, otherwise show the login page
  if (token) {
    return <Home />
  }

  return (
    <view className="app-container">
      <view className="login-container">
        <text className="login-title">Welcome to ReptiRealm</text>
        <view className="input-group">
          <input
            className="login-input"
            value={email}
            bindinput={handleEmailChange}
            placeholder="Email"
          />
        </view>
        <view className="input-group">
          <input
            className="login-input"
            value={password}
            bindinput={handlePasswordChange}
            placeholder="Password"
            type="password"
          />
        </view>
        <view className="login-button" bindtap={handleLogin}>
          <text className="login-button-text">Login</text>
        </view>
      </view>
    </view>
  )
}
