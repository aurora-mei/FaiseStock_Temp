import { createSlice, createAsyncThunk } from "@reduxjs/toolkit";
import { DUser, fetchRank } from "../api/UserApi";
interface HomePageState {
  rank: DUser[];
  loading: boolean;
}
export const fetchRanks = createAsyncThunk("user/fetchRank", async () => {
  return await fetchRank();
});
const initialState: HomePageState = { rank: [], loading: false };
const UserSlice = createSlice({
  name: "user",
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder
      .addCase(fetchRanks.pending, (state) => {
        state.loading = true;
      })
      .addCase(fetchRanks.fulfilled, (state, action) => {
        state.rank = action.payload;
        state.loading = false;
      });
  },
});
export default UserSlice.reducer;
