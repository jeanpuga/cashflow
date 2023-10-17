import React,{ createContext, useContext, useState } from "react";
import { initialSession, Session } from "../models/Session";
import { fetcherCommandWithResult } from "./fetcher";

const API_BASE_URL = process.env.REACT_APP_API_BASE_URL;

export const SessionContext = createContext<{
  session:any; 
  setSession:Function;
  login:(user:string, password:string, navigate: Function, failback: Function) => Promise<any>}>(
  { session:{},setSession: () => {}, login: async () => {}}
);

export const useSessionContext = () => useContext(SessionContext);




export const SessionContextProvider: React.FC = (props) => {
  const [session, setSession] = useState(initialSession);
  const urlPost = () => `${API_BASE_URL}/Auth`;

  async function login(user:string, password:string, navigate: Function, failback: Function) {
    const getHeaders=()=>{
      const headers = new Headers();
      headers.append("accept", "*/*");
      headers.append("Content-Type", "application/json");
      return headers;
    }
    
    const setAuth = (result:Session)=>{
      setSession({ ...result, isAuthenticated: true });
      navigate();
    }
  
    const options = {
      method: "POST",
      headers: getHeaders(),
      body: JSON.stringify({ user,password}),
      redirect: "follow",
    } as RequestInit;

    return await fetcherCommandWithResult(urlPost(), options, setAuth, failback);
  }

  return (
    <SessionContext.Provider value={{session, setSession, login}}>
      {props.children}
    </SessionContext.Provider>
  );
}