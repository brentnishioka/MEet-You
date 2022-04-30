import React, { useState, Component, useEffect } from 'react';
import { useLocation } from 'react-router-dom';

const useAnswer = () => {
    const city = sessionStorage.getItem("city");
    const date = sessionStorage.getItem("date");
    const state = sessionStorage.getItem("state");
    const location = useLocation();

    const [answer, setAnwser] = useState();
  
    const getAnswer = async () => {
      const res = await fetch("https://localhost:9000/GetEvent?category=" + location.state.categories[0].label + "&location=" + city + " " + state + "&date= " + date);
      const answer = await res.json();
      setAnwser(answer);
    };
  
    useEffect(() => {
      getAnswer();
    }, []);
    return answer;
  };

  //Take in a JSON object and parse it
const converToObjArray = (jObject) => {
    const resObject = JSON.parse(jObject);
    
    return resObject;
}


export default function Events() {
    const answer = useAnswer();
    const answerObject = converToObjArray(answer);

    // const city = sessionStorage.getItem("city");
    // const date = sessionStorage.getItem("date");
    // const state = sessionStorage.getItem("state");
    // const location = useLocation();
    // const category = location.state.categories[0].label;

    
    // const [Suggestions, setSuggestions] = useState();
    // const [randomEvent, setrandomEvent] = useState();
    // const [rData, setrData] = useState([]);

    // console.log(location.state.categories[0].label);


    // const fetchSuggestions = async () => {
    //     await fetch("https://localhost:9000/GetEvent?category=" + location.state.categories[0].label + "&location=" + city + " " + state + "&date= " + date)
    //         .then(response => console.log(response.json()))
    //         .then(data => setSuggestions(data))
    //         .then(body => console.log(body))
    // }

    // const getAnswer = async () => {
    //     const res = await fetch("https://localhost:9000/GetEvent?category=" + location.state.categories[0].label + "&location=" + city + " " + state + "&date= " + date);
    //     const answer = await res.json();
    //     await setSuggestions(answer);
    //     await setrData(Suggestions["data"]);
    //     await console.log(answer);
    //   };

    //   const geRandomAnswer = async () => {
    //     const res = await fetch("https://localhost:9000/GetRandomEvent");
    //     const answer = await res.json();
    //     setrandomEvent(answer);
    //     console.log(answer);
    //   };

    //   useEffect(() => {
    //     getAnswer();
    //   }, [rData]);

      //const suggestionList = JSON.stringify(Suggestions);
      //let eResponse = JSON.parse(Suggestions);
      //const rMessage = Suggestions["message"];
      //const rData = Suggestions["data"]; //array 
      //const firstEvent = rData[0];
      //const txt = '{"name":"John", "age":30, "city":"New York"}'
      //const obj = JSON.parse(txt);

    // async function getSuggestion(){
    //     const result = await fetch("https://localhost:9000/GetEvent?category=" + location.state.categories[0].label + "&location=" + city + " " + state + "&date= " + date)
    //     .then(response => setSuggestions(response.json()))
    //     .then(body => console.log(body))

    //     return result;
    // }

    // async function getSuggestion(){
    //     const response = await fetch("https://localhost:9000/GetEvent?category=" + location.state.categories[0].label + "&location=" + city + " " + state + "&date= " + date);
    //     const responseJ = response.json();
    //     console.log(responseJ)
    //     setSuggestions(responseJ)
    // }
    
    //setSuggestions(getSuggestion())
    // console.log("The response Object", Suggestions);
    // console.log("The first event message: ", rMessage);
    // console.log("The first object: ", firstEvent["eventName"]);
    // console.log("Example obj: " + obj.name + " age: " + obj.age)
    // console.log("Event Object", Suggestions.data[0]);


    // useEffect(() => {
    //     fetchSuggestions();
    // }, [])

    // console.log("TEST LONG: ", eventSuggestions);
    // console.log("Event List: ", eventSuggestions.data);
    //const eventObject = eventSuggestions.data[0];

    //const rData = answer["data"];
    //const rEvent = rData[0];
    return (
            //<p> Hello world! {Suggestions.message} </p>

            <div>
                {/* {JSON.stringify(Suggestions)} */}
                {JSON.stringify(answer)}
                {answerObject.message}
                {/* {rData.map(item => (
                    <h1>{item.eventName}</h1>
                ))} */}
            </div>
        //      {rObject.data.map((item) => (
        //     <div>
        //         <h1>{item.eventName}</h1>
        //         <p>{item.description}</p>
        //         <p>{item.address}</p>
        //         <p>{item.eventDate}</p>
        //     </div>
        // ))}


    );
}