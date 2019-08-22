import React from 'react'
import { Link } from "react-router-dom"
import { makeStyles } from '@material-ui/core/styles'
import Table from '@material-ui/core/Table'
import TableBody from '@material-ui/core/TableBody'
import TableCell from '@material-ui/core/TableCell'
import TableHead from '@material-ui/core/TableHead'
import TableRow from '@material-ui/core/TableRow'
import Paper from '@material-ui/core/Paper'
import Button from '@material-ui/core/Button'
import Icon from '@material-ui/core/Icon'

const useStyles = makeStyles(theme => ({
    table: {
        minWidth: 650,
    },
    button:{
        margin: "20px"
    },
    link:{
        textDecoration: "none",
        color:"white"
    }

}))
function createData(name, value) {
    return { name, value }
  }
const Summary = ({
    id,
    account_number, 
    btc_quantity,
    contact_by_email, 
    email, 
    phone_number, 
    wallet,
    status
    }) => {
    const classes = useStyles()
    const rows = [
        createData('Order id',  id),
        createData('Btc Quantity', btc_quantity),
        createData('Account number', account_number),
        contact_by_email ? createData('Email', email) : createData('Phone number', phone_number),
        createData('Wallet', wallet),
        createData('Status', status)
    ];    
    return (
        <div className="App-content">
            <Paper className={classes.root}>
                <Table className={classes.table}>
                    <TableHead>
                        <TableRow>
                        <TableCell>Name</TableCell>
                        <TableCell align="right">Value</TableCell>              
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {rows.map(row => (
                        <TableRow key={row.name}>
                            <TableCell component="th" scope="row">
                                {row.name}
                            </TableCell>
                            <TableCell align="right">{row.value}</TableCell>                
                        </TableRow>
                        ))}
                    </TableBody>
                </Table>
            </Paper>             
            <Link className={classes.link} to="/">
                <Button  variant="contained" color="primary" className={classes.button}>
                    Create new transaction
                    <Icon className={classes.rightIcon}>send</Icon>
                </Button> 
            </Link>                                                                                 
      </div>       
    )
}

export default Summary