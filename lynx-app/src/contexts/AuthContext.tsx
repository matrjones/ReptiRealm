import { createContext, useContext, useState } from '@lynx-js/react'

interface AuthContextType {
  token: string | null
  expiration: string | null
  username: string | null
  login: (email: string, password: string) => Promise<void>
  logout: () => void
}

const AuthContext = createContext<AuthContextType | null>(null)

export function AuthProvider({ children }: { children: React.ReactNode }) {
  const [token, setToken] = useState<string | null>(null)
  const [expiration, setExpiration] = useState<string | null>(null)
  const [username, setUsername] = useState<string | null>(null)

  const login = async (email: string, password: string) => {
    try {
      const response = await fetch('https://api-stage.pineappleexplorers.com/Identity/Login', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json'
        },
        body: JSON.stringify({ email, password })
      })
      const data = await response.json()
      
      if (response.ok) {
        setToken(data.token)
        setExpiration(data.expiration)
        setUsername(data.username)
      } else {
        throw new Error(data.message || 'Login failed')
      }
    } catch (error) {
      throw error
    }
  }

  const logout = () => {
    setToken(null)
    setExpiration(null)
    setUsername(null)
  }

  return (
    <AuthContext.Provider value={{ token, expiration, username, login, logout }}>
      {children}
    </AuthContext.Provider>
  )
}

export function useAuth() {
  const context = useContext(AuthContext)
  if (!context) {
    throw new Error('useAuth must be used within an AuthProvider')
  }
  return context
} 