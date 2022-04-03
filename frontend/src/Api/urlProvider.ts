
const apiUrl = process.env.REACT_APP_API_URL;

export const getCleanerAddress = (cleanerId: string) => apiUrl + "Cleaner/" + cleanerId;

export const getCleanerOrdersAddress = (cleanerId: string) => getCleanerAddress(cleanerId) + "Orders/";

export const getClientAddress = (clientId: string) => apiUrl + "Client/" + clientId;

export const getCommissionAddress = () => apiUrl + "Commission/";

export const getOrdersUrl = () => apiUrl + "Orders/";

export const getOrderUrl = (orderId: number) => getOrdersUrl() + orderId.toString();

const getUserUrl = (userId: string) => apiUrl + "User/" + userId;

export const getUserRateUrl = (userId: string) => getUserUrl(userId) + "/Rate/";

export const getUserBanUrl = (userId: string) => getUserUrl(userId) + "/Ban/";
