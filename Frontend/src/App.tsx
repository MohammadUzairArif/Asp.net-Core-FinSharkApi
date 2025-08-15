import React, { useState, type ChangeEvent, type SyntheticEvent } from 'react'
import './App.css';

import CardList from './components/Cardlist/CardList';
import Search from './components/Search/Search';
import type { CompanySearch } from './company';
import { searchCompanies } from './api';
import ListPortfolio from './components/portfolio/ListPortfolio/ListPortfolio';

const App = () => {
    const [search, setSearch] = useState<string>("");
  const [searchResult, setSearchResult] = useState<CompanySearch[]>([]);
  const [portfolioValues, setPortfolioValues] = useState<string[]>([]);
  const [serverError, setServerError] = useState<string | null>(null);

   const handleSearchChange = (e: ChangeEvent<HTMLInputElement>) => {
    setSearch(e.target.value);
   
  };

    const onPortfolioCreate = (e: any) => {
    e.preventDefault();
     //DO NOT DO THIS
    // portfolioValues.push(event.target[0].value)
    // setPortfolioValues(portfolioValues);
    const exists = portfolioValues.find((value) => value === e.target[0].value);
    if (exists) return;
    const updatedPortfolio = [...portfolioValues, e.target[0].value];
    setPortfolioValues(updatedPortfolio);
  };

  const onPortfolioDelete = (e: any) => {
    e.preventDefault();
    const removed = portfolioValues.filter((value) => {
      return value !== e.target[0].value;
    });
    setPortfolioValues(removed);
  };

  const onSearchSubmit = async (e: SyntheticEvent) => {
    e.preventDefault();
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
     <Search
        onSearchSubmit={onSearchSubmit}
        search={search}
        handleSearchChange={handleSearchChange}
      />
      <ListPortfolio portfolioValues={portfolioValues} onPortfolioDelete={onPortfolioDelete} />
      <CardList
        searchResults={searchResult}
        onPortfolioCreate={onPortfolioCreate}
      />
        {serverError && <div>Unable to connect to API</div>}
      {/* {serverError ? <div>Connected</div> : <div>Unable to connect to api</div>} */}
    </div>
  )
}

export default App