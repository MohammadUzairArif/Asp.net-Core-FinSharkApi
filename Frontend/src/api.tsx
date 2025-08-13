import axios from "axios";
import type { CompanySearch } from "./company.d.ts";

interface SearchResponse {
  data: CompanySearch[];
}

export const searchCompanies = async (query: string) => {
  try {
    const data = await axios.get<SearchResponse>(
      `https://financialmodelingprep.com/stable/search-symbol?query=${query}&apikey=${process.env.REACT_APP_API_KEY}`
    );
    return data;
  } catch (error) {
    if ((axios as any).isAxiosError && (axios as any).isAxiosError(error)) {
      const errorMessage = (error as Error).message;
      console.log("error message: ", errorMessage);
      return errorMessage;
    } else {
      console.log("unexpected error: ", error);
      return "An expected error has occured.";
    }
  }
};