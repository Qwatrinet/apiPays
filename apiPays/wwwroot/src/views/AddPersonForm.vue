<script>
import Person from '@/classes/Person';
import DataManagement from '@/classes/DataManagement';
    export default{
        data(){
            return{
                nom:"",
                prenom:"",
            }
        },
        methods:{
            async postPerson(){
                if (this.nom=="" || this.prenom==""){
                    console.log("empty field error")
                    return
                    
                    
                }
                if(this.nom.length>60 || this.prenom.length>60){
                    console.log("La base n'accepte pas de nom de plus de 60 caractères")
                    return
                }
                const person= new Person(this.nom,this.prenom)
                await DataManagement.postNewPerson(person)
                this.nom=""
                this.prenom=""
            }
        }
    }
</script>
<template>
    <RouterLink to="/person"><button>Retour au menu Personnes</button></RouterLink>
    <h1>Ajouter une personne</h1>
    <form action="">

            <label for="nom">Nom de la personne: </label>
            <input type="text" id="nom" name="nom" v-model="nom">

            <label for="prenom">Prenom de la personne: </label>
            <input type="text" id="prenom" name="prenom" v-model="prenom">

        <button type="button" @click="postPerson()">Ajouter la personne</button>
    </form>
</template>

<style scoped>
    form{
        display: grid;
        grid-template-columns: 1fr 2fr
    }
    input{
        margin-bottom: 2rem;
    }
    h1,label,button{
        margin-left:1rem
    }
</style>