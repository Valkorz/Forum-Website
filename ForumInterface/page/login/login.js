const url = "http://1.1.1.1:5000/User/Login";

document.getElementById("Return").addEventListener("click", function() {
  window.location.href = '/';
});

document.getElementById("confirm").addEventListener("click", function() {
    
    //Get email and password inputs from form elements
    let emailInput = document.getElementById("email");
    let passInput = document.getElementById("password");
    let errorMsg = document.getElementById("errorMsg");


    data = {
        "email" : emailInput.value,
        "password" : passInput.value
    }; //Transform form data into dictionary

    fetch(url, {
        method: 'POST', //Notify API of POST request 
        headers: {
          'Content-Type': 'application/json', //Add metadata
        },
        body: JSON.stringify(data), //Transform data into json body
      }) 
      .then(response => { //Check if request was successful
          if(!response.ok){
            return response.json().then(err => {
              throw new Error(`HTTP error! status: ${response.status} at ${url}, message: ${err.message}`);
            });
          }
          return response.json();
      })
      .then(data => {
        console.log('Success:', data);
        alert(`Success: ${data}`);
        errorMsg.style.visibility = "hidden";
        localStorage.setItem("logged", true);
        window.location.href = '/';
      })
      .catch((error) => {
        console.error('Error:', error);
        errorMsg.style.visibility = "visible";
        errorMsg.textContent = error; 
        localStorage.setItem("logged", false);
        alert(`Error: ${error}`);     
      }); 
        
    alert(`Logged in.`);
});