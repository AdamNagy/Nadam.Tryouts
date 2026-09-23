function createScrapPanel(markup, css, bridge) {
    let panel;
    let toggle;
    let content;
    let status;

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
        if (window !== window.top || document.getElementById('poc-scrap-panel')) return;

        const host = document.createElement('div');
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
        document.body.appendChild(host);
    }

    return { mount };
}
