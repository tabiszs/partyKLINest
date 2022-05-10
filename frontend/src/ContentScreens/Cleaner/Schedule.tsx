import {useEffect, useState} from 'react';
import TextField from '@mui/material/TextField';
import Button from '@mui/material/Button';
import Heading from '../../Components/Heading';
import ScheduleEntry from '../../DataClasses/ScheduleEntry';
import CleanerInfo from '../../DataClasses/CleanerInfo';
import MessLevel from '../../DataClasses/MessLevel';
import DayOfWeek, {dayOfWeekGetFromStr, dayOfWeektoStr} from '../../DataClasses/DayOfWeek';
import Typography from '@mui/material/Typography';
import Rating from '@mui/material/Rating';
import {getCleanerInfo, postCleanerInfo} from '../../Api/endpoints';
import MessSelector from '../../Components/MessSelector';
import './Schedule.css';
import Token from '../../DataClasses/Token';

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

interface AvailabilityTableProps {
  token: Token;
}

const AvailabilityTable = (props: AvailabilityTableProps) => {

  const defaultCleanerInfo = {
    scheduleEntries: [],
    maxMess: MessLevel.Disaster,
    minClientRating: 5,
    minPrice: 0.0,
    maxLocationRange: 100000,
  };

  const [cleanerInfo, setCleanerInfo] = useState<CleanerInfo>(defaultCleanerInfo);

  useEffect(() => {
    getCleanerInfo(props.token.oid)
    .then(setCleanerInfo)
    .catch((err) => {
      console.log(err);
      setCleanerInfo(defaultCleanerInfo);
    });
  })

  const [scheduleText, setScheduleText] = useState<string>(generateScheduleText(cleanerInfo.scheduleEntries));
  const [schedule, setSchedule] = useState<ScheduleEntry[] | null>(cleanerInfo.scheduleEntries);
  const [messLevel, setMessLevel] = useState<MessLevel>(cleanerInfo.maxMess);
  const [minClientRating, setMinClientRating] = useState<number>(cleanerInfo.minClientRating);
  const [minPriceText, setMinPriceText] = useState<string>(cleanerInfo.minPrice.toString());
  const [minPrice, setMinPrice] = useState<number | null>(cleanerInfo.minPrice);
  const [maxLocationRangeText, setMaxLocationRangeText] = useState<string>(cleanerInfo.maxLocationRange.toString());
  const [maxLocationRange, setMaxLocationRange] = useState<number | null>(cleanerInfo.maxLocationRange);

  return (
    <div className='schedule-container'>
      <Heading content='Twoje szczegóły' />
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
      <MessSelector
        label='Maksymalny poziom bałaganu'
        value={messLevel}
        onChange={(level: MessLevel) => setMessLevel(level)}
      />
      <div className='field-container'>
        <Typography component='legend'>Minimalna ocena klienta</Typography>
        <Rating
          value={minClientRating / 2}
          onChange={(_event, newRating) => newRating != null && setMinClientRating(newRating * 2)}
          precision={0.5}
        />
      </div>
      <div className='field-container'>
        <TextField
          label='Minimalna cena usługi (zł)'
          error={minPrice === null}
          value={minPriceText}
          onChange={(event: any) => {
            const val = event.target.value;
            setMinPriceText(val);
            if (val === '') {
              setMinPrice(0);
              return;
            }

            if (/^\d+([,.]\d\d)?$/.test(val)) {
              const priceString = val.replace(',', '.');
              setMinPrice(parseFloat(priceString) * 100);
            }
            else {
              setMinPrice(null);
            }
          }}
        />
      </div>
      <div className='field-container'>
        <TextField
          label='Maksymalna odległość (w łokciach)'
          value={maxLocationRangeText}
          error={maxLocationRange === null}
          onChange={(event: any) => {
            const val = event.target.value;
            setMaxLocationRangeText(val);
            const numberString = val.replace(',', '.');

            try {
              const range = parseFloat(numberString);
              if (isNaN(range)) {
                setMaxLocationRange(null);
              }
              else {
                setMaxLocationRange(range);
              }
            }
            catch {
              setMaxLocationRange(null);
            }
          }}
        />
      </div>
      <div className='field-container'>
        <Button
          variant='contained'
          onClick={() => {
            if (schedule === null || minPrice === null || maxLocationRange === null) {
              return;
            }

            const newCleanerInfo = {
              scheduleEntries: schedule,
              maxMess: messLevel,
              maxLocationRange: maxLocationRange,
              minPrice: minPrice,
              minClientRating: minClientRating
            }
            console.log(newCleanerInfo);
            postCleanerInfo(props.token.oid, newCleanerInfo);
          }}
        >
          Zatwierdź
        </Button>
      </div>
    </div>
  );
}

export default AvailabilityTable;
