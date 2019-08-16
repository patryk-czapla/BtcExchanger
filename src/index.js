import React from 'react'
import { render }  from 'react-dom'
import { createStore, applyMiddleware } from 'redux'
import { Provider } from 'react-redux'
import App from './containers/App'
import thunk from 'redux-thunk'
import rootReducer from './reducers'
import './style/index.css'
import * as serviceWorker from './serviceWorker'

const middleware = [ thunk ];
const store = createStore(
    rootReducer,
    applyMiddleware(...middleware)
)
render( 
    <Provider store={store}>
        <App />
    </Provider>,
    document.getElementById('root')
)

// If you want your app to work offline and load faster, you can change
// unregister() to register() below. Note this comes with some pitfalls.
// Learn more about service workers: https://bit.ly/CRA-PWA
serviceWorker.unregister()
