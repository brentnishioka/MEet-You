import React from "react";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import './App.css';
import Navbar from "./Components/Navbar";
import SignUp from './pages/signup';
import Login from './pages/signin';
import Home from './pages/home'
import MyCalendar from './pages/mycalendar';
import Rating from "./pages/rating";
import Categories from './pages/categories';
import Events from './pages/events';
import Getrandomsuggestion from './pages/getrandomsuggestion';
import CreateItinerary from "./pages/itinerary";
import Userpd from "./pages/Userprofiledashboard"
import SettingsPage from "./pages/Settings";
import SettingsIcon from "./Components/UserProfileDashboard/SettingsPanel";


import Hyperlink from './pages/hyperlink';
import MemoryAlbumList from './pages/memoryalbumlist';

function App() {
    return (
        <Router>
            <Navbar />
            <Routes>
                <Route path="/" element={<Home />} />
                <Route path="/login" element={<Login />} />
                <Route path="/calendar" element={<MyCalendar />} />
                <Route path="/rating" element={<Rating />} />
                <Route path="/signup" element={<SignUp />} />
                <Route path="/memoryalbumlist" element={<MemoryAlbumList />} />
                <Route path="/categories" element={<Categories />} />
                <Route path="/events" element={<Events />} />
                <Route path="/getrandomsuggestion" element={<Getrandomsuggestion />} />
                <Route path="/itinerary" element={<CreateItinerary />} />
                <Route path="/Userprofiledashboard" element={<Userpd />} />
                <Route path="/settings" element={<SettingsIcon />} />
                <Route path="/share" element={<Hyperlink />} />
                
            </Routes>
        </Router>
    );
}

export default App;

