import { push, goBack,replace } from 'connected-react-router'
import { updateIdAction, updateErrorMessageAction, updateWallet, updateStatus } from './transaction'
import { updateVerificationErrorMessageAction, updateVerificationCode } from './verification'
export * from './transaction'
export * from './verification'


export const pushLoader = () => (dispatch) => {
  dispatch(push('/loader'))
}

export const pushVerification = (id,status) => (dispatch) => {
  dispatch(updateIdAction(id))
  dispatch(updateStatus(status))
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

export const pushSummary = (wallet,status) => (dispatch) => {
  dispatch(updateWallet(wallet))
  dispatch(updateStatus(status))
  dispatch(updateErrorMessageAction({}))
  dispatch(updateVerificationErrorMessageAction({}))
  dispatch(replace('/summary'))
}
