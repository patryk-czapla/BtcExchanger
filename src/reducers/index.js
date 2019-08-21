import { combineReducers } from 'redux'
import { connectRouter } from 'connected-react-router'
import transaction, * as fromTransaction from './transaction'
import verification, * as fromVerification from './verification'
const rootReducer = (history) => combineReducers({
    router: connectRouter(history),
    transaction,
    verification
})
export default rootReducer

export const getId = (state) => fromTransaction.getId(state.transaction)
export const getBTCQuanity = (state) => fromTransaction.getBTCQuanity(state.transaction)
export const getAccountNumber = (state) => fromTransaction.getAccountNumber(state.transaction)
export const getEmail = (state) => fromTransaction.getEmail(state.transaction)
export const getPhoneNumber = (state) => fromTransaction.getPhoneNumber(state.transaction)
export const getContactByEmail = (state) => fromTransaction.getContactByEmail(state.transaction)
export const getErrorMessage = (state) => fromTransaction.getErrorMessage(state.transaction)
export const getWallet = (state) => fromTransaction.getWallet(state.transaction)


export const getVerificationCode = (state) => fromVerification.getVerificationCode(state.verification)
export const getVerificationErrorMessage = (state) => fromVerification.getVerificationErrorMessage(state.verification)