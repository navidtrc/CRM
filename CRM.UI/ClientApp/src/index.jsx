import React from "react";
import ReactDOM from "react-dom/client";
import { ThemeProvider } from "@mui/material/styles";
import { CacheProvider } from "@emotion/react";
import './fonts/IranSans/ttf/IRANSansWeb_Light.ttf'
import "./index.css";
import {theme , cacheRtl} from './context/theme'

const root = ReactDOM.createRoot(
  document.getElementById("root")
);

root.render(
  <CacheProvider value={cacheRtl}>
    <ThemeProvider theme={theme}>
      <App />
    </ThemeProvider>
  </CacheProvider>
);
