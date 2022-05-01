import React, {useState} from 'react'

export default function Login() {
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");

    const handleSubmit = (e) => {
        e.preventDefault();
        const url = 'http://localhost:3000/?'
        const requestOptions = {
            method: 'POST',
            headers: {'Content-Type': 'application/json'},
            body: JSON.stringify({email, password})
        };
        fetch('http://localhost:3000/api/logout', requestOptions)
          .then(response => response.json())
          .then(data => this.setState({ postId: data.id }));
        console.log(JSON)
    };

    return(
      <div className="login-wrapper">
        <h1>Please Log In</h1>
        <form>
          <div>
            <button type="Logout" onClick = {handleSubmit}>Submit</button>
          </div>
        </form>
      </div>
    )
  }