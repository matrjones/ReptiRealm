import { useCallback, useEffect, useState } from '@lynx-js/react'
import './App.css'

export function App() {
  const [activeTab, setActiveTab] = useState('dashboard')
  const [reptiles, setReptiles] = useState<string[]>([])
  const [searchQuery, setSearchQuery] = useState('')

  useEffect(() => {
    const fetchReptiles = async () => {
      try {
        const response = await fetch('https://api-stage.pineappleexplorers.com/Reptile/GetAll', {
          headers: {
            'Authorization': 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiQXJyYW40NTY3QGdtYWlsLmNvbSIsImp0aSI6ImYwYjY2MmM5LWI4NTEtNDI3NC05N2ZiLWM0NWJmZWEwYjVkMyIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IlVzZXIiLCJleHAiOjE3NDQ5OTI1OTksImlzcyI6Imh0dHBzOi8vYXBpLXN0YWdlLnBpbmVhcHBsZWV4cGxvcmVycy5jb20vIiwiYXVkIjoiaHR0cHM6Ly9hcGktc3RhZ2UucGluZWFwcGxlZXhwbG9yZXJzLmNvbS8ifQ.MFr93VMz7Yf4rwlGCH3Cj8OFYLDzkEVV2l-Qw3VG7fQ'
          }
        })
        const data = await response.json()
        setReptiles(data.map((reptile: any) => reptile.name))
      } catch (error) {
        throw error
      }
    }

    fetchReptiles()
  }, [])

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
