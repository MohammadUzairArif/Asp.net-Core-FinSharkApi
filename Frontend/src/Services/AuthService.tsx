import axios from "axios";
import type { UserProfileToken } from "../Models/User";
import { handleError } from "../Helpers/ErrorHandler";



const api = "https://localhost:7037/api/";

export const loginAPI = async (userName: string, password: string) => {
  try {
    const data = await axios.post<UserProfileToken>(api + "account/login", {
      userName: userName,
      password: password,
    });
    return data;
  } catch (error) {
    handleError(error);
  }
};

export const registerAPI = async (
  email: string,
  userName: string,
  password: string
) => {
  try {
    const data = await axios.post<UserProfileToken>(api + "account/register", {
      email: email,
      userName: userName,
      password: password,
    });
    return data;
  } catch (error) {
    handleError(error);
  }
};