import { Component, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';

@Component({
  standalone: true,
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  template: `<div class="height-includer"></div>
    <ndm-parallax
      height="300"
      src="https://images.pexels.com/photos/1183099/pexels-photo-1183099.jpeg?cs=srgb&dl=pexels-simon73-1183099.jpg&fm=jpg"
    >
    </ndm-parallax>

    <div class="height-includer"></div>`,
  styles: ` .height-includer {
			height: 600px;
		}`,
})
export class ParallaxDemoComponent {}
