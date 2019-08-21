import { push, goBack,replace } from 'connected-react-router'
import { updateIdAction, updateErrorMessageAction, updateWallet } from './order'
import { updateVerificationErrorMessageAction, updateVerificationCode } from './verification'
export * from './order'
export * from './verification'


export const pushLoader = () => (dispatch) => {
  dispatch(push('/loader'))
}

export const pushVerification = (id) => (dispatch) => {
  dispatch(updateIdAction(id))
  dispatch(updateErrorMessageAction({}))
  dispatch(updateVerificationErrorMessageAction({}))
  dispatch(updateVerificationCode(''))
  dispatch(replace('/verification'))
}

export const getBackWithError = (error_message) => (dispatch) => {
  dispatch(updateErrorMessageAction(error_message))
  dispatch(goBack())
}

export const getBackWithVerificationError = (verification_error_message) => (dispatch) => {
  dispatch(updateVerificationErrorMessageAction(verification_error_message))
  dispatch(goBack())
}

export const pushSummary = (wallet) => (dispatch) => {
  dispatch(updateWallet(wallet))
  dispatch(updateErrorMessageAction({}))
  dispatch(updateVerificationErrorMessageAction({}))
  dispatch(replace('/summary'))
}
