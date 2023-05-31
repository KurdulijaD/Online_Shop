import api from '../helpers/ConfigHelper';

export const GetMyProfile = async (token) => {
    return await api.get(`/user/GetMyProfile`)};

export const register = async (registerData) => {
   return await api.post(`/user`, registerData)};