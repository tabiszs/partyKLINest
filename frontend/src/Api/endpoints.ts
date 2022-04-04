import CleanerInfo from '../DataClasses/CleanerInfo';
import Commission from '../DataClasses/Commission';
import NewOrder from '../DataClasses/NewOrder';
import Order from '../DataClasses/Order';
import Rating from '../DataClasses/Rating';
import * as api from './urlProvider';

const get = <T>(address: string) => {
  return fetch(address)
    .then((response) => response.json())
    .then((data) => data as T);
}

const post = (address: string, data?: any) => {
  return fetch(address, {
    method: "POST",
    body: JSON.stringify(data)
  });
}

const del = (address: string) => {
  return fetch(address, {
    method: "DELETE"
  });
}

export const getCleanerInfo = (cleanerId: string) => get<CleanerInfo>(api.getCleanerAddress(cleanerId));

export const postCleanerInfo = (cleanerId: string, cleanerInfo: CleanerInfo) => post(api.getCleanerAddress(cleanerId), cleanerInfo);

export const getCleanerOrders = (cleanerId: string) => get<Order[]>(api.getCleanerAddress(cleanerId));

export const deleteClient = (clientId: string) => del(api.getClientAddress(clientId));

export const postCommission = (commission: Commission) => post(api.getCommissionAddress(), commission);

export const getAllOrders = () => get<Order[]>(api.getOrdersUrl());

export const postNewOrder = (newOrder: NewOrder) => post(api.getOrdersUrl(), newOrder);

export const getOrder = (orderId: number) => get<Order>(api.getOrderUrl(orderId));

export const postOrder = (orderId: number, order: Order) => post(api.getOrderUrl(orderId), order);

export const deleteOrder = (orderId: number) => del(api.getOrderUrl(orderId));

export const postRateUser = (userId: string, rating: Rating) => post(api.getUserRateUrl(userId), rating);

export const postBanUser = (userId: string) => post(api.getUserBanUrl(userId));
