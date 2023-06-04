import api from '../helpers/ConfigHelper';

export const getMyProfile = async () => {
    return await api.get(`/user/GetMyProfile`)};

export const register = async (registerData) => {
   return await api.post(`/user`, registerData)};

export const update = async (updateData) => {
    return await api.put(`/user`, updateData)};