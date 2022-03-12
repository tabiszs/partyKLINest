import TextField from '@mui/material/TextField';
import Button from '@mui/material/Button';
import './PostAnnouncement.css';

export interface AnnouncementFormFieldProps {
  label: string;
}

const AnnouncementFormField = (props: AnnouncementFormFieldProps) => {
  return (
    <div className='announcement-form-field'>
      <TextField label={props.label} />
    </div>
  );
}

const PostAnnouncement = () => {
  return (
    <div className='announcement-screen'>
      <AnnouncementFormField label='Adres' />
      <AnnouncementFormField label='Data' />
      <AnnouncementFormField label='Godzina' />
      <AnnouncementFormField label='Koszt' />
      <AnnouncementFormField label='Opis' />
      <AnnouncementFormField label='Stopień bałaganu' />
      <AnnouncementFormField label='Minimalny Próg oceny' />
      <AnnouncementFormField label='Dodaj zdjęcie' />
      <AnnouncementFormField label='Kontakt' />
      <div className='announcement-form-field'>
        <Button variant='contained'>Zatwierdź</Button>
      </div>
    </div>
  );
}

export default PostAnnouncement;
