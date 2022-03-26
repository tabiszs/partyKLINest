export default class Address {
  street: string = '';
  buildingNumber: string = '';
  flatNumber: number | null = null;
  city: string = '';
  postalCode: string = '';
  country: string = '';

  isCorrect() {
    return this.street !== '' &&
      this.buildingNumber !== '' &&
      this.flatNumber !== 0 && (this.flatNumber === null || !isNaN(this.flatNumber)) &&
      this.city !== '' &&
      this.postalCode !== '' &&
      this.country !== '';
  }
}
