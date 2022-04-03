interface Address {
    street: string;
    buildingNo: string;
    flatNo?: number;
    city: string;
    postalCode: string;
    country: string;
}

export function isAddressCorrect(address: Address): boolean {
    return address.street !== '' &&
        address.buildingNo !== '' &&
        address.flatNo !== 0 && (address.flatNo === undefined || !isNaN(address.flatNo)) &&
        address.city !== '' &&
        address.postalCode !== '' &&
        address.country !== '';
}

export function emptyAddress(): Address {
    return {
        street: '',
        buildingNo: '',
        flatNo: undefined,
        city: '',
        postalCode: '',
        country: ''
    };
}

export default Address;
