import { ChangeEvent, useState } from "react";
import { useNavigate } from "react-router";
import { useSessionContext } from "../../contexts/SessionContext";
import { Logotype } from "../../components/Logotype";
import { Button } from "../../components/Button";
import { Backleft, Backright } from "../../components/Backgrounds";
import Snack from '../../components/Snack'
import { Severety } from "../../components/Snack/Severety";


export default function Login() {
  const { login } = useSessionContext();
  const navigate = useNavigate();
  const [loginUser, setLoginUser] = useState('')
  const [loginPass, setLoginPass] = useState('')
  const initMessageSnack = { message: "", severety: Severety.INFO };
  const [messageSnack, setMessageSnack] = useState(initMessageSnack);

  const handleChangeLoginUser = (event: ChangeEvent<HTMLInputElement>) => {
    setLoginUser(event.target.value);
  };
  
  const handleChangeLoginPass = (event: ChangeEvent<HTMLInputElement>) => {
    setLoginPass(event.target.value);
  };

  const handleLogin = async() => {
    await login(loginUser, loginPass, ()=>navigate('/operations'), ()=>setMessageSnack({ message: 'Senha ou email incorreto!', severety: Severety.ERROR }));
  };

  return <>
    <Backleft>
      <Logotype horizontal={false}/>
    </Backleft>
    <Backright>
        <form action="">
          <div className="group_form">
            <input type="email" name="email" id="email" placeholder="digite o email!" value={loginUser} onChange={handleChangeLoginUser} required/>
            <label htmlFor="email">email</label>
          </div>
          <div className="group_form">
            <input type="password" name="pass" id="pass" placeholder="digite sua senha!" value={loginPass} onChange={handleChangeLoginPass} required />
            <label htmlFor="fullname">password</label>
          </div>
          <Button onClick={handleLogin}>login</Button>
        </form>
    </Backright>
    <Snack state={messageSnack} />
  </>;
}
