function getSuggestion() {
  fetch("https://meetandyou.me:8001/GetEvent?category=food%20and%20drink&location=Long%20Beach&date=May%204")
    .then(response => response.json())
    .catch(error => console.error('Unable to get items.', error));
}

const saveEvent = (request, itinID, userID) => {
  fetch('https://meetandyou.me:8001/SaveEvent?itinID=' + itinID + '&userID=' + userID, request)
    .then(response => response.json())
    .then(data => this.setState({ postId: data.id }));
}