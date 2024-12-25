import { BrowserRouter, Routes, Route, Navigate } from "react-router-dom";
import LoginPage from "./views/loginPage";
import RegisterPage from "./views/registerPage";
import ResetPasswordPage from "./views/resetPasswordPage";
import Home from "./components/layout/home";
import UserInfo from "./components/layout/userInfo";
import Layout from "./views/layout";
import "antd/dist/reset.css"; // v5版本引入這個就好
import "./assets/all.scss";

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Navigate to="/login" />} />
        <Route path="/login" element={<LoginPage />} />
        <Route path="/register" element={<RegisterPage />} />
        <Route path="/reset-password" element={<ResetPasswordPage />} />
        <Route path="/layout" element={<Layout />}>
          <Route index element={<Navigate to="/layout/home" replace />} />
          <Route path="home" element={<Home />} />
          <Route path="user-info" element={<UserInfo />} />
        </Route>
      </Routes>
    </BrowserRouter>
  );
}

export default App;
