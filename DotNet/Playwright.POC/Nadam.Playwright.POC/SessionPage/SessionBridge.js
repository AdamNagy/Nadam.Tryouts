function createSessionBridge() {
    return {
        getVisitedUrls: () => window.pocGetVisitedUrls(),
        notify: message => window.notifyScraper(message)
    };
}
