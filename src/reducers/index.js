import { combineReducers } from 'redux'
import order, * as fromOrder from './order'


export default combineReducers({
    order,
})

export const getBTCQuanity = (state) => fromOrder.getBTCQuanity(state.order)
export const getAccountNumber = (state) => fromOrder.getAccountNumber(state.order)