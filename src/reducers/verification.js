
const initialState = {
    verification_code:'',
    verification_error_message: {}
  }
  
  const verification = (state = initialState, action) => {
    switch (action.type) { 
      case 'UPDATE_VERIFICATION_CODE':
        return {
          ...state, 
          verification_code: action.verification_code
        }
      case 'UPDATE_VERIFICATION_ERROR_MESSAGE':  
        return { 
          ...state, 
          verification_error_message: action.verification_error_message
        }
        
      default:
        return state
    }
  }
  export const getVerificationCode = (state) => state.verification_code
  export const getVerificationErrorMessage = (state) => state.verification_error_message
  
  export default verification