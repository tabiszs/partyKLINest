
const apiUrl = process.env.REACT_APP_API_URL + "/api/";

export const getCleanerAddress = (cleanerId: string) => apiUrl + "Cleaner/" + cleanerId;

//export const getCleanerOrdersAddress = (cleanerId: string) => getCleanerAddress(cleanerId) + "Orders/"; // deprecated

export const getClientAddress = (clientId: string) => apiUrl + "Client/" + clientId;

export const getCommissionAddress = () => apiUrl + "Configuration/Commission/";

export const getOrdersUrl = () => apiUrl + "Orders/";

export const getOrderUrl = (orderId: number) => getOrdersUrl() + orderId.toString();

export const getCleanerOrdersUrl = () => getOrdersUrl() + "Cleaner/";

export const getClientOrdersUrl = () => getOrdersUrl() + "Client/"

export const getOrderRateUrl = (orderId: number) => getOrderUrl(orderId) + "/Rate";

export const getUsersUrl = () => apiUrl + "User/"

export const getUserUrl = (userId: string) => getUsersUrl() + userId;

export const getUserRateUrl = (userId: string) => getUserUrl(userId) + "/Rate/";

export const getUserBanUrl = (userId: string) => getUserUrl(userId) + "/Ban/";

