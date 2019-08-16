
const initialState = {
  id: 0,
  btc_quantity: 0,
  account_number: '',
}

const order = (state = initialState, action) => {
  switch (action.type) { 
    case 'UPDATE_BTC_QUANTITY':
      return {
        ...state, 
        btc_quantity: action.btc_quantity
      }
    case 'UPDATE_ACCOUNT_NUMBER':      
      return { 
        ...state, 
        account_number: action.account_number
      }
    default:
      return state
  }
}

export const getBTCQuanity = (state) => state.btc_quantity
export const getAccountNumber = (state) => state.account_number

export default order