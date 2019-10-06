import { Component, OnInit } from '@angular/core';
import * as signalR from '@aspnet/signalr';

@Component({
	selector: 'app-home',
	templateUrl: './home.component.html',
	styleUrls: ['./home.css']
})
export class HomeComponent implements OnInit {

	// connection: signalR.HubConnectionBuilder;
	messages: string[] = new Array<string>();
	currentMessage = 'Type here';
	connection: any;
	messageArea = document.getElementById('divMessages');

	ngOnInit(): void {
		this.messages = new Array<string>();
		this.connection = new signalR.HubConnectionBuilder()
			.withUrl('/chathub', {
				skipNegotiation: true,
				transport: signalR.HttpTransportType.WebSockets
			})
			.configureLogging(signalR.LogLevel.Information)
			// .withHubProtocol(new HubProtocol())
			.build();

		this.connection.on('messageReceived', (username: string, message: string) => {
			console.log(`${username} said: ${message}`);
			const newMessage = document.createElement('p');
			newMessage.innerHTML = `${username} said: ${message}`;
			this.messageArea.appendChild(newMessage);
		});

		this.connection.on('broadcastMessage', function (username, message) {
			console.log(`${username} said: ${message}`);
			if ( this.message === undefined) {
				this.messages = new Array<string>();
			}
			this.messages.push(`${username} said: ${message}`);
		});

		this.connection
			.start()
			.then(function () {
				console.log('connection started');
			})
			.catch(err => console.error(err));
				console.log('SignalR running');
	}

	Send(): void {
		console.log(`Sending message: ${this.currentMessage}`);
		this.connection.invoke('NewMessage', 'Adam Nagy', this.currentMessage)
			.then(() => this.currentMessage = '')
			.catch((err) => console.error(err));
	}
}

// export class HubProtocol implements signalR.IHubProtocol {
// 	name: string;	version: number;
// 	transferFormat: signalR.TransferFormat;
// 	parseMessages(input: string | ArrayBuffer | Buffer, logger: signalR.ILogger): signalR.HubMessage[] {
// 		throw new Error('Method not implemented.');
// 	}
// 	writeMessage(message: signalR.HubMessage): string | ArrayBuffer {
// 		throw new Error('Method not implemented.');
// 	}
// }
