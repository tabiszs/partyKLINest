
const apiUrl = process.env.REACT_APP_API_URL;

export const getCleanerAddress = (cleanerId: number) => apiUrl + "Cleaner/" + cleanerId;

export const getCleanerOrdersAddress = (cleanerId: number) => getCleanerAddress(cleanerId) + "Orders/";

export const getClientAddress = (clientId: number) => apiUrl + "Client/" + clientId;

export const getCommissionAddress = () => apiUrl + "Commission/";

export const getOrdersUrl = () => apiUrl + "Orders/";

export const getOrderUrl = (orderId: number) => getOrdersUrl() + orderId.toString();

const getUserUrl = (userId: number) => apiUrl + "User/" + userId;

export const getUserRateUrl = (userId: number) => getUserUrl(userId) + "/Rate/";

export const getUserBanUrl = (userId: number) => getUserUrl(userId) + "/Ban/";
