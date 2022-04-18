enum DayOfWeek {
  Monday = 'Monday',
  Tuesday = 'Tuesday',
  Wednesday = 'Wednesday',
  Thursday = 'Thursday',
  Friday = 'Friday',
  Saturday = 'Saturday',
  Sunday = 'Sunday'
}

export const dayOfWeektoStr = (dayOfWeek: DayOfWeek) => {
  switch (dayOfWeek) {
    case DayOfWeek.Monday: return 'PON';
    case DayOfWeek.Tuesday: return 'WTO';
    case DayOfWeek.Wednesday: return 'SRO';
    case DayOfWeek.Thursday: return 'CZW';
    case DayOfWeek.Friday: return 'PIA';
    case DayOfWeek.Saturday: return 'SOB';
    case DayOfWeek.Sunday: return 'NIE';
    default: return null;
  }
}

export const dayOfWeekGetFromStr = (str: string) => {
  switch (str) {
    case 'PON': return DayOfWeek.Monday;
    case 'WTO': return DayOfWeek.Tuesday;
    case 'SRO': return DayOfWeek.Wednesday;
    case 'CZW': return DayOfWeek.Thursday;
    case 'PIA': return DayOfWeek.Friday;
    case 'SOB': return DayOfWeek.Saturday;
    case 'NIE': return DayOfWeek.Sunday;
    default: return null;
  }
}

export default DayOfWeek;
