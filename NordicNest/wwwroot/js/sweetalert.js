// Function to handle cancellation and show SweetAlert
function cancelSubscription(planName) {
	// Show the initial SweetAlert with "Yes" and "No" buttons
	swal({
		title: 'Are you sure?',
		text: `Cancel "${planName}" subscription? Click "No" to go back or "Yes" to continue`,
		icon: 'warning',
		buttons: {
			cancel: {
				text: 'No',
				value: null,
				visible: true,
				className: '',
				closeModal: true,
			},
			confirm: {
				text: 'Yes',
				value: true,
				visible: true,
				className: '',
				closeModal: true
			}
		}
	}).then((result) => {
		// Check the result
		if (result) {
			// User clicked "Yes", show the success SweetAlert
			showSuccessSweetAlert(planName);
		} else {
			// User clicked "No", you can handle this case as needed
			swal('Cancelled', 'Operation cancelled', 'info');
		}
	});
}

// Function to show the success SweetAlert
function showSuccessSweetAlert(planName) {
	swal({
		title: 'Subscription Cancelled',
		text: `Your ${planName} subscription has been cancelled`,
		icon: 'success',
		button: 'Continue'
	});
}