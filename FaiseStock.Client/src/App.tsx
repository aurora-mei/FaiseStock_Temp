import { useSelector, useDispatch } from "react-redux";
import { RootState, AppDispatch } from "./state/store";
import { fetchRanks } from "./state/UserSlice";
import "./App.css";
import { useEffect } from "react";
import { DUser } from "./api/UserApi";
import Top3User from "./components/top3user";
function App() {
  const dispatch = useDispatch<AppDispatch>();
  const { rank } = useSelector((state: RootState) => state.user);
  useEffect(() => {
    dispatch(fetchRanks());
  }, [dispatch]);
  return (
    <div className="flex flex-col gap-4">
      <div className="text-2xl p-4 text-left bg-gray-200 text-orange-700 font-semibold rounded-md">
        Bảng xếp hạng
        <hr className="text-red-500 font-bold" />
      </div>
      <div className="grid grid-cols-2 gap-4">
        <div className="flex flex-col text-left">
          {rank.slice(0, 3).map((u: DUser) => (
            <div className="py-7" key={u.userId}>
              <div className="flex items-center">
                <img
                  className="h-10 w-10 rounded-full mr-2"
                  src="https://cdn4.iconfinder.com/data/icons/success-filloutline/64/trophy-cup-champion-award-winner-256.png"
                  alt="bronze cup"
                />
                <span>Thứ hạng</span>
                <sup>{u.rank}</sup>
              </div>
              <Top3User u={u} outTop3User={false} />
            </div>
          ))}
        </div>
        <div className="flex flex-col">
          {rank.slice(3, 10).map((u: DUser) => (
            <Top3User key={u.userId} u={u} outTop3User={true} />
          ))}
        </div>
      </div>
    </div>
  );
}

export default App;
