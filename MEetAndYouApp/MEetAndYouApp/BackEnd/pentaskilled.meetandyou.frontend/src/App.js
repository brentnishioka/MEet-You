import React from "react";
import './App.css';
import Navbar from "./Components/Navbar";
import { BrowserRouter as Router, Switch, Route } from "react-router-dom";
import SignUp from './pages/signup';
import Home from './pages/home'
import MyCalendar from './pages/mycalendar';

function App() {
    return (
        <Router>
            <Navbar />
            <Switch>
               
                <Route path="/home" component={Home} />
                <Route path="/calendar" component={MyCalendar} />
                <Route path="/signup" component={SignUp} />
               
            </Switch>
        </Router>
    );
}

export default App;
