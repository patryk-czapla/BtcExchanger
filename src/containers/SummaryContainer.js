import React from 'react'
import { connect } from 'react-redux'
import { getId, getAccountNumber, getBTCQuanity, getContactByEmail, getEmail, getPhoneNumber, getWallet} from '../reducers'
import Summary from '../components/Summary'

const SummaryContainer = ( {
        id,
        account_number, 
        btc_quantity,
        contact_by_email, 
        email,
        phone_number,
        wallet
        } ) => (
        <Summary
            id = {id}
            account_number = {account_number}
            btc_quantity = {btc_quantity}
            contact_by_email = {contact_by_email}
            email = {email}
            phone_number = {phone_number}
            wallet= {wallet}
        />
    )

const mapStateToProps = (state) => ({
    id: getId(state),
    account_number: getAccountNumber(state),
    btc_quantity: getBTCQuanity(state),
    contact_by_email: getContactByEmail(state),
    email: getEmail(state),
    phone_number: getPhoneNumber(state),
    wallet: getWallet(state),
})

export default connect( 
    mapStateToProps   
)(SummaryContainer)