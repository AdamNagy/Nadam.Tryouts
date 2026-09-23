function createScrapPanel(markup, css, bridge) {
    let panel;
    let toggle;
    let content;
    let status;
    let host;
    let observer;

    function ensureAttached() {
        // Reuse the same node so open state and event handlers survive page redraws.
        if (document.body && !host.isConnected) document.body.appendChild(host);
    }

    function resume() {
        observer.observe(document, { childList: true, subtree: true });
        ensureAttached();
    }

    function createNode(node) {
        if ('text' in node) return document.createTextNode(node.text);
        const element = document.createElement(node.tag);
        for (const [name, value] of Object.entries(node.attributes)) {
            element.setAttribute(name, value);
        }
        for (const child of node.children) element.appendChild(createNode(child));
        return element;
    }

    function setOpen(isOpen) {
        panel.classList.toggle('open', isOpen);
        toggle.setAttribute('aria-expanded', String(isOpen));
        content.inert = !isOpen;
    }

    function showStatus(message) {
        status.textContent = message;
    }

    function mount() {
        if (window !== window.top) return;
        if (host) {
            ensureAttached();
            return;
        }

        host = document.createElement('div');
        host.id = 'poc-scrap-panel';
        const root = host.attachShadow({ mode: 'open' });
        const style = document.createElement('style');
        style.textContent = css;
        root.append(style, createNode(markup));

        panel = root.querySelector('.panel');
        toggle = root.querySelector('.toggle');
        content = root.querySelector('#content');
        status = root.querySelector('[role="status"]');

        setOpen(false);
        toggle.addEventListener('click', () => setOpen(!panel.classList.contains('open')));
        root.querySelector('.action').addEventListener('click', async () => {
            try {
                await bridge.openSession();
                showStatus('Session opened.');
            } catch (error) {
                showStatus('Error: ' + error.message);
            }
        });
        // Observe child changes only; normal updates need just an isConnected check.
        observer = new MutationObserver(ensureAttached);
        window.addEventListener('pagehide', () => observer.disconnect());
        window.addEventListener('pageshow', resume);
        resume();
    }

    return { mount };
}
