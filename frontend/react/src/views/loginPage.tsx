import React, { useState } from "react";
import Login from "../components/login";
import ForgotPassword from "../components/forgotPassword";

const LoginPage: React.FC = () => {
  const [showForgot, setShowForgot] = useState<boolean>(false);

  return (
    <>
      {showForgot ? (
        <ForgotPassword setShowForgot={setShowForgot} />
      ) : (
        <Login setShowForgot={setShowForgot} />
      )}
    </>
  );
};

export default LoginPage;
