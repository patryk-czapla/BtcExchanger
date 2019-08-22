import axios from 'axios'

export default {
    putVerification: ({
        id,
        verification_code,
        getBackWithVerificationError,
        pushSummary
    }) => {
        let data = {
            'TransactionId': id,
            'verification_code': verification_code
        }
        axios.put(process.env.REACT_APP_API_URL+'/api/verification', data)
        .then(function (response) {
            pushSummary(response.data.wallet,response.data.status)
            //console.log(response)
        })
        .catch(function (error) {
            getBackWithVerificationError(error.response.data.errors)
            //console.log(error.response.data.errors)
        })
    }   
}