// this is the top level js file because this one is invluced in the HTML file as script tag

// import { cube, foo, graph } from './my-module.js';

graph.options = {
    color:'blue',
    thickness:'3px'
};
 
graph.draw();
console.log(cube(3)); // 27
console.log(foo);    // 4.555806215962888