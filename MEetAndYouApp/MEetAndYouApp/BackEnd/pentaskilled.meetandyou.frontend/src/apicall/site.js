function getSuggestion() {
    fetch("https://localhost:9000/GetEvent?category=food%20and%20drink&location=Long%20Beach&date=May%204")
      .then(response => response.json())
      .catch(error => console.error('Unable to get items.', error));
  }