import React, { useState, useEffect } from 'react';
import MemoryAlbumList from './memoryalbumlist';

const initFieldValues = {
    imageId: 0,
    imageName : '',
    imageExtension: '',
    imagePath: ''
}
function MemoryAlbum() {
    const [values, setValues] = useState()

    return (
        <>

            <div className='container text-center'>
                <p className='lead'>Images</p>
            </div>
            <form autoComplete='off' noValidate>
                <div className='card'>
                    <div className='card-body'></div>

                </div>
            </form>

        </>


    )

}

export default MemoryAlbum;