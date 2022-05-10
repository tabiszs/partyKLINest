import Select from '@mui/material/Select';
import InputLabel from '@mui/material/InputLabel';
import FormControl from '@mui/material/FormControl';
import MenuItem from '@mui/material/MenuItem';
import MessLevel from '../DataClasses/MessLevel';

interface MessLevelProps {
  value: MessLevel;
  onChange: (messLevel: MessLevel) => void;
  label?: string;
}

const MessSelector = (props: MessLevelProps) => {
  return (
    <div className='mess-selector-container'>
      <FormControl>
        <InputLabel id='mess-level'>{props.label ? props.label : 'Poziom bałaganu'}</InputLabel>
        <Select
          sx={{
            width: '16em'
          }}
          labelId='mess-level-label'
          id='mess-level'
          label={props.label ? props.label : 'Poziom bałaganu'}
          value={props.value}
          onChange={(event: any) => props.onChange(event.target.value)}
        >
          <MenuItem value={MessLevel.Low}>Niski</MenuItem>
          <MenuItem value={MessLevel.Moderate}>Średni</MenuItem>
          <MenuItem value={MessLevel.Huge}>Duży</MenuItem>
          <MenuItem value={MessLevel.Disaster}>Katastrofa</MenuItem>
        </Select>
      </FormControl>
    </div>
  );
}

export default MessSelector;
