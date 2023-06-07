import api from "../helpers/ConfigHelper";

export const getMyProducts = async () => {
  return await api.get(`/product/GetMyProducts`);
};

export const createNewProduct = async (productData) => {
  return await api.post(`/product`, productData);
};

export const updateProduct = async (id, productData) => {
  return await api.put(`/product` + id, productData);
};

export const deleteProduct = async (id) => {
  return await api.delete(`/product/` + id);
};
