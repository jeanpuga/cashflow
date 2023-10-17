import { GridRowId } from '@mui/x-data-grid';



export interface IDataRowModel {
  id: GridRowId;
  [price: string]: number | string;
}
