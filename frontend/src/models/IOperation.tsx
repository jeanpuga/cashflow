import { OperationType } from "./OperationType";


export interface IOperation {
  describe: string;
  value: number;
  type: OperationType;
}
