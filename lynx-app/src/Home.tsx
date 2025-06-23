import { useCallback, useEffect, useState } from '@lynx-js/react'
import './Home.css'
import { useAuth } from './contexts/AuthContext.jsx'

export function Home() {
  const [activeTab, setActiveTab] = useState('dashboard')
  const [reptiles, setReptiles] = useState<string[]>([])
  const [searchQuery, setSearchQuery] = useState('')
  const { token } = useAuth()

  useEffect(() => {
    const fetchReptiles = async () => {
      try {
        const response = await fetch('https://api-stage.pineappleexplorers.com/Reptile/GetAll', {
          headers: {
            'Authorization': `Bearer ${token}`
          }
        })
        const data = await response.json()
        setReptiles(data.map((reptile: any) => reptile.name))
      } catch (error) {
        throw error
      }
    }

    if (token) {
      fetchReptiles()
    }
  }, [token])

  const filteredReptiles = reptiles.filter(reptile => 
    reptile.toLowerCase().includes(searchQuery.toLowerCase())
  )

  const handleSearchChange = (e: any) => {
    setSearchQuery(e.detail?.value)
  }

  const handleAddReptile = () => {
    throw new Error('Not implemented')
  }

  return (
    <view className="app-container">
      <view className="banner">
        <text className="banner-text">ReptiRealm</text>
      </view>

      <view className="main-content">
        <view className="search-container">
          <input
            className="search-bar"
            value={searchQuery}
            bindinput={handleSearchChange}
            placeholder="Search reptiles..."
          />
        </view>

        <view className="reptiles-list">
          {filteredReptiles.map((reptile, index) => (
            <view key={index} className="reptile-button">
              <text className="reptile-name">{reptile}</text>
            </view>
          ))}
          {!searchQuery && (
            <view className="reptile-button add-button" bindtap={handleAddReptile}>
              <text className="add-text">+</text>
            </view>
          )}
        </view>
      </view>

      <view className="navigation">
        <view className="nav-item" bindtap={() => setActiveTab('dashboard')}>
          <text className="nav-text">Dashboard</text>
        </view>
        <view className="nav-item" bindtap={() => setActiveTab('reptiles')}>
          <text className="nav-text">My Reptiles</text>
        </view>
        <view className="nav-item" bindtap={() => setActiveTab('settings')}>
          <text className="nav-text">Settings</text>
        </view>
      </view>
    </view>
  )
} 