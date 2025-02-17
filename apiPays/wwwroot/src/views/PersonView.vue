<script>
    export default{
        data(){
            return{
                
            }
        },
        components:{
        },
        methods:{
        }
    }
</script>
<template>
    <RouterLink to="/trip"><button>Retour au menu principal</button></RouterLink>
    <h1>Gestion Des Personnes</h1>

    <p>Que voulez-vous faire?</p>
    <ul>
        <RouterLink to="/person/show"><li>Afficher la liste des personnes</li></RouterLink>
        <RouterLink to="/person/add"><li>Ajouter une personne</li></RouterLink>
        <RouterLink to="/person/show"><li>Modifier une personne existante</li></RouterLink>
        <RouterLink to="/person/show"><li>Supprimer une personne existante</li></RouterLink>
    </ul>
</template>

<style scoped>
a{
      color:white;
      text-decoration: none;
      &.router-link-active {
        font-weight: bold;
      }
    }
</style>