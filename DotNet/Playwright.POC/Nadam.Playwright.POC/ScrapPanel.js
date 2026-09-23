(() => {
    if (window !== window.top) return;

    function mount() {
        if (document.getElementById('poc-scrap-panel')) return;

        const host = document.createElement('div');
        host.id = 'poc-scrap-panel';
        host.style.cssText = 'all:initial!important;position:fixed!important;top:35%!important;right:0!important;width:280px!important;height:180px!important;z-index:2147483647!important;display:block!important;';
        const root = host.attachShadow({ mode: 'open' });
        root.innerHTML = `
            <style>
                .panel { box-sizing: border-box; position: relative; width: 280px; height: 180px;
                    padding: 20px 20px 20px 56px; border-radius: 12px 0 0 12px;
                    background: #172033; color: white; font: 14px system-ui, sans-serif;
                    box-shadow: 0 4px 18px #0004; transform: translateX(236px);
                    transition: transform 220ms ease; pointer-events: auto; }
                .panel.open { transform: translateX(0); }
                button { font: inherit; cursor: pointer; color: white; border: 0; }
                .toggle { position: absolute; left: 0; top: 0; width: 44px; height: 100%;
                    border-radius: 12px 0 0 12px; background: #2458d3; writing-mode: vertical-rl; }
                .action { padding: 10px 14px; border-radius: 6px; background: #2458d3; }
                button:focus-visible { outline: 2px solid white; outline-offset: -4px; }
                h2 { font-size: 16px; margin: 0 0 16px; }
                p { font-size: 12px; margin-top: 12px; }
                @media (prefers-reduced-motion: reduce) { .panel { transition: none; } }
            </style>
            <div class="panel">
                <button class="toggle" type="button" aria-expanded="false" aria-controls="content">Scrap panel</button>
                <div id="content" inert>
                    <h2>Scrap panel</h2>
                    <button class="action" type="button">Call C#</button>
                    <p role="status"></p>
                </div>
            </div>`;

        // Only the visible panel should capture clicks, not its hidden footprint.
        host.style.setProperty('pointer-events', 'none', 'important');
        const panel = root.querySelector('.panel');
        const toggle = root.querySelector('.toggle');
        const content = root.querySelector('#content');
        toggle.addEventListener('click', () => {
            const open = panel.classList.toggle('open');
            toggle.setAttribute('aria-expanded', String(open));
            content.inert = !open;
        });
        root.querySelector('.action').addEventListener('click', async () => {
            const status = root.querySelector('[role="status"]');
            try {
                status.textContent = await window.pocScrapPanelAction();
            } catch (error) {
                status.textContent = 'Error: ' + error.message;
            }
        });
        document.body.appendChild(host);
    }

    if (document.readyState === 'loading') {
        document.addEventListener('DOMContentLoaded', mount, { once: true });
    } else {
        mount();
    }
})();
