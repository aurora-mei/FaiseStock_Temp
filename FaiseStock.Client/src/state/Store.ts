import UserReducer from "./UserSlice";
import { configureStore } from "@reduxjs/toolkit";
const store = configureStore({
  reducer: {
    user: UserReducer,
  },
});
export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;
export default store;
