import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import Layout from "../components/layout";
import Loading from "../components/loading";

function getCookie(name: string): string | null {
  const cookies = document.cookie.split("; ");
  // 可能會有很多個cookie，先做切割
  // 再把各個cookie在切割成key,value 比對key
  for (const cookie of cookies) {
    const [key, value] = cookie.split("=");
    if (key === name) {
      return value;
    }
  }
  return null;
}

const Home: React.FC = () => {
  const navigate = useNavigate();

  const [token, setToken] = useState<string | null>(null);
  const [isLoading, setIsLoading] = useState<boolean>(true);

  useEffect(() => {
    const retrievedToken = getCookie("token");
    if (!retrievedToken) {
      navigate("/loginPage");
    } else {
      const waitLoading = setTimeout(() => {
        setIsLoading(false);
      }, 2000);
      setToken(retrievedToken);
      return () => clearTimeout(waitLoading);
    }
  }, [navigate]);

  // if (isLoading) {
  //   return <Loading />;
  // }

  return (
    <>
      {isLoading && <Loading />}
      <Layout>您的token是{token}</Layout>
    </>
  );
};

export default Home;
