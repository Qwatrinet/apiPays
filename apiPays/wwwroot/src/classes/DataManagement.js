export default class DataManagement{

    //Countries
    static async fetchAllCountries(){
        return await (await fetch('https://localhost:7134/api/Pays')).json()
    }


     //cities
    static async fetchAllCities(){
        return await (await fetch('https://localhost:7134/api/Villes')).json()
    }

    static async fetchCitiesByCountry(countryId){
        return (await (await fetch('https://localhost:7134/api/Pays/'+countryId.toString())).json()).villes
    }
    
    static async postNewCity(city){
        return await (await fetch('https://localhost:7134/api/Villes', {
            method: 'POST',
            headers: {"Content-Type": "application/json"}, 
            body: JSON.stringify(city)
        })).json()
    }


    //trips

    static async fetchTrips(){
        return await (await fetch('https://localhost:7134/api/Trajets')).json()
    }

    static async postNewTrip(trip) {
        console.log(trip)
        return await (await fetch('https://localhost:7134/api/Trajets', {
            method: 'POST',
            headers: {"Content-Type": "application/json"}, 
            body: JSON.stringify(trip)
        })).json()
    }

    static async changeTrip(trip){
        return await (await fetch('https://localhost:7134/api/Trajets/'+trip.id, {
            method: 'PUT',
            headers: {"Content-Type": "application/json"}, 
            body: JSON.stringify(trip)
        }))
    }

    static async deleteTrip(trip){
        await (await fetch('https://localhost:7134/api/Trajets/'+trip.id, {
            method: 'DELETE'
        }))
    }

    //persontrip

    static async fetchPersonTrips() {
        return await (await fetch('https://localhost:7134/api/PersonneTrajets')).json()
    }

    static async postNewPersonTrip(personTrip) {
        console.log(personTrip)
        const r =  await (await fetch('https://localhost:7134/api/PersonneTrajets', {
            method: 'POST',
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(personTrip)
        })).json()
        return r
    }


    //people
    static async fetchpeople(){
        return await (await fetch('https://localhost:7134/api/Personnes')).json()
    }

    static async postNewPerson(person){
        return await (await fetch('https://localhost:7134/api/Personnes', {
            method: 'POST',
            headers: {"Content-Type": "application/json"}, 
            body: JSON.stringify(person)
        })).json()
    }

    static async changePerson(person){
        return await (await fetch('https://localhost:7134/api/Personnes/'+person.id, {
            method: 'PUT',
            headers: {"Content-Type": "application/json"}, 
            body: JSON.stringify(person)
        }))
    }

    static async deletePerson(person){
        await (await fetch('https://localhost:7134/api/Personnes/'+person.id, {
            method: 'DELETE'
        }))
    }
}