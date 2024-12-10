import { HashRouter, Routes, Route, Navigate } from "react-router-dom";
import LoginPage from "./views/loginPage";
import RegisterPage from "./views/registerPage";
import ResetPasswordPage from "./views/resetPasswordPage";
import "antd/dist/reset.css"; // v5版本引入這個就好
import "./assets/all.scss";

function App() {
  return (
    <HashRouter>
      <Routes>
        <Route path="/" element={<Navigate to="/loginPage" />} />
        <Route path="/loginPage" element={<LoginPage />} />
        <Route path="/registerPage" element={<RegisterPage />} />
        <Route path="/resetPasswordPage" element={<ResetPasswordPage />} />
      </Routes>
    </HashRouter>
  );
}

export default App;
