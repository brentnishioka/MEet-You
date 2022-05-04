import React, { useState, useEffect } from 'react';

const defaultImageSrc = '../img/default.jpg'

const initFieldValues = {
    imageId: 0,
    imageName : '',
    imageExtension: '',
    imagePath: defaultImageSrc,
    itineraryID: [],
    imageFile: null
}

function MemoryAlbum(props) {
    const { addOrEdit, recordForEdit } = props

    const [values, setValues] = useState(initFieldValues)
    const [errors, setErrors] = useState({})


    useEffect(() => {
        if (recordForEdit != null)
            setValues(recordForEdit);
    }, [recordForEdit])

    const handleInputChange = e => {
        const { name, value } = e.target;
        setValues({
            ...values,
            [name]: value
        })
    }

    const showPreview = e => {
        if (e.target.files && e.target.files[0]) {
            let imageFile = e.target.files[0];
            const reader = new FileReader();
            reader.onload = x => {
                setValues({
                    ...values,
                    imageFile,
                    imagePath: x.target.result
                })
            }
            reader.readAsDataURL(imageFile)
        }
        else {
            setValues({
                ...values,
                imageFile: null,
                imagePath: defaultImageSrc
            })
        }
    }

    const validate = () => {
        let temp = {}
        temp.imageName = values.imageName == "" ? false : true;
        temp.imagePath = values.imagePath == defaultImageSrc ? false : true;
        setErrors(temp)
        return Object.values(temp).every(x => x == true)
    }

    const resetForm = () => {
        setValues(initFieldValues)
        document.getElementById('image-uploader').value = null;
        setErrors({})
    }

    const handleFormSubmit = e => {
        e.preventDefault()
        if (validate()) {
            const formData = new FormData()
            formData.append('imageName', values.imageName)
            formData.append('imageExtension', values.imageName.split('.').pop())
            formData.append('imagePath', values.imagePath)
            formData.append('imageFile', values.imageFile)
            addOrEdit(formData, resetForm)
        }
    }

    const applyErrorClass = field => ((field in errors && errors[field] == false) ? ' invalid-field' : '')

    return (
        <>
            <div className="container text-center">
                <p className="lead">Image Uploader</p>
            </div>
            <form autoComplete="off" noValidate onSubmit={handleFormSubmit}>
                <div className="card">
                    <img src={values.imagePath} alt="preview" className="card-img-top" />
            
                    <div className="card-body">
                        <div className="form-group">
                            <input type="file" accept="image/*" className={"form-control-file" + applyErrorClass('imagePath')}
                                onChange={showPreview} id="image-uploader" />
                        </div>
                        {/* <div className="form-group">
                            <input className={"form-control" + applyErrorClass('imageName')} placeholder="Image Name" name="imageName"
                                value={values.imageName}
                                onChange={handleInputChange} />
                        </div>
                        <div className="form-group">
                            <input className="form-control" placeholder="Image Extension" name="imageExtension"
                                value={values.imageExtension}
                                onChange={handleInputChange} />
                        </div> */}
                        <div className="form-group text-center">
                            <button type="submit" className="btn btn-light" >Submit
                            </button>
                        </div>
                    </div>
                </div>
            </form>
        </>

    )

}

export default MemoryAlbum;