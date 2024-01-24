import * as React from "react";
import { useState } from "react";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import AuthService from "./services/auth.service";
import SignIn from "./components/forms/authentication/SignIn";
import DashboardLayout from "./components/layouts/DashboardLayout";

export default function App() {
  const [currentUser, setCurrentUser] = useState("a");
  React.useEffect(() => {
    // const user = AuthService.getCurrentUser();
    // if (user) {
    //   setCurrentUser(user);
    // }
    //setCurrentUser("Navid");
  }, []);

  return (
    <>
      {currentUser ? (
        <>
          <DashboardLayout />
        </>
      ) : (
        <>
          <SignIn />
        </>
      )}
    </>
  );
}
