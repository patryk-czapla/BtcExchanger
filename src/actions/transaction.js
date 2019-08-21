export const updateErrorMessage = error_message => (dispatch) => {
    dispatch(updateErrorMessageAction(error_message))
  }
  
export  const updateErrorMessageAction = error_message => ({
    type: 'UPDATE_ERROR_MESSAGE',
    error_message
  })
  
  export  const updateWallet = wallet => ({
    type: 'UPDATE_WALLET',
    wallet
  })

  export const updateId = id => (dispatch) => {
    dispatch(updateIdAction(id))
  }
  
  export  const updateIdAction = id => ({
    type: 'UPDATE_ID',
    id
  })
  
  export const updateBTCQuanity = btc_quantity => (dispatch) => {
    dispatch(updateBTCQuanityAction(btc_quantity))
  }
  
  const updateBTCQuanityAction = btc_quantity => ({
    type: 'UPDATE_BTC_QUANTITY',
    btc_quantity
  })
  
  export const updateAccountNumber = account_number => (dispatch) => {
    dispatch(updateAccountNumberAction(account_number))
  }
  
  const updateAccountNumberAction = account_number => ({
    type: 'UPDATE_ACCOUNT_NUMBER',
    account_number
  })
  
  export const updateContactByEmail = contact_by_email => (dispatch) => {
    dispatch(updateContactByEmailAction(contact_by_email))
  }
  
  const updateContactByEmailAction = contact_by_email => ({
    type: 'UPDATE_CONTACT_BY_EMAIL',
    contact_by_email
  })
  
  export const updateEmail = email => (dispatch) => {
    dispatch(updateEmailAction(email))
  }
  
  const updateEmailAction = email => ({
    type: 'UPDATE_EMAIL_NUMBER',
    email
  })
  
  export const updatePhoneNumber = phone_number => (dispatch) => {
    dispatch(updatePhoneNumberAction(phone_number))
  }
  
  const updatePhoneNumberAction = phone_number => ({
    type: 'UPDATE_PHONE_NUMBER',
    phone_number
  })