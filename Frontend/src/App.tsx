import React, { useState, type ChangeEvent, type SyntheticEvent } from 'react'
import './App.css';

import CardList from './components/Cardlist/CardList';
import Search from './components/Search/Search';
import type { CompanySearch } from './company';
import { searchCompanies } from './api';

const App = () => {
    const [search, setSearch] = useState<string>("");
  const [searchResult, setSearchResult] = useState<CompanySearch[]>([]);
  const [serverError, setServerError] = useState<string | null>(null);

  const handleChange = (e: ChangeEvent<HTMLInputElement>) => {
    setSearch(e.target.value);
   
  };

   const onClick = async (e: SyntheticEvent) => {
    const result = await searchCompanies(search);
    //setServerError(result.data);
    if (typeof result === "string") {
      setServerError(result);
    } else if (Array.isArray(result.data)) {
      setSearchResult(result.data);

    }
    
  };

  return (
    <div className="app">
      <Search onClick={onClick} search={search} handleChange={handleChange}/>
      <CardList searchResults={searchResult} />
        {serverError && <div>Unable to connect to API</div>}
      {/* {serverError ? <div>Connected</div> : <div>Unable to connect to api</div>} */}
    </div>
  )
}

export default App