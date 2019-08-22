export const updateVerificationCode = verification_code => (dispatch) => {
    dispatch(updateVerificationCodeAction(verification_code))
}
  
const updateVerificationCodeAction = verification_code => ({
    type: 'UPDATE_VERIFICATION_CODE',
    verification_code
})

export const updateVerificationErrorMessageAction = verification_error_message => ({
    type: 'UPDATE_VERIFICATION_ERROR_MESSAGE',
    verification_error_message
})



