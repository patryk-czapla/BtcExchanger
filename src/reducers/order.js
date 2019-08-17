
const initialState = {
  id: 0,
  btc_quantity: 0,
  account_number: '',
  contact_by_email: true,
  email: '',
  phone_number: '',
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
    case 'UPDATE_CONTACT_BY_EMAIL':      
        return { 
          ...state, 
          contact_by_email: action.contact_by_email
        }
    case 'UPDATE_EMAIL_NUMBER':      
      return { 
        ...state, 
        email: action.email
      }
    case 'UPDATE_PHONE_NUMBER':      
      return { 
        ...state, 
        phone_number: action.phone_number
      }
    default:
      return state
  }
}

export const getBTCQuanity = (state) => state.btc_quantity
export const getAccountNumber = (state) => state.account_number
export const getContactByEmail = (state) => state.contact_by_email
export const getEmail = (state) => state.email
export const getPhoneNumber = (state) => state.phone_number

export default order