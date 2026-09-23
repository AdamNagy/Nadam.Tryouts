function createSessionImages(bridge) {
    const grid = document.getElementById('images-grid');
    const status = document.getElementById('images-status');
    const cards = new Map();
    let stopped = false;
    const observer = new IntersectionObserver(entries => {
        for (const entry of entries) {
            if (!entry.isIntersecting) continue;
            observer.unobserve(entry.target);
            loadPreview(entry.target);
        }
    }, { rootMargin: '200px' });

    async function loadPreview(card) {
        const image = card.querySelector('img');
        const message = card.querySelector('.preview-status');
        try {
            const dataUrl = await bridge.getImageData(Number(card.dataset.imageId));
            if (stopped) return;
            image.onload = () => { message.textContent = ''; };
            image.onerror = () => { message.textContent = 'Preview unavailable for this image.'; };
            image.src = dataUrl;
        } catch (error) {
            if (!stopped) message.textContent = 'Could not load preview: ' + error.message;
        }
    }

    function render(images) {
        for (const item of images) {
            if (cards.has(item.id)) continue;
            const card = document.createElement('figure');
            card.className = 'image-card';
            card.dataset.imageId = item.id;
            const image = document.createElement('img');
            image.alt = 'Captured image';
            const caption = document.createElement('figcaption');
            const url = document.createElement('p');
            url.textContent = item.url;
            const details = document.createElement('p');
            details.textContent = `${item.contentType} · ${Math.ceil(item.byteLength / 1024)} KB`;
            const message = document.createElement('p');
            message.className = 'preview-status';
            message.textContent = 'Loading preview...';
            caption.append(url, details, message);
            card.append(image, caption);
            cards.set(item.id, card);
            grid.appendChild(card);
            observer.observe(card);
        }
        status.textContent = images.length === 0 ? 'No captured images yet.' : `${images.length} images`;
        document.getElementById('images-tab').textContent = `Images (${images.length})`;
    }

    async function refresh() {
        try {
            const images = await bridge.getImages();
            if (!stopped) render(images);
        } catch (error) {
            if (!stopped) status.textContent = 'Could not load images: ' + error.message;
        }
    }

    function stop() {
        stopped = true;
        observer.disconnect();
    }

    return { refresh, stop };
}
