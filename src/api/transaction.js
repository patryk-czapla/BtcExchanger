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
        
        axios.post('http://localhost:5000/api/transaction', transaction)
        .then(function (response) {
            pushVerification(response.data.id,response.data.status)
            //console.log(response.data.id)
        })
        .catch(function (error) {           
            getBackWithError(error.response.data.errors)
            //console.log(error)
        })
    }, 
}