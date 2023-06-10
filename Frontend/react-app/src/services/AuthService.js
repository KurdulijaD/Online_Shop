import api from "../helpers/ConfigHelper";

export const login = async (logInData) => {
  return await api.post(`/auth`, logInData);
};

export const googleLogin = async (logInData) => {
  return await api.post(`/auth/GoogleLogin`, logInData, {headers: {"Content-Type":"multipart/form-data"}});
};
