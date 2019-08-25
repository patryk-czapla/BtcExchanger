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
            if(error.response != null){
                getBackWithVerificationError(error.response.data.errors)
            }else{
                getBackWithVerificationError({'connection-error':'could not connet with API at ' + process.env.REACT_APP_API_URL})
                //console.log(error)
            }
        })
    }   
}