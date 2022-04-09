import {useState} from 'react';
import Button from '@mui/material/Button';
import TextField from '@mui/material/TextField';
import Comission from '../../DataClasses/Commission';
import {postCommission} from '../../Api/endpoints';
import "./ComissionForm.css";

const ComissionForm = () => {
  const [comission, setComission] = useState<Comission | null>(null);

  return (
    <div className='comission-container'>
      <div className='field-container'>
        <TextField
          label='Nowa prowizja'
          error={comission === null}
          onChange={(event: any) => {
            const val = event.target.value;

            if (/^0[,.]\d+$/.test(val)) {
              const comissionString = val.replace(',', '.');
              setComission({newProvision: parseFloat(comissionString)});
            }
            else {
              setComission(null);
            }
          }}
        />
      </div>
      <div className='field-container'>
        <Button
          variant='contained'
          onClick={async () => {
            if (comission !== null) {
              await postCommission(comission);
            }
          }}
        >
          Zatwierdź
        </Button>
      </div>
    </div>
  );
};

export default ComissionForm;
