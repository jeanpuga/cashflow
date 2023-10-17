import { useState, forwardRef, useEffect } from 'react';

import Snackbar from '@mui/material/Snackbar';
import MuiAlert from '@mui/material/Alert';
import Slide from '@mui/material/Slide';
import { Severety } from './Severety';

function SlideTransition(props) {
  return <Slide {...props} direction="left" />;
}

const Alert = forwardRef(function Alert(props, ref) {
  return <MuiAlert elevation={6} ref={ref} variant="filled" {...props} />;
});

const Snack = ({ state }) => {
  const [snackSettings, setSnackSettings] = useState({
    open: false,
    timing: 3000,
    message: "Sucesso!",
    transition: SlideTransition,
    severety: Severety.SUCCESS
  })

  const colors = {
    "0": "error",
    "1": "warning",
    "2": "info",
    "3": "success",
  };

  useEffect(() => {
    if (!!state.message) {
      handleSnackSettings(state)
    }
  }, [state]);

  const handleCloseSnack = (event, reason) => {
    if (reason === 'clickaway') {
      return;
    }
    setSnackSettings({ ...snackSettings, open: false });
  };


  const handleSnackSettings = ({ message, severety }) =>
    setSnackSettings({
      ...snackSettings,
      message,
      severety,
      open: true,
    });


  return ((<Snackbar
    open={snackSettings.open}
    autoHideDuration={snackSettings.timing}
    onClose={handleCloseSnack}
    key="snackSettings"
    TransitionComponent={snackSettings.transition}
  >
    <Alert onClose={handleCloseSnack} severity={colors[snackSettings.severety]} sx={{ width: '300px' }}>
      {snackSettings.message}
    </Alert>
  </Snackbar>)

  );
}

export default Snack;