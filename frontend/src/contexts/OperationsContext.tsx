import React, { createContext, useContext, useState, useEffect } from "react";
import { fetcherCommand, fetcherQuery } from "./fetcher";
import { IOperation } from "../models/IOperation";
import { useSessionContext } from "./SessionContext";

const API_BASE_URL = process.env.REACT_APP_API_BASE_URL;

export const OperationsContext = createContext<{
  operationsTodayData: any;
  operationsConsolidateData: any;
  operationsBalanceData: any;
  getOperationsToday: (refresh: Function) => Promise<any>;
  getOperationsConsolidate: (refresh: Function) => Promise<any>;
  getOperationsBalance: (refresh: Function) => Promise<any>;
  addOperation: (payload: IOperation, callback: Function, failback:Function) => Promise<any>;
}>({
  operationsTodayData: [],
  operationsConsolidateData: [],
  operationsBalanceData: [],
  getOperationsToday: async () => {},
  getOperationsConsolidate: async () => {},
  getOperationsBalance: async () => {},
  addOperation: async () => {},
});

export const useOperationsContext = () => useContext(OperationsContext);

const GetHeaders = (token: string): Headers => {
  const headers = new Headers();
  headers.append("accept", "*/*");
  headers.append("Content-Type", "application/json");
  headers.append("Authorization", `Bearer ${token}`);
  return headers;
};

export const OperationsContextProvider: React.FC = (props) => {
  const [operationsTodayData, setOperationsTodayData] = useState([]);
  const [operationsConsolidateData, setOperationsConsolidateData] = useState([]);
  const [operationsBalanceData, setOperationsBalanceData] = useState();
  
  const {session} = useSessionContext();
 

  const urlPost = () => `${API_BASE_URL}/Operations`;
  const urlGetToday = () => `${API_BASE_URL}/Operations?Type=Today&AccountId=1`;
  const urlGetConsolidate = () => `${API_BASE_URL}/Operations?Type=Consolidate&AccountId=1`;
  const urlGetBalance = () => `${API_BASE_URL}/Operations?Type=Balance&AccountId=1`;

  useEffect(()=>{
    if(!!session && session.isAuthenticated){
      getOperationsToday(()=>{});
      getOperationsBalance(()=>{});
    }
  },[session]);
  
  

  async function getOperationsToday(refresh: Function): Promise<any> {
    const _headers = GetHeaders(session.token);
    const options = {
      method: "GET",
      headers: _headers,
      redirect: "follow",
    } as RequestInit;

    return await fetcherQuery(urlGetToday(), options, setOperationsTodayData, refresh);
  }

  async function getOperationsConsolidate(refresh: Function): Promise<any> {
    const _headers = GetHeaders(session.token);
    const options = {
      method: "GET",
      headers: _headers,
      redirect: "follow",
    } as RequestInit;

    return await fetcherQuery(urlGetConsolidate(), options, setOperationsConsolidateData, refresh);
  }

  async function getOperationsBalance(refresh: Function): Promise<any> {
    const _headers = GetHeaders(session.token);
    const options = {
      method: "GET",
      headers: _headers,
      redirect: "follow",
    } as RequestInit;

    return await fetcherQuery(urlGetBalance(), options, setOperationsBalanceData, refresh);
  }

  async function addOperation(payload: IOperation, callback: Function, failback:Function) {
    const _headers = GetHeaders(session.token);
   
    const options = {
      method: "POST",
      headers: _headers,
      body: JSON.stringify(payload),
      redirect: "follow",
    } as RequestInit;

    return await fetcherCommand(urlPost(), options, callback, failback);
  }

  
  return (
    <OperationsContext.Provider value={{ operationsTodayData, operationsConsolidateData, operationsBalanceData, getOperationsToday, getOperationsConsolidate, getOperationsBalance, addOperation }} >
      {props.children}
    </OperationsContext.Provider>
  );
};
