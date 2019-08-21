import React from 'react'
import { render }  from 'react-dom'
import { createBrowserHistory } from 'history'
import { createStore, compose, applyMiddleware } from 'redux'
import { Provider } from 'react-redux'
import App from './containers/App'
import thunk from 'redux-thunk'
import rootReducer from './reducers'
import './style/index.css'
import * as serviceWorker from './serviceWorker'
import { routerMiddleware } from 'connected-react-router'
import { ConnectedRouter } from 'connected-react-router'

export const history = createBrowserHistory()

const store = createStore(
    rootReducer(history),
    compose(
        applyMiddleware(
            routerMiddleware(history),
            thunk
        )
    )
)

render( 
    <Provider store={store}>
        <ConnectedRouter history={history}>
            <App/>
        </ConnectedRouter>
    </Provider>,
    document.getElementById('root')
)

// If you want your app to work offline and load faster, you can change
// unregister() to register() below. Note this comes with some pitfalls.
// Learn more about service workers: https://bit.ly/CRA-PWA
serviceWorker.unregister()
