declare class StaticSpa {
    private diContainer;
    private queryString;
    readonly activeQueryString: string;
    private navigations;
    private currentLink;
    DetectRoute(): void;
    RegisterRoute(route: string, page: any): void;
    ActivateRoute(link: string, initialNavigation?: boolean): void;
    private UpdateState;
}
export declare const StaticSpaManager: StaticSpa;
export {};
