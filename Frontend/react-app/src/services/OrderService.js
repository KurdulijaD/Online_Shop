import api from "../helpers/ConfigHelper";

export const getAllOrders = async () => {
  return await api.get(`/order/GetAllOrders`);
};
export const getCustomerDeliveredOrders = async () => {
  return await api.get(`/order/GetCustomerDeliveredOrders`);
};
export const getSalesmanDeliveredOrders = async () => {
  return await api.get(`/order/GetSalesmanDeliveredOrders`);
};

export const getCustomerInProgressOrders = async () => {
  return await api.get(`/order/GetCustomerInProgressOrders`);
};
export const getSalesmanInProgressOrders = async () => {
  return await api.get(`/order/GetSalesmanInProgressOrders`);
};

export const createOrder = async (orderData) => {
  return await api.post(`/order/CreateOrder`, orderData);
};
