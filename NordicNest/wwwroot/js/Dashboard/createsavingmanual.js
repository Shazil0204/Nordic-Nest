function showExplanationforsaving() {
  Swal.fire({
    title: "User Options",
    html: `<ul class="text-pretty text-black font-mono">
                        <li class="mt-2 border border-black rounded-lg uppercase text-yellow-400">this goes only for the div with yellow border</li>
                        <li class="mt-2 border border-black rounded-lg text-green-700 font-bold">If you have all the data, you can fill in all the fields.</li>
                        <li class="mt-2 border border-black rounded-lg">If you know the <strong class="uppercase ">Total Amount</strong> and the <strong class="uppercase ">deadline</strong>, as well as the <strong class="uppercase ">frequency</strong> of insertion, <span class="uppercase ">the system can calculate how much you need to insert each time.</span></li>
                        <li class="mt-2 border border-black rounded-lg">If you know how often you will insert <strong class="uppercase ">(Frequency)</strong>, the <strong class="uppercase ">value to insert each time</strong>, and the <strong class="uppercase ">total amount</strong>, <span class="uppercase ">the system can determine the deadline</span>.</li>
                        <li class="mt-2 border border-black rounded-lg">If you know how often you will insert <strong class="uppercase ">(Frequency)</strong>, the <strong class="uppercase ">value to insert each time</strong>, and the <strong class="uppercase ">Deadline</strong>, <span class="uppercase ">the system can determine the total amount</span>.</li>
                        <li class="mt-2 border border-black rounded-lg uppercase ">If you provide all the data but something is incorrect, i will suggest let system calculate the <strong class="uppercase">Value to insert each time</strong>.</li>
                    </ul>`,
    showCloseButton: true,
    confirmButtonText: "Got it!",
    customClass: {
      content: "text-white",
    },
  });
  window.location.hash = "";
}
