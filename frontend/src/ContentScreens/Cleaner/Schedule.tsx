import {useState} from 'react';
import TextField from '@mui/material/TextField';
import Button from '@mui/material/Button';
import Heading from '../../Components/Heading';
import ScheduleEntry from '../../DataClasses/ScheduleEntry';
import CleanerInfo from '../../DataClasses/CleanerInfo';
import MessLevel from '../../DataClasses/MessLevel';
import DayOfWeek, {dayOfWeekGetFromStr, dayOfWeektoStr} from '../../DataClasses/DayOfWeek';
import {getCleanerInfo, postCleanerInfo} from '../../Api/endpoints';
import './Schedule.css';

const scheduleRegEx = /^(...) (\d\d):(\d\d)-(\d\d):(\d\d)$/;

const parseAndSetScheduleText = (text: string, setSchedule: (schedule: ScheduleEntry[] | null) => void) => {
  const lines = text.split('\n');
  const schedule: ScheduleEntry[] = [];

  for (const line of lines) {
    if (!scheduleRegEx.test(line)) {
      console.log('line: ' + line);
      if (line === '') {
        continue;
      }

      setSchedule(null);
      return;
    }

    const match = line.match(scheduleRegEx);
    if (match?.length !== 6) {
      console.log('len');
      setSchedule(null);
      return;
    }

    const dayOfWeek = dayOfWeekGetFromStr(match[1]);
    if (dayOfWeek === null) {
      console.log('week');
      setSchedule(null);
      return;
    }

    let h1;
    let m1;
    let h2;
    let m2;

    try {
      h1 = parseInt(match[2]);
      m1 = parseInt(match[3]);
      h2 = parseInt(match[4]);
      m2 = parseInt(match[5]);
    }
    catch (e: any) {
      console.log('catch');
      setSchedule(null);
      return;
    }

    if (h1 >= 24 || h2 >= 24 || m1 >= 60 || m2 >= 60 || h1 > h2 || (h1 === h2 && m1 > m2)) {
      console.log('range');
      setSchedule(null);
      return;
    }

    schedule.push({
      dayOfWeek: dayOfWeek,
      start: `${match[2]}:${match[3]}`,
      end: `${match[4]}:${match[5]}`
    });
  }

  setSchedule(schedule);
}

const generateScheduleText = (schedule: ScheduleEntry[]) => {
  let text = '';
  for (const entry of schedule) {
    text += `${dayOfWeektoStr(entry.dayOfWeek)} ${entry.start}-${entry.end}\n`;
  }

  return text;
}

const AvailabilityTable = () => {
  // TODO: gdy będzie API to podmienić
  // const cleanerInfo = getCleanerInfo(/* Jakoś dostać swoje ID */);
  // Mock
  const cleanerInfo: CleanerInfo = {
    scheduleEntries: [
      {dayOfWeek: DayOfWeek.Monday, start: '21:00', end: '22:35'},
      {dayOfWeek: DayOfWeek.Tuesday, start: '08:00', end: '12:35'},
      {dayOfWeek: DayOfWeek.Tuesday, start: '21:00', end: '22:35'},
      {dayOfWeek: DayOfWeek.Wednesday, start: '07:00', end: '15:00'}
    ],
    maxMess: MessLevel.Disaster,
    minClientRating: 0,
    minPrice: 0,
    maxLocationRange: 100,
  };

  const [scheduleText, setScheduleText] = useState<string>(generateScheduleText(cleanerInfo.scheduleEntries));
  const [schedule, setSchedule] = useState<ScheduleEntry[] | null>(cleanerInfo.scheduleEntries);

  return (
    <div className='schedule-container'>
      <Heading content='Twoja dostępność' />
      <div className='schedule-instructions'>
        Format grafiku dostępności jest postaci linii zawierających znaki "DT HH:MM-HH:MM",
        gdzie DT to dzień tygodnia (PON, WTO, SRO, CZW, PIA, SOB, NIE),
        a HH:MM-HH:MM to zakres godzinowy dostępności.
      </div>
      <div className='schedule-textfield'>
        <TextField
          value={scheduleText}
          label='Dostępność'
          variant='outlined'
          multiline
          error={schedule === null}
          onChange={(event: any) => {
            setScheduleText(event.target.value);
            parseAndSetScheduleText(event.target.value, setSchedule);
          }}
        />
      </div>
      <Button
        variant='contained'
        onClick={() => {
          if (schedule === null) {
            return;
          }

          cleanerInfo.scheduleEntries = schedule;
          console.log(cleanerInfo);
          postCleanerInfo('moje ID', cleanerInfo) // TODO: Jakoś dostać swoje ID
        }}
      >
        Zatwierdź
      </Button>
    </div>
  );
}

export default AvailabilityTable;
