import { root } from '@lynx-js/react'
import { App } from './App.jsx'
import { AuthProvider } from './contexts/AuthContext.jsx'

root.render(
  <AuthProvider>
    <App />
  </AuthProvider>
)

if (import.meta.webpackHot) {
  import.meta.webpackHot.accept()
}
