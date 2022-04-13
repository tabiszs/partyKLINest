enum MessLevel {
  Low = 'Low',
  Moderate = 'Moderate',
  Huge = 'Huge',
  Disaster = 'Disaster'
}

export function messLevelText(messLevel: MessLevel): string {
  switch (messLevel) {
    case MessLevel.Low: return 'Niski';
    case MessLevel.Moderate: return 'Średni';
    case MessLevel.Huge: return 'Ogromny';
    case MessLevel.Disaster: return 'Katastrofa';
    default: return 'Nieznany';
  }
}

export default MessLevel;
