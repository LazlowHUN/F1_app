let pilots = [];
let cars = [];
let teams = [];
let connection = null;
let tableSelect = null;

let pilotIdToUpdate = -1;
let carIdToUpdate = -1;
let teamIdToUpdate = -1;

getdata();
setupSignalR();

tableSelect.addEventListener('change', function () {
	getdata();
});


function setupSignalR() {
	connection = new signalR.HubConnectionBuilder()
		.withUrl("http://localhost:4608/hub")
		.configureLogging(signalR.LogLevel.Information)
		.build();

	connection.on("PilotCreated", (user, message) => {
		getdata();
	});
	connection.on("PilotDeleted", (user, message) => {
		getdata();
	});
	connection.on("PilotUpdated", (user, message) => {
		getdata();
	});

	connection.on("CarCreated", (user, message) => {
		getdata();
	});
	connection.on("CarDeleted", (user, message) => {
		getdata();
	});
	connection.on("CarUpdated", (user, message) => {
		getdata();
	});

	connection.on("TeamCreated", (user, message) => {
		getdata();
	});
	connection.on("TeamDeleted", (user, message) => {
		getdata();
	});
	connection.on("TeamUpdated", (user, message) => {
		getdata();
	});

	connection.onclose(async () => {
		await start();
	});
	start();
}

async function start() {
	try {
		await connection.start();
		console.log("SignalR Connected.");
	} catch (err) {
		console.log(err);
		setTimeout(start, 5000);
	}
};


async function getdata() {
	tableSelect = document.getElementById("table-select");
	await fetch('http://localhost:4608/'+tableSelect.value)
		.then(x => x.json())
		.then(y => {
		if (tableSelect.value == 'pilot') {
			pilots = y;
			display();
		} else if (tableSelect.value === 'car') {
			cars = y;
			display();
		} else if (tableSelect.value === 'team') {
			teams = y;
			display();
		}
		});
}


function display() {
	document.getElementById('resultarea').innerHTML = "";
	const tableHead = document.querySelector("thead tr");
	const inputFieldsDiv = document.getElementById("inputFields");
	inputFieldsDiv.innerHTML = "";

	if (tableSelect.value == 'pilot') {
		inputFieldsDiv.innerHTML = `
		<label>Pilot Name</label>
        <input type="text" id="pilotName" />
        <label>Pilot Age</label>
        <input type="text" id="pilotAge" />
        <label>TeamID</label>
        <input type="text" id="teamId" />
		<button type="button" onclick="create()">Add Pilot</button>
            `;
		tableHead.innerHTML = `
		 <th>ID</th>
		 <th>Name</th>
		 <th>Age</th>
		 <th>TeamID</th>
		 <th>Actions</th>
		`;
		pilots.forEach(t => {
			document.getElementById('resultarea').innerHTML +=
				"<tr><td>" + t.pilotId + "</td><td>" + t.pilotName + "</td><td>" + t.pilotAge + "</td><td>"
				+ t.teamId + "</td><td>" +
				`<button type="button" onclick="remove(${t.pilotId})">Delete</button>` + `<button type="button" onclick="showupdate(${t.pilotId})">Update</button>`
				+ "</td></tr>";
		});
	}
	else if (tableSelect.value == 'car') {
		inputFieldsDiv.innerHTML = `
		<label>Engine brand</label>
        <input type="text" id="engineBrand" />
        <label>Max speed</label>
        <input type="text" id="maxSpeed" />
        <label>Horsepower</label>
        <input type="text" id="horsepower" />
		<button type="button" onclick="create()">Add Car</button>
            `;
		tableHead.innerHTML = `
		 <th>CarID</th>
		 <th>Enginebrand</th>
		 <th>Max speed</th>
		 <th>Horsepower</th>
		 <th>Actions</th>
		`;
		cars.forEach(t => {
			document.getElementById('resultarea').innerHTML +=
				"<tr><td>" + t.carId + "</td><td>" + t.engineBrand + "</td><td>" + t.maxSpeed + "</td><td>" + t.horsepower + "</td><td>" +
				`<button type="button" onclick="remove(${t.carId})">Delete</button>` + `<button type="button" onclick="showupdate(${t.carId})">Update</button>`
				+ "</td></tr>";
		});
	}
	else if (tableSelect.value == 'team') {
		inputFieldsDiv.innerHTML = `
		<label>Team name</label>
        <input type="text" id="teamName" />
        <label>CarID</label>
        <input type="text" id="carId" />
        <label>Budget</label>
        <input type="text" id="budget" />
		<label>Team points</label>
        <input type="text" id="teamPoints" />
		<button type="button" onclick="create()">Add Team</button>
            `;
		tableHead.innerHTML = `
		 <th>TeamID</th>
		 <th>Team name</th>
		 <th>CarID</th>
		 <th>Budget</th>
		 <th>Team points</th>
		 <th>Actions</th>
		`;
		teams.forEach(t => {
			document.getElementById('resultarea').innerHTML +=
				"<tr><td>" + t.teamId + "</td><td>" + t.teamName + "</td><td>" + t.carId + "</td><td>" + t.budget + "</td><td>" + t.teamPoints + "</td><td>"
				+ `<button type="button" onclick="remove(${t.teamId})">Delete</button>` + `<button type="button" onclick="showupdate(${t.teamId})">Update</button>`
				+ "</td></tr>";
		});
	}
}

function remove(id) {
	if (tableSelect.value == "pilot") {
		fetch('http://localhost:4608/pilot/' + id, {
			method: 'DELETE',
			headers: { 'Content-Type': 'application/json', },
			body: null
		})
			.then(response => response)
			.then(data => {
				console.log('Success:', data);
				getdata();
			})
			.catch((error) => { console.error('Error:', error); });
	}
	else if (tableSelect.value == "car") {
		fetch('http://localhost:4608/car/' + id, {
			method: 'DELETE',
			headers: { 'Content-Type': 'application/json', },
			body: null
		})
			.then(response => response)
			.then(data => {
				console.log('Success:', data);
				getdata();
			})
			.catch((error) => { console.error('Error:', error); });
	}
	else if (tableSelect.value == "team") {
		fetch('http://localhost:4608/team/' + id, {
			method: 'DELETE',
			headers: { 'Content-Type': 'application/json', },
			body: null
		})
			.then(response => response)
			.then(data => {
				console.log('Success:', data);
				getdata();
			})
			.catch((error) => { console.error('Error:', error); });
	}
}

function showupdate(id) {
	const updateformdiv = document.getElementById("updateformdiv");
	updateformdiv.innerHTML = "";

	if (tableSelect.value == "pilot") {
		updateformdiv.innerHTML = `
			<label>Pilot Name</label>
            <input type="text" id="pilotNameToUpdate" />
            <label>Pilot Age</label>
            <input type="text" id="pilotAgeToUpdate" />
            <label>TeamID</label>
            <input type="text" id="teamIdToUpdate" />
            <button type="button" onclick="update()">Update Pilot</button>
            `;
		document.getElementById('pilotNameToUpdate').value = pilots.find(t => t['pilotId'] == id)['pilotName']
		document.getElementById('pilotAgeToUpdate').value = pilots.find(t => t['pilotId'] == id)['pilotAge']
		document.getElementById('teamIdToUpdate').value = pilots.find(t => t['pilotId'] == id)['teamId']
		document.getElementById('updateformdiv').style.display = 'flex';
		pilotIdToUpdate = id;
	}
	else if (tableSelect.value == "car") {
		updateformdiv.innerHTML = `
			<label>Engine brand</label>
			<input type="text" id="engineBrandToUpdate" />
			<label>Max speed</label>
			<input type="text" id="maxSpeedToUpdate" />
			<label>Horsepower</label>
			<input type="text" id="horsepowerToUpdate" />
            <button type="button" onclick="update()">Update Pilot</button>
            `;
		document.getElementById('engineBrandToUpdate').value = cars.find(t => t['carId'] == id)['engineBrand']
		document.getElementById('maxSpeedToUpdate').value = cars.find(t => t['carId'] == id)['maxSpeed']
		document.getElementById('horsepowerToUpdate').value = cars.find(t => t['carId'] == id)['horsepower']
		document.getElementById('updateformdiv').style.display = 'flex';
		carIdToUpdate = id;
	}
	else if (tableSelect.value == "team") {
		updateformdiv.innerHTML = `
			<label>Team name</label>
			<input type="text" id="teamNameToUpdate" />
			<label>CarID</label>
			<input type="text" id="carIdToUpdate" />
			<label>Budget</label>
			<input type="text" id="budgetToUpdate" />
			<label>Team points</label>
			<input type="text" id="teamPointsToUpdate" />
            <button type="button" onclick="update()">Update Pilot</button>
            `;
		document.getElementById('teamNameToUpdate').value = teams.find(t => t['teamId'] == id)['teamName']
		document.getElementById('carIdToUpdate').value = teams.find(t => t['teamId'] == id)['carId']
		document.getElementById('budgetToUpdate').value = teams.find(t => t['teamId'] == id)['budget']
		document.getElementById('teamPointsToUpdate').value = teams.find(t => t['teamId'] == id)['teamPoints']
		document.getElementById('updateformdiv').style.display = 'flex';
		teamIdToUpdate = id;
	}
}

function update() {
	document.getElementById('updateformdiv').style.display = 'none';

	if (tableSelect.value == "pilot") {
		let name = document.getElementById("pilotNameToUpdate").value;
		let age = document.getElementById("pilotAgeToUpdate").value;
		let teamID = document.getElementById("teamIdToUpdate").value;

		fetch('http://localhost:4608/pilot', {
			method: 'PUT',
			headers: { 'Content-Type': 'application/json', },
			body: JSON.stringify({ pilotName: name, pilotAge: age, teamId: teamID, pilotId: pilotIdToUpdate })
		})
			.then(response => response)
			.then(data => {
				console.log('Success:', data);
				getdata();
			})
			.catch((error) => { console.error('Error:', error); });
	}
	else if (tableSelect.value == "car") {
		let engine = document.getElementById('engineBrandToUpdate').value;
		let speed = document.getElementById('maxSpeedToUpdate').value;
		let power = document.getElementById('horsepowerToUpdate').value;

		fetch('http://localhost:4608/car', {
			method: 'PUT',
			headers: { 'Content-Type': 'application/json', },
			body: JSON.stringify({ engineBrand: engine, maxSpeed: speed, horsepower: power, carId: carIdToUpdate })
		})
			.then(response => response)
			.then(data => {
				console.log('Success:', data);
				getdata();
			})
			.catch((error) => { console.error('Error:', error); });
	}
	else if (tableSelect.value == "team") {
		let name = document.getElementById('teamNameToUpdate').value;
		let car = document.getElementById('carIdToUpdate').value;
		let budg = document.getElementById('budgetToUpdate').value;
		let points = document.getElementById('teamPointsToUpdate').value;

		fetch('http://localhost:4608/team', {
			method: 'PUT',
			headers: { 'Content-Type': 'application/json', },
			body: JSON.stringify({ teamName: name, carId: car, budget: budg, teamPoints: points, teamId: teamIdToUpdate })
		})
			.then(response => response)
			.then(data => {
				console.log('Success:', data);
				getdata();
			})
			.catch((error) => { console.error('Error:', error); });
	}
}

function create() {
	if (tableSelect.value == "pilot") {
		let name = document.getElementById("pilotName").value;
		let age = document.getElementById("pilotAge").value;
		let teamID = document.getElementById("teamId").value;

		fetch('http://localhost:4608/pilot', {
			method: 'POST',
			headers: { 'Content-Type': 'application/json', },
			body: JSON.stringify({ pilotName: name, pilotAge: age, teamId: teamID })
		})
			.then(response => response)
			.then(data => {
				console.log('Success:', data);
				getdata();
			})
			.catch((error) => { console.error('Error:', error); });
	}
	else if (tableSelect.value == "car") {
		let brand = document.getElementById("engineBrand").value;
		let speed = document.getElementById("maxSpeed").value;
		let power = document.getElementById("horsepower").value;

		fetch('http://localhost:4608/car', {
			method: 'POST',
			headers: { 'Content-Type': 'application/json', },
			body: JSON.stringify({ engineBrand: brand, maxSpeed: speed, horsepower: power })
		})
			.then(response => response)
			.then(data => {
				console.log('Success:', data);
				getdata();
			})
			.catch((error) => { console.error('Error:', error); });
	}
	else if (tableSelect.value == "team") {
		let name = document.getElementById("teamName").value;
		let id = document.getElementById("carId").value;
		let budge = document.getElementById("budget").value;
		let points = document.getElementById("teamPoints").value;

		fetch('http://localhost:4608/team', {
			method: 'POST',
			headers: { 'Content-Type': 'application/json', },
			body: JSON.stringify({ teamName: name, carId: id, budget: budge, teamPoints: points })
		})
			.then(response => response)
			.then(data => {
				console.log('Success:', data);
				getdata();
			})
			.catch((error) => { console.error('Error:', error); });
	}
}