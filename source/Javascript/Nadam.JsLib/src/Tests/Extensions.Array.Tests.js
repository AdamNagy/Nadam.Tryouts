"use stricts";

window.Nadam = window.Nadam || {};
Nadam.JsLib = Nadam.JsLib || {};

Nadam.JsLib.Tests = new function() {

    var AddResult = function() {

    }

    this.FirstOrDefaultTests = new function() {

        this.Test1 = function() {

            var arr = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];
            var number5 = arr.FirstOrDefault((item) => item == 6);
            var testPass = false;

            if (number5 === 5) {
                testPass = true;
            }

            console.log("Nadam.Extensions.Array.Tests.FirstOrDefaultTests Test1: " + testPass);
        }
    }
}