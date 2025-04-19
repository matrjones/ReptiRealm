import { createContext, useContext, useState, useEffect } from '@lynx-js/react'

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

  // Check if token has expired
  const isTokenExpired = (expirationDate: string) => {
    return new Date(expirationDate) < new Date()
  }

  // Check token expiration periodically
  useEffect(() => {
    if (!token || !expiration) return

    const checkExpiration = () => {
      if (isTokenExpired(expiration)) {
        logout()
      }
    }

    // Check immediately
    checkExpiration()

    // Set up interval to check every minute
    const interval = setInterval(checkExpiration, 60000)

    return () => clearInterval(interval)
  }, [token, expiration])

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
        // Check if token is already expired before setting it
        if (data.expiration && isTokenExpired(data.expiration)) {
          throw new Error('Token is expired')
        }
        
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