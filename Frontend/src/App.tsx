import React, { useState, type ChangeEvent, type SyntheticEvent } from 'react'
import './App.css';

import CardList from './components/Cardlist/CardList';
import Search from './components/Search/Search';

const App = () => {
    const [search, setSearch] = useState<string>("");

  const handleChange = (e: ChangeEvent<HTMLInputElement>) => {
    setSearch(e.target.value);
    console.log(e);
  };

  const onClick = (e: SyntheticEvent) => {
    console.log(e);
  };
  return (
    <div className="app">
      <Search onClick={onClick} search={search} handleChange={handleChange}/>
      <CardList />
    </div>
  )
}

export default App