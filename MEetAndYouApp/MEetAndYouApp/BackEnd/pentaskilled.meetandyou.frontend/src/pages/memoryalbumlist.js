import React, { useState, useEffect } from 'react';
import Image from './memoryalbum'
import axios from "axios";
import MemoryAlbum from './memoryalbum';

function MemoryAlbumList() {
    const [memoryAlbumList, setmemoryAlbumList] = useState([])
    const [recordForEdit, setRecordForEdit] = useState(null)

    var allowCrossDomain = function(req, res, next) {
        res.header('Access-Control-Allow-Origin', "*");
        res.header('Access-Control-Allow-Methods', 'GET,PUT,POST,DELETE');
        res.header('Access-Control-Allow-Headers', 'Content-Type');
        next();
    };
    
    app.use(allowCrossDomain);
    useEffect(() => {
        refreshmemoryAlbumList();
    }, [])

    const imagesAPI = (url = 'https://localhost:9000/MemoryAlbum') => {
        return {
            fetchAll: () => axios.get(url),
            create: newRecord => axios.post(url, newRecord),
            update: (id, updatedRecord) => axios.put(url + id, updatedRecord),
            delete: id => axios.delete(url + id)
        }
    }

    function refreshmemoryAlbumList() {
        imagesAPI().fetchAll()
            .then(res => {
                setmemoryAlbumList(res.data)
            })
            .catch(err => console.log(err))
    }

    const addOrEdit = (formData, onSuccess) => {
        if (formData.get('imageID') == "0")
            imagesAPI().create(formData)
                .then(res => {
                    onSuccess();
                    refreshmemoryAlbumList();
                })
                .catch(err => console.log(err))
        else
            imagesAPI().update(formData.get('imageID'), formData)
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
            imagesAPI().delete(id)
                .then(res => refreshmemoryAlbumList())
                .catch(err => console.log(err))
    }

    const imageCard = data => (
        <div className="card" onClick={() => { showRecordDetails(data) }}>
            <img src={data.imageSrc} className="card-img-top rounded-circle" />
            <div className="card-body">
                <h5>{data.ImageName}</h5>
                <span>{data.occupation}</span> <br />
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