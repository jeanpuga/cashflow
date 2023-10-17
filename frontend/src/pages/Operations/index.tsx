import React, { useEffect, useState, useContext, ChangeEvent} from "react";
import { useNavigate } from "react-router";
import { Button } from "../../components/Button";
import { useSessionContext } from "../../contexts/SessionContext";
import { OperationsContext, useOperationsContext } from "../../contexts/OperationsContext";

import { Backcenter, Box } from "../../components/Backgrounds";
import { Logotype } from "../../components/Logotype";
import { FormControlLabel,  Radio, RadioGroup, TextField, Button as ButtomMui, Typography  } from '@mui/material';
import Fab from '@mui/material/Fab';
import CheckIcon from '@mui/icons-material/Check';
import LogoutTwoToneIcon from '@mui/icons-material/LogoutTwoTone';
import { DataGrid } from '@mui/x-data-grid';
import { columnsConsolidateConfig, columnsTodayConfig } from "./DatagridConfig";

import CurrencyFormat from "react-currency-format";

import "./style.css"
import { mock } from "./mock";
import { IOperation } from "../../models/IOperation";
import { OperationType } from "../../models/OperationType";

import Snack from '../../components/Snack'
import { Severety } from "../../components/Snack/Severety";

enum ViewType{
  ReportToday,
  ReportConsolidate,
}
export default function Operations() {
  const { operationsTodayData, operationsConsolidateData, operationsBalanceData, getOperationsToday, getOperationsConsolidate, getOperationsBalance, addOperation } = useContext(OperationsContext);
  const {session, setSession} = useSessionContext();

  const [transactionDescribe, setTransactionDescribe] = useState('')
  const [transactionValue, setTransactionValue] = useState(0)
  const [transactionType, setTransactionType] = useState(1)
  
  const [viewType, setViewType] = useState(ViewType.ReportToday)
  const [viewMessageHeader, setViewMessageHeader] = useState('Visão Consolidada')
  const [viewMessageButton, setViewMessageButton] = useState('Acompanhamento de Hoje')
  const [columnsConfig, setColumnsConfig] = useState(columnsTodayConfig)
  
  const navigate = useNavigate();
  const [dataOperation, setDataOperation] = useState([...mock]);
  const [dataOperationBalance, setDataOperationBalance] = useState('R$ 0,00');
  const initMessageSnack = { message: "", severety: Severety.INFO };
  const [messageSnack, setMessageSnack] = useState(initMessageSnack);

  useEffect(()=>{
    if(operationsTodayData && !!operationsTodayData?.result ){
      setDataOperation(operationsTodayData.result);
      setColumnsConfig(columnsTodayConfig)
    }
  },[operationsTodayData]);

  useEffect(()=>{
    if(operationsBalanceData && !!operationsBalanceData?.result && !!operationsBalanceData?.result?.length ){
      const {value} =  operationsBalanceData?.result[0];
      const currency =  new Intl.NumberFormat('pt-BR', { style: 'currency', currency: 'BRL' }).format(value);
      setDataOperationBalance(currency)
    }
  },[operationsBalanceData]);


  useEffect(()=>{
    if(operationsConsolidateData && !!operationsConsolidateData?.result ){
      setDataOperation(operationsConsolidateData.result);
      setColumnsConfig(columnsConsolidateConfig)
    }
  },[operationsConsolidateData]);



  useEffect(()=>{
    if(viewType===ViewType.ReportToday){
      setViewMessageButton('Clique para visão Consolidada')
      setViewMessageHeader('Extrato de hoje')
      getOperationsToday(()=>setMessageSnack({ message: 'Extrato do dia carregado!', severety: Severety.INFO }))
    }else{
      setViewMessageButton('Clique para visão Detalhada de Hoje')
      setViewMessageHeader('Extrato Consolidado')
      getOperationsConsolidate(()=>setMessageSnack({ message: 'Extrato consolidado carregado!', severety: Severety.INFO }))
    }
  },[viewType]);

  useEffect(()=>{
    clearOperation()
  },[dataOperation])

  const handleChangeReport = () => {
    if(viewType===ViewType.ReportToday){
      setViewType(ViewType.ReportConsolidate)
    }else{
      setViewType(ViewType.ReportToday)
    }
  };

  const handleLogout = () => {
    setSession({ ...session, isAuthenticated: false });
    navigate('/');
  };

  
  const handleChangeDescribeTransaction = (event: ChangeEvent<HTMLInputElement>) => {
    let newValue = event.target.value;
    setTransactionDescribe(newValue);
  };
  
  const handleChangeValueTransaction = (event: ChangeEvent<HTMLInputElement>) => {
    let newValue = event.target.value;
    if(!newValue)return;
    let strip = newValue.split(' ');
    if(!(strip?.length??null))return;
    let valueFloat =parseFloat(strip[1].replaceAll('.','').replaceAll(',','.')) 
    setTransactionValue(valueFloat);
  };
  
  const handleChangeTypeTransaction = (event: ChangeEvent<HTMLInputElement>) => {
    let newValue = parseInt(event.target.value);
    setTransactionType(newValue);
  };
  
  const handleAddTransaction = () => {
    const newValue:IOperation = {
      "describe":transactionDescribe, 
      "value":transactionValue, 
      "type":transactionType as OperationType
    };
    addOperation(newValue, 
      ()=>{
        getOperationsBalance(()=>{})
        if(viewType===ViewType.ReportToday){
          getOperationsToday(()=>{})
        }else{
          getOperationsConsolidate(()=>{})
        };
        setMessageSnack({ message: 'Transação realizada com sucesso!', severety: Severety.SUCCESS })
      },
      ()=>setMessageSnack({ message: 'Não foi possível efetivar a transação!', severety: Severety.ERROR }));
  };

  const clearOperation = () => {
    setTransactionDescribe('');
    setTransactionValue(0);
    setTransactionType(1);
  };

  return <>
            <Logotype horizontal={true}/>;
            <Box style={{position:"absolute", right:"15px", top:"15px", height:"50px", backgroundColor: "rgba(255, 99, 71, 0)"}}  $alignHR>
              <Typography  variant="h5" component="h6" style={{marginRight:"10px", color:"#B6C13F"}}>{session.user}</Typography>
              <Fab color="primary" aria-label="add" onClick={handleLogout} >
                <LogoutTwoToneIcon/>
              </Fab>
            </Box>
            <Backcenter $column $alignHL  >
              <Box style={{ top:"-16px"}}  $alignHSA>
                <TextField id="describe" label="Descrição" variant="filled" sx={{width:"100%"}} placeholder="Digite um descritivo..." onChange={handleChangeDescribeTransaction} value={transactionDescribe}/>
                <CurrencyFormat id="value" variant="filled" label="Valor" style={{width:"100%"}} customInput={TextField} prefix="R$ " decimalSeparator=","  onChange={handleChangeValueTransaction}  value={transactionValue}/>
                <RadioGroup row aria-labelledby="transaction-row-radio-buttons-group-label" style={{width:"100%"}} name="transaction-buttons-group" defaultValue="1" onChange={handleChangeTypeTransaction} value={transactionType}>
                  <FormControlLabel value="1" control={<Radio />} label="Crédito" />
                  <FormControlLabel value="2" control={<Radio />} label="Débito" />
                </RadioGroup>
                <Fab color="primary" aria-label="add" onClick={handleAddTransaction} style={{minWidth:"56px"}}>
                  <CheckIcon/>
                </Fab>
              </Box>
              <Box $row $alignHSB>
                <Typography  variant="h5" component="h6" style={{marginLeft:"10px"}}>{viewMessageHeader}</Typography>
                <Typography  variant="h5" component="h6" style={{marginRight:"10px"}}>Saldo {dataOperationBalance}</Typography>
              </Box>
              <Box $row style={{ bottom:"-16px", height:"96%"}}>
                <DataGrid  rows={dataOperation} columns={columnsConfig} sx={{height:"400px"}}/>
              </Box>
             <Button onClick={handleChangeReport} style={{top:"8px"}}>{viewMessageButton}</Button>
            </Backcenter>
            <Snack state={messageSnack} />
        </>;
}


