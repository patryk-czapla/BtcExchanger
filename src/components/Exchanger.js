import React from 'react'

import { makeStyles } from '@material-ui/core/styles'
import Paper from '@material-ui/core/Paper'
import Grid from '@material-ui/core/Grid'
import Button from '@material-ui/core/Button'
import Icon from '@material-ui/core/Icon'
import { loadCSS } from 'fg-loadcss'
import clsx from 'clsx'

import BTCTextField from '../components/BTCTextField'
import AccountTextField from '../components/AccountTextField'
import ContactSwitch from '../components/ContactSwitch'
import EmailTextField from '../components/EmailTextField'
import PhoneNumberTextField from '../components/PhoneNumberTextField'

import API from '../api/order'

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
    contactHeader:{
        margin: 10        
    },
    centerArrow:{
        margin: '30px 20px'
    },
    error_message:{
        color: 'red'
    }

}))

const Exchanger = ({
    account_number, 
    btc_quantity,
    contact_by_email, 
    email, 
    phone_number, 
    error_message,
    updateAccountNumber, 
    updateBTCQuanity,
    updateContactByEmail, 
    updateEmail, 
    updatePhoneNumber,
    pushLoader,
    getBackWithError,
    pushVerification
    }) => {
        
    const classes = useStyles()
    React.useEffect(() => {
        loadCSS(
          'https://use.fontawesome.com/releases/v5.1.0/css/all.css',
          document.querySelector('#font-awesome-css'),
        )
      }, [])
    return (
        <div className="App-content">
            <h1>BTC Exchanger</h1>            
            {  /*Show errors if they occur on the backend.*/
                Object.entries(error_message).length === 0 && error_message.constructor === Object ?                 
                ''                                    
                : 
                <div className="container">
                    <h3>There where some problems with Your form, please corect:</h3>
                    <ul className={classes.error_list}>
                    {
                        Object.keys(error_message).map(function(object,i) {
                            return <li key={i} className={classes.error_message}>{error_message[object]}</li>
                        })
                    }
                    </ul>
                </div>
            }
            <form onSubmit={e => {
                e.preventDefault()
                pushLoader()
                API.postOrder({
                    btc_quantity,
                    account_number,
                    contact_by_email,
                    email,
                    phone_number, 
                    getBackWithError,
                    pushVerification
                })
            }}>
            <Paper className={classes.paper}>
                <Grid container wrap="nowrap" justify="space-between">
                    <Grid item >
                        <BTCTextField
                            btc_quantity = { btc_quantity } 
                            onChanegeFnc = { val => { updateBTCQuanity(val) } }
                        />
                    </Grid>
                    <Grid className={classes.centerArrow}>
                        <Icon className={clsx(classes.icon, 'fas fa-angle-double-right')}/>
                    </Grid>
                    <Grid item >
                        <AccountTextField 
                            account_number = { account_number } 
                            onChanegeFnc = { val => { updateAccountNumber(val) } }
                        />
                    </Grid>
                </Grid>
            </Paper>               
            <Paper className={classes.paper} >
                <Grid container
                    wrap="nowrap"
                    direction="row"
                    justify="space-between"
                    alignItems="center">
                    <Grid item>                        
                        <Grid container
                            justify="space-evenly"
                            alignItems="center"
                            wrap="nowrap">
                            <h4 className={classes.contactHeader}>Email</h4>                    
                            <ContactSwitch 
                                className={ classes.contactSwitch }
                                onChanegeFnc = { () => { updateContactByEmail(!contact_by_email)} }/>
                            <h4 className={classes.contactHeader}>Phone</h4>                             
                        </Grid>   
                    </Grid>  
                    <Grid item>
                    { contact_by_email ?                 
                        <EmailTextField 
                            email = { email } 
                            onChanegeFnc = { val => { updateEmail(val) } }
                        />
                        : 
                        <PhoneNumberTextField 
                            phone_number = { phone_number } 
                            onChanegeFnc = { val => { updatePhoneNumber(val) } }
                        />
                    }
                    </Grid>
                </Grid> 
            </Paper>
            <Grid container
                direction="row"
                justify="flex-end"
                alignItems="center"
                >
                <Button type="submit" variant="contained" color="primary" className={classes.button}>
                    Submit                            
                    <Icon className={classes.rightIcon}>send</Icon>
                </Button>       
            </Grid>  
            <Grid container
                direction="row"
                justify="flex-end"
                alignItems="center"
                >                     
            </Grid>                                         
            </form>             
        </div>
    )
}

export default Exchanger