import React from "react";
import './App.css';
import Navbar from "./Components/Navbar";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import SignUp from './pages/signup';
import Home from './pages/home'
import MyCalendar from './pages/mycalendar';
import MemoryAlbum from './pages/mycalendar';

function App() {
    return (
        <Router>
            <Navbar />
            <Routes>            
                <Route path="/home" element={<Home/>} />
                <Route path="/calendar" element={<MyCalendar/>} />
                <Route path="/signup" element={<SignUp/>} />
                <Route path="/memoryalbum" element={<MemoryAlbum />} />

            </Routes>
        </Router>
    );
}

export default App;
