import React, { useState, useEffect } from 'react';
import Image from './memoryalbum'
import axios from "axios";
import MemoryAlbum from './memoryalbum';

function MemoryAlbumList() {
    const [memoryAlbumList, setmemoryAlbumList] = useState([])
    const [recordForEdit, setRecordForEdit] = useState(null)


    const [imageName, setImageName] = useState("");
    const [imageExtension, setExtension] = useState("");
    const [imagePath, setPath] = useState("");
    const [itineraryID, setitineraryID] = useState();

    const [response, setResponse] = useState("");
    useEffect(() => {
        refreshmemoryAlbumList();
    }, [])



    const AddImages = async (request) => {
        const requestOptions = {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' }
        }

        console.log("Image Name", imageName)
        console.log("Image Extension", imageExtension)
        console.log("Image Path:", imagePath)
        console.log("Itinerary ID:", itineraryID)

        try {
            const res = await fetch('https://meetandyou.me:8001/MemoryAlbum/PostImages?ImageName=' + imageName + '&ImageExtension=' + imageExtension + '&ImagePath=' + imagePath + '&itineraryID=' + itineraryID, requestOptions);
            const AddedImage = await res.json();

            setmemoryAlbumList(AddedImage.data)
            setResponse(AddedImage.response)
            console.log(AddedImage)

        }
        catch (error) {
            console.log('error');
        }
    }


    const RemoveImage = async (request) => {
        const requestOptions = {
            method: 'DELETE',
            headers: { 'Content-Type': 'application/json' }
        }

        console.log("Image Name", imageName)
        console.log("Itinerary ID:", itineraryID)

        try {
            const res = await fetch('https://meetandyou.me:8001/MemoryAlbum/DeleteImage?/' + itineraryID + '?ImageName=' + imageName, requestOptions);
            const DeletedImage = await res.json();

            setmemoryAlbumList(DeletedImage.data)
            setResponse(DeletedImage.response)
            console.log(DeletedImage)

        }
        catch (error) {
            console.log('error');
        }
    }

    const GetImage = async (request) => {
        const requestOptions = {
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }


        console.log("Itinerary ID:", itineraryID)

        try {
            const res = await fetch('https://meetandyou.me:8001/MemoryAlbum/GetImage?/' + itineraryID, requestOptions);
            const RequestedImage = await res.json();

            setmemoryAlbumList(RequestedImage.data)
            setResponse(RequestedImage.response)
            console.log(RequestedImage)

        }
        catch (error) {
            console.log('error');
        }
    }


    function refreshmemoryAlbumList() {
        GetImage()
            .then(res => {
                setmemoryAlbumList(res.data)
            })
            .catch(err => console.log(err))
    }

    const addOrEdit = (formData, onSuccess) => {
        if (formData.get('imageID') == "0")
            AddImages(formData)
                .then(res => {
                    onSuccess();
                    refreshmemoryAlbumList();
                })
                .catch(err => console.log(err))


    }

    const showRecordDetails = data => {
        setRecordForEdit(data)
    }

    const onDelete = (e, id) => {
        e.stopPropagation();
        if (window.confirm('Are you sure to delete this record?'))
            RemoveImage()
                .then(res => refreshmemoryAlbumList())
                .catch(err => console.log(err))
    }




    const imageCard = data => (
        <div className="card" onClick={() => { showRecordDetails(data) }}>
            <img src={data.imageSrc} className="card-img-top rounded-circle" />
            <div className="card-body">
                <h5>{data.ImageName}</h5>
                <span>{data.imageSrc}</span> <br />
                <button className="btn btn-light delete-button" onClick={e => onDelete(e, parseInt(data.imageID))}>
                    <i className="far fa-trash-alt"></i>
                </button>
            </div>
        </div>
    )

 
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
                <MemoryAlbum
                    addOrEdit={addOrEdit}
                    recordForEdit={recordForEdit}
                />
            </div>
            <div className="col-md-8">
                <table>
                    <tbody>
                        {
                            //tr > 3 td
                            [...Array(Math.ceil(memoryAlbumList.length / 3))].map((e, i) =>
                                <tr key={i}>
                                    <td>{imageCard(memoryAlbumList[3 * i])}</td>
                                    <td>{memoryAlbumList[3 * i + 1] ? imageCard(memoryAlbumList[3 * i + 1]) : null}</td>
                                    <td>{memoryAlbumList[3 * i + 2] ? imageCard(memoryAlbumList[3 * i + 2]) : null}</td>
                                </tr>
                            )
                        }
                    </tbody>
                </table>
            </div>
        </div>
    )
}

export default MemoryAlbumList;