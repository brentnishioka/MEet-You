import React, {useState} from 'react'

export default function Login() {
    // const [userID, setUserID] = useState(null);
    const[userEmail, setEmail] = useState("")
    const [userToken] = useState("token");

    const DeleteAccountHandler = (e) => {
      e.preventDefault();

      const requestOptions = {
          method: 'DELETE',
          headers: { 
            'Accept': 'application/json',
            'Content-Type': 'application/json',
          },
          body: JSON.stringify({ 
            _email: userEmail,
            _token: userToken
          })
      };
      fetch('https://cors-anywhere.herokuapp.com/http://localhost:5001/api/delaccount', requestOptions)
          .then(response => response.json())
          .then(data => {
            if (data === true) {
              console.log('The account was deleted successfully.')
            }
            else {
              console.log('The account was not deleted successfully.')
            }
          });
  }

    return(
      <div className="accountdeletion-wrapper">
        <h1>Would you like to delete your account?</h1>
        <form>
          <label>
            <p>User Email</p>
            <input type="text" placeholder ="Email" onChange={e => setEmail(e.target.value)}/>
          </label>
          <div>
            <button type="delete" onClick = {(e) => DeleteAccountHandler(e)}>Delete Account</button>
          </div>
        </form>
      </div>
    )
  }