document.addEventListener('DOMContentLoaded', function() {
    const circle = document.querySelector('.progress-ring__circle');
    const radius = circle.r.baseVal.value;
    const circumference = radius * 2 * Math.PI;
    const number = document.getElementById('progressNumber');

    circle.style.strokeDasharray = `${circumference} ${circumference}`;
    circle.style.strokeDashoffset = `${circumference}`;

    function setProgress(percent) {
        const offset = circumference - (percent / 100) * circumference;
        circle.style.strokeDashoffset = offset;
        number.textContent = `${percent}%`;
    }

    let progress = 0;
    const interval = setInterval(() => {
        progress += 1;
        setProgress(progress);
        if (progress === 100) clearInterval(interval);
    }, 50); // Adjust time interval as needed
});
