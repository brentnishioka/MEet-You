import React from "react";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import './App.css';
import Navbar from "./Components/Navbar";
import SignUp from './pages/signup';
import Login from './pages/signin';
import Home from './pages/home'
import MyCalendar from './pages/mycalendar';
import GetSuggestion from "./pages/getsuggestion";

function App() {
    return (
        <Router>
            <Navbar />
            <Routes>            
                <Route path="/" element={<Home />} />
                <Route path="/login" element={<Login />} />
                <Route path="/calendar" element={<MyCalendar/>} />
                <Route path="/signup" element={<SignUp/>} />
                <Route path="/getsuggestion" element={<GetSuggestion/>} />
            </Routes>
        </Router>
    );
}

export default App;
