function setVertical(line, x) {
  line.style.left = `${x}px`;
  line.style.top = "0";
}

function setHorizontal(line, y) {
  line.style.left = "0";
  line.style.top = `${y}px`;
}

const image = document.getElementById("the-image");
const pointerV = document.getElementById("pointer-v");
const pointerH = document.getElementById("pointer-h");

const imageV = document.getElementById("image-v");
const imageH = document.getElementById("image-h");

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
  setVertical(pointerV, x);
  setHorizontal(pointerH, y);

  const imageX = x - xCorrection;
  const imageY = y - yCorrection;
  setVertical(imageV, imageX);
  setHorizontal(imageH, imageY);

  image.style.left = `${imageX}px`;
  image.style.top = `${imageY}px`;
}

image.addEventListener("drag", setImagePosition);
image.addEventListener("dragend", setImagePosition);

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
