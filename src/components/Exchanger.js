import React from 'react'

import { makeStyles } from '@material-ui/core/styles';
import Paper from '@material-ui/core/Paper';
import Grid from '@material-ui/core/Grid';

import BTCTextField from '../components/BTCTextField'
import AccountTextField from '../components/AccountTextField'

const useStyles = makeStyles(theme => ({
    root: {
        flexGrow: 1,
        overflow: 'hidden',
        padding: theme.spacing(0, 3),
    },
    paper: {
        maxWidth: 800,
        margin: `${theme.spacing(1)}px auto`,
        padding: theme.spacing(2),
    },
}))

const Exchanger = ({account_number, btc_quantity, updateAccountNumber, updateBTCQuanity }) => {
  
    const classes = useStyles()

    return (
        <div className="App-content">
            <h1>BTC Exchanger</h1>
            <form onSubmit={e => {
                e.preventDefault()
                console.log({
                    "values.btc_quantity": btc_quantity,
                    "values.account_number": account_number
                })                               
            }}>
            <Paper className={classes.paper}>
            <Grid container wrap="nowrap" spacing={2}>
                <Grid item xs>
                    <BTCTextField
                        btc_quantity = { btc_quantity } 
                        onChanegeFnc = { val => { updateBTCQuanity(val) } }
                    />
                </Grid>
            </Grid>
            </Paper>
            <Paper className={classes.paper}>
            <Grid container wrap="nowrap" spacing={2}>
                <Grid item xs>
                    <AccountTextField 
                        account_number = { account_number } 
                        onChanegeFnc = { val => { updateAccountNumber(val) } }
                    />
                </Grid>
            </Grid>
            </Paper>  
            <button type="submit">
                Submit
            </button>
            </form>       
        </div>
    )
}

export default Exchanger