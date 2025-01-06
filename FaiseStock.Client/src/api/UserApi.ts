import { API_URL } from "./config";

export type DUser = {
  userId?: string;
  userName?: string;
  rank?: number;
  increasedAmount?: number;
  roic?: number;
  createAt?: Date;
};
export const fetchRank = async () => {
  const currentDate = new Date();

  const year = currentDate.getFullYear();
  const month = String(currentDate.getMonth() + 1).padStart(2, "0"); // Thêm 0 nếu cần
  const day = String(currentDate.getDate()).padStart(2, "0");
  const formattedDate = `${year}-${month}-${day}`;

  const response = await fetch(`${API_URL}/rank/${formattedDate}`);
  return await response.json();
};
