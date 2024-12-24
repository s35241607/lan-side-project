import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import Loading from "../loading";
import { useDispatch } from "react-redux";
import type { AppDispatch } from "../../store";
import { setToken } from "../../stores/userSlice";
import { getCookie } from "../../utils/getCookie";

const Home: React.FC = () => {
  const navigate = useNavigate();
  const dispatch: AppDispatch = useDispatch();
  const [isLoading, setIsLoading] = useState<boolean>(true);

  useEffect(() => {
    const retrievedToken = getCookie("token");
    if (!retrievedToken) {
      navigate("/login");
    } else {
      const waitLoading = setTimeout(() => {
        setIsLoading(false);
      }, 2000);
      dispatch(setToken(retrievedToken));
      return () => clearTimeout(waitLoading);
    }
  }, [navigate]);

  // if (isLoading) {
  //   return <Loading />;
  // }

  return (
    <>
      {isLoading && <Loading />}
      這裡是首頁
    </>
  );
};

export default Home;
