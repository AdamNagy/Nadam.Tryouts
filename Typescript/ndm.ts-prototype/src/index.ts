import { configureStore } from "@reduxjs/toolkit"
import { createSlice } from '@reduxjs/toolkit';
import { debounce } from "lodash";
import { createStore } from "redux";

// setup the ridákker
class AccordionState {
	currentlyOpen: number = 0;
	acordionItems: AccordionElementConfig[] = [];
}

var initialAccordionState = {
	currentlyOpen: 0,
	acordionItems: []
};

const initialState = [
	{ id: '1', title: 'First Post!', content: 'Hello!' },
	{ id: '2', title: 'Second Post', content: 'More text' }
  ]

var accordionReducer = (state = initialAccordionState, action: any) => {
	switch(action.type) {
		case "OPEN":
			return {
				...state,
				currentlyOpen: action.data
			};

		case "ADD":
			return {
				...state,
				acordionItems: state.acordionItems.concat(action.data)
			};

		case "REMOVE":
			return {
				...state,
				acordionItems: state.acordionItems.slice(1, 1)
			};
		
		default: return state;
	}
};

const accordionSlice = createSlice({
	name: 'posts',
	initialState,
	reducers: { }
})

const store = configureStore({
	reducer: {
		accordion: accordionSlice.reducer
	}
});

store.subscribe(() => {
	var state = store.getState();
	console.log(state);
})

// setup basic dom

var input = document.createElement("input");
input.setAttribute("type", "text");
input.setAttribute("id", "input");

var button = document.createElement("button");
button.addEventListener("click", () => {
	var input = (document.getElementById("input") as HTMLInputElement).value;
	store.dispatch({ type: "OPEN_ACCORDION", data: input.toString() });
});
button.innerText = "Go!";

document.body.append(input);
document.body.append(button);

var paraghraph1 = document.createElement("p");
paraghraph1.innerText = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.";
var accordionItem1: AccordionElementConfig = {
	title: "heading 1",
	contentElement: paraghraph1
};

var paraghraph2 = document.createElement("p");
paraghraph2.innerText = "Contrary to popular belief, Lorem Ipsum is not simply random text. It has roots in a piece of classical Latin literature from 45 BC, making it over 2000 years old. Richard McClintock, a Latin professor at Hampden-Sydney College in Virginia, looked up one of the more obscure Latin words, consectetur, from a Lorem Ipsum passage, and going through the cites of the word in classical literature, discovered the undoubtable source.";
var accordionItem2: AccordionElementConfig = {
	title: "heading 2",
	contentElement: paraghraph2
};

var paraghraph3 = document.createElement("p");
paraghraph3.innerText = "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English. Many desktop publishing packages and web page editors now use Lorem Ipsum as their default model text, and a search for 'lorem ipsum' will uncover many web sites still in their infancy. Various versions have evolved over the years, sometimes by accident, sometimes on purpose (injected humour and the like).";
var accordionItem3: AccordionElementConfig = {
	title: "heading 3",
	contentElement: paraghraph3
};

var accordionItems: AccordionElementConfig[] = [];
accordionItems.push(accordionItem1);
accordionItems.push(accordionItem2);
accordionItems.push(accordionItem3);

var acc = new AccordionElement(accordionItems);
document.body.append(acc);