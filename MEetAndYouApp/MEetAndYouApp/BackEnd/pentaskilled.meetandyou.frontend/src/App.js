import './App.css';
import React from 'react';
import Login from './Scenes/Sign/Scenes/Login/login.js';
//import Logout from './components/pages/Logout'
import { BrowserRouter, Switch, Route, } from 'react-router-dom'

function App() {
    return (
        <div className="App">
            <BrowserRouter>
                <Switch>
                    <Route path='/login' component={Login} />
                    {/* <Redirect to='/home' /> */}
                </Switch>
            </BrowserRouter>
        </div>
    );
}

export default App;
