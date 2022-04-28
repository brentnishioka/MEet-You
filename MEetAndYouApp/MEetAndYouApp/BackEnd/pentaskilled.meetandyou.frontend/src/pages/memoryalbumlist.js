import React, { useState, useEffect } from 'react';

function MemoryAlbumList() {

    return (
        <div className="row">

            <div className="col-md-4">
                <Image/>
            </div>

            <div className="col-md-8">
                <div> List of Image Records </div>

            </div>
        </div>
        )
}

export default MemoryAlbumList;