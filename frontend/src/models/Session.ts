export type Session = {
  isAuthenticated?: boolean;
  redirectPath: string;
  user: string;
  token: string;
}

export const initialSession: Session = {
  redirectPath: '',
  isAuthenticated:false,
  user:"",
  token:""
};
