import {useState} from 'react';
import TextField, {TextFieldProps} from '@mui/material/TextField';
import Select from '@mui/material/Select';
import InputLabel from '@mui/material/InputLabel';
import FormControl from '@mui/material/FormControl';
import MenuItem from '@mui/material/MenuItem';
import Button from '@mui/material/Button';
import DateTimePicker from '@mui/lab/DateTimePicker';
import Rating from '@mui/material/Rating';
import Typography from '@mui/material/Typography';
import Heading from '../Components/Heading';
import {postNewOrder} from '../Api/endpoints';
import Address, {isAddressCorrect, emptyAddress} from '../DataClasses/Address';
import MessLevelE from '../DataClasses/MessLevel';
import './PostAnnouncement.css';

const fieldWidth = '16em';

const AnnouncementFormField = (props: any) => {
  return (
    <div className='announcement-form-field'>
      <TextField
        on
        {...props}
        sx={{
          width: fieldWidth
        }}
      />
    </div>
  );
}

interface AddressFieldsProps {
  value: Address;
  onChange: (field: string, value: any) => void;
}

const AddressFields = (props: AddressFieldsProps) => {
  const [flatNumber, setFlatNumber] = useState<string>();
  const [isFlatNumberWrong, setIsFlatNumberWrong] = useState<boolean>(false);

  return (
    <>
      <AnnouncementFormField
        label='Miasto'
        value={props.value.city}
        onChange={(event: any) => props.onChange('city', event.target.value)}
      />
      <AnnouncementFormField
        label='Ulica'
        value={props.value.street}
        onChange={(event: any) => props.onChange('street', event.target.value)}
      />
      <AnnouncementFormField
        label='Numer budynku'
        value={props.value.buildingNo}
        onChange={(event: any) => props.onChange('buildingNo', event.target.value)}
      />
      <AnnouncementFormField
        label='Numer mieszkania'
        error={isFlatNumberWrong}
        value={flatNumber}
        onChange={(event: any) => {
          const val = event.target.value;
          setFlatNumber(val);

          if (val === '') {
            setIsFlatNumberWrong(false);
            props.onChange('flatNo', undefined);
            return;
          }

          const wrong = !/^[1-9]\d*$/.test(val);
          setIsFlatNumberWrong(wrong);
          if (wrong) {
            props.onChange('flatNo', NaN);
          }
          else {
            props.onChange('flatNo', parseInt(val));
          }
        }}
      />
      <AnnouncementFormField
        label='Kod pocztowy'
        value={props.value.postalCode}
        onChange={(event: any) => props.onChange('postalCode', event.target.value)}
      />
      <AnnouncementFormField
        label='Kraj'
        value={props.value.country}
        onChange={(event: any) => props.onChange('country', event.target.value)}
      />
    </>
  );
}

interface CleaningDateProps {
  value: Date;
  onChange: (time: Date) => void;
}

const CleaningDate = (props: CleaningDateProps) => {
  return (
    <>
      <div className='announcement-form-field'>
        <DateTimePicker
          renderInput={(props: TextFieldProps) => <TextField
            {...props}
            sx={{
              width: fieldWidth
            }}
          />}
          label='Czas sprzątania'
          value={props.value}
          onChange={(cleaningTime: Date | null) =>
            cleaningTime != null && props.onChange(cleaningTime)}
          minDateTime={new Date()}
        />
      </div>
    </>
  );
};

interface MessLevelProps {
  value: MessLevelE;
  onChange: (messLevel: MessLevelE) => void;
}

const MessLevel = (props: MessLevelProps) => {
  return (
    <div className='announcement-form-field'>
      <FormControl>
        <InputLabel id='mess-level'>Poziom bałaganu</InputLabel>
        <Select
          sx={{
            width: fieldWidth
          }}
          labelId='mess-level-label'
          id='mess-level'
          label='Poziom bałaganu'
          value={props.value}
          onChange={(event: any) => props.onChange(event.target.value)}
        >
          <MenuItem value={MessLevelE.Low}>Niski</MenuItem>
          <MenuItem value={MessLevelE.Moderate}>Średni</MenuItem>
          <MenuItem value={MessLevelE.Huge}>Duży</MenuItem>
          <MenuItem value={MessLevelE.Disaster}>Katastrofa</MenuItem>
        </Select>
      </FormControl>
    </div>
  );
}

const getDefaultDate = () => {
  const defaultDate = new Date();
  defaultDate.setHours(defaultDate.getHours() + 12);
  defaultDate.setMinutes(0);

  return defaultDate;
}

const PostAnnouncement = () => {
  const [address, setAddress] = useState<Address>(emptyAddress());
  const [description, setDescription] = useState<string>('');
  const [cleaningTime, setCleaningTime] = useState<Date>(getDefaultDate());
  const [messLevel, setMessLevel] = useState<MessLevelE>(MessLevelE.Low);
  const [minRating, setMinRating] = useState<number>(0.5);
  const [maxPrice, setMaxPrice] = useState<number | null>(Infinity);

  return (
    <div className='announcement-screen'>
      <Heading content='Dodaj nowe ogłoszenie' />
      <AddressFields
        value={address}
        onChange={(field: string, value: any) => {
          const addressCopy: Address = {...address};
          (addressCopy as any)[field] = value;
          setAddress(addressCopy);
        }}
      />
      <CleaningDate
        value={cleaningTime}
        onChange={(cleaningTime: Date) => {
          setCleaningTime(cleaningTime);
        }}
      />
      <AnnouncementFormField
        label='Maksymalna cena usługi (zł)'
        error={maxPrice === null}
        onChange={(event: any) => {
          const val = event.target.value;
          if (val === '') {
            setMaxPrice(Infinity);
            return;
          }

          if (/^\d+([,.]\d\d)?$/.test(val)) {
            const priceString = val.replace(',', '.');
            setMaxPrice(parseFloat(priceString) * 100);
          }
          else {
            setMaxPrice(null);
          }
        }}
      />
      <AnnouncementFormField
        label='Opis'
        value={description}
        onChange={(event: any) => setDescription(event.target.value)}
        multiline
        sx={{
          width: fieldWidth
        }}
      />
      <MessLevel
        value={messLevel}
        onChange={(messLevel: MessLevelE) => setMessLevel(messLevel)}
      />
      <div className='announcement-form-field'>
        <Typography component='legend'>Minimalna ocena</Typography>
        <Rating
          value={minRating / 2}
          onChange={(_event, newRating) => newRating != null && setMinRating(newRating * 2)}
          precision={0.5}
        />
      </div>
      <div className='announcement-form-field'>
        <Button
          variant='contained'
          onClick={() => {
            if (!isFormFilledCorrectly(address, maxPrice, cleaningTime)) {
              alert('Formularz nie został wypełniony poprawnie!');
              return;
            }

            const newOrder = {
              clientId: 'placeholder',
              maxPrice: maxPrice!,
              minRating: minRating,
              messLevel: messLevel,
              date: cleaningTime,
              address: address
            };

            postNewOrder(newOrder);
            console.log(newOrder);
          }}
        >
          Zatwierdź
        </Button>
      </div>
    </div>
  );
}

const isFormFilledCorrectly = (
  address: Address,
  maxPrice: number | null,
  cleaningTime: Date
) => {
  return isAddressCorrect(address) &&
    maxPrice !== null && maxPrice !== Infinity;
}

export default PostAnnouncement;
