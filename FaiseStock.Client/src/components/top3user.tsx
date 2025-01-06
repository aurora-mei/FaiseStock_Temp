import { DUser } from "../api/UserApi";

interface Top3UserProps {
  u: DUser;
  outTop3User: boolean;
}
const Top3User = ({ u, outTop3User }: Top3UserProps) => {
  return (
    <div className="text-xs flex gap-2 w-60 items-center mb-2" key={u.userId}>
      {outTop3User && (
        <div className="bg-blue-900 h-8 w-8 rounded-full text-white flex items-center justify-center font-bold text-md">
          {u.rank}
        </div>
      )}
      <div className="flex items-center justify-center gap-x-2 w-52 bg-white text-gray-900 p-2 rounded-xl shadow-md">
        <img
          className="h-12 w-12 rounded-full"
          src="https://cdn-icons-png.flaticon.com/512/7070/7070249.png"
          alt={u.userName}
        />
        <div className="text-left">
          <b className="text-blue-800" title={u.userName}>
            {u.userName}
          </b>
          <br />
          <div className="grid grid-cols-2 w-36 text-left">
            <span className="text-gray-700 font-semibold">Tiền tăng:</span>
            <span
              className={
                outTop3User
                  ? "text-gray-500 font-semibold"
                  : "text-[#FF3C00] font-semibold"
              }
            >
              {u.increasedAmount}
              <sup>đ</sup>
            </span>
            <span className="text-gray-700 font-semibold">ROIC:</span>
            <span
              className={
                outTop3User
                  ? "text-gray-500 font-semibold"
                  : "text-[#FF3C00] font-semibold"
              }
            >
              {u.roic}%
            </span>
          </div>
        </div>
      </div>
    </div>
  );
};
export default Top3User;
