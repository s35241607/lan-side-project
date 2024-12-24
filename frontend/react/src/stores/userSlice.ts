import { createSlice } from "@reduxjs/toolkit";

const initialState = {
  token: <string>"",
};

const userSlice = createSlice({
  name: "user",
  initialState: initialState,
  reducers: {
    setToken(state, action) {
      state.token = action.payload;
    },
    clearToken(state) {
      state.token = "";
    },
  },
});

export const { setToken, clearToken } = userSlice.actions;
export default userSlice.reducer;
