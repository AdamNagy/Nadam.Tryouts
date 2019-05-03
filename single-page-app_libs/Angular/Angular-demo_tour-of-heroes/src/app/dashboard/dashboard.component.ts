import { Component, OnInit } from '@angular/core';
import { Hero } from '../hero';
import { HeroService } from '../hero.service';
import {CdkDragDrop, moveItemInArray} from '@angular/cdk/drag-drop';

@Component({
selector: 'app-dashboard',
templateUrl: './dashboard.component.html',
styleUrls: [ './dashboard.component.css' ]
})
export class DashboardComponent implements OnInit {

    constructor(private heroService: HeroService) { }
    heroes: Hero[] = [];

    ngOnInit() {
        this.getHeroes();
    }

    getHeroes(): void {
        this.heroService.getHeroes()
        .subscribe(heroes => this.heroes = heroes.slice(1, 5));
    }

    drop(event: CdkDragDrop<string[]>) {
        moveItemInArray(this.heroes, event.previousIndex, event.currentIndex);
    }
}
