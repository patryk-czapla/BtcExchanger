import React from 'react'

import { connect } from 'react-redux'
import { getAccountNumber, getBTCQuanity } from '../reducers'
import { updateAccountNumber, updateBTCQuanity } from '../actions'
import Exchanger from '../components/Exchanger'


const ExchangerContainer = ( {account_number, btc_quantity, updateAccountNumber, updateBTCQuanity } ) => (
    <Exchanger
        account_number = {account_number}
        btc_quantity = {btc_quantity}
        updateAccountNumber = { val => { updateAccountNumber(val)}}
        updateBTCQuanity = { val => { updateBTCQuanity(val)}}/>
)

const mapStateToProps = (state) => ({
    account_number: getAccountNumber(state),
    btc_quantity: getBTCQuanity(state),
})

export default connect( 
    mapStateToProps,
    {
        updateAccountNumber,
        updateBTCQuanity
    }
)(ExchangerContainer)