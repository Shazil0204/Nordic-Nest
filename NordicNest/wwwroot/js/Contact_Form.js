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

