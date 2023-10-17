import ProtectedRoute, { ProtectedRouteProps } from "../components/ProtectedRoute";
import { useSessionContext } from "../contexts/SessionContext";
import { Route, Routes } from 'react-router';
import Operations from "./Operations";
import Login from "./Login";

export default function App() {
  // const [sessionContext, updateSessionContext] = useSessionContext();

  const {session,setSession, login} = useSessionContext();


  const setRedirectPath = (path: string) => {
    setSession({...session, redirectPath: path});
  }

  if(!session.redirectPath) {
    setRedirectPath('/operations');
  }

  const defaultProtectedRouteProps: Omit<ProtectedRouteProps, 'outlet'> = {
    isAuthenticated: !!session.isAuthenticated,
    authenticationPath: '/',
    redirectPath: session.redirectPath,
    setRedirectPath: setRedirectPath
  };

  return (
    <div>
      <Routes>
        <Route path='/' element={<Login />} />
        <Route path='operations' element={<ProtectedRoute {...defaultProtectedRouteProps} outlet={<Operations />} />} />
        <Route path='*' element={<Login />} />
      </Routes>
    </div>
  );
};
