import * as React from "react";
import Avatar from "@mui/material/Avatar";
import Button from "@mui/material/Button";
import CssBaseline from "@mui/material/CssBaseline";
import TextField from "@mui/material/TextField";
import FormControlLabel from "@mui/material/FormControlLabel";
import Checkbox from "@mui/material/Checkbox";
import Link from "@mui/material/Link";
import Grid from "@mui/material/Grid";
import Box from "@mui/material/Box";
import LockOutlinedIcon from "@mui/icons-material/LockOutlined";
import Typography from "@mui/material/Typography";
import Container from "@mui/material/Container";
import { createTheme, ThemeProvider } from "@mui/material/styles";
import { useState } from "react";
import AuthService from "../../../services/auth.service";

function Copyright(props) {
  return (
    <Typography
      variant="body2"
      color="text.secondary"
      align="center"
      {...props}
    >
      {"Copyright © "}
      <Link color="inherit" href="https://www.linkedin.com/in/navidtrc/">
        NAVID TAFRESHI
      </Link>{" "}
      {new Date().getFullYear()}
      {"."}
    </Typography>
  );
}

// TODO remove, this demo shouldn't need to reset the theme.
const defaultTheme = createTheme();

export default function SignIn() {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const handleUsernameChange = (e) => {
    setUsername(e.currentTarget.value);
  };
  const handlePasswordChange = (e) => {
    setPassword(e.currentTarget.value);
  };
  const handleLogin = () => {
    AuthService.login(username, password).then(
      () => {
        debugger;
        // navigate("/profile");
        // window.location.reload();
      },
      (error) => {
        const resMessage =
          (error.response &&
            error.response.data &&
            error.response.data.message) ||
          error.message ||
          error.toString();

        // setLoading(false);
        // setMessage(resMessage);
      }
    );
  };

  return (
    <ThemeProvider theme={defaultTheme}>
      <Container component="main" maxWidth="xs">
        <CssBaseline />
        <Box
          sx={{
            marginTop: 8,
            display: "flex",
            flexDirection: "column",
            alignItems: "center",
          }}
        >
          <Avatar sx={{ m: 1, bgcolor: "secondary.main" }}>
            <LockOutlinedIcon />
          </Avatar>
          <Typography component="h1" variant="h5">
            ورود
          </Typography>
          <Box>
            <TextField
              value={username}
              onChange={handleUsernameChange}
              margin="normal"
              required
              fullWidth
              type="text"
              label="نام کاربری"
              name="username"
              autoFocus
            />
            <TextField
              value={password}
              onChange={handlePasswordChange}
              margin="normal"
              required
              fullWidth
              name="password"
              label="کلمه عبور"
              type="password"
            />
            <Button
              onClick={handleLogin}
              type="button"
              fullWidth
              variant="contained"
              sx={{ mt: 3, mb: 2 }}
            >
              ورود
            </Button>
            <Grid container>
              <Grid item xs>
                <Link href="#" variant="body2">
                  فراموشی رمز عبور؟
                </Link>
              </Grid>
             
            </Grid>
          </Box>
        </Box>
        <Copyright sx={{ mt: 8, mb: 4 }} />
      </Container>
    </ThemeProvider>
  );
}
