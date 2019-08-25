import React from 'react'
import { makeStyles, withStyles} from '@material-ui/core/styles'
import TextField from '@material-ui/core/TextField'

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
const BTCTextField = ({btc_quantity,onChanegeFnc}) => {
  
  const classes = useStyles()

  const btcProps = {
    step: 0.000001, 
    min: 0.000001,
  }
  
  return (
    <ValidationTextField
      id="btc-quantity"
      label="BTC to exchange "
      value={ btc_quantity }
      onChange={ (e) => onChanegeFnc( e.target.value )}
      type="number"
      className={classes.textField}
      InputLabelProps={{
        shrink: true,
      }}
      inputProps={btcProps}
      margin="normal"
      variant="outlined"
      required
    />
  )
}

export default BTCTextField