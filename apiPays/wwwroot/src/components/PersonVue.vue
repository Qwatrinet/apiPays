<script>
import DataManagement from '@/classes/DataManagement';
import PersonLike from '@/classes/PersonLike';
export default {
    data() {
        return {
            deleting: false,
            modifying: false,
            nom: "",
            prenom: "",
            truite: false
        }
    },
    props: {
        person: Object
    },
    mounted() {
        if (this.person.nom.toLowerCase() == "truite" && this.person.prenom.toLowerCase() == "truite") {
            this.truite = true
        }
    },
    methods: {
        ModifyPerson() {
            this.nom = this.person.nom
            this.prenom = this.person.prenom
            this.modifying = true
        },
        AskDelete() {
            this.deleting = true
        },
        async ConfirmDelete() {
            const pers = new PersonLike(this.person.id, this.nom, this.prenom)
            await DataManagement.deletePerson(pers)
            window.location = ''
        },
        CancelDelete() {
            this.deleting = false
        },
        async FireModif() {
            if ((this.person.nom.toLowerCase() == this.nom.toLowerCase() && this.person.prenom.toLowerCase() == this.prenom.toLowerCase()) || this.nom.length < 2 || this.prenom.length < 2) {
                this.nom = ""
                this.prenom = ""
                this.modifying = false
                console.log("modif invalide")
                return
            }

            const pers = new PersonLike(this.person.id, this.nom, this.prenom)
            await DataManagement.changePerson(pers)
            this.nom = ""
            this.prenom = ""
            this.modifying = false
            window.location = ''

        }

    }
}
</script>
<template>
    <div>
        <p>{{ person.nom }} {{ person.prenom }}</p>
        <button @click="ModifyPerson">Modifier</button>
        <button @click="AskDelete">Supprimer</button>
    </div>
    <div v-if="deleting">
        <p>Voulez-vous vraiment supprimer {{ person.nom }} {{ person.prenom }}?</p>
        <button @click="ConfirmDelete">Oui</button>
        <button @click="CancelDelete">Non</button>
    </div>
    <form id="modif" v-if="modifying">
        <label for="nom">Nom: </label>
        <input type="text" id="nom" name="nom" v-model="nom">
        <label for="prenom">Prénom: </label>
        <input type="text" id="prenom" name="prenom" v-model="prenom">
        <button type="button" @click="FireModif()">Valider</button>
    </form>
    <img src="/LaTruite.png" alt="" v-if="truite == true" id="truite">
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

#truite {
    height: 100%;
    width: 100%;
}

div {
    display: grid;
    grid-template-columns: 1fr 1fr 1fr;

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