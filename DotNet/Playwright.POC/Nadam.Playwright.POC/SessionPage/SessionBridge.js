function createSessionBridge() {
    return {
        getVisitedUrls: () => window.pocGetVisitedUrls(),
        getImages: () => window.pocGetImages(),
        getImageData: id => window.pocGetImageData(id),
        notify: message => window.notifyScraper(message)
    };
}
