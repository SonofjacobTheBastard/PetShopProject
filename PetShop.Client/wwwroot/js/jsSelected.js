

function getSelected(selectedOption) {
    sourceURL = somename.js
    let categorySelection = document.getElementById("categorySelection");
    Array.prototype.forEach.call(categorySelection.options, function (option, index) {
        if (option.value = selectedOption){
        option.setAttribute("selected");
    }
    index++;
})
        }