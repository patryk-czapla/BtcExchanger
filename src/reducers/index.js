import { combineReducers } from 'redux'
import { connectRouter } from 'connected-react-router'
import order, * as fromOrder from './order'
import verification, * as fromVerification from './verification'
const rootReducer = (history) => combineReducers({
    router: connectRouter(history),
    order,
    verification
})
export default rootReducer

export const getId = (state) => fromOrder.getId(state.order)
export const getBTCQuanity = (state) => fromOrder.getBTCQuanity(state.order)
export const getAccountNumber = (state) => fromOrder.getAccountNumber(state.order)
export const getEmail = (state) => fromOrder.getEmail(state.order)
export const getPhoneNumber = (state) => fromOrder.getPhoneNumber(state.order)
export const getContactByEmail = (state) => fromOrder.getContactByEmail(state.order)
export const getErrorMessage = (state) => fromOrder.getErrorMessage(state.order)
export const getWallet = (state) => fromOrder.getWallet(state.order)


export const getVerificationCode = (state) => fromVerification.getVerificationCode(state.verification)
export const getVerificationErrorMessage = (state) => fromVerification.getVerificationErrorMessage(state.verification)