import React from "react";
import { BrowserRouter as Router, Routes, Route, BrowserRouter } from "react-router-dom";
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
import Settings from "./pages/Settings";


import Hyperlink from './pages/hyperlink';
import MemoryAlbumList from './pages/memoryalbumlist';
import Layout from "./Layout";
import RequireAuth from "./Components/RequireAuth";
import Unauthorized from "./pages/unauthorized"

function App() {
    return (
        <BrowserRouter>
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

                <Route path="/settings" element={<Settings />} />
                <Route path="/userprofiledashboard" element={<Userpd />} />
                
                <Route path="/share" element={<Hyperlink />} />
                
            </Routes>
        </BrowserRouter>
    );
}

export default App;

