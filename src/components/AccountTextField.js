import React from 'react';
import { makeStyles, withStyles} from '@material-ui/core/styles';
import TextField from '@material-ui/core/TextField';

const ValidationTextField = withStyles({
  root: {
    '& input:valid + fieldset': {
      borderColor: 'green',
      borderWidth: 2,
    },
    '& input:invalid + fieldset': {
      borderColor: 'red',
      borderWidth: 2,
    },
    '& input:valid:focus + fieldset': {
      borderLeftWidth: 6,
      padding: '4px !important', // override inline-style
    },
  },
})(TextField)

const useStyles = makeStyles(theme => ({
  root: {
    display: 'flex',
    flexWrap: 'wrap',
  },
  margin: {
    margin: theme.spacing(1),
  },
}))

const AccountTextField = ({account_number, onChanegeFnc}) => {
  
  const classes = useStyles()

  return (
    <ValidationTextField
      id="account-number"
      label="Account number"
      value={account_number}
      onChange={(e)=>onChanegeFnc(e.target.value)}
      className={classes.textField}
      InputLabelProps={{
        shrink: true,
      }}
      margin="normal"
      variant="outlined"
    />
  )
}

export default AccountTextField