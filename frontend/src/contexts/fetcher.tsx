import React from "react";

const DEBUG = process.env.REACT_APP_FETCHER_DEBUG === "true";

const fetcherQuery = async (
    url:string, 
    options?:RequestInit, 
    set?:React.Dispatch<React.SetStateAction<any>>, 
    callback?:Function, 
    failback?:Function
  ) => {
  try {
    const response = await fetch(url, options);
    const result = await response.json();

    if (!!set) {
      set(result);
    }

    if (!!callback) {
      return await callback(result);
    }
    else {
      if (!!DEBUG) console.log(`%cOK query  ${url}`, 'background: LawnGreen; color: Black; font-weight: bold;');
    }
  } catch (error) {
    if (!!DEBUG) console.log(`%cAn error ${error} occured command ${url}`, 'background: IndianRed; color: Black; font-weight: bold;');
    if (!!failback) {
      return await failback()
    }
  }
};

const fetcherCommand = async (
    url:string, 
    options?:RequestInit, 
    callback?:Function, 
    failback?:Function
  ) => {
  try {
    await fetch(url, options);
    if (!!callback) {
      return await callback();
    }
    else {
      if (!!DEBUG) console.log(`%cOK command  ${url}`, 'background: LawnGreen; color: Black; font-weight: bold;');
    }
  } catch (error) {
    if (!!DEBUG) console.log(`%cAn error ${error} occured command ${url}`, 'background: IndianRed; color: Black; font-weight: bold;');

    if (!!failback) {
      return await failback()
    }
  }
};

const fetcherCommandWithResult = async (
  url:string, 
  options?:RequestInit, 
  callback?:Function, 
  failback?:Function
) => {
  try {
    const response = await fetch(url, options);
    const result = await response.json();

    if (!!callback && response.status===200) {
      return await callback(result);
    }
    else {
      if (!!DEBUG) console.log(`%cOK command  ${url}`, 'background: LawnGreen; color: Black; font-weight: bold;');
      if (!!failback) {
        return await failback()
      }
    }
  } catch (error) {
    if (!!DEBUG) console.log(`%cAn error ${error} occured command ${url}`, 'background: IndianRed; color: Black; font-weight: bold;');

    if (!!failback) {
      return await failback()
    }
  }
};
export { fetcherQuery, fetcherCommand, fetcherCommandWithResult};

