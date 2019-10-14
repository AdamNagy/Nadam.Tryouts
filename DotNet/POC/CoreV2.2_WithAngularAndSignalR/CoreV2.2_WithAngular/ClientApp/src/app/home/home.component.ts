import { Component, OnInit } from '@angular/core';
import * as signalR from '@aspnet/signalr';

@Component({
	selector: 'app-home',
	templateUrl: './home.component.html',
	styleUrls: ['./home.css']
})
export class HomeComponent implements OnInit {

	currentMessage = 'Type here';
	connection: any;
	messageArea: HTMLElement;

	ngOnInit(): void {
		this.messageArea = document.getElementById('divMessages');
		this.connection = new signalR.HubConnectionBuilder()
			.withUrl('/chathub', {
				skipNegotiation: true,
				transport: signalR.HttpTransportType.WebSockets
			})
			.configureLogging(signalR.LogLevel.Information)
			.build();

		this.connection.on('messageReceived', (username: string, message: string) => this.HandleMessageEvent(username, message));

		this.connection.on('broadcastMessage', (username, message) => this.HandleNewParticipant(username, message));

		this.connection
			.start()
			.then(function () {
				console.log('connection started');
			})
			.catch(err => console.error(err));

		console.log('SignalR running');
	}

	HandleMessageEvent(sender: string, message: string): void {
		console.log(`${sender} said: ${message}`);
		const newMessage = document.createElement('p');
		newMessage.innerHTML = `${sender} said: ${message}`;
		this.messageArea.appendChild(newMessage);
	}

	HandleNewParticipant(sender: string, message: string): void {
		console.log(`${sender} said: ${message}`);
		const newMessage = document.createElement('p');
		newMessage.innerHTML = `${sender} said: ${message}`;
		this.messageArea.appendChild(newMessage);
	}

	Send(): void {
		console.log(`Sending message: ${this.currentMessage}`);
		this.connection.invoke('NewMessage', 'Adam Nagy', this.currentMessage)
			.then(() => this.currentMessage = '')
			.catch((err) => console.error(err));
	}

	checkLoginState() {
		(window as any).FB.getLoginStatus((response: any) => {
			if ( response.connected === 'connected' ) {
				console.log(response.authResponse.userId);
			} else {
				console.log('Noone is logged in with FB');
			}
		});

	}
}
