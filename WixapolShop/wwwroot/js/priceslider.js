const rangeInput = document.querySelectorAll(".range-input input"),
    priceInput = document.querySelectorAll(".price-input input"),
    progress = document.querySelector(".slider .progress-price");

let priceGap = 50;

let minVal = parseInt(rangeInput[0].value),
    maxVal = parseInt(rangeInput[1].value);
progress.style.left = (minVal / rangeInput[0].max) * 100 + "%";
progress.style.right = 100 - (maxVal / rangeInput[1].max) * 100 + "%";


priceInput.forEach(input => {
    input.addEventListener("input", val => {

        //Getting values and parsing them to int
        let minVal = parseInt(priceInput[0].value),
            maxVal = parseInt(priceInput[1].value);

        if (maxVal - minVal >= priceGap) {
            if (val.target.className === "input-min") {
                rangeInput[0].value = minVal;
                progress.style.left = (minVal / rangeInput[0].max) * 100 + "%";
            }
            else {

                rangeInput[1].value = Math.min(rangeInput[1].max, maxVal);
                progress.style.right = 100 - (Math.min(rangeInput[1].max, maxVal) / rangeInput[1].max) * 100 + "%";
            }
        }

    });
});
rangeInput.forEach(input => {
    input.addEventListener("input", val => {

        //Getting values and parsing them to int
        let minVal = parseInt(rangeInput[0].value),
            maxVal = parseInt(rangeInput[1].value);

        if (maxVal - minVal < priceGap) {
            if (val.target.className === "range-min") {
                rangeInput[0].value = maxVal - priceGap;
            }
            else {

                rangeInput[1].value = minVal + priceGap;
            }
        }
        else {
            priceInput[0].value = minVal;
            priceInput[1].value = maxVal;
            progress.style.left = (minVal / rangeInput[0].max) * 100 + "%";
            progress.style.right = 100 - (maxVal / rangeInput[1].max) * 100 + "%";
        }

    });
});