// Javascrip async programming

// https://javascript.info/async-await
// region First 
async function f() {

  return new Promise((resolve, reject) => {
    setTimeout(() => resolve("done!"), 1000)
  });
}

let promise = f();

promise.then(item => alert("then")); // its like a subscribe to the success event of the promise

alert("done"); // this gonna written first
// endregion