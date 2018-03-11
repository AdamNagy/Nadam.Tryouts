import { Component, OnInit, Inject} from "@angular/core";
import { JQ_TOKEN, TOASTR_TOKEN } from "./../wrappers/index";
import { MathOperations } from "./../../nang-components/nang.index";

@Component({
  templateUrl: "./collapsable-demo.component.html",
  // styleUrls: ["./collapsable-demo.component.less"]
})
export class CollapsableDemoComponent {

	articles: ITabModel[];

	constructor(
		@Inject(TOASTR_TOKEN) private toastr: any,
		// @Inject(JQ_TOKEN) private $: any,
		private math: MathOperations) { }

	ngOnInit(): void {
		// $("#app-body div:first-child").css("color", "red");
		this.articles = tabdData;

		console.log("square of 5 is: " + this.math.square(5));
	}

	SayToast(): void {
		this.toastr.success("I want to tell a story!");
	}

	SayWarning(): void {
		this.toastr.warning("I warn you not to do that!");
	}

	SayError(): void {
		this.toastr.error("I told you not to click that button, now error happend");
	}
}

interface ITabModel {
	Id: number;
	Title: String;
	Text: String;
	IsActive: boolean;
}

let tabdData: ITabModel[] = [
	{
		Id: 1,
		Title: "Title 1",
		Text: "In in lorem vitae est cursus auctor a eget neque. Aenean commodo, diam id auctor vestibulum, dolor nisi" +
			" ultrices dolor, vel efficitur nulla magna quis nisl. Cras porttitor urna lectus, imperdiet pulvinar lacus"+
			" condimentum interdum. Suspendisse scelerisque ligula ex, id aliquam felis ultrices vel. Morbi vulputate" +
			" lacus massa, in semper augue congue non. Suspendisse consequat luctus nisi, pretium mattis erat aliquet" +
			" dignissim. Pellentesque imperdiet pulvinar libero, at ultricies sem tincidunt id.",
			IsActive: false
	},
	{
		Id: 2,
		Title: "Title 2",
		Text: "In hac habitasse platea dictumst. Curabitur id fringilla urna. Integer ac pellentesque lacus, eget"+
			" porttitor purus. Etiam accumsan metus ut lacus luctus eleifend. Donec pellentesque interdum nisi a finibus."+
			" Nullam ut viverra dolor, non rutrum metus. Nulla facilisi. In dictum metus felis, quis suscipit felis "+
			"eleifend sed. Donec rhoncus purus a dui imperdiet, in molestie massa finibus. Cras ultrices turpis et efficitur feugiat.",
		IsActive: false
	},
	{
		Id: 3,
		Title: "Title 3",
		Text: "Vestibulum tincidunt euismod finibus. Duis dictum metus at leo molestie, et scelerisque libero luctus."+
			" Morbi arcu nibh, faucibus eget semper eu, egestas eget urna. Quisque et nunc non est tristique mollis id vel"+
			" neque. Sed ligula nunc, ultricies sit amet risus ut, feugiat convallis nisl. Etiam at mauris magna. Etiam"+
			" quis rutrum ipsum, elementum aliquam orci.",
		IsActive: false
	},
	{
		Id: 4,
		Title: "Title 4",
		Text: "Phasellus sit amet aliquam nisi. Nulla facilisi. Donec a lectus ante. Maecenas pretium aliquam dolor"+
			" eget tempor. Donec sodales odio ante, sit amet mollis lectus eleifend eu. In ex enim, placerat vel aliquet"+
			" eget, fermentum non odio. Donec pulvinar bibendum congue. Curabitur dolor nunc, scelerisque ac lorem sit "+
			"amet, viverra consequat mi. Sed porta dui ante, id facilisis tellus lobortis a. Praesent vestibulum ligula "+
			"mi. Duis fringilla sapien non molestie pellentesque. Cras eleifend quam id viverra laoreet. Nunc luctus "+
			"ipsum ac orci fringilla posuere. Vivamus aliquet luctus tortor quis rutrum.",
		IsActive: false
	},
];