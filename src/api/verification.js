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
        axios.put('http://localhost:5000/api/verification', data)
        .then(function (response) {
            pushSummary(response.data.wallet)
            //console.log(response)
        })
        .catch(function (error) {
            getBackWithVerificationError(error.response.data.errors)
            //console.log(error.response.data.errors)
        })
    }   
}