import React from 'react'
import { connect } from 'react-redux'
import { getAccountNumber, getBTCQuanity, getContactByEmail, getEmail, getPhoneNumber, getErrorMessage} from '../reducers'
import { updateAccountNumber, updateBTCQuanity, updateContactByEmail, updateEmail, updatePhoneNumber, pushLoader, getBackWithError, pushVerification } from '../actions'
import Exchanger from '../components/Exchanger'

const ExchangerContainer = ( {
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
    } ) => (
    <Exchanger
        account_number = {account_number}
        btc_quantity = {btc_quantity}
        contact_by_email = {contact_by_email}
        email = {email}
        phone_number = {phone_number}
        error_message = {error_message}
        updateAccountNumber = { val => { updateAccountNumber(val)}}
        updateBTCQuanity = { val => { updateBTCQuanity(val)}} 
        updateContactByEmail = { val => {updateContactByEmail(val)}}
        updateEmail = { val => { updateEmail(val)}}
        updatePhoneNumber = { val => { updatePhoneNumber(val)}} 
        pushLoader = { () =>{pushLoader()}}
        getBackWithError = { (val) =>{getBackWithError(val)}}
        pushVerification = { (val,val2) =>{pushVerification(val,val2)}}
        />
)

const mapStateToProps = (state) => ({
    account_number: getAccountNumber(state),
    btc_quantity: getBTCQuanity(state),
    contact_by_email: getContactByEmail(state),
    email: getEmail(state),
    phone_number: getPhoneNumber(state),
    error_message: getErrorMessage(state)
})

export default connect( 
    mapStateToProps,
    {
        updateAccountNumber,
        updateBTCQuanity,
        updateContactByEmail,
        updateEmail,
        updatePhoneNumber,
        pushLoader,
        getBackWithError,
        pushVerification
    }
)(ExchangerContainer)