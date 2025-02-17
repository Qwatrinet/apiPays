<script>
import Trip from '@/classes/Trip';
import DataManagement from '@/classes/DataManagement';
import Person from '@/classes/Person';
import PersonTrip from '@/classes/PersonTrip';
export default {
    data() {
        return {
            depart: "",
            arrivee: "",
            villeDepart: "",
            villeArrivee: "",
            passagers: [],
            addPassenger: false,
            nom: "",
            prenom: "",
            estConducteur: false,
            personnes: [],
            villes:[]
        }
    },
    async mounted() {
        this.personnes = await DataManagement.fetchpeople()
        this.villes=await DataManagement.fetchAllCities()
    },
    methods: {
        displayAddPassenger() {
            this.addPassenger = true
        },
        confirmPassenger() {
            this.passagers.push({ person: new Person(this.nom, this.prenom), estConducteur: this.estConducteur })
            this.addPassenger = false
            this.nom = ""
            this.prenom = ""
        },
        suppr(passager) {
            const index = this.passagers.indexOf(passager)
            if (index > -1) {
                this.passagers.splice(index, 1);
            }              
        },
        async postTrip() {
            if (this.depart == ""|| this.villeDepart == "" || this.villeArrivee == "") {
                console.log("empty field error.")
                return
            }
            if (this.villeDepart == this.villeArrivee) {
                console.log("Départ et arrivée au même endroit!")
                return
            }

            if (this.arrivee === "") {
                this.arrivee=null
            }

            if (this.arrivee != null) {
                if (new Date(this.depart).getTime() >= new Date(this.arrivee).getTime()) {
                    console.log("Invalide! Le départ est après l'arrivée!")
                    return
                }
            }
            

            const cities = await DataManagement.fetchAllCities()
            let endroitArrivee = ""
            let endroitDepart = ""
            for (const city of cities) {
                if (city.nomVille.toLowerCase() == this.villeDepart.toLowerCase()) {
                    endroitDepart = city.id
                }
                if (city.nomVille.toLowerCase() == this.villeArrivee.toLowerCase()) {
                    endroitArrivee = city.id

                }
            }
            if (endroitArrivee == "" || endroitDepart == "") {
                console.log("Impossible de trouver la ville.")
                return
            }
            const idPassagers = []
            let persToFind = ""
            if (this.passagers.length == 0) {
                console.log("Pas de passagers!")
                return
            }
            for (const passager of this.passagers) {
                persToFind = this.personnes.find((personne) => personne.nom.toLowerCase() == passager.person.nom.toLowerCase() && personne.prenom.toLowerCase() == passager.person.prenom.toLowerCase())
                if (persToFind === undefined) {
                    console.log(passager.person.nom + " " + passager.person.prenom + " est introuvable.")
                    return
                }
                if (idPassagers.find((p) => p.idPerson == persToFind.id) != null) {
                    console.log("Passager en double!")
                    return
                }
                idPassagers.push({ idPerson: persToFind.id, estConducteur: passager.estConducteur })
            }
            if (idPassagers.find((passager)=> passager.estConducteur)==undefined) {
                console.log("Il faut un conducteur!")
                return
            }
            

            const trip = new Trip(this.depart, this.arrivee, endroitDepart, endroitArrivee)
            const response = await DataManagement.postNewTrip(trip)
            for (const passager of idPassagers) {
                const perstrip = new PersonTrip(passager.idPerson, passager.estConducteur, response.id)
                DataManagement.postNewPersonTrip(perstrip)
            }
            this.depart = ""
            this.arrivee = ""
            this.villeArrivee = ""
            this.villeDepart = ""
            this.passagers=[]
        }
    }
}
</script>

<template>
    <RouterLink to="/move"><button>Retour au menu Trajets</button></RouterLink>
    <h1>Ajouter un trajet</h1>
    <div v-for="passager of this.passagers" :key="passager.id" class="personne">
        <p>Nom: {{ passager.person.nom }} Prénom: {{ passager.person.prenom }} Conducteur: {{ passager.estConducteur ?
            'Oui' : 'Non' }}
        </p>
        <button type="button" @click="suppr(passager)">Supprimer</button>
    </div>
    <form action="">

        <label for="depart">Date de départ: </label>
        <input type="date" id="depart" name="depart" v-model="depart" min="2024-01-01">

        <label for="arrivee">Date d'arrivée (facultatif): </label>
        <input type="date" id="arrivee" name="arrivee" v-model="arrivee" min="2024-01-01">

        <label for="villeDepart">Ville de départ: </label>
        <input type="text" id="villeDepart" name="villeDepart" v-model="villeDepart" list="villes">

        <label for="villeArrivee">Ville d'arrivée: </label>
        <input type="text" id="villeArrivee" name="villeArrivee" v-model="villeArrivee" list="villes">
        <datalist id="villes">
            <option v-for="ville of villes" :value="ville.nomVille" :key="ville.id"></option>
        </datalist>

        <button type="button" @click="postTrip">Ajouter le trajet</button>
    </form>
    <button type="button" @click="displayAddPassenger">Ajouter un passager</button>
    <form v-if="addPassenger" id="addPassenger">
        <label for="nom">Nom de la personne: </label>
        <input type="text" id="nom" name="nom" v-model="nom" list="noms">
        <datalist id="noms">
            <option v-for="personne of personnes" :value="personne.nom" :key="personne.id"></option>
        </datalist>

        <label for="prenom">Prenom de la personne: </label>
        <input type="text" id="prenom" name="prenom" v-model="prenom" list="prenoms">
        <datalist id="prenoms">
            <option v-for="personne of personnes" :value="personne.prenom" :key="personne.id"></option>
        </datalist>
        <label for="conducteur">Est-ce le conducteur?</label>
        <select name="select-conducteur" id="select-conducteur" v-model="estConducteur">
            <option :value="true">
                Oui
            </option>
            <option :value="false">
                Non
            </option>
        </select>


        <button type="button" @click="confirmPassenger">Ajouter le passager</button>
    </form>
</template>

<style scoped>
.personne {
    display: grid;
    grid-template-columns: 1fr 2fr;
    margin-bottom: 2rem;

    button {
        margin-bottom: 1rem;
        margin-top: 1rem;
    }

    p {
        margin-left: 1rem
    }
}

form {
    display: grid;
    grid-template-columns: 1fr 2fr;
    margin-bottom: 4rem;
}

input {
    margin-bottom: 2rem;
}

h1,
label,
button,
select {
    margin-left: 1rem
}

#addPassenger {
    margin-top: 4rem;
}
</style>