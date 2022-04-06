import DayOfWeek from "./DayOfWeek";

interface ScheduleEntry {
    dayOfWeek: DayOfWeek;
    start: Date;
    end: Date;
}

export default ScheduleEntry;