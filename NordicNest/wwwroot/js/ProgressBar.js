
var progressBars = getProgressBarsFromCSharp();
var firstProgressBar = progressBars[0];
var secondProgressBar = progressBars[1];

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

updateProgress('progress-bar-1', 'progress-bar-lingering-1', 'progress-text-1', firstProgressBar);
updateProgress('progress-bar-2', 'progress-bar-lingering-2', 'progress-text-2', secondProgressBar);