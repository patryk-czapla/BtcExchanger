import React from 'react'
import { connect } from 'react-redux'
import { getId, getVerificationCode,getVerificationErrorMessage} from '../reducers'
import { pushLoader, getBackWithVerificationError, updateVerificationCode, pushSummary} from '../actions'
import Verification from '../components/Verification'

const VerificationContainer = ( {
    id,
    verification_error_message,
    verification_code,
    updateVerificationCode,
    pushLoader,
    getBackWithVerificationError,
    pushSummary
    } ) => (
    <Verification
        id = {id}
        verification_error_message = {verification_error_message}
        verification_code = {verification_code}
        updateVerificationCode = { (val) =>{updateVerificationCode(val)}}
        pushLoader = { () =>{pushLoader()}}
        getBackWithVerificationError = { (val) =>{getBackWithVerificationError(val)} }
        pushSummary = {(val)=>pushSummary(val)}
        />
)

const mapStateToProps = (state) => ({
    id: getId(state),
    verification_error_message: getVerificationErrorMessage(state),
    verification_code: getVerificationCode(state),
})

export default connect( 
    mapStateToProps,
    {
        updateVerificationCode,
        pushLoader,
        getBackWithVerificationError,
        pushSummary
    }
)(VerificationContainer)