import axios from 'axios'

export default {
    postOrder: ({ 
        btc_quantity, 
        account_number, 
        contact_by_email, 
        email, 
        phone_number,
        getBackWithError,
        pushVerification
        }) => {
    
        let transaction = contact_by_email ? {
            "btc_quantity": btc_quantity,
            "account_number": account_number,
            "email": email, 
        }:{
            "btc_quantity": btc_quantity,
            "account_number": account_number,
            "phone_number": phone_number
        }
        
        axios.post(process.env.REACT_APP_API_URL+'/api/transaction', transaction)
        .then(function (response) {
            pushVerification(response.data.id,response.data.status)
            //console.log(response.data.id)
        })
        .catch(function (error) {     
            if(error.response != null){
                getBackWithError(error.response.data.errors)
            }else{
                getBackWithError({'errors':'could not connet with API at ' + process.env.REACT_APP_API_URL})
                //console.log(error)
            }
        })
    }, 
}