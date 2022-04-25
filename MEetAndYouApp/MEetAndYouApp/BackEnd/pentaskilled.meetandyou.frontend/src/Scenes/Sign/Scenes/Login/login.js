import React, { useState } from 'react';
import axios from 'axios';
import { setUserSession } from '../../../../Util/Common';

function Login(props) {
    const [loading, setLoading] = useState(false);
    const username = useFormInput('');
    const password = useFormInput('');
    const [error, setError] = useState(null);

    // handle button click of login form
    /*const handleLogin = () => {
        setError(null);
        setLoading(true);
        axios.post('http://localhost:9000/Login/SignIn', { username: username.value, password: password.value }).then(response => {
            setLoading(false);
            setUserSession(response.data.token, response.data.userID, response.data.roles);
        }).catch(error => {
            setLoading(false);
            if (error.response.status === 401) setError(error.response.data.message);
            else setError("Something went wrong. Please try again later.");
        });
    }*/

    const handleLogin = async () => {
        try {
            const response = await axios.post('http://localhost:9000/Login/SignIn', { username: username.value, password: password.value })
            setUserSession(response.data.token, response.data.userID, response.data.roles);
            console.log(response.data);
        } catch (e) {
            console.log(e);
        }
    }

    return (
        <div>
            Login<br /><br />
            <div>
                Username<br />
                <input type="text" {...username} autoComplete="new-password" />
            </div>
            <div style={{ marginTop: 10 }}>
                Password<br />
                <input type="password" {...password} autoComplete="new-password" />
            </div>
            {error && <><small style={{ color: 'red' }}>{error}</small><br /></>}<br />
            <input type="button" value={loading ? 'Loading...' : 'Login'} onClick={handleLogin} disabled={loading} /><br />
        </div>
    );
}

const useFormInput = initialValue => {
    const [value, setValue] = useState(initialValue);

    const handleChange = e => {
        setValue(e.target.value);
    }
    return {
        value,
        onChange: handleChange
    }
}

export default Login;