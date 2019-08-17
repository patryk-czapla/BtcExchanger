import React from 'react'

import { connect } from 'react-redux'
import { getAccountNumber, getBTCQuanity, getContactByEmail, getEmail, getPhoneNumber} from '../reducers'
import { updateAccountNumber, updateBTCQuanity, updateContactByEmail, updateEmail, updatePhoneNumber } from '../actions'
import Exchanger from '../components/Exchanger'


const ExchangerContainer = ( {
    account_number, 
    btc_quantity,
    contact_by_email, 
    email,
    phone_number,
    updateAccountNumber, 
    updateBTCQuanity,
    updateContactByEmail,
    updateEmail,
    updatePhoneNumber } ) => (
    <Exchanger
        account_number = {account_number}
        btc_quantity = {btc_quantity}
        contact_by_email = {contact_by_email}
        email = {email}
        phone_number = {phone_number}
        
        updateAccountNumber = { val => { updateAccountNumber(val)}}
        updateBTCQuanity = { val => { updateBTCQuanity(val)}} 
        updateContactByEmail = { val => {updateContactByEmail(val)}}
        updateEmail = { val => { updateEmail(val)}}
        updatePhoneNumber = { val => { updatePhoneNumber(val)}} />
)

const mapStateToProps = (state) => ({
    account_number: getAccountNumber(state),
    btc_quantity: getBTCQuanity(state),
    contact_by_email: getContactByEmail(state),
    email: getEmail(state),
    phone_number: getPhoneNumber(state),
})

export default connect( 
    mapStateToProps,
    {
        updateAccountNumber,
        updateBTCQuanity,
        updateContactByEmail,
        updateEmail,
        updatePhoneNumber
    }
)(ExchangerContainer)