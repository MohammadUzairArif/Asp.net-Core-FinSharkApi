import React from 'react'
import './App.css';

import CardList from './components/Cardlist/CardList';
import Search from './components/Search/Search';

const App = () => {
  return (
    <div className="app">
      <Search/>
      <CardList />
    </div>
  )
}

export default App