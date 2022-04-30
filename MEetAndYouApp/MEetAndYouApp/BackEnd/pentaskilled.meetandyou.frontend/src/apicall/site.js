function getSuggestion() {
    fetch("https://localhost:9000/GetEvent")
      .then(response => response.json())
      .then(data => _displayItems(data))
      .catch(error => console.error('Unable to get items.', error));
  }