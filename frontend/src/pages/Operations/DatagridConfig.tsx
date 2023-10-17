import { GridColDef } from "@mui/x-data-grid"
import clsx from "clsx";

export const columnsTodayConfig: GridColDef[] =  [
  { field: 'id', headerName: '#', width: 70,  align: 'center', editable: false, headerAlign: 'center' },
  { field: 'dateRef', headerName: 'Data da Transação', width: 200, align: 'center', editable: false, headerAlign: 'center'},
  { field: 'describe', headerName: 'Descrição', width: 600, align: 'left', editable: false, headerAlign: 'center' },
  { field: 'value', type: 'number', headerName: 'Valor', width: 130, align: 'right', editable: false, headerAlign: 'center',
    valueFormatter: params => new Intl.NumberFormat('pt-BR', { style: 'currency', currency: 'BRL' }).format(params.value),},
  { field: 'type', headerName: 'Tipo',align: 'center', width: 130, editable: false, headerAlign: 'center', 
    valueFormatter: params => params.value==='Credit'?'Crédito':'Débito',
    cellClassName: params => clsx(params.value==='Credit'?'credito':'debito')}
];


export const columnsConsolidateConfig: GridColDef[] =  [
  { field: 'id', headerName: '#', width: 70,  align: 'center', editable: false, headerAlign: 'center' },
  { field: 'dateRef', headerName: 'Data da Transação', width: 200, align: 'center', editable: false, headerAlign: 'center'},
  { field: 'value', type: 'number', headerName: 'Valor', width: 200, align: 'right', editable: false, headerAlign: 'center', 
    valueFormatter: params => new Intl.NumberFormat('pt-BR', { style: 'currency', currency: 'BRL' }).format(params.value),
    cellClassName: params => clsx(params.value>=0?'credito':'debito') },
];
