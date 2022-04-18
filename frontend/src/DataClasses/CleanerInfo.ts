import ScheduleEntry from "./ScheduleEntry";
import MessLevel from "./MessLevel";

interface CleanerInfo {
    scheduleEntries: ScheduleEntry[];
    maxMess: MessLevel;
    minClientRating: number;
    minPrice: number;
    maxLocationRange: number;
}

export default CleanerInfo;
