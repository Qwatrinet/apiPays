<script>
import DataManagement from '@/classes/DataManagement';
import TripLike from '@/classes/TripLike';
import ChoseCity from './ChoseCity.vue';
export default {
    data() {
        return {
            deleting: false,
            modifying: false,
            cityChoiceStart: "",
            cityChoiceEnd: "",
            dateDepart: "",
            dateArrivee: "",
            villeDepart: "",
            villeArrivee: "",
            villeDepartOrig: "",
            villeArriveeOrig: "",
            villes: [],
            passagers: [],
        }
    },
    computed: {
        TripDateDepart() {
            let date = (this.trip.dateDepart.split("T")[0]).split("-")
            return (date[2] + "/" + date[1] + "/" + date[0])

        },
        TripDateArrivee() {
            if (this.trip.dateArrivee != null) {
                let date = (this.trip.dateArrivee.split("T")[0]).split("-")
                return (date[2] + "/" + date[1] + "/" + date[0])
            }
            return "Pas encore arrivé"
        },
    },
    components: {
        ChoseCity
    },
    props: {
        trip: Object
    },
    async mounted() {
        this.villes = await DataManagement.fetchAllCities()
        this.villeDepartOrig = (this.villes.find((ville) => ville.id == this.trip.villeDepartId)).nomVille
        this.villeArriveeOrig = (this.villes.find((ville) => ville.id == this.trip.villeArriveeId)).nomVille
        const personnes = await DataManagement.fetchpeople()
        const personTrips = await DataManagement.fetchPersonTrips()
        for (const persontrip of personTrips) {
            if (persontrip.trajetId == this.trip.id) {
                this.passagers.push({ person: (personnes.find((personne) => personne.id == persontrip.personneId)), estConducteur: persontrip.estConducteur })
            }
        }

    },
    methods: {
        async ModifyTrip() {
            this.villeDepart = (this.villes.find((ville) => ville.id == this.trip.villeDepartId)).nomVille
            this.villeArrivee = (this.villes.find((ville) => ville.id == this.trip.villeArriveeId)).nomVille
            this.dateDepart = this.trip.dateDepart.split('T')[0]
            if (this.trip.dateArrivee != null) {
                this.dateArrivee = this.trip.dateArrivee.split('T')[0]
            }

            this.modifying = true
        },

        cityNameToCity(nomVille) {
            return this.villes.filter((city) => city.nomVille.toLowerCase() == nomVille.toLowerCase())
        },

        async FireModif(startingCities = [], finalCities = []) {
            //init
            if (startingCities.length == 0) {
                startingCities = this.cityNameToCity(this.villeDepart)
            }
            if (finalCities.length == 0) {
                finalCities = this.cityNameToCity(this.villeArrivee)
            }

            //Check si dateDepart et arrivée valides
            if (this.dateArrivee.length > 0) {
                if (isNaN(new Date(this.dateDepart)) || isNaN(new Date(this.dateArrivee))) {
                    console.log("Date invalide.")
                    return
                }
                if (new Date(this.dateDepart).getTime() >= new Date(this.dateArrivee).getTime()) {
                    console.log("Invalide! Le départ est après l'arrivée!")
                    return
                }
            }

            //check champs existants et non vides
            if (startingCities.length == 0) {
                console.log("ville de départ inconnue.")
                return
            }
            if (finalCities.length == 0) {
                console.log("ville d'arrivée inconnue.")
                return
            }
            if (this.dateDepart == '') {
                console.log('Une date de départ est requise!')
                return
            }

            //check si modifications à faire
            let arriv = ""
            if (this.trip.dateArrivee == null) {
                arriv = null
            }
            else {
                arriv = this.trip.dateArrivee.split("T")[0]
            }
            if (arriv == this.dateArrivee && this.trip.dateDepart.split("T")[0] == this.dateDepart && this.villeDepart == this.villeDepartOrig && this.villeArrivee == this.villeArriveeOrig) {
                console.log("Pas de modification.")
                this.modifying = false
                return
            }

            // gestion des villes homonymes
            if (startingCities.length > 1) {
                console.log("quelle est la bonne ville de départ?")
                this.cityChoiceStart = startingCities
                return
            }
            if (finalCities.length > 1) {
                console.log("quelle est la bonne ville d'arrivée'?")
                this.cityChoiceEnd = finalCities
                return
            }

            //post
            if (this.dateArrivee.length == 0) {
                this.dateArrivee = null
            }
            const tripToChange = new TripLike(this.trip.id, this.dateDepart, this.dateArrivee, startingCities[0].id, finalCities[0].id)
            await DataManagement.changeTrip(tripToChange)

            //réinitialisation de la vue
            this.dateArrivee = ""
            this.dateDepart = ""
            this.villeDepart = ""
            this.villeArrivee = ""
            this.modifying = false
            window.location = ''


        },
        StartChosen(city) {
            this.cityChoiceStart = ""
            this.FireModif([city], [])
        },
        EndChosen(city) {
            this.cityChoiceEnd = ""
            this.FireModif([], [city])
        },

        AskDelete() {
            this.deleting = true
        },
        async ConfirmDelete() {
            const delTrip = new TripLike(this.trip.id, this.trip.dateDepart, this.trip.dateArrivee, this.trip.villeDepartId, this.trip.villeArriveeId)
            await DataManagement.deleteTrip(delTrip)
            window.location = ''
        },
        CancelDelete() {
            this.deleting = false
        },


    }
}
</script>
<template>
    <div>
        <p>Départ le: {{ TripDateDepart }} de {{ villeDepartOrig }}</p>
        <p> Arrivée le: {{ TripDateArrivee }} à {{ villeArriveeOrig }}</p>
        <button v-if="TripDateArrivee == 'Pas encore arrivé'" @click="ModifyTrip">Modifier</button>
        <button v-if="TripDateArrivee == 'Pas encore arrivé'" @click="AskDelete">Supprimer</button>
    </div>
    <div></div>
    <div v-if="deleting">
        <p>Voulez-vous vraiment supprimer ce trajet?</p>
        <button @click="ConfirmDelete">Oui</button>
        <button @click="CancelDelete">Non</button>
    </div>
    <div v-for="passager of this.passagers" :key="passager.id" class="personne">
        <p>{{ passager.person.nom }} {{ passager.person.prenom }} </p>
        <p>Conducteur: {{ passager.estConducteur ? 'Oui' : 'Non' }}</p>
        <button type="button" v-if="modifying">Modifier Passager</button>
        <button type="button" v-if="modifying">Supprimer Passager</button>
    </div>
    <form id="modif" v-if="modifying">
        <label for="dateDepart">Date de départ: </label>
        <input type="date" id="dateDepart" name="dateDepart" v-model="dateDepart">
        <label for="dateArrivee">Date d'arrivée: </label>
        <input type="date" id="dateArrivee" name="dateArrivee" v-model="dateArrivee">
        <label for="villeDepart">Ville de départ: </label>
        <input list="villes" type="text" id="villeDepart" name="villeDepart" v-model="villeDepart">
        <label for="villeArrivee">Ville d'arrivée: </label>
        <input list="villes" type="text" id="villeArrivee" name="villeArrivee" v-model="villeArrivee">
        <button type="button" @click="FireModif()">Valider</button>
        <datalist id="villes">
            <option v-for="ville of villes" :value="ville.nomVille" :key="ville.id"></option>
        </datalist>
    </form>
    <dialog v-if="cityChoiceStart != ''" open>
        <p>Quelle est la bonne ville?</p>
        <ul>
            <li v-for="city in cityChoiceStart" :key="city.id">
                <ChoseCity :city="city"></ChoseCity>
                <button type="button" @click="StartChosen(city)">Celle là!</button>
            </li>
        </ul>
    </dialog>
    <dialog v-if="cityChoiceEnd != ''" open>
        <p>Quelle est la bonne ville?</p>
        <ul>
            <li v-for="city in cityChoiceEnd" :key="city.id">
                <ChoseCity :city="city"></ChoseCity>
                <button type="button" @click="EndChosen(city)">Celle là!</button>
            </li>
        </ul>
    </dialog>
</template>

<style scoped>
#modif {
    display: block;
    margin: 4rem;

    input,
    label {
        margin-right: 1rem
    }


}

div {
    display: grid;
    grid-template-columns: 1fr 1fr 1fr 1fr;

}

p {
    display: inline;
    margin-right: 1rem;
}

button {
    margin-right: 1rem;
    margin-bottom: 1rem;
    margin-top: 1rem;
}
</style>