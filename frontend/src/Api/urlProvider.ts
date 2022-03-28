
const apiUrl = "https://localhost:7165/api/";

export const getCleanerAddress = (cleanerId: number) => apiUrl + "Cleaner/" + cleanerId.toString();

export const getCleanerOrdersAddress = (cleanerId: number) => getCleanerAddress(cleanerId) + "Orders/";

export const getClientAddress = (clientId: number) => apiUrl + "Client/" + clientId.toString();

export const getCommissionAddress = () => apiUrl + "Commission/";

export const getOrdersUrl = () => apiUrl + "Orders/";

export const getOrderUrl = (orderId: number) => getOrdersUrl() + orderId.toString();

const getUserUrl = (userId: number) => apiUrl + "User/" + userId.toString()

export const getUserRateUrl = (userId: number) => getUserUrl(userId) + "/Rate/";

export const getUserBanUrl = (userId: number) => getUserUrl(userId) + "/Ban/";