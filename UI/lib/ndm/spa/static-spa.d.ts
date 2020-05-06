declare class StaticSpa {
    appRoot: string;
    private diContainer;
    private queryString;
    readonly ActiveQueryString: string;
    private pageContainer;
    private navigations;
    constructor();
    DetectRoute(): void;
    RegisterRoute(route: string, componentDefinition: any): void;
    ActivateRoute(link: string, initialNavigation?: boolean): void;
    private InitPage;
}
export declare const StaticSpaManager: StaticSpa;
export {};
