//function getCSRFToken() {
//    return document.getElementById('csrfToken').value;
//}

//// Event listeners for checkboxes
//var isClientCheckbox = document.getElementById("isClient");
//var notClientCheckbox = document.getElementById("notClient");

//isClientCheckbox.addEventListener("change", function () {
//    handleCheckboxChange(this, notClientCheckbox);
//});

//notClientCheckbox.addEventListener("change", function () {
//    handleCheckboxChange(this, isClientCheckbox);
//});

//// Handle checkbox change
//function handleCheckboxChange(changedCheckbox, otherCheckbox) {
//    if (changedCheckbox.checked) {
//        otherCheckbox.checked = false;
//        updateClientStatus(changedCheckbox.id === "isClient");
//        toggleForms(changedCheckbox.id);
//    } else {
//        toggleForms();
//    }
//}

//// Function to send AJAX request
//function updateClientStatus(isClient) {
//    var csrfToken = getCSRFToken();

//    fetch('/Contact_Form?handler=UpdateClientStatus', {
//        method: 'POST',
//        headers: {
//            'Content-Type': 'application/json',
//            'RequestVerificationToken': csrfToken
//        },
//        body: JSON.stringify({ isClient: isClient })
//    })
//        // ... rest of your AJAX call ...

//        .then(response => {
//            console.log('Status updated');
//        })
//        .catch(error => {
//            console.error('Error updating status', error);
//        });
//}

//// Toggle forms based on selection
//function toggleForms(selectedId) {
//    var clientFormYes = document.getElementById("clientFormYes");
//    var clientFormNo = document.getElementById("clientFormNo");
//    var ifClientNo = document.getElementById("firstOption");
//    var mainContainer = document.getElementById("mainContainer");

//    clientFormYes.classList.add("hidden");
//    clientFormNo.classList.add("hidden");

//    if (selectedId === "isClient") {
//        clientFormYes.classList.remove("hidden");
//    } else if (selectedId === "notClient") {
//        clientFormNo.classList.remove("hidden");
//        ifClientNo.classList.add("hidden");
//        mainContainer.style.width = "100%";
//    }
//}

//// Initial call to set default form state
//toggleForms("isClient");

var isClientCheckbox = document.getElementById("isClient");
var notClientCheckbox = document.getElementById("notClient");
var firstOption = document.getElementById("firstOption");
var mainContainer = document.getElementById("mainContainer");

isClientCheckbox.addEventListener("change", function () {
    handleCheckboxChange(this, notClientCheckbox);
});

notClientCheckbox.addEventListener("change", function () {
    handleCheckboxChange(this, isClientCheckbox);
});

function handleCheckboxChange(changedCheckbox, otherCheckbox) {
    if (changedCheckbox.checked) {
        otherCheckbox.checked = false;
        toggleForms(changedCheckbox.id);
    } else {
        toggleForms();
    }
}

function toggleForms(selectedId) {
    var clientFormYes = document.getElementById("clientFormYes");
    var clientFormNo = document.getElementById("clientFormNo");

    clientFormYes.classList.add("hidden");
    clientFormNo.classList.add("hidden");

    if (selectedId === "isClient") {
        clientFormYes.classList.remove("hidden");
    } else if (selectedId === "notClient") {
        clientFormNo.classList.remove("hidden");
        firstOption.classList.add("hidden");
        mainContainer.style.width = "100%";
    }
}

// Initial call to show the form based on the default state of checkboxes
toggleForms("isClient");

