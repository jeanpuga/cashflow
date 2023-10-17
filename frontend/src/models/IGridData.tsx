import { GridColDef } from '@mui/x-data-grid';
import { IDataRowModel } from "./IDataRowModel";



export interface IGridData {
  columns: GridColDef[];
  rows: IDataRowModel[];
}
