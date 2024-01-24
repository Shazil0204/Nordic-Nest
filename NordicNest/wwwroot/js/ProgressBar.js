function updateProgress(progressBarId, progressBarLingeringId, progressTextId, target) {
	let progress = 0;
	const progressBar = document.getElementById(progressBarId);
	const progressBarLingering = document.getElementById(progressBarLingeringId);
	const progressText = document.getElementById(progressTextId);

	function incrementProgress() {
		progress += 1;
		progressText.innerText = progress + '%';
		const offset = 339.292 * (1 - progress / 100);
		progressBar.style.strokeDashoffset = offset;
		progressBarLingering.style.strokeDashoffset = offset;

		if (progress < target) {
			setTimeout(incrementProgress, 30);
		}
	}

	incrementProgress();
}