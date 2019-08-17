import { combineReducers } from 'redux'
import order, * as fromOrder from './order'

export default combineReducers({
    order,
})

export const getBTCQuanity = (state) => fromOrder.getBTCQuanity(state.order)
export const getAccountNumber = (state) => fromOrder.getAccountNumber(state.order)
export const getEmail = (state) => fromOrder.getEmail(state.order)
export const getPhoneNumber = (state) => fromOrder.getPhoneNumber(state.order)
export const getContactByEmail = (state) => fromOrder.getContactByEmail(state.order)