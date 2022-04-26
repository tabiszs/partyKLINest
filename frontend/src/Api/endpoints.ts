import { GetAuthorizationHeader } from '../Authentication/MsalService';
import CleanerInfo from '../DataClasses/CleanerInfo';
import Commission from '../DataClasses/Commission';
import NewOrder from '../DataClasses/NewOrder';
import Order from '../DataClasses/Order';
import Rating from '../DataClasses/Rating';
import UserInfo from '../DataClasses/UserInfo';
import * as api from './urlProvider';

const get = <T>(address: string) => {
    return GetAuthorizationHeader()
        .then((headers) => fetch(address, { headers }))
        .then((response) => response.json())
        .then((data) => data as T);
}

const post = (address: string, data?: any) => {
    return GetAuthorizationHeader()
        .then((headers) => fetch(address, {
            method: "POST",
            body: JSON.stringify(data),
            headers
        }));
}

const del = (address: string) => {
    return GetAuthorizationHeader()
        .then((headers) => fetch(address, {
            method: "DELETE",
            headers
        }));
}

export const getAllOrders = () => get<Order[]>(api.getOrdersUrl());
export const postNewOrder = (newOrder: NewOrder) => post(api.getOrdersUrl(), newOrder);
export const getOrder = (orderId: number) => get<Order>(api.getOrderUrl(orderId));
export const postOrder = (orderId: number, order: Order) => post(api.getOrderUrl(orderId), order);
export const deleteOrder = (orderId: number) => del(api.getOrderUrl(orderId));
export const getCleanerOrders = () => get<Order[]>(api.getCleanerOrdersUrl());
export const getClientOrders = () => get<Order[]>(api.getClientOrdersUrl());
export const postOrderRate = (orderId: number, rating: Rating) => post(api.getOrderRateUrl(orderId), rating);
export const getMatchingCleanerIdsForOrder = (orderId: number) => get<string[]>(api.getOrderMatchingUrl(orderId));

export const getCleanerInfo = (cleanerId: string) => get<CleanerInfo>(api.getCleanerAddress(cleanerId));
export const postCleanerInfo = (cleanerId: string, cleanerInfo: CleanerInfo) => post(api.getCleanerAddress(cleanerId), cleanerInfo);

export const deleteClient = (clientId: string) => del(api.getClientAddress(clientId));

export const postBanUser = (userId: string) => post(api.getUserBanUrl(userId));
export const deleteUser = (userId: string) => del(api.getUserUrl(userId));
export const getAllUsers = () => get<UserInfo>(api.getUsersUrl());

export const postCommission = (commission: Commission) => post(api.getCommissionAddress(), commission);
