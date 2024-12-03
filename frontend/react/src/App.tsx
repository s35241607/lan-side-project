import { BrowserRouter, Routes, Route, Navigate } from "react-router-dom";
import LoginPage from "./views/loginPage";
import "antd/dist/reset.css"; // v5版本引入這個就好
import "./assets/all.scss";

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Navigate to="/loginPage" />} />
        <Route path="/loginPage" element={<LoginPage />} />
      </Routes>
    </BrowserRouter>
  );
}

export default App;
