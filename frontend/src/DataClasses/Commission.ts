interface Commission {
  newProvision: number;
}

export const isCommissionCorrect = (commission: Commission) =>
  commission.newProvision >= 0 && commission.newProvision <= 1;

export default Commission;
