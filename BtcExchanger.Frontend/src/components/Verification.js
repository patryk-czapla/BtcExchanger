import React from 'react'
import { makeStyles, withStyles} from '@material-ui/core/styles'
import TextField from '@material-ui/core/TextField'
import Grid from '@material-ui/core/Grid'
import Paper from '@material-ui/core/Paper'
import API from '../api/verification'
import Button from '@material-ui/core/Button'
import Icon from '@material-ui/core/Icon'

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
        flexGrow: 1,
        overflow: 'hidden',
        padding: theme.spacing(0, 1)
    },
    paper: {
        maxWidth: 900,        
        margin: `${theme.spacing(1)}px auto`,
        padding: theme.spacing(1, 5)
    },
    error_message:{
        color: 'red'
    }
}))
  
const Verification = ( { 
    id, 
    verification_error_message, 
    verification_code, 
    updateVerificationCode, 
    pushLoader, 
    getBackWithVerificationError,
    pushSummary }) => {
        
    const classes = useStyles()
    return (
        <div className="App-content">
            <h3>Please enter the verification code</h3>
            {  /*Show errors if they occur on the backend.*/
                Object.entries(verification_error_message).length === 0 && verification_error_message.constructor === Object ?                 
                ''                                    
                : 
                <div className="container">
                    <h3>There where some problems, please corect:</h3>
                    <ul className={classes.error_list}>
                    {
                        Object.keys(verification_error_message).map(function(object,i) {
                            return <li id={object+'-error'} key={i} className={classes.error_message}>{verification_error_message[object]}</li>
                        })
                    }
                    </ul>
                </div>
           }
            <form onSubmit={e => {
                e.preventDefault()
                pushLoader()
                API.putVerification({id,verification_code,getBackWithVerificationError,pushSummary})                                        
            }}>
                <Paper className={classes.paper}>
                    <Grid container wrap="nowrap" justify="space-between">                                      
                        <Grid item >
                        <ValidationTextField
                            id="verification-code"
                            label="Verification code"
                            value={verification_code}                        
                            className={classes.textField}
                            onChange={(e) => updateVerificationCode(e.target.value)}
                            InputLabelProps={{
                                shrink: true,
                            }}
                            margin="normal"
                            variant="outlined"
                            />
                        </Grid>
                    </Grid>
                </Paper> 
                <Grid 
                container
                direction="row"
                justify="flex-end"
                alignItems="center" >
                    <Grid item >
                    <Button type="submit" variant="contained" color="primary" className={classes.button}>
                        verify                            
                        <Icon className={classes.rightIcon}>send</Icon>
                    </Button>   
                    </Grid>
                </Grid>    
            </form> 
        </div>      
    )
}

export default Verification
