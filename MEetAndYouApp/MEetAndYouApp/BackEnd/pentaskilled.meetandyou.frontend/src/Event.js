// import React, { Component } from 'react';


// export default class App extends Component {
//     static displayName = App.name;

//     constructor(props) {
//         super(props);
//         this.state = { loginresponse: [], loading: true };
//     }

//     componentDidMount() {
//         this.populateWeatherData();
//     }

//     static renderForecastsTable(forecasts) {
//         return (
//             <div>
//                 Login<br /><br />
//                 <div>
//                     Username<br />
//                     <input type="text" {...username} autoComplete="new-password" />
//                 </div>
//                 <div style={{ marginTop: 10 }}>
//                     Password<br />
//                     <input type="password" {...password} autoComplete="new-password" />
//                 </div>
//                 {error && <><small style={{ color: 'red' }}>{error}</small><br /></>}<br />
//                 <input type="button" value={loading ? 'Loading...' : 'Login'} onClick={handleLogin} disabled={loading} /><br />
//             </div>
//         );
//     }

//     render() {
//         let contents = this.state.loading
//             ? <p><em>Loading... Please refresh once the ASP.NET backend has started. See <a href="https://aka.ms/jspsintegrationreact">https://aka.ms/jspsintegrationreact</a> for more details.</em></p>
//             : App.renderForecastsTable(this.state.forecasts);

//         return (
//             <div>
//                 <h1 id="tabelLabel" >Weather forecast</h1>
//                 <p>This component demonstrates fetching data from the server.</p>
//                 {contents}
//             </div>
//         );
//     }

//     async populateWeatherData() {
//         const response = await fetch('Login');
//         const data = await response.json();
//         this.setState({ loginresponse: data, loading: false });
//     }
// }
