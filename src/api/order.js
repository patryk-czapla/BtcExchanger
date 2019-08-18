import axios from 'axios'

export default {
    postOrder: ({ 
        btc_quantity, 
        account_number, 
        contact_by_email, 
        email, 
        phone_number}) => {
            
        let order = contact_by_email ? {
            "btc_quantity": btc_quantity,
            "account_number": account_number,
            "email": email, 
        }:{
            "btc_quantity": btc_quantity,
            "account_number": account_number,
            "phone_number": phone_number
        }
        
        axios.post('http://localhost:5000/api/order', order)
        .then(function (response) {
            console.log(response);
        })
        .catch(function (error) {
            console.log(error);
        })
    }
            
}