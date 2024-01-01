var isClientCheckbox = document.getElementById("isClient");
var notClientCheckbox = document.getElementById("notClient");

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
    }
}

// Initial call to show the form based on the default state of checkboxes
toggleForms("isClient");
