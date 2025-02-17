<template>
    <RouterLink to="/trip"><button>Retour au menu principal</button></RouterLink>
    <h1>Gestion des trajets</h1>
    <p>Que voulez-vous faire?</p>
    <ul>
        <RouterLink to="/trip/show"><li>Afficher la liste des trajets</li></RouterLink>
        <RouterLink to="/trip/add"><li>Ajouter un trajet</li></RouterLink>
        <RouterLink to="/trip/show"><li>Modifier un trajet</li></RouterLink>
        <RouterLink to="/trip/show"><li>Supprimer un trajet</li></RouterLink>
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