export interface ScrollEvent extends WheelEvent {
  wheelDelta: number;
  deltaY: number;
}

export function checkScrollDirection(event: ScrollEvent) {
  if (checkScrollDirectionIsUp(event)) {
    return 1;
  } else {
    return -1;
  }
}

export function checkScrollDirectionIsUp(event: ScrollEvent) {
  if (event.wheelDelta) {
    return event.wheelDelta > 0;
  }
  return event.deltaY < 0;
}
