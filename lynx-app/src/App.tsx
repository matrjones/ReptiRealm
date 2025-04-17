import { useCallback, useEffect, useState } from '@lynx-js/react'
import './App.css'

export function App() {
  const [activeTab, setActiveTab] = useState('dashboard')
  const [reptiles, setReptiles] = useState<string[]>([
    'Ra',
    'Kaiba',
    'Lucifer',
    'Anubis',
    'Iris'
  ])
  const [searchQuery, setSearchQuery] = useState('')

  const filteredReptiles = reptiles.filter(reptile =>
    reptile.toLowerCase().startsWith(searchQuery.toLowerCase())
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
