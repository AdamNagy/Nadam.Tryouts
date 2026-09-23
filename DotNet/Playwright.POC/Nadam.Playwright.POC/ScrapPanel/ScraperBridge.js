function createScraperBridge() {
    return {
        openSession: () => window.pocShowSession()
    };
}
