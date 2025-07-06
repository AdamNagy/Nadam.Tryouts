function setVertical(line, x) {
  line.style.left = `${x}px`;
  line.style.top = "0";
}

function setHorizontal(line, y) {
  line.style.left = "0";
  line.style.top = `${y}px`;
}

function setCross(cross, x, y) {
  setVertical(cross.v, x);
  setHorizontal(cross.h, y);
}

const image = document.getElementById("the-image");
const pointerCross = {
  v: document.getElementById("pointer-v"),
  h: document.getElementById("pointer-h")
}

const imageCross = {
  v: document.getElementById("image-v"),
  h: document.getElementById("image-h")
}

let xCorrection = 0;
let yCorrection = 0;

image.addEventListener("mousedown", (event) => {
  const x = event.clientX;
  const y = event.clientY;

  const imageX = image.offsetLeft;
  const imageY = image.offsetTop;

  xCorrection = x - imageX;
  yCorrection = y - imageY;
});

function setImagePosition(event) {
  const x = event.clientX;
  const y = event.clientY;

  if(x <= 0 || y <= 0) {
    return;
  }

  setCross(pointerCross, x, y);

  const imageX = x - xCorrection;
  const imageY = y - yCorrection;
  setCross(imageCross, imageX, imageY);

  image.style.left = `${imageX}px`;
  image.style.top = `${imageY}px`;
}

image.addEventListener("drag", setImagePosition);

function checkScrollDirection(event) {
  if (checkScrollDirectionIsUp(event)) {
    return 1;
  } else {
    return -1;
  }
}

function checkScrollDirectionIsUp(event) {
  if (event.wheelDelta) {
    return event.wheelDelta > 0;
  }
  return event.deltaY < 0;
}

const scrollAmount = 90;
image.addEventListener("wheel", (e) => {
  e.stopImmediatePropagation();
  e.preventDefault();
  const imageX = image.clientWidth;

  const scrollDirection = checkScrollDirection(e);

  if (scrollDirection == -1) {
    image.style.width = `${imageX - scrollAmount}px`;
  } else {
    image.style.width = `${imageX + scrollAmount}px`;
  }
});
