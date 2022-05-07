import './App.css';
import React from 'react';
import Login from './components/pages/Login'
import Logout from './components/pages/Logout'
import { BrowserRouter, Switch, Route, } from 'react-router-dom'

function App() {
  return (
    <div className="App">
      <BrowserRouter>
        <Switch>
          <Route path='/login'  component={Login} />
          <Route path='/logout' component={Logout} />
          {/* <Redirect to='/home' /> */}
        </Switch>
      </BrowserRouter>
    </div>
  );
}

export default App;
