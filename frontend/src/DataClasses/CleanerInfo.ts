import ScheduleEntry from "./ScheduleEntry";
import MessLevel from "./MessLevel";

interface CleanerInfo {
    scheduleEntries: ScheduleEntry[];
    maxMess: MessLevel;
    minClientRating: number;
    minPrice: number;
    maxLocationRange: number;
}

export const defaultCleanerInfo = () : CleanerInfo => ({
    scheduleEntries: [],
    maxMess: MessLevel.Disaster,
    minClientRating: 5,
    minPrice: 0.0,
    maxLocationRange: 100000
});

export default CleanerInfo;
