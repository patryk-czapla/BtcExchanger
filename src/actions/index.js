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
