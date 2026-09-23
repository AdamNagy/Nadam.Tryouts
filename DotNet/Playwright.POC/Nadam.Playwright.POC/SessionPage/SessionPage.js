function createSessionPage(bridge) {
    const list = document.getElementById('visited-urls');
    const visitsStatus = document.getElementById('visits-status');
    const notifyButton = document.getElementById('notify');
    const status = document.getElementById('status');
    let lastUrls = '';
    let refreshTimer;
    let stopped = true;

    function renderUrls(urls) {
        const serialized = JSON.stringify(urls);
        if (serialized !== lastUrls) {
            const items = urls.map(url => {
                const item = document.createElement('li');
                item.textContent = url;
                return item;
            });
            list.replaceChildren(...items);
            lastUrls = serialized;
        }
        visitsStatus.textContent = urls.length === 0 ? 'No visited URLs yet.' : `${urls.length} visits`;
    }

    async function refresh() {
        try {
            const urls = await bridge.getVisitedUrls();
            if (!stopped) renderUrls(urls);
        } catch (error) {
            if (!stopped) visitsStatus.textContent = 'Could not load visited URLs: ' + error.message;
        } finally {
            if (!stopped) refreshTimer = setTimeout(refresh, 1000);
        }
    }

    async function notify() {
        try {
            status.textContent = await bridge.notify('Hello from the session page!');
        } catch (error) {
            status.textContent = 'Error: ' + error.message;
        }
    }

    function stop() {
        stopped = true;
        clearTimeout(refreshTimer);
        notifyButton.removeEventListener('click', notify);
        window.removeEventListener('pagehide', stop);
    }

    function start() {
        if (!stopped) return;
        stopped = false;
        notifyButton.addEventListener('click', notify);
        window.addEventListener('pagehide', stop);
        refresh();
    }

    return { start, stop };
}
