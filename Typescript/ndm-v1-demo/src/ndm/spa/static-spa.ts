import { Route } from "./route";

class StaticSpa {

	private diContainer: Route[] = [];
	private queryString: string;
	get activeQueryString() {
		return this.queryString;
	}

	private navigations = 0;
	private currentLink: string;

	public DetectRoute() {
		const appRoute = document.location.hash;
		if ( appRoute === "") {
			history.replaceState({ navigation: ++this.navigations  }, "home page", "#home");
			this.ActivateRoute("home", true);
		} else {
			const page = appRoute.slice(1, appRoute.length);
			this.ActivateRoute(page);
		}
	}

	public RegisterRoute(route: string, page: any) {

		this.diContainer.push({
			PageElement: undefined,
			PageType: page,
			Route: route,
		});
	}

	public ActivateRoute(link: string, initialNavigation = false) {

		const splitted = link.split("?");
		if (splitted.length > 1) {
			this.queryString = splitted[1];
		} else {
			this.queryString = "";
		}

		for (const route of this.diContainer) {
			if (route.PageElement)
				route.PageElement.Hide();

			if ( route.Route === link ) {

				this.currentLink = link;

				if (!initialNavigation)
					history.pushState({ navigation: ++this.navigations }, link, "#" + link);

				if ( route.PageElement === undefined ) {
					route.PageElement = Reflect.construct(route.PageType, []);
					document.body.append(route.PageElement);
				}

				route.PageElement.Show();
			}
		}
	}

	private UpdateState() {
		history.replaceState({ navigation: this.navigations  }, this.currentLink, `#${this.currentLink}`);
	}
}

export const StaticSpaManager = new StaticSpa();
