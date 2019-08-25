
const initialState = {
  id: -1,
  btc_quantity: 0.000001,
  account_number: '',
  contact_by_email: true,
  email: '',
  phone_number: '',
  wallet:'',
  error_message: {},
  status: ''
}

const transaction = (state = initialState, action) => {
  switch (action.type) { 
    case 'UPDATE_ID':
      return {
        ...state, 
        id: action.id
      }
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
    case 'UPDATE_ERROR_MESSAGE':  
      return { 
        ...state, 
        error_message: action.error_message
      }
    case 'UPDATE_WALLET':  
      return { 
        ...state, 
        wallet: action.wallet
      } 
    case 'UPDATE_STATUS':
      return {
        ...state,
        status: action.status
      }
    default:
      return state
  }
}
export const getId = (state) => state.id
export const getBTCQuanity = (state) => state.btc_quantity
export const getAccountNumber = (state) => state.account_number
export const getContactByEmail = (state) => state.contact_by_email
export const getEmail = (state) => state.email
export const getPhoneNumber = (state) => state.phone_number
export const getErrorMessage = (state) => state.error_message
export const getWallet = (state) => state.wallet
export const getStatus = (state) => state.status

export default transaction