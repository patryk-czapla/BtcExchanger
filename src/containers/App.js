import React from 'react'
import '../style/App.css'
import { Route, Switch } from "react-router"

import ExchangerContainer from './ExchangerContainer'
import LoaderContainer from './LoaderContainer'
import VerificationContainer from './VerificationContainer'
import SummaryContainer from './SummaryContainer'

const App = () => (
  <Switch>
    <Route path="/" exact component={ExchangerContainer} />
    <Route path="/loader" component={LoaderContainer} />
    <Route path="/verification" component={VerificationContainer} />
    <Route path="/summary" component={SummaryContainer} />
  </Switch>
)

export default App