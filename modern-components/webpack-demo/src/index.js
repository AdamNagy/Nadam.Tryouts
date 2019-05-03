//import _ from 'lodash';
import './style.css';
import bg from './bg-img.jpeg';
import Data from './data.xml';
import printMe from './print.js';
import statusimg from './webpack-demo.png';
import { cube } from './math.js';

if (process.env.NODE_ENV !== 'production') {
    console.log('Looks like we are in development mode!');
}

/***********************************************************************/
// Code splitting

/* Sync */
// function getComponent() {

//     //TsClass.printMessage();

//     return import ( /* webpackChunkName: "lodash" */ 'lodash').then(_ => {
//         var element = document.createElement('div');

//         element.innerHTML = _.join(['Hello', 'webpack'], ' ');

//         return element;

//     }).catch(error => 'An error occurred while loading the component');
// }

// getComponent().then(component => {
//     document.body.appendChild(component);
// })

/* Async */
// async function getComponent() {

//     var element = document.createElement('div');
//     const _ = await
//     import ( /* webpackChunkName: "lodash" */ 'lodash');

//     element.innerHTML = _.join(['Hello', 'webpack'], ' ');

//     return element;
// }
/***********************************************************************/

// Original (without code splitting)
function component() {
    var element = document.createElement('div');
    var btn = document.createElement('button');
    var mathElement = document.createElement('pre');

    mathElement.innerHTML = [
        'Hello webpack!',
        '5 cubed is equal to ' + cube(5)
    ].join('\n\n');

    element.innerHTML = 'Hello webpack';
    element.classList.add('hello');

    var statusImg = new Image();
    statusImg.src = statusimg;

    console.log(Data);

    btn.innerHTML = 'Click me and check the console!';
    btn.onclick = printMe;

    element.appendChild(btn);
    element.appendChild(statusImg);
    element.appendChild(mathElement);
    return element;
}

document.body.appendChild(component());