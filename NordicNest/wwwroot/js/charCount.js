// Get the textarea and character count label elements
const messageTextarea = document.getElementById('messageTextarea');
const charCountLabel = document.getElementById('charCountLabel');

// Function to update character count
const updateCharCount = () => {
	const currentLength = messageTextarea.value.length;
	charCountLabel.textContent = 'Character Count: ' + currentLength;

	if (currentLength == 255) {
		charCountLabel.textContent = 'You have wrote more then the limit';
	}
};

// Add an event listener to the textarea
// This event listener will call the updateCharCount function every time the user types something
messageTextarea.addEventListener('input', updateCharCount);