import React, { useState, useEffect } from 'react';
import Image from './memoryalbum'

function MemoryAlbumList() {

    return (
        <div className="row">
            <div className="col-md-12">
                <div className="jumbotron jumbotron-fluid py-4">
                    <div className="container text-center">
                        <h1 className="display-4">Image Register</h1>
                    </div>
                </div>
            </div>
            <div className="col-md-4">
                <Image />
            </div>
            <div className="col-md-8">
                <div>List of Images Records</div>
            </div>
        </div>
    )
}

export default MemoryAlbumList;